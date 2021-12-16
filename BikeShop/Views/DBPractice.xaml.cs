using BikeShop.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Console;

namespace BikeShop.Views
{
    /// <summary>
    /// DBPractice.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DBPractice : Page
    {
        public DBPractice()
        {
            InitializeComponent();
        }

        //OracleConnection : SQL 서버를 사용하기 위해서는 클라이언트 프로그램은 == = == 서버 연결 실행 용도
        //SqlConnection을 사용하여 먼저 서버와 접속을 해야한다.
        //서버와 접속을 위해서는 접속시 사용하는 Connection String이 필요한데, 이에는 서버명, 인증방법, 초기 DB명 등을 지정
        OracleConnection conn;

        private void DB_Connect(object sender, RoutedEventArgs e)
        {
            try
            {
                string strCon = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=loonshot.cgxkzseoyswk.us-east-2.rds.amazonaws.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id=loonshot;Password=loonshot123;";
                conn = new OracleConnection(strCon);
                conn.Open();

                MessageBox.Show("DB Connection OK...");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

               
        private void Select_Data(object sender, RoutedEventArgs e)
        {
            string sql = "select PATIENT_ID, PATIENT_NAME from PATIENT";

            /* Connection, Command, DataReader를 통한 데이터 추출 */
            //OracleCommand : SQL 서버에 어떤 명령을 내리기 위해 사용하는 클래스 ===== 명령문 실행 용도
            //데이타를 가져오거나(SELECT), 테이블 내용을 삽입(INSERT), 갱신(UPDATE), 삭제(DELETE) 하기 위해
            //이 클래스를 사용할 수 있으며, 저장 프로시져(Stored Procedure)를 사용할 때도 사용
            OracleCommand comm = new OracleCommand();
            if (conn == null)
                new DBConnection();
                //DB_Connect(this, null);

            comm.Connection = conn;
            comm.CommandText = sql;

            //ExecuteReader : Sends the CommandText to the Connection and builds a SqlDataReader.
            //SQL Server와 연결을 유지한 상태에서 한번에 한 레코드(One Row)씩 데이타를 가져오는데 사용
            //DataReader는 하나의 Connection에 하나만 Open되어 있어야 하며, 사용이 끝나면 Close() 메서드를 호출하여 닫아 준다.
            OracleDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
            List<DataViewModel> datas = new List<DataViewModel>(); //listView에 데이터 뿌리기 위한 틀

            int a = 0;

            while (reader.Read())// 다음 레코드 계속 가져와서 루핑 - Advances the SqlDataReader to the next record. true if there are more rows; otherwise false.
            {
                datas.Add(new DataViewModel()
                {
                    //GetString : Gets the column ordinal, given the name of the column.
                    //가져올 데이터의 컬럼명을 인수로 넣어줌
                    UserId = reader.GetInt32(reader.GetOrdinal("PATIENT_ID")),
                    UserName = reader.GetString(reader.GetOrdinal("PATIENT_NAME")) //listView의 textblock id(?)
                });
            }

            listView.ItemsSource = datas; //listview의 데이터 바인딩 진행
            //WriteLine(listView.ItemsSource);

            DataList dataList = new DataList(); //자식 창
            dataList.ShowDialog();
            //reader.Close();


        }

        /*private void Show_Data_NewPage(object sender, RoutedEventArgs e)
        {
            //새 창 띄우기
            DataList dataList = new DataList("안녕하세요"); //자식 창
            dataList.ShowDialog();
        }*/
    }
}
