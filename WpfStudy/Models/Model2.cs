using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace WpfStudy.Models
{
    public class Model2 : ObservableObject
    {
        //환자 이름
        private string patientName;
        public string PatientName
        {
            get => patientName;
            set => SetProperty(ref patientName, value);
        }

        //예약한 날짜
        private DateTime reservationDate;
        public DateTime ReservationDate
        {
            get => reservationDate;
            set => SetProperty(ref reservationDate, value);
        }

        //증상
        private string symptom;
        public string Symptom
        {
            get => symptom;
            set => SetProperty(ref symptom, value);
        }

        //담당 의사
        private string doctor;
        public string Doctor
        {
            get => doctor;
            set => SetProperty(ref doctor, value);
        }
    }
}
