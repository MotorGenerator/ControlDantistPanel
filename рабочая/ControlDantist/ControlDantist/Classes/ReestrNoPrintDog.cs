using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ ������� ������ � ������������
    /// </summary>
    class ReestrNoPrintDog
    {
        private string number;
        private string numDog;
        private string fio;

        /// <summary>
        /// ����������� �����
        /// </summary>
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }


        /// <summary>
        /// ����� ��������
        /// </summary>
        public string NumDog
        {
            get
            {
                return numDog;
            }
            set
            {
                numDog = value;
            }
        }

        /// <summary>
        /// ��� ���������
        /// </summary>
        public string Fio
        {
            get
            {
                return fio;
            }
            set
            {
                fio = value;
            }
        }
    }
}
