using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ ������ ������ ��� ����������� ���������
    /// </summary>
    public class ErrorReestr
    {
        private string ���;
        private List<ErrorsReestrUnload> listError;

        private decimal ������������������������;
        private decimal �������������������;

        /// <summary>
        /// ���������� ��������� ����� ��� ����������� ������������
        /// </summary>
        public decimal ������������������������
        {
            get
            {
                return �������������������;
            }
            set
            {
                ������������������� = value;
            }
        }

        /// <summary>
        /// ��������� ��� ������ ������� ��� �����������.
        /// </summary>
        public string ������������������� { get; set; }
        

        /// <summary>
        /// ������ ����� �� ������� ��� ����������� ���������
        /// </summary>
        public decimal Error������������������������
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

        /// <summary>
        /// ������ ������ ������ � ������� ��� ����������� ���������
        /// </summary>
        public List<ErrorsReestrUnload> ErrorList������
        {
            get
            {
                return listError;
            }
            set
            {
                listError = value;
            }
        }

    }
}
