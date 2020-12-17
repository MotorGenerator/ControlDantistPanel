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

        private decimal ����;
        private decimal ����������;

        private decimal �����;
        private decimal �����������;



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
