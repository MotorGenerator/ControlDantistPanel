using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ����� �������� ���������� �������� �������� � ������
    /// </summary>
    class DisplayReestrCheck
    {
        private string �������������;
        private string ������������;
        private string ��������;
        private string ��������;
        private string �����;
        List<ErrorsReestrUnload> errorList;
        private bool flagError;
        //private decimal sum;
        private string sum;
        //private decimal sumAct;
        private string sumAct;

        private bool flagAddContract;

        /// <summary>
        /// �������� ����� ����� ����������� ����.
        /// </summary>
        public string ������������������������� { get; set; }

        /// <summary>
        /// ��������� ��� ������� ��� �����������.
        /// </summary>
        public bool FlagErrorAct������ { get; set; }

       /// <summary>
        /// ��������� ��� ���� ��� ����������
        /// </summary>
        public bool FlagAddContract
        {
            get
            {
                return flagAddContract;
            }
            set
            {
                flagAddContract = value;
            }
        }

        /// <summary>
        /// ������ ����� ���� ����������� �����
        /// </summary>
        public string ���������
        {
            get
            {
                return sumAct;
            }
            set
            {
                sumAct = value;
            }
        }

        /// <summary>
        /// ������ ���� ����
        /// </summary>
        public string ��������
        {
            get
            {
                return ��������;
            }
            set
            {
                �������� = value;
            }
        }

        /// <summary>
        ///������ ���� ��������
        /// </summary>
        public string ������������
        {
            get
            {
                return ������������;
            }
            set
            {
                ������������ = value;
            }
        }

        /// <summary>
        /// ������ ����� � ���� ��������
        /// </summary>
        public string �������������
        {
            get
            {
                return �������������;
            }
            set
            {
                ������������� = value;
            }
        }

        /// <summary>
        /// ������ ����� ����������� ����� ��� ����������� ��������
        /// </summary>
        public string Sum
        {
            get
            {
                return sum;
            }
            set
            {
                sum = value;
            }
        }

        /// <summary>
        /// ��� ���������
        /// </summary>
        public string ���_��������
        {
            get
            {
                return ��������;
            }
            set
            {
                �������� = value;
            }
        }

        /// <summary>
        /// ���� ��������� ��� ��� ����� �������� ������ � ����� �����
        /// </summary>
        public bool FlagError
        {
            get
            {
                return flagError;
            }
            set
            {
                flagError = value;
            }
        }

        /// <summary>
        /// ����� ���� ����������� �����
        /// </summary>
        public string ���������
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

        /// <summary>
        /// ������ ������
        /// </summary>
        public List<ErrorsReestrUnload> ������������
        {
            get
            {
                return errorList;
            }
            set
            {
                errorList = value;
            }
        }

        /// <summary>
        /// ���� ��������� �� ������� ������ � �������� ���������
        /// </summary>
        public bool Flag����������������� { get; set; }

        /// <summary>
        /// ��������� ������� �������� ���������.
        /// </summary>
        //public string ���������������� { get; set; }

        /// <summary>
        /// ��������� ����� �������� ��������� ������� � �� � � �����.
        /// </summary>
        public PreferentCategory ����������������� { get; set; }

    }
}
