using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    //�������� ������ ������� � �������
    class ReestrErrorControl
    {
        private string �����;
        private string ���;
        private string ������������������;
        private string ����Error;
        private string ����Control;
        private string ����Error;
        private string �����Control;
        private string ���������������Error;
        private string ���������������Control;


        /// <summary>
        /// ������ ��� ���������
        /// </summary>
        public string ���������������
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
        /// ������ ������������ ������
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
        /// ������ �������� ��������� ������ � �������
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

        /// <summary>
        /// ������ ����������� �������� ��������� ������
        /// </summary>
        public string ����Control
        {
            get
            {
                return ����Control;
            }
            set
            {
                ����Control = value;
            }
        }
        
        //������ ����� ������ � �������
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

        //������ ����� ������ �����������
        public string �����Control
        {
            get
            {
                return �����Control;
            }
            set
            {
                �����Control = value;
            }
        }

        //������ �������� ������ � �������
        public string ���������������Error
        {
            get
            {
                return ���������������Error;
            }
            set
            {
                ���������������Error = value;
            }
        }
       
        //������ ����������� �������� ��������� ��������� �����
        public string ���������������Control
        {
            get
            {
                return ���������������Control;
            }
            set
            {
                ���������������Control = value;
            }
        }

    }
}
