using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStudy
{
    public partial class Model1 : ObservableObject
    {
        //환자 이름
        private string wPatientName;
        public string WPatientName
        {
            get => wPatientName;
            set => SetProperty(ref wPatientName, value);
        }

        //환자 성별
        private string patientGender;
        public string PatientGender
        {
            get => patientGender;
            set => SetProperty(ref patientGender, value);
        }

        //환자 전화번호
        private string patientPhoneNum;
        public string PatientPhoneNum
        {
            get => patientPhoneNum;
            set => SetProperty(ref patientPhoneNum, value);
        }

        //환자 집 주소
        private string patientAddress;
        public string PatientAddress
        {
            get => patientAddress;
            set => SetProperty(ref patientAddress, value);
        }

        //대기 등록 시간
        private DateTime requestToWait;
        public DateTime RequestToWait
        {
            get => requestToWait;
            set => SetProperty(ref requestToWait, value);
        }

        //환자의 증상
        private string wSymptom;
        public string WSymptom
        {
            get => wSymptom;
            set => SetProperty(ref wSymptom, value);
        }
    }
}
