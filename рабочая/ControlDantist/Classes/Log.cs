using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ����� �������� ��� �������� ����� �� ������������� � ����
    /// </summary>
    class Log
    {
        private string ������������;
        private string ����;

        private string ����������������;
        private string ��������;

        private string ������������Error;
        private string ����Error;

        /// <summary>
        /// ������������ �� SQL ������
        /// </summary>
        public string ������������SQL
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
        /// ���� �� SQL ������
        /// </summary>
        public string ����SQL
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
        /// ������������ ������ � ����� ��������
        /// </summary>
        public string ����������������
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
        /// ���� ������ � ����� ��������
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
        /// ������������ ������ � ����� �������� 
        /// </summary>
        public string ������������Error
        {
            get
            {
                return ������������Error;
            }
            set
            {
                ������������Error = value;
            }
        }

        /// <summary>
        /// ���� ������������ � ����� ��������
        /// </summary>
        public string ����Error
        {
            get
            {
                return ����Error;
            }
            set
            {
                ����Error = value;
            }
        }
    }
}
