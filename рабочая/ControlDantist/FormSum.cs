using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ControlDantist
{
    public partial class FormSum : Form
    {
        private decimal sum���������;
        private decimal sum�����������;

        /// <summary>
        /// ������ ����� ��������� ���������
        /// </summary>
        public decimal Sum���������
        {
            get
            {
                return sum���������;
            }
            set
            {
                sum��������� = value;
            }
        }


        /// <summary>
        /// ������ ����� �� ��������� ��� ���������� ���������
        /// </summary>
        public decimal Sum�����������
        {
            get
            {
                return sum�����������;
            }
            set
            {
                sum����������� = value;
            }
        }


        public FormSum()
        {
            InitializeComponent();
        }

        private void FormSum_Load(object sender, EventArgs e)
        {
            this.label1.Text = "����� ��������� ��������� �������� = " + this.Sum���������.ToString("c");
            this.label2.Text = "����� ��������� �� ��������� �������� = " + this.Sum�����������.ToString("c");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}