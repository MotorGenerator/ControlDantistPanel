using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ ��������� ������� ��� ��������� � ����������
    /// </summary>
    class PrintContractsValidate
    {
        private string ���;
        private string ����������;
        private string ���������������;

        //������ ��� � ����� ��������� ��������
        public string ���_�����_��������������
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
        /// ������ ����� �������� ��������
        /// </summary>
        public string �������������������
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
        /// ������ ��������� ����������� �����
        /// </summary>
        public string ���������������
        {
            get
            {
                return ���������������;
            }
            set
            {
                ��������������� = value;
            }
        } 
    }
}
