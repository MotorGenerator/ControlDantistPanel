using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    public class DisplayReestr
    {
        private string �����;
        List<ErrorsReestrUnload> errorList;
        private bool flagError;
        private ReestrControl ��������;
        private decimal sum;
        private decimal errorSum;
        private bool errorPerson;

        //��� �������� ��� ����������
        private bool flagAddContract;

        //��� �������� ����� � ��� �� ������� ��� �������� ��������
        private decimal sumContractServ;

        //������ ����� ���� ����������� ����� � �������� ��������
        private decimal sumAct;

        /// <summary>
        /// ��������� ��� �������� ������, ������� ��� �����������.
        /// </summary>
        public bool FlagError����������������� { get; set; }

        /// <summary>
        /// �������� ����� ����������� ����.
        /// </summary>
        public string �������������������� { get; set; }

        /// <summary>
        /// ������ ��������� ���� �� ��� ����������
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
        /// ������ ����� ���� ����������� ����� � �������� �������� ������� ������ �� ����� �������� ������� ����������� �����
        /// </summary>
        public decimal �������������������������
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
        /// ����� �������� �������� ������� ��������� � ��� �� �������
        /// </summary>
        public decimal ������������������
        {
            get
            {
                return sumContractServ;
            }
            set
            {
                sumContractServ = value;
            }
        }

        /// <summary>
        /// ������ ����� ����������� ����� ��������� ���������
        /// </summary>
        public decimal SumError
        {
            get
            {
                return errorSum;
            }
            set
            {
                errorSum = value;
            }
        }


        /// <summary>
        /// ������ ����� ����� ��������� ���������
        /// </summary>
        public decimal Sum
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
        /// �������� ��������� ��������
        /// </summary>
        public ReestrControl ��������
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
        /// ���� ���� ����� false �� ��������� ��� ��� ����� �������� ������ � ����� ����� 
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
        /// ���� true ���� ��������� ��� � ������������ ������ �������� ������
        /// </summary>
        public bool ErrorPerson
        {
            get
            {
                return errorPerson;
            }
            set
            {
                errorPerson = value;
            }
        }

        /// <summary>
        /// ������ ���� �������� ��������� �� �������.
        /// </summary>
        public bool ErrorPrefCategory { get; set; }

        public PreferentCategory ����������������� { get; set; }
    }
}
