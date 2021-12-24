using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MultiCommandEx
{
    public class StringObject: ObservableObject
    {
        /// <summary>
        /// 문자열 데이터입니다.
        /// </summary>
        private string stringData;
        public string StringData
        {
            get => stringData;
            set => SetProperty(ref stringData, value);
        }

        /// <summary>
        /// 오브젝트 데이터입니다.
        /// </summary>
        private object objectData;
        public object ObjectData
        {
            get => objectData;
            set => SetProperty(ref objectData, value);
        }

        /// <summary>
        /// 생성자입니다.
        /// </summary>
        /// <param name="stringData">문자열 데이터입니다.</param>
        /// <param name="objectData">오브젝트 데이터입니다.</param>
        public StringObject(string stringData, object objectData)
        {
            this.stringData = stringData;
            this.objectData = objectData;
        }
    }
}
