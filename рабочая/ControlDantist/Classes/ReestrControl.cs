using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// �������� ��������� � ������� ��������� ��������
    /// </summary>
    public class ReestrControl
    {
        private string �����;
        private string ���;
        private string �����������������;
        private string �����������������������;
        private string ��������������;
        private string ��������������;

        /// <summary>
        /// ������ �����
        /// </summary>
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

        /// <summary>
        /// ��������� ������ ��������� �������� ���������
        /// </summary>
        public string ��������������
        {
            get
            {
                return ��������������;
            }
            set
            {
                �������������� = value;
            }
        }

        /// <summary>
        /// ����� � ���� ��������� � ����� �� ������
        /// </summary>
        public string ��������������
        {
            get
            {
                return ��������������;
            }
            set
            {
                �������������� = value;
            }
        }

        /// <summary>
        /// ����� � ���� ���� ��������� �����
        /// </summary>
        public string �����������������������
        {
            get
            {
                return �����������������������;
            }
            set
            {
                ����������������������� = value;
            }
        }

        /// <summary>
        /// ������ ������ �� �������� � �������������� �����
        /// </summary>
        public string �����������������
        {
            get
            {
                return �����������������;
            }
            set
            {
                ����������������� = value;
            }
        }

        /// <summary>
        /// ������ ��� ���������
        /// </summary>
        public string ���
        {
            get
            {
                return ���;
            }
            set
            {
                ��� = value;
            }
        }
    }
}
