using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Xaml.Behaviors.Core;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using WpfStudy.Models;

namespace WpfStudy
{
    /**
     * Send의 역할을 하는 ViewModel
     */
    public class ViewModel1 : ObservableRecipient
    {
        //ViewModel의 역할은 model1의 값을 가져와서 View에 뿌려주기 전에 전처리 하는 것이다
        //ViewModel은 Messenger를 사용하는 통신에 직접적인 영향을 미친다

        private readonly ILogger _logger;
        string strCon = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=loonshot.cgxkzseoyswk.us-east-2.rds.amazonaws.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id=loonshot;Password=loonshot123;";

        //Model1과 Model2의 모든 값을 ObservableCollection(사실상 List 비슷한 형식)으로 받아옴
        //DB의 column을 쭉 가져온다고 생각하면 편함
        //Model1과 Model2는 같은 View에서 보여지기 때문에(엄밀히 따지면 다른 View지만 MainWindow에서 보여짐)
        private ObservableCollection<Model1> model1;
        public ObservableCollection<Model1> Model1s
        {
            get { return model1; }
            set { SetProperty(ref model1, value); }
        }

        private ObservableCollection<Model2> model2;
        public ObservableCollection<Model2> Model2s
        {
            get { return model2; }
            set { SetProperty(ref model2, value); }
        }

        public ViewModel1(ILogger<ViewModel1> logger)
        {
            _logger = logger;
            _logger.LogInformation("{@ILogger}", logger);

            Model1s = new ObservableCollection<Model1>();
            Model1s.CollectionChanged += ContentCollectionChanged;
            
            Model2s = new ObservableCollection<Model2>();
            Model2s.CollectionChanged += ContentCollectionChanged;
        }

        //== Messenger 기초 start ==//
        //데이터의 변경을 감지하기 위함
        //데이터 변경이나 데이터 가져오기가 발생하는 Model을 작성해서 처리해야 함...
        private void ContentCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged removed in e.OldItems)
                {
                    removed.PropertyChanged -= ProductOnPropertyChanged;
                    _logger.LogInformation("해당 리스트 가져오기 삭제");
                }
            }
            else
            {
                foreach (INotifyPropertyChanged added in e.NewItems)
                {
                    added.PropertyChanged += ProductOnPropertyChanged;
                    _logger.LogInformation("해당 리스트 가져오기 성공");
                }
            }
        }

        private void ProductOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var model1 = sender as Model1;
            if (model1 != null)
            {
                _logger.LogInformation("{@model1}", model1);
                WeakReferenceMessenger.Default.Send(Model1s); //Send() 필수
                _logger.LogInformation("send 성공");
            }

            var model2 = sender as Model2; 
            if (model2 != null)
            {
                _logger.LogInformation("{@model2}", model2);
                WeakReferenceMessenger.Default.Send(Model2s);
                _logger.LogInformation("send 성공");
            }
        }
        //== Messenger 기초 end ==//

        //SQL Query
        private void GetWaitingPatientList()
        {
            string sql =
                "SELECT p.PATIENT_NAME, p.GENDER, p.PHONE_NUM, p.ADDRESS, w.REQUEST_TO_WAIT, w.REQUIREMENTS " +
                "FROM WAITING w, PATIENT p " +
                "WHERE w.PATIENT_ID = p.PATIENT_ID " +
                "AND TO_CHAR(w.REQUEST_TO_WAIT, 'YYYYMMDD') = TO_CHAR(SYSDATE, 'YYYYMMDD') ORDER BY w.REQUEST_TO_WAIT";

            using (OracleConnection conn = new OracleConnection(strCon))
            {
                try
                {
                    conn.Open();
                    _logger.LogInformation("DB Connection OK...");
                    
                    using (OracleCommand comm = new OracleCommand())
                    {
                        comm.Connection = conn;
                        comm.CommandText = sql;

                        //방문해서 대기 중인 환자 리스트를 가져옴
                        using (OracleDataReader reader = comm.ExecuteReader())
                        {
                            _logger.LogInformation("select 실행");
                            _logger.LogInformation("[SQL QUERY] " + sql);
                            
                            try
                            {
                                while (reader.Read())
                                {
                                    Model1s.Add(new Model1()
                                    {
                                        PatientName = reader.GetString(reader.GetOrdinal("PATIENT_NAME")),
                                        PatientGender = reader.GetString(reader.GetOrdinal("GENDER")),
                                        PatientPhoneNum = reader.GetString(reader.GetOrdinal("PHONE_NUM")),
                                        PatientAddress = reader.GetString(reader.GetOrdinal("ADDRESS")),
                                        RequestToWait = reader.GetDateTime(reader.GetOrdinal("REQUEST_TO_WAIT")),
                                        Symptom = reader.GetString(reader.GetOrdinal("REQUIREMENTS"))
                                    });
                                }
                            }
                            finally
                            {
                                _logger.LogInformation("병원에서 대기 중인 환자 데이터 읽어오기 성공");
                                reader.Close();
                            }
                        }
                        
                        //진료를 예약한 환자 리스트를 가져옴
                        sql = 
                            "SELECT p.PATIENT_NAME, r.RESERVATION_DATE, r.SYMPTOM, ms.STAFF_NAME " +
                            "FROM RESERVATION r " +
                            "JOIN PATIENT p ON r.PATIENT_ID = p.PATIENT_ID " +
                            "JOIN MEDI_STAFF ms ON r.MEDICAL_STAFF_ID = ms.STAFF_ID ";

                        comm.CommandText = sql;
                        using (OracleDataReader reader = comm.ExecuteReader())
                        {
                            _logger.LogInformation("select 실행");
                            _logger.LogInformation("[SQL QUERY] " + sql);

                            try
                            {
                                while (reader.Read())
                                {
                                    Model2s.Add(new Model2()
                                    {
                                        PatientName = reader.GetString(reader.GetOrdinal("PATIENT_NAME")),
                                        ReservationDate = reader.GetDateTime(reader.GetOrdinal("RESERVATION_DATE")),
                                        Symptom = reader.GetString(reader.GetOrdinal("SYMPTOM")),
                                        Doctor = reader.GetString(reader.GetOrdinal("STAFF_NAME"))
                                    });
                                }
                            }
                            finally
                            {
                                _logger.LogInformation("예약 환자 데이터 읽어오기 성공");
                                reader.Close();
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    _logger.LogInformation(err.ToString());
                }
            }
        }

        private RelayCommand waitingListUpdateBtn;
        public ICommand WaitingListUpdateBtn => waitingListUpdateBtn ??= new RelayCommand(GetWaitingPatientList);

        //== row 더블 클릭 시 화면에 있던 데이터를 새로운 윈도우로 넘기기 위한 기초 과정 start ==//
        public ICollectionView CollectionView { get; set; }

        private ActionCommand doubleClickCommand;
        public ICommand DoubleClickCommand => doubleClickCommand ??= new ActionCommand(DoubleClick);

        private Model1 selectedItem;
        public Model1 SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }
        //== row 더블 클릭 시 화면에 있던 데이터를 새로운 윈도우로 넘기기 위한 기초 과정 end ==//

        private void DoubleClick()
        {
            var selected = SelectedItem;
            _logger.LogInformation("선택된 행의 환자 이름은 " + selected.PatientName +
                ", 증상은 \"" + selected.Symptom + "\" 입니다.");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ObservableCollection과 함수, 버튼을 따로 두고 각 버튼을 누를 때 마다 값을 따로 가져옴 - 성공
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*private void GetReservationPatientList()
        {
            string sql =
                "SELECT p.PATIENT_NAME, r.RESERVATION_DATE, r.SYMPTOM, ms.STAFF_NAME " +
                "FROM RESERVATION r " +
                "JOIN PATIENT p ON r.PATIENT_ID = p.PATIENT_ID " +
                "JOIN MEDI_STAFF ms ON r.MEDICAL_STAFF_ID = ms.STAFF_ID ";

            using (OracleConnection conn = new OracleConnection(strCon))
            {
                try
                {
                    conn.Open();
                    _logger.LogInformation("DB Connection OK...");

                    using (OracleCommand comm = new OracleCommand())
                    {
                        comm.Connection = conn;
                        comm.CommandText = sql;

                        using (OracleDataReader reader = comm.ExecuteReader())
                        {
                            _logger.LogInformation("select 실행");
                            _logger.LogInformation("[SQL QUERY] " + sql);

                            try
                            {
                                while (reader.Read())
                                {
                                    Model2s.Add(new Model2()
                                    {
                                        PatientName = reader.GetString(reader.GetOrdinal("PATIENT_NAME")),
                                        ReservationDate = reader.GetDateTime(reader.GetOrdinal("RESERVATION_DATE")),
                                        Symptom = reader.GetString(reader.GetOrdinal("SYMPTOM")),
                                        Doctor = reader.GetString(reader.GetOrdinal("STAFF_NAME"))
                                    });
                                }
                            }
                            finally
                            {
                                _logger.LogInformation("데이터 읽어오기 성공");
                                reader.Close();
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    _logger.LogInformation(err.ToString());
                }
            }
        }
        private RelayCommand reservationListUpdateBtn;
        public ICommand ReservationListUpdateBtn => reservationListUpdateBtn ??= new RelayCommand(GetReservationPatientList);*/
    }
}
