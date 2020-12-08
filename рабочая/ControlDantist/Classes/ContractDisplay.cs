using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    class ContractDisplay
    {
        private string numbContract;
        private bool validEsrn;
        private bool validServ;
        private bool controlSaveBD;
        private string ���;
        private string sumServ;

        //// Id ������ ������� ��� ������ ��������.
        //public string IdRegion { get; set; }


        //// ����� ���������.
        //public string Sinls { get; set; }
        

        /// <summary>
        /// ���������� ����� �� �������.
        /// </summary>
        //public string ��������������� { get; set; }

        /// <summary>
        /// ����� �������
        /// </summary>
        public string �������������
        {
            get
            {
                return numbContract;
            }
            set
            {
                numbContract = value;
            }
        }

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
        /// ��������� ������ �������� �������� � ���� ��� ��� 
        /// </summary>
        public bool ������������
        {
            get
            {
                return validEsrn;
            }
            set
            {
                validEsrn = value;
            }
        }

        /// <summary>
        /// ��������� ������ �� �������� �� ��������� �����
        /// </summary>
        public bool �������������
        {
            get
            {
                return validServ;
            }
            set
            {
                validServ = value;
            }
        }

        /// <summary>
        /// ��������� ������� � ���� ������
        /// </summary>
        public bool ����������������
        {
            get
            {
                return controlSaveBD;
            }
            set
            {
                controlSaveBD = value;
            }
        }


        /// <summary>
        /// ����� ����� �� ��������
        /// </summary>
        public string SumService
        {
            get
            {
                return sumServ;
            }
            set
            {
                sumServ = value;
            }
        }



    }
}
