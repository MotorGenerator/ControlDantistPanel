using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ ����������� ���������� ��� ����������� ��������� � �������
    /// </summary>
    public class ErrorsReestrUnload
    {
        private string ������������������;
        private string ������������������������;

        private int ����������;
        private int ����������������;

        private decimal ����;
        private decimal ����������;

        private decimal �����;
        private decimal �����������;

        private bool flag;

        /// <summary>
        /// ��������� ���������� �����
        /// </summary>
        public int ����������������
        {
            get
            {
                return ����������������;
            }
            set
            {
                ���������������� = value;
            }
        }


        /// <summary>
        /// ���������� ����� 
        /// </summary>
        public int ����������
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
        /// ��������� ��� ������ ������� ��������� ��� ��������� ��� ������������
        /// </summary>
        public bool FlagWrite
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }


        /// <summary>
        /// ������ � ����� ��� ���������� ������
        /// </summary>
        public decimal Error�����
        {
            get
            {
                return �����������;
            }
            set
            {
                ����������� = value;
            }
        }

        /// <summary>
        /// ������ ���������� �����
        /// </summary>
        public decimal �����
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
        /// ������ ���������� ����
        /// </summary>
        public decimal ����
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
        /// ������ ���� � �������
        /// </summary>
        public decimal Error����
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
        /// ������ ���������� ������������ ������
        /// </summary>
        public string ������������������
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
        /// ������ ��������� ������������ ������
        /// </summary>
        public string Error������������������
        {
            get
            {
                return ������������������������;
            }
            set
            {
                ������������������������ = value;
            }
        }
    }
}
