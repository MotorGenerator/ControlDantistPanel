using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ ������ ���������
    /// </summary>
    class ContractListItem
    {
        private string num;
        private string fio;
        private string numContrCurrent;
        private string numContracts;

        /// <summary>
        /// ����� �� �������
        /// </summary>
        public string Num
        {
            get
            {
                return num;
            }
            set
            {
                num = value;
            }
        }

        /// <summary>
        /// ��� ��������� � ������� ���������
        /// </summary>
        public string FIO
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

        /// <summary>
        /// ����� �������� ��������
        /// </summary>
        public string NumCurrentContract
        {
            get
            {
                return numContrCurrent;
            }
            set
            {
                numContrCurrent = value;
            }
        }

        /// <summary>
        /// ������ ���������
        /// </summary>
        public string NumContracts
        {
            get
            {
                return numContracts;
            }
            set
            {
                numContracts = value;
            }
        }


    }
}
