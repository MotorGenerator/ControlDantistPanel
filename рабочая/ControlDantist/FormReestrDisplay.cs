using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Classes;
using System.Linq;
using ControlDantist.Reports;
using DantistLibrary;

namespace ControlDantist
{
    public partial class FormReestrDisplay : Form
    {

        public bool FlagLetter { get; set; }
        public string Date { get; set; }

        public FormReestrDisplay()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ��������� ������ � ������ ����������.
        private List<int> LoadYear()
        {
            // ������ ������� ���.
            int year = DateTime.Today.Year;

            List<int> listYear = new List<int>();

            do
            {
                listYear.Add(year);

                year--;
            }
            while (year >= 2013);

            return listYear;
        }

        private void FormReestrDisplay_Load(object sender, EventArgs e)
        {
            this.label4.Visible = false;

           // this.listYear.DataSource = LoadYear();

            if (this.FlagLetter == true)
            {
                this.dtnWrite.Enabled = false;
                this.dateTimePicker1.Visible = false;
                this.label2.Visible = false;

                LoadUpdate();
            }
            else
            {
                this.btnPoust.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //������� ������
            //LoadUpdate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           //LoadUpdate();
        }

        private void dtnWrite_Click(object sender, EventArgs e)
        {
            //������� ��� ������ ���������
            string logWrite = MyAplicationIdentity.GetUses();

            //������� ����
            DateTime dt = DateTime.Today;

            //�������� ������ � ������ ����������
            StringBuilder builder = new StringBuilder();
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                int id = Convert.ToInt32(row.Cells["id_���"].Value);

                string query = "UPDATE ������������������� " +
                               "SET [����������] = '" + this.dateTimePicker1.Value.ToShortDateString().Trim() + "' " +
                               ",[logWrite] = '" + logWrite.Trim() +"' " +
                               ",[logDate] = '"+ dt.ToShortDateString() +"' " +
                               "WHERE id_��� = "+ id +" ";

                builder.Append(query);
            }

            //�������� ������ �� ���������� �������
            ExecuteQuery.Execute(builder.ToString());
            //this.Close();

            LoadUpdate();
        }

        /// <summary>
        /// ��������� ����������
        /// </summary>
        private void LoadUpdate()
        {
            string num_�����������;
            string unmReestr;

            // ���� ���� �������.
            bool flagCF = false;

            // ���� �������.
            bool flagReestr = false;

            // ���� ��������� �� ����� � �� ������ ������� � �� ������ ����-�������.
            bool flagFullFind = false;

            if (this.textBox1.Text.Trim() != "")
            {
                unmReestr = this.textBox1.Text.Trim();

                flagReestr = true;
            }
            else
            {
                unmReestr = "";
            }

            if (this.textBox2.Text.Trim() != "")
            {
                num_����������� = this.textBox2.Text.Trim();

                flagCF = true;
            }
            else
            {
                num_����������� = "";
            }


            string query = string.Empty;

            if (this.checkBox1.Checked == true)
            {
                flagFullFind = true;
            }

            if (this.FlagLetter == false)
            {

                if (flagFullFind == false)
                {
                    // ����� ������� �� ������ � �� ����
                    query = "SELECT �������������������.id_���,�������������������.���������,�������������������.������������,�������������������.����������������,�������.id_�������, �������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������������������.����������,�������������������.logWrite,�������������������.logDate,dbo.��������.id_��������,dbo.��������.id_�����,dbo.��������.id_��������,��������.�����,��������.���������,��������.������,��������.�������������  FROM ������� " +
                                   "INNER JOIN �������� ON  " +
                                   "dbo.�������.id_�������� = dbo.��������.id_��������  " +
                                   "INNER JOIN dbo.����������������� ON " +
                                   "dbo.��������.id_����������������� = dbo.�����������������.id_�����������������  " +
                                   "INNER JOIN dbo.���������������� ON  " +
                                   "dbo.�������.id_������� = dbo.����������������.id_�������  " +
                                   "INNER JOIN ������������������� " +
                                   "ON dbo.�������.id_������� = �������������������.id_������� " +
                                   "where �������.������������ = 'True' and (�������������������.��������������� >= '" + �����.����(this.dateTimePicker1.Value.ToShortDateString()) + "' and �������������������.��������������� <= '" + �����.����(this.dateTimePicker2.Value.ToShortDateString()) + "' ) " + 
                                   //" (�������������������.������������ = '" + unmReestr + "') " +
                                   "and ((�������������������.������������ = '" + unmReestr + "') or (�������������������.���������������� = '" + num_����������� + "')) " +
                                   "Group by �������������������.id_���,�������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������������������.���������,�������������������.������������,�������������������.����������������,�������������������.����������,�������������������.logWrite,�������������������.logDate,dbo.��������.id_��������,dbo.��������.id_�����,dbo.��������.id_��������,��������.�����,��������.���������,��������.������,��������.������������� ";
                }
                else 
                {
                    query = "";
                }

            }

            if (this.FlagLetter == true)
            {
                query = "SELECT �������������������.id_���,�������������������.���������,�������������������.������������,�������������������.����������������,�������.id_�������, �������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������������������.����������,�������������������.logWrite,�������������������.logDate,dbo.��������.id_��������,dbo.��������.id_�����,dbo.��������.id_��������,��������.�����,��������.���������,��������.������,��������.�������������  FROM ������� " +
                         "INNER JOIN �������� ON  " +
                         "dbo.�������.id_�������� = dbo.��������.id_��������  " +
                         "INNER JOIN dbo.����������������� ON " +
                         "dbo.��������.id_����������������� = dbo.�����������������.id_�����������������  " +
                         "INNER JOIN dbo.���������������� ON  " +
                         "dbo.�������.id_������� = dbo.����������������.id_�������  " +
                         "INNER JOIN ������������������� " +
                         "ON dbo.�������.id_������� = �������������������.id_������� " +
                         ////"INNER JOIN �������������� ON " +
                         ////"dbo.��������.id_�������� = ��������������.id_�������� " +
                         //// "INNER JOIN ������������������ ON " +
                         //// "dbo.��������.id_����� = ������������������.id_����� " +
                         //"where �������.������������ = 'True' and ((�������������������.������������ = '" + unmReestr + "') or (�������������������.���������������� = '" + num_����������� + "')) " +
                         "where �������.������������ = 'True' and �������������������.���������� = '"+ this.Date +"' " +
                         "Group by �������������������.id_���,�������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������������������.���������,�������������������.������������,�������������������.����������������,�������������������.����������,�������������������.logWrite,�������������������.logDate,dbo.��������.id_��������,dbo.��������.id_�����,dbo.��������.id_��������,��������.�����,��������.���������,��������.������,��������.������������� ";

            }

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                SqlTransaction transact = con.BeginTransaction();

                //DataTable dtContract = ���������.GetTableSQL(query, "����������������", con, transact);

                string iTestQuery = query;

                SqlCommand com = new SqlCommand(query, con);
                com.Transaction = transact;


                DataTable tab = new DataTable();
                DataColumn col0 = new DataColumn("id_���", typeof(int));
                tab.Columns.Add(col0);
                DataColumn col1 = new DataColumn("���������", typeof(string));
                tab.Columns.Add(col1);
                DataColumn col2 = new DataColumn("������������", typeof(string));
                tab.Columns.Add(col2);
                DataColumn col3 = new DataColumn("����������������", typeof(string));
                tab.Columns.Add(col3);
                DataColumn col4 = new DataColumn("id_�������", typeof(int));
                tab.Columns.Add(col4);
                DataColumn col5 = new DataColumn("�������������", typeof(string));
                tab.Columns.Add(col5);
                DataColumn col6 = new DataColumn("�������", typeof(string));
                tab.Columns.Add(col6);
                DataColumn col7 = new DataColumn("���", typeof(string));
                tab.Columns.Add(col7);
                DataColumn col8 = new DataColumn("��������", typeof(string));
                tab.Columns.Add(col8);
                DataColumn col9 = new DataColumn("�����������������", typeof(string));
                tab.Columns.Add(col9);
                DataColumn col10 = new DataColumn("�����", typeof(string));
                tab.Columns.Add(col10);
                DataColumn col11 = new DataColumn("����������", typeof(string));
                tab.Columns.Add(col11);

                if (this.FlagLetter == false)
                {
                    DataColumn col12 = new DataColumn("logWrite", typeof(string));
                    tab.Columns.Add(col12);
                    DataColumn col13 = new DataColumn("logDate", typeof(string));
                    tab.Columns.Add(col13);
                }

                //������������ ���������� ������
                DataColumn col14 = new DataColumn("������������", typeof(string));
                tab.Columns.Add(col14);

                DataColumn col15 = new DataColumn("�����", typeof(string));
                tab.Columns.Add(col15);

                DataColumn col16 = new DataColumn("�����", typeof(string));
                tab.Columns.Add(col16);


                int iCountD = 0;

                SqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    DataRow row = tab.NewRow();
                    row["id_���"] = read["id_���"].ToString().Trim();

                    row["id_�������"] = read["id_�������"].ToString().Trim();
                    row["���������"] = read["���������"].ToString().Trim();
                    row["������������"] = read["������������"].ToString().Trim();

                    row["����������������"] = read["����������������"].ToString().Trim();
                    row["�������������"] = read["�������������"].ToString().Trim();
                    row["�������"] = read["�������"].ToString().Trim();

                    row["���"] = read["���"].ToString().Trim();
                    row["��������"] = read["��������"].ToString().Trim();
                    row["�����������������"] = read["�����������������"].ToString().Trim();

                    row["�����"] = Convert.ToDecimal(read["�����"]).ToString("c").Trim();
                    if (read["����������"] != DBNull.Value)
                    {
                        row["����������"] = read["����������"].ToString().Trim();
                    }

                    if (this.FlagLetter == false)
                    {
                        row["logWrite"] = read["logWrite"].ToString().Trim();
                        row["logDate"] = read["logDate"].ToString().Trim();
                    }

                    int id_����� = 0;
                    int id_�������� = 0;

                    if (read["id_�����"] != DBNull.Value)
                    {
                        id_����� = Convert.ToInt32(read["id_�����"]);

                        string queryR = "SELECT ������������ FROM ������������������ where id_����� = "+ id_����� +" ";

                        DataTable tab����� = ���������.GetTableSQL(queryR, "��������������");
                        if (tab�����.Rows.Count != 0)
                        {
                            string ����� = ���������.GetTableSQL(queryR, "��������������").Rows[0][0].ToString().Trim();
                            row["�����"] = �����;
                        }
                        

                    }

                    if (read["id_��������"] != DBNull.Value)
                    {
                        id_�������� = Convert.ToInt32(read["id_��������"]);

                        string queryA = "SELECT ������������ FROM �������������� where id_�������� = " + id_�������� + " ";
                        DataTable tab����� = ���������.GetTableSQL(queryA, "��������������");

                        if (tab�����.Rows.Count != 0)
                        {
                            string ����� = ���������.GetTableSQL(queryA, "��������������").Rows[0][0].ToString().Trim();
                            row["������������"] = �����;
                        }

                    }




                    //������� ������ �� ������ ���������� ���������


                    //row["������������"] = read["������������"].ToString().Trim();

                    ////,��������.�����,��������.���������,��������.������,��������.�������������
                    StringBuilder buldAdr = new StringBuilder();
                    buldAdr.Append(read["�����"].ToString().Trim() + " ");

                    buldAdr.Append(read["���������"].ToString().Trim() + " ");
                    //buldAdr.Append("����. " + read["������"].ToString().Trim() + " ");

                    buldAdr.Append(read["������"].ToString().Trim() + " ");
                    buldAdr.Append(read["�������������"].ToString().Trim());

                    row["�����"] = buldAdr.ToString().Trim();

                    //// ����� �������
                    //row["�����"] = read["������������"].ToString().Trim();

                    ////if (read["����������"] == "1900-01-01")
                    ////{
                    ////    row["����������"] = ""; 
                    ////}
                    ////else
                    ////{
                    ////    row["����������"] = Convert.ToDateTime(read["����������"]).ToShortDateString().Trim();
                    ////}

                    ////if (read["logWrite"] != DBNull.Value)
                    ////{
                    ////    row["����������"] = read["logWrite"].ToString().Trim();
                    ////}
                    tab.Rows.Add(row);

                    iCountD++;
                }




                this.dataGridView1.DataSource = tab;

                this.dataGridView1.Columns["id_���"].Width = 100;
                this.dataGridView1.Columns["id_���"].Visible = false;

                this.dataGridView1.Columns["id_�������"].Width = 100;
                this.dataGridView1.Columns["id_�������"].Visible = false;

                this.dataGridView1.Columns["���������"].Width = 100;
                this.dataGridView1.Columns["���������"].DisplayIndex = 1;
                this.dataGridView1.Columns["���������"].HeaderText = "� ����";

                this.dataGridView1.Columns["������������"].Width = 100;
                this.dataGridView1.Columns["������������"].DisplayIndex = 2;
                this.dataGridView1.Columns["������������"].HeaderText = "� �������";

                this.dataGridView1.Columns["����������������"].Width = 100;
                this.dataGridView1.Columns["����������������"].DisplayIndex = 3;
                this.dataGridView1.Columns["����������������"].HeaderText = "� ����-�������";

                this.dataGridView1.Columns["�������������"].Width = 100;
                this.dataGridView1.Columns["�������������"].DisplayIndex = 4;
                this.dataGridView1.Columns["�������������"].HeaderText = "� ��������";

                this.dataGridView1.Columns["�������"].Width = 200;
                this.dataGridView1.Columns["�������"].DisplayIndex = 5;

                this.dataGridView1.Columns["���"].Width = 200;
                this.dataGridView1.Columns["���"].DisplayIndex = 6;

                this.dataGridView1.Columns["��������"].Width = 200;
                this.dataGridView1.Columns["��������"].DisplayIndex = 7;

                this.dataGridView1.Columns["�����������������"].Width = 300;
                this.dataGridView1.Columns["�����������������"].DisplayIndex = 8;

                this.dataGridView1.Columns["�����"].Width = 200;
                this.dataGridView1.Columns["�����"].DisplayIndex = 9;

                this.dataGridView1.Columns["����������"].Width = 70;
                this.dataGridView1.Columns["����������"].DisplayIndex = 10;

                if (this.FlagLetter == false)
                {
                    this.dataGridView1.Columns["logWrite"].Width = 70;
                    this.dataGridView1.Columns["logWrite"].DisplayIndex = 11;
                    this.dataGridView1.Columns["logWrite"].HeaderText = "��� �������";

                    this.dataGridView1.Columns["logDate"].Width = 70;
                    this.dataGridView1.Columns["logDate"].DisplayIndex = 12;
                    this.dataGridView1.Columns["logDate"].HeaderText = "���� ������";
                

                    //�������� ���� � ���� logDate ������� ����� �� ������ ���������� ������ ������� �� ��������
                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        if (row.Cells["logDate"].Value != "")
                        {
                            this.dtnWrite.Enabled = true;
                        }
                        else
                        {
                            this.dtnWrite.Enabled = false;
                        }
                    }

                }

                //������������ ���������� ������
                this.dataGridView1.Columns["������������"].Width = 70;
                this.dataGridView1.Columns["������������"].DisplayIndex = 13;
                this.dataGridView1.Columns["������������"].HeaderText = "��������� �����";

                // ����� ���������� ���������
                this.dataGridView1.Columns["�����"].Width = 70;
                this.dataGridView1.Columns["�����"].DisplayIndex = 14;
                this.dataGridView1.Columns["�����"].HeaderText = "�����";

                // ����� ������� � ������� ��������� ��������
                this.dataGridView1.Columns["�����"].Width = 70;
                this.dataGridView1.Columns["�����"].DisplayIndex = 15;
                this.dataGridView1.Columns["�����"].HeaderText = "�����";

                //������� ���������� ��������� �� �����
                this.label4.Visible = true;
                this.label4.Text = String.Format("���������� ��������� = {0}",iCountD);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //if (this.checkBox1.Checked == true)
            //{
            //    LoadUpdate(this.dateTimePicker1.Value.ToShortDateString());
            //}
            
        }

        private void LoadUpdate(string date)
        {
            string num_�����������;
            string unmReestr;

            if (this.textBox1.Text.Trim() != "")
            {
                unmReestr = this.textBox1.Text.Trim();
            }
            else
            {
                unmReestr = "";
            }

            if (this.textBox2.Text.Trim() != "")
            {
                num_����������� = this.textBox2.Text.Trim();
            }
            else
            {
                num_����������� = "";
            }

            //����� ������� �� ������ � �� ����
            string query = "SELECT �������������������.id_���,�������������������.���������,�������������������.������������,�������������������.����������������,�������.id_�������, �������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������������������.����������,�������������������.logWrite,�������������������.logDate  FROM ������� " +
                            "INNER JOIN �������� ON  " +
                            "dbo.�������.id_�������� = dbo.��������.id_��������  " +
                            "INNER JOIN dbo.����������������� ON " +
                            "dbo.��������.id_����������������� = dbo.�����������������.id_�����������������  " +
                            "INNER JOIN dbo.���������������� ON  " +
                            "dbo.�������.id_������� = dbo.����������������.id_�������  " +
                            "INNER JOIN ������������������� " +
                            "ON dbo.�������.id_������� = �������������������.id_������� " +
                            "where �������.������������ = 'True' and �������.������������������ = '" + date + "' " + //((�������������������.������������ = '" + unmReestr + "') or (�������������������.���������������� = '" + num_����������� + "')) " +
                            "Group by �������������������.id_���,�������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������������������.���������,�������������������.������������,�������������������.����������������,�������������������.����������,�������������������.logWrite,�������������������.logDate  ";

            //DataTable dtContract = ���������.GetTableSQL(query, "���������������");


            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                SqlTransaction transact = con.BeginTransaction();

                //DataTable dtContract = ���������.GetTableSQL(query, "����������������", con, transact);
                SqlCommand com = new SqlCommand(query, con);
                com.Transaction = transact;


                DataTable tab = new DataTable();
                DataColumn col0 = new DataColumn("id_���", typeof(int));
                tab.Columns.Add(col0);
                DataColumn col1 = new DataColumn("���������", typeof(string));
                tab.Columns.Add(col1);
                DataColumn col2 = new DataColumn("������������", typeof(string));
                tab.Columns.Add(col2);
                DataColumn col3 = new DataColumn("����������������", typeof(string));
                tab.Columns.Add(col3);
                DataColumn col4 = new DataColumn("id_�������", typeof(int));
                tab.Columns.Add(col4);
                DataColumn col5 = new DataColumn("�������������", typeof(string));
                tab.Columns.Add(col5);
                DataColumn col6 = new DataColumn("�������", typeof(string));
                tab.Columns.Add(col6);
                DataColumn col7 = new DataColumn("���", typeof(string));
                tab.Columns.Add(col7);
                DataColumn col8 = new DataColumn("��������", typeof(string));
                tab.Columns.Add(col8);
                DataColumn col9 = new DataColumn("�����������������", typeof(string));
                tab.Columns.Add(col9);
                DataColumn col10 = new DataColumn("�����", typeof(string));
                tab.Columns.Add(col10);
                DataColumn col11 = new DataColumn("����������", typeof(string));
                tab.Columns.Add(col11);
                DataColumn col12 = new DataColumn("logWrite", typeof(string));
                tab.Columns.Add(col12);
                DataColumn col13 = new DataColumn("logDate", typeof(string));
                tab.Columns.Add(col13);


                SqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    DataRow row = tab.NewRow();
                    row["id_���"] = read["id_���"].ToString().Trim();

                    row["id_�������"] = read["id_�������"].ToString().Trim();
                    row["���������"] = read["���������"].ToString().Trim();
                    row["������������"] = read["������������"].ToString().Trim();

                    row["����������������"] = read["����������������"].ToString().Trim();
                    row["�������������"] = read["�������������"].ToString().Trim();
                    row["�������"] = read["�������"].ToString().Trim();

                    row["���"] = read["���"].ToString().Trim();
                    row["��������"] = read["��������"].ToString().Trim();
                    row["�����������������"] = read["�����������������"].ToString().Trim();

                    row["�����"] = Convert.ToDecimal(read["�����"]).ToString("c").Trim();
                    if (read["����������"] != DBNull.Value)
                    {
                        row["����������"] = read["����������"].ToString().Trim();
                    }

                    row["logWrite"] = read["logWrite"].ToString().Trim();
                    row["logDate"] = read["logDate"].ToString().Trim();

                    //if (read["����������"] == "1900-01-01")
                    //{
                    //    row["����������"] = ""; 
                    //}
                    //else
                    //{
                    //    row["����������"] = Convert.ToDateTime(read["����������"]).ToShortDateString().Trim();
                    //}

                    //if (read["logWrite"] != DBNull.Value)
                    //{
                    //    row["����������"] = read["logWrite"].ToString().Trim();
                    //}
                    tab.Rows.Add(row);
                }


                this.dataGridView1.DataSource = tab;

                this.dataGridView1.Columns["id_���"].Width = 100;
                this.dataGridView1.Columns["id_���"].Visible = false;

                this.dataGridView1.Columns["id_�������"].Width = 100;
                this.dataGridView1.Columns["id_�������"].Visible = false;

                this.dataGridView1.Columns["���������"].Width = 100;
                this.dataGridView1.Columns["���������"].DisplayIndex = 1;
                this.dataGridView1.Columns["���������"].HeaderText = "� ����";

                this.dataGridView1.Columns["������������"].Width = 100;
                this.dataGridView1.Columns["������������"].DisplayIndex = 2;
                this.dataGridView1.Columns["������������"].HeaderText = "� �������";

                this.dataGridView1.Columns["����������������"].Width = 100;
                this.dataGridView1.Columns["����������������"].DisplayIndex = 3;
                this.dataGridView1.Columns["����������������"].HeaderText = "� ����-�������";

                this.dataGridView1.Columns["�������������"].Width = 100;
                this.dataGridView1.Columns["�������������"].DisplayIndex = 4;
                this.dataGridView1.Columns["�������������"].HeaderText = "� ��������";

                this.dataGridView1.Columns["�������"].Width = 200;
                this.dataGridView1.Columns["�������"].DisplayIndex = 5;

                this.dataGridView1.Columns["���"].Width = 200;
                this.dataGridView1.Columns["���"].DisplayIndex = 6;

                this.dataGridView1.Columns["��������"].Width = 200;
                this.dataGridView1.Columns["��������"].DisplayIndex = 7;

                this.dataGridView1.Columns["�����������������"].Width = 300;
                this.dataGridView1.Columns["�����������������"].DisplayIndex = 8;

                this.dataGridView1.Columns["�����"].Width = 200;
                this.dataGridView1.Columns["�����"].DisplayIndex = 9;

                this.dataGridView1.Columns["����������"].Width = 70;
                this.dataGridView1.Columns["����������"].DisplayIndex = 10;

                this.dataGridView1.Columns["logWrite"].Width = 70;
                this.dataGridView1.Columns["logWrite"].DisplayIndex = 11;
                this.dataGridView1.Columns["logWrite"].HeaderText = "��� �������";

                this.dataGridView1.Columns["logDate"].Width = 70;
                this.dataGridView1.Columns["logDate"].DisplayIndex = 12;
                this.dataGridView1.Columns["logDate"].HeaderText = "���� ������";


                ////�������� ���� � ���� logDate ������� ����� �� ������ ���������� ������ ������� �� ��������
                //foreach (DataGridViewRow row in this.dataGridView1.Rows)
                //{
                //    if (row.Cells["logDate"].Value == "")
                //    {
                //        this.dtnWrite.Enabled = false;
                //    }
                //}


            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.checkBox1.Checked == true)
            //{
            //    dtnWrite.Enabled = false;
            //}
            //else
            //{
            //    dtnWrite.Enabled = true;
            //}
        }

        private void btnPoust_Click(object sender, EventArgs e)
        {
            List<ControlDantist.Classes.Letter> list = new List<ControlDantist.Classes.Letter>();

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                SqlTransaction transact = con.BeginTransaction();

                //���������� �����
                int countRow = this.dataGridView1.Rows.Count;

                //������� �����
                int cRow = 1;

                StringBuilder sTest = new StringBuilder();

                //������� �� ���������� DataGridView
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    ControlDantist.Classes.Letter let = new ControlDantist.Classes.Letter();

                    let.Id��� = Convert.ToInt32(row.Cells["id_���"].Value);

                    if (countRow != cRow)
                    {
                        //����� ������������ ��������, ��� ������������ � ��������� (��������� ������ �����)
                        string queryId����� = "select * from dbo.�������������������� " +
                                              "where id_��������� in ( " +
                                              "select id_���������� from dbo.������������������ " +
                                              "where id_����� in ( " +
                                              "select id_����� from dbo.�������� " +
                                              "where id_�������� in ( " +
                                              "select id_�������� from dbo.������� " +
                                              "where id_������� in ( " +
                                              "select id_������� from dbo.������������������� " +
                                              "where id_��� = " + Convert.ToInt32(row.Cells["id_���"].Value) + ")))) ";

                        sTest.Append(queryId�����);

                        DataTable tab����� = ���������.GetTableSQL(queryId�����, "���������", con, transact);

                        if (tab�����.Rows.Count != 0)
                        {
                            //������� ������������ ������
                            let.������� = tab�����.Rows[0]["����������������������"].ToString().Trim();

                            //������� ��� ����������
                            let.��������� = tab�����.Rows[0]["���"].ToString().Trim();

                            //������� ���������
                            let.��������� = tab�����.Rows[0]["���������"].ToString().Trim();
                        }
                        else
                        {
                            //������� ������������ ������
                            let.������� = "��� �� " + " ���� �� ������������ ������ "+" ";

                            //������� ��� ����������
                            let.��������� = "�.� �������";

                            //������� ���������
                            let.��������� = "��������� ������� ��������!";

                        }

                       

                        list.Add(let);

                    }

                    cRow++;

                }

                foreach(ControlDantist.Classes.Letter ln in list)
                {
                    string query = "SELECT * FROM [View_4] where id_��� = "+ ln.Id��� +" ";
                    DataTable tab��� = ���������.GetTableSQL(query,"View");

                    foreach(DataRow r in tab���.Rows)
                    {
                        string ��� = r["�������"].ToString().Trim() + " " + r["���"].ToString().Trim() + " " + r["��������"].ToString().Trim();
                        ln.��� = ���;

                        string ���� = string.Empty;
                        if (r["������"].ToString().Trim() != "")
                        {
                            ���� = "����. " + r["������"].ToString().Trim();
                        }

                        string �� = string.Empty;
                        if (r["������"].ToString().Trim() != "")
                        {
                            �� = " ��. " + r["�������������"].ToString().Trim();
                        }


                        string ����� = "��. " + r["�����"].ToString().Trim() + "  �. " + r["���������"].ToString().Trim() + " " + ���� + " " + ��;
                        ln.����� = �����;

                        string numDoc = r["��������������"].ToString().Trim() + " " + r["��������������"].ToString().Trim();
                        ln.�������������� = numDoc;

                        string summ = Convert.ToDecimal(r["�������������������������"]).ToString("c");
                        ln.C�������� = summ;
                    }

                }


                List<ControlDantist.Classes.Letter> iList = list;

                //��������� �� ������� � �������� ��������

                string queryTO = "SELECT ����������������������  FROM ��������������������";
                DataTable tab��������� = ���������.GetTableSQL(queryTO, "��������������������");

                foreach (DataRow row in tab���������.Rows)
                {
                    string ������������������ = row["����������������������"].ToString();

                    IEnumerable<ControlDantist.Classes.Letter> listLetter = list.Where(lst => lst.������� == ������������������).Select(lst => lst);
                    foreach (ControlDantist.Classes.Letter letPrint in listLetter)
                    {

                    }
                }



            }
        


            ////������� �� ���������� DataGridView
            //foreach (DataGridViewRow row in this.dataGridView1.Rows)
            //{

            //    int i = Convert.ToInt32(row.Cells["id_���"].Value);

            //    string query = "SELECT     ��������������������_1.���������������������� AS '���� �����', derivedtbl_1.���, derivedtbl_3.���������, derivedtbl_2.�������, " +
            //                   "derivedtbl_2.���, derivedtbl_2.��������, derivedtbl_2.�����, derivedtbl_2.���������, derivedtbl_2.������, derivedtbl_2.�������������, " +
            //                   "derivedtbl_2.��������������, derivedtbl_2.��������������, derivedtbl_2.�������������������, " +
            //                   "derivedtbl_4.������������������������� " +
            //                   "FROM         (SELECT     ���������������������� " +
            //                   "FROM          dbo.�������������������� AS �������������������� " +
            //                   "WHERE      (id_��������� IN " +
            //                                      "(SELECT     id_���������� " +
            //                                        "FROM          dbo.������������������ " +
            //                                        "WHERE      (id_����� = " +
            //                                                                   "(SELECT     id_����� " +
            //                                                                     "FROM          dbo.�������� " +
            //                                                                     "WHERE      (id_�������� = " +
            //                                                                                                "(SELECT     id_�������� " +
            //                                                                                                  "FROM          dbo.������� " +
            //                                                                                                  "WHERE      (id_������� = " +
            //                                                                                                                             "(SELECT     id_������� " +
            //                                                                                                                               "FROM          dbo.������������������� " +
            //                                                                                                                               "WHERE      (id_��� = " + Convert.ToInt32(row.Cells["id_���"].Value) + ")))))))))) AS ��������������������_1 CROSS JOIN " +
            //              "(SELECT     ��� " +
            //                "FROM          dbo.�������������������� AS ��������������������_2 " +
            //                "WHERE      (id_��������� IN " +
            //                                           "(SELECT     id_���������� " +
            //                                             "FROM          dbo.������������������ AS ������������������_1 " +
            //                                             "WHERE      (id_����� = " +
            //                                                                        "(SELECT     id_����� " +
            //                                                                          "FROM          dbo.�������� AS ��������_1 " +
            //                                                                          "WHERE      (id_�������� = " +
            //                                                                                                     "(SELECT     id_�������� " +
            //                                                                                                       "FROM          dbo.������� AS �������_1 " +
            //                                                                                                       "WHERE      (id_������� = " +
            //                                                                                                                                  "(SELECT     id_������� " +
            //                                                                                                                                    "FROM          dbo.������������������� AS �������������������_1 " +
            //                                                                                                                                    "WHERE      (id_��� = " + Convert.ToInt32(row.Cells["id_���"].Value) + ")))))))))) AS derivedtbl_1 CROSS JOIN " +
            //              "(SELECT     ��������� " +
            //                "FROM          dbo.�������������������� AS ��������������������_3 " +
            //                "WHERE      (id_��������� IN " +
            //                                           "(SELECT     id_���������� " +
            //                                             "FROM          dbo.������������������ AS ������������������_2 " +
            //                                             "WHERE      (id_����� = " +
            //                                                                        "(SELECT     id_����� " +
            //                                                                          "FROM          dbo.�������� AS ��������_2 " +
            //                                                                          "WHERE      (id_�������� = " +
            //                                                                                                     "(SELECT     id_�������� " +
            //                                                                                                       "FROM          dbo.������� AS �������_2 " +
            //                                                                                                       "WHERE      (id_������� = " +
            //                                                                                                                                  "(SELECT     id_������� " +
            //                                                                                                                                    "FROM          dbo.������������������� AS �������������������_2 " +
            //                                                                                                                                    "WHERE      (id_��� = " + Convert.ToInt32(row.Cells["id_���"].Value) + ")))))))))) AS derivedtbl_3 CROSS JOIN " +
            //              "(SELECT     �������, ���, ��������, �����, ���������, ������, �������������, ��������������, ��������������, " +
            //                                       "������������������� " +
            //                "FROM          dbo.�������� AS ��������_3 " +
            //                "WHERE      (id_�������� = " +
            //                                           "(SELECT     id_�������� " +
            //                                             "FROM          dbo.������� AS �������_3 " +
            //                                             "WHERE      (id_������� = " +
            //                                                                        "(SELECT     id_������� " +
            //                                                                          "FROM          dbo.������������������� AS �������������������_3 " +
            //                                                                          "WHERE      (id_��� = " + Convert.ToInt32(row.Cells["id_���"].Value) + ")))))) AS derivedtbl_2 CROSS JOIN " +
            //              "(SELECT     ������������������������� " +
            //                "FROM          dbo.������� AS �������_4 " +
            //                "WHERE      (id_������� = " +
            //                                           "(SELECT     id_������� " +
            //                                             "FROM          dbo.������������������� AS �������������������_4 " +
            //                                             "WHERE      (id_��� = " + Convert.ToInt32(row.Cells["id_���"].Value) + ")))) AS derivedtbl_4 ";

            //    //�������� �����
            //    DataTable table = ���������.GetTableSQL(query, "Letter");

            //    foreach (DataRow r in table.Rows)
            //    {
            //        Letter letter = new Letter();
            //        letter.������� = r["���� �����"].ToString().Trim();

            //        letter.��������� = r["���"].ToString().Trim();
            //        letter.��������� = r["���������"].ToString().Trim();

            //        string fio = r["�������"].ToString().Trim() + " " + r["���"].ToString().Trim() + " " + r["��������"].ToString().Trim();
            //        letter.��� = fio;

            //        string address = r["�����"].ToString().Trim() + " " + r["���������"].ToString().Trim() + " " + r["������"].ToString().Trim() + " " + r["�������������"].ToString().Trim();
            //        letter.����� = address.Trim();

            //        string numDoc = r["��������������"].ToString().Trim() + " " + r["��������������"].ToString().Trim() + " " + r["�������������������"].ToString().Trim();
            //        letter.�������������� = numDoc.Trim();

            //        letter.C�������� = Convert.ToDecimal(r["�������������������������"]).ToString("c");
            //        list.Add(letter);

            //    }


            //}

            //List<Letter> iList = list;

            //string queryTab = "SELECT [����������������������] FROM [��������������������]";
            //DataTable dt = ���������.GetTableSQL(queryTab, "��������������������");

            //int iCount = 0;
            //Dictionary<int, Letter> dictionary = new Dictionary<int, Letter>();

            //foreach (DataRow rdt in dt.Rows)
            //{

            //    foreach (Letter let in list)
            //    {
            //        if (let.������� == rdt["����������������������"].ToString().Trim())
            //        {
            //            dictionary.Add(iCount, let);
            //        }
            //    }

            //    iCount++;
            //}

           // Dictionary<int, Letter> iTest = dictionary;


        }

        private void button1_Click(object sender, EventArgs e)
        {

            // ��������� ������� ��������� �����.

             if (this.checkBox1.Checked == false)
             {
                  //������������ ����� �� ������ ����-�������.
                 if (this.textBox2.Text.Length > 0 && this.textBox1.Text.Length == 0)
                 {
                      //����� �� ������ ���� �������.
                     FindReestrInvoce reestr = new FindReestrInvoce();
                     DataTable tab = reestr.FindInvoice(this.textBox2.Text, this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString());

                     if (tab.Rows.Count > 0)
                     {
                         LoadDataGridView(tab, reestr.GetCountContract(), false);
                     }
                 }

                 // ����� �� ������ �������.
                 if (this.textBox2.Text.Length == 0 && this.textBox1.Text.Length > 0)
                 {
                      //����� �� ������ �������.
                     //FindReestrInvoce reestr = new FindReestrInvoce();
                     //DataTable tab = reestr.FindNumRegistr(this.textBox1.Text, this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString());

                     FindNumRegistr registr = new FindNumRegistr();
                     DataTable tab = registr.FindRegistrNum(this.textBox1.Text, this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString());

                     if (tab.Rows.Count > 0)
                     {
                         LoadDataGridView(tab, registr.GetCountContract(), true);
                     }
                 }
             }
             else
             {
                  //����� �� ������ ������� � ������ ���� �������.
                 //FindReestrInvoce reestr = new FindReestrInvoce();
                 //DataTable tab = reestr.FindInvoiceAndNumRegistr(this.textBox2.Text, this.textBox1.Text, this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString());

                 FIndRegistrInvoiceAndNumRegistr reestr = new FIndRegistrInvoiceAndNumRegistr();
                 DataTable tab = reestr.FindInvoiceAndNumRegistr(this.textBox2.Text, this.textBox1.Text, this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString());

                 if (tab.Rows.Count > 0)
                 {
                     LoadDataGridView(tab, reestr.GetCountContract(), true);
                 }

             }


        }

        /// <summary>
        /// ����� ������ �� �����.
        /// </summary>
        /// <param name="tab">������� � ���������</param>
        /// <param name="iCountD">���������� ���������</param>
        private void LoadDataGridView(DataTable tab, int iCountD, bool flagLetter)
        {
            this.dataGridView1.DataSource = tab;

            this.dataGridView1.Columns["id_���"].Width = 100;
            this.dataGridView1.Columns["id_���"].Visible = false;

            this.dataGridView1.Columns["id_�������"].Width = 100;
            this.dataGridView1.Columns["id_�������"].Visible = false;

            this.dataGridView1.Columns["���������"].Width = 100;
            this.dataGridView1.Columns["���������"].DisplayIndex = 1;
            this.dataGridView1.Columns["���������"].HeaderText = "� ����";

            this.dataGridView1.Columns["������������"].Width = 100;
            this.dataGridView1.Columns["������������"].DisplayIndex = 2;
            this.dataGridView1.Columns["������������"].HeaderText = "� �������";

            this.dataGridView1.Columns["����������������"].Width = 100;
            this.dataGridView1.Columns["����������������"].DisplayIndex = 3;
            this.dataGridView1.Columns["����������������"].HeaderText = "� ����-�������";

            this.dataGridView1.Columns["�������������"].Width = 100;
            this.dataGridView1.Columns["�������������"].DisplayIndex = 4;
            this.dataGridView1.Columns["�������������"].HeaderText = "� ��������";

            this.dataGridView1.Columns["�������"].Width = 200;
            this.dataGridView1.Columns["�������"].DisplayIndex = 5;

            this.dataGridView1.Columns["���"].Width = 200;
            this.dataGridView1.Columns["���"].DisplayIndex = 6;

            this.dataGridView1.Columns["��������"].Width = 200;
            this.dataGridView1.Columns["��������"].DisplayIndex = 7;

            this.dataGridView1.Columns["�����������������"].Width = 300;
            this.dataGridView1.Columns["�����������������"].DisplayIndex = 8;

            this.dataGridView1.Columns["�����"].Width = 200;
            this.dataGridView1.Columns["�����"].DisplayIndex = 9;

            this.dataGridView1.Columns["������������������"].Width = 70;
            this.dataGridView1.Columns["������������������"].DisplayIndex = 10;

            this.dataGridView1.Columns["�����������"].Width = 70;
            this.dataGridView1.Columns["�����������"].DisplayIndex = 11;

            //this.dataGridView1.Columns["������������������"].Width = 70;
            //this.dataGridView1.Columns["������������������"].DisplayIndex = 12;

            if (flagLetter == false)
            {
                this.dataGridView1.Columns["������������������������"].Width = 70;
                this.dataGridView1.Columns["������������������������"].DisplayIndex = 12;


                this.dataGridView1.Columns["logWrite"].Width = 70;
                this.dataGridView1.Columns["logWrite"].DisplayIndex = 13;
                this.dataGridView1.Columns["logWrite"].HeaderText = "��� �������";

                //this.dataGridView1.Columns["logDate"].Width = 70;
                //this.dataGridView1.Columns["logDate"].DisplayIndex = 14;
                //this.dataGridView1.Columns["logDate"].HeaderText = "���� ������";


                //�������� ���� � ���� logDate ������� ����� �� ������ ���������� ������ ������� �� ��������
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if (row.Cells["logDate"].Value != "")
                    {
                        this.dtnWrite.Enabled = true;
                    }
                    else
                    {
                        this.dtnWrite.Enabled = false;
                    }
                }



                //������������ ���������� ������
                this.dataGridView1.Columns["������������"].Width = 70;
                this.dataGridView1.Columns["������������"].DisplayIndex = 14;
                this.dataGridView1.Columns["������������"].HeaderText = "��������� �����";

                // ����� ���������� ���������
                this.dataGridView1.Columns["�����"].Width = 70;
                this.dataGridView1.Columns["�����"].DisplayIndex = 15;
                this.dataGridView1.Columns["�����"].HeaderText = "�����";

                // ����� ������� � ������� ��������� ��������
                this.dataGridView1.Columns["�����"].Width = 70;
                this.dataGridView1.Columns["�����"].DisplayIndex = 16;
                this.dataGridView1.Columns["�����"].HeaderText = "�����";
            }
            else
            {
                //������������ ���������� ������
                this.dataGridView1.Columns["������������"].Width = 70;
                this.dataGridView1.Columns["������������"].DisplayIndex = 14;
                this.dataGridView1.Columns["������������"].HeaderText = "��������� �����";

                // ����� ���������� ���������
                this.dataGridView1.Columns["�����"].Width = 70;
                this.dataGridView1.Columns["�����"].DisplayIndex = 15;
                this.dataGridView1.Columns["�����"].HeaderText = "�����";

                // ����� ������� � ������� ��������� ��������
                this.dataGridView1.Columns["�����"].Width = 70;
                this.dataGridView1.Columns["�����"].DisplayIndex = 16;
                this.dataGridView1.Columns["�����"].HeaderText = "�����";
            }

            //������� ���������� ��������� �� �����
            this.label4.Visible = true;
            this.label4.Text = String.Format("���������� ��������� = {0}", iCountD);
        }
    }
}