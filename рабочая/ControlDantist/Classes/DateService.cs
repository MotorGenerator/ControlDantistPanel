using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ ������ �� �������
    /// </summary>
    class DateService
    {
        private string ������������������;
        //private decimal ����;
        //private decimal �����;
        private string ����;
        private string �����;
        private string ����������;

        /// <summary>
        /// ����������
        /// </summary>
        public string ����������
        {
            get
            {
                return ����������;
            }
            set
            {
                ���������� = value;
            }
        }

        /// <summary>
        /// ������ ������������
        /// </summary>
        public string ������������
        {
            get
            {
                return ������������������;
            }
            set
            {
                ������������������ = value;
            }
        }

        /// <summary>
        /// ������ ��������� ������
        /// </summary>
        //public decimal ����
        public string ����
        {
            get
            {
                return ����;
            }
            set
            {
                ���� = value;
            }
        }

        /// <summary>
        /// ������ ����� ������
        /// </summary>
        //public decimal �����
        public string �����
        {
            get
            {
                return �����;
            }
            set
            {
                ����� = value;
            }
        }
    }
}
