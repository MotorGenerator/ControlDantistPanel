using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ � ����������� ��������������� �� Excel
    /// </summary>
    public class RowExcel
    {
        private string num;
        private string �������;
        private string ���;
        private string ��������;

        /// <summary>
        /// ������ ����� ��������
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
        /// ������� ���������
        /// </summary>
        public string �������
        {
            get
            {
                return �������;
            }
            set
            {
                ������� = value;
            }
        }

        /// <summary>
        /// ��� ���������
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
        /// �������� ���������
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
                
    }
}
