using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using WpfStudy.Models;

namespace WpfStudy
{
    public class ViewModel1 : ObservableRecipient
    {
        //ViewModel의 역할은 model1의 값을 가져와서 View에 뿌려주기 전에 전처리 하는 것이다
        //ViewModel은 Messenger를 사용하는 통신에 직접적인 영향을 미친다

        private readonly ILogger _logger;
        string strCon = 
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=loonshot.cgxkzseoyswk.us-east-2.rds.amazonaws.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id=loonshot;Password=loonshot123;";

        //Model1의 모든 값을 ObservableCollection(사실상 List 비슷한 형식)으로 받아옴
        //DB의 column을 쭉 가져온다고 생각하면 편함
        private ObservableCollection<Model1> model1;
        public ObservableCollection<Model1> Model1s
        {
            get { return model1; }
            set { SetProperty(ref model1, value); }
        }

        public ViewModel1(ILogger<ViewModel1> logger)
        {
            _logger = logger;
            _logger.LogInformation("{@ILogger}", logger);
            Model1s = new ObservableCollection<Model1>();
            Model1s.CollectionChanged += ContentCollectionChanged;
        }

        //== Messenger 기초 start ==//
        //데이터의 변경을 감지하기 위함
        private void ContentCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged removed in e.OldItems)
                {
                    removed.PropertyChanged -= ProductOnPropertyChanged;
                    _logger.LogInformation("대기자 리스트 가져오기 삭제");
                }
            }
            else
            {
                foreach (INotifyPropertyChanged added in e.NewItems)
                {
                    added.PropertyChanged += ProductOnPropertyChanged;
                    _logger.LogInformation("대기자 리스트 가져오기 성공");
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
        }
        //== Messenger 기초 end ==//

        //SQL Query : 방문해서 대기 중인 환자 리스트를 가져옴
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

                        using (OracleDataReader reader = comm.ExecuteReader())
                        {
                            _logger.LogInformation("select 실행");
                            _logger.LogInformation("[SQL QUERY] " + sql);
                            
                            try
                            {
                                while (reader.Read())
                                {
                                    Model1s.Add(new Model1() //.Add()를 해야지 데이터의 변화를 감지할 수 있음
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

        private RelayCommand waitingListUpdateBtn;
        public ICommand WaitingListUpdateBtn => waitingListUpdateBtn ??= new RelayCommand(GetWaitingPatientList);


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        private ObservableCollection<Model2> model2;
        public ObservableCollection<Model2> Model2s
        {
            get { return model2; }
            set { SetProperty(ref model2, value); }
        }


    }
}
