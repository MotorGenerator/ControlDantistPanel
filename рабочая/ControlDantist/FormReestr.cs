using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ControlDantist
{
    public partial class FormReestr : Form
    {
        private string ������������;
        private string �����������;
        private string ���������������;
        private string ����������������;

        /// <summary>
        /// ����� ���� �������
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
        /// ���� ���� �������
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

        /// <summary>
        /// ������ ���� �������
        /// </summary>
        public string �����������
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
        /// ������ ����� �������
        /// </summary>
        public string ������������
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
                


        public FormReestr()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //������� � �������� ����� ������ ������� � ���� ������� � ���� ��������

            //������� ���� �������
            this.����������� = this.dateTimePicker1.Value.ToShortDateString().Trim();

            //������� ����� ��������
            this.������������ = this.textBox1.Text.Trim();

            //������� ����� ����-�������
            this.���������������� = this.textBox2.Text.Trim();

            this.��������������� = this.dateTimePicker2.Value.ToShortDateString().Trim();


        }

       
    }
}