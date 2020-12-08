using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ControlDantist.Classes;

namespace ControlDantist
{
    public partial class FormDispContract : Form
    {
        private DataTable tabContract;
        private DataTable tabPerson;

        /// <summary>
        /// ������ id ��������
        /// </summary>
        public int Id������� { get; set; }
        
            

        /// <summary>
        /// ��������� � ������ ������� � ������� �� ���������
        /// </summary>
        public DataTable �����������������
        {
            get
            {
                return tabContract;
            }
            set
            {
                tabContract = value;
            }
        }

        /// <summary>
        /// ��������� � ������ ������ �� ���������
        /// </summary>
        public DataTable �����������������
        {
            get
            {
                return tabPerson;
            }
            set
            {
                tabPerson = value;
            }
        }
                

        public FormDispContract()
        {
            InitializeComponent();
        }

        private void FormDispContract_Load(object sender, EventArgs e)
        {

            DataTable tab = new DataTable();
            DataColumn col0 = new DataColumn("������������������", typeof(string));
            tab.Columns.Add(col0);
            DataColumn col1 = new DataColumn("����", typeof(string));
            tab.Columns.Add(col1);
            DataColumn col2 = new DataColumn("����������", typeof(string));
            tab.Columns.Add(col2);
            DataColumn col3 = new DataColumn("�����", typeof(string));
            tab.Columns.Add(col3);
            //DataColumn col4 = new DataColumn("����", typeof(string));
            //tab.Columns.Add(col4);

            decimal sum = 0.0m;

            foreach(DataRow row in �����������������.Rows)
            {
                DataRow row1 = tab.NewRow();
                row1["������������������"] = row["������������������"].ToString().Trim();
                row1["����"] = Convert.ToDecimal(row["����"]).ToString("c").Trim();
                row1["����������"] = row["����������"].ToString().Trim();

                sum = Math.Round(Math.Round(sum, 2) + Math.Round(Convert.ToDecimal(row["�����"]), 2), 2);

                row1["�����"] = Convert.ToDecimal(row["�����"]).ToString("c").Trim();
                tab.Rows.Add(row1);
            }

            this.lblSum.Text = sum.ToString("c").Trim();

            //��������� � ������������� ������ � �������
            this.dataGridView1.DataSource = tab;


            this.dataGridView1.Columns["������������������"].Width = 200;
            this.dataGridView1.Columns["������������������"].DisplayIndex = 0;

            this.dataGridView1.Columns["����"].Width = 100;
            this.dataGridView1.Columns["����"].DisplayIndex = 1;

            this.dataGridView1.Columns["����������"].Width = 100;
            this.dataGridView1.Columns["����������"].DisplayIndex = 2;

            this.dataGridView1.Columns["�����"].Width = 200;
            this.dataGridView1.Columns["�����"].DisplayIndex = 3;

           // this.dataGridView1.DataSource = �����������������;

            DataRow rowL = this.�����������������.Rows[0];

            this.lbl�������.Text = rowL["�������������"].ToString().Trim();
            string ���� = Convert.ToDateTime(rowL["������������"]).ToShortDateString().Trim();

            if (���� == "01.01.1900")
            {
                this.lbl������������.Text = "������� �� ��������";
            }
            else
            {
                this.lbl������������.Text = ����;
            }

            //������� � ������� �� ����� ��� ���������
            string ������� = rowL["�������"].ToString().Trim();
            string ��� = rowL["���"].ToString().Trim();
            string �������� = rowL["��������"].ToString().Trim();
            string ������������ = Convert.ToDateTime(rowL["������������"]).ToShortDateString().Trim();

            string FIO = ������� + " " + ��� + " " + �������� + " " + ������������ + " �. �������� ";
            this.lbl���.Text = FIO.Trim();

            //������� � ������� �� ����� ����� ���������
            string ����� = rowL["�����"].ToString().Trim();
            
            string ��������� = "�. " + rowL["���������"].ToString().Trim();

            string ���� = string.Empty;
            string ������ = rowL["������"].ToString().Trim();

            if(������.Length != 0)
            {
                ���� = "����. " + ������;
            }

            string �� = "��. " + rowL["�������������"].ToString().Trim();
            string ����� = ����� + " " + ��������� + " " + ���� + " " + ��;

            this.lblAdress.Text = "����� " + �����;

            //������� � ������� �������
            string ������������� = "������� " + rowL["�������������"].ToString().Trim() + "  � " + rowL["�������������"].ToString().Trim() + " ����� " + Convert.ToDateTime(rowL["������������������"]).ToShortDateString().Trim();
            this.lblL�������.Text = �������������.Trim();

            //������� � ������� �������� �� ��������� �������� �������� ������
            string �������� = "����� ��������� " + rowL["��������������"].ToString().Trim() + " � " + rowL["��������������"].ToString().Trim() + " ����� " + Convert.ToDateTime(rowL["�������������������"]).ToShortDateString().Trim();
            this.lblDoc.Text = ��������.Trim();

            //========������� ������ �������� (�.� ������� ������ �������� ��� ������� �� ������ ��������)
            //��� �����  ������� id ��������
            //string stringStstus = "select ������������ from dbo.������� where id_������� = "+ this.Id������� +" ";
            //bool flagStatus = Convert.ToBoolean(���������.GetTableSQL(stringStstus, "�������").Rows[0]["������������"]);

            bool flagStatus = Convert.ToBoolean(rowL["������������"]);

            if (flagStatus == true)
            {
                txtStatus.Text  = " ������� ������ �������� ";
            }
            else
            {
                txtStatus.Text = " ������� �� ������ �������� ";
            }
           

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}