using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCommandEx
{
    public class StringObject
    {
        /// <summary>

        /// 문자열 데이터입니다.

        /// </summary>

        public string StringData;



        /// <summary>

        /// 오브젝트 데이터입니다.

        /// </summary>

        public object ObjectData;



        /// <summary>

        /// 생성자입니다.

        /// </summary>

        /// <param name="stringData">문자열 데이터입니다.</param>

        /// <param name="objectData">오브젝트 데이터입니다.</param>

        public StringObject(string stringData, object objectData)

        {

            this.StringData = stringData;

            this.ObjectData = objectData;

        }

    }
}
