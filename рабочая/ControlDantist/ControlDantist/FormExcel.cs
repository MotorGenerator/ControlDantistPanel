using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Classes;


using System.Globalization;
using System.Threading;


namespace ControlDantist
{
    public partial class FormExcel : Form
    {
        private List<RowExcel> listExecel;

        /// <summary>
        /// ������ ������ ���������� ��������������� �� ����� Excel
        /// </summary>
        public List<RowExcel> ListExcel
        {
            get
            {
                return listExecel;
            }
            set
            {
                listExecel = value;
            }
        }

        public FormExcel()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormExcel_Load(object sender, EventArgs e)
        {
            CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCInfo;

            //��������� ���������� ���� ��� ���� � ������ Excel
            this.dataGridView1.DataSource = this.ListExcel;
            this.dataGridView1.Columns["Num"].HeaderText = "����� ��������";
            this.dataGridView1.Columns["Num"].DisplayIndex = 0;
            this.dataGridView1.Columns["Num"].Visible = true;

            this.dataGridView1.Columns["�������"].HeaderText = "�������";
            this.dataGridView1.Columns["�������"].DisplayIndex = 1;
            this.dataGridView1.Columns["�������"].Visible = true;

            this.dataGridView1.Columns["���"].HeaderText = "���";
            this.dataGridView1.Columns["���"].DisplayIndex = 2;
            this.dataGridView1.Columns["���"].Visible = true;

            this.dataGridView1.Columns["��������"].HeaderText = "��������";
            this.dataGridView1.Columns["��������"].DisplayIndex = 3;
            this.dataGridView1.Columns["��������"].Visible = true;


        }
    }
}