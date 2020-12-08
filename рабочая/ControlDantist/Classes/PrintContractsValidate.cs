using System;
using System.Collections.Generic;
using System.Text;
using ControlDantist.ValidPersonContract;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ ��������� ������� ��� ��������� � ����������
    /// </summary>
    public class PrintContractsValidate
    {
        private string ���;
        private string ����������;
        private string ���������������;

        /// <summary>
        /// ������ ���������� ���������
        /// </summary>
        public string ��������������� { get; set; }

        public List<ValidItemsContract> listContracts { get; set; }// = new List<ValidItemsContract>();

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
