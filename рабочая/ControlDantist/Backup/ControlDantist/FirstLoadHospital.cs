using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Classes;


using System.Globalization;
using System.Threading;


namespace ControlDantist
{
    public partial class FirstLoadHospital : Form
    {
        //������ ��������� ������� � ����� ����� �� ��������� ��
        private DataTable tab���������;

        //������ ��������� ������� � ��������������� ����� �� ���������� ��
        private DataTable tab������������;
        
        //������ id ������������
        private int id_Hosp;

        //���������� ������ �������� �������������� �����
        private string ������������������ = string.Empty;

        //������ id �������������
        private int _id;

        //������ id �������
        private int id_time;

        /// <summary>
        /// ������ �������� ���������� �������������
        /// </summary>
        public int ID_�������������
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        //������ id ������
        private int id_�����;

        public FirstLoadHospital()
        {
            InitializeComponent();

            //��������� ������� ��������
            CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCInfo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "���� ���� ������";
            openFileDialog1.Filter = "|*.mdb";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtBdPach.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.txtNameHospital.Text.Trim() != "" && this.txt���.Text.Trim() != "" && this.textBox1.Text.Length != 0 && this.txtBdPach.Text.Length != 0)
            {
                //================������� ������ �� ��������� ��

                //������� ������ ����������� � ��������� ��
                string connectStringAcess = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.txtBdPach.Text + " ";
                using (OleDbConnection con = new OleDbConnection(connectStringAcess))
                {
                    con.Open();
                    OleDbTransaction transact = con.BeginTransaction();

                    //������� ������������� �����
                    string query������� = "select * from �������������������";
                    tab������������ = ���������.GetTable(query�������, "�������������������", con, transact);

                    ////������� ���� �����
                    //string query�������� = "select * from ���������";
                    //tab��������� = ���������.GetTable(query��������, "�������������������", con, transact);

                }

                //������� ���������� ������ � ���� �� SQL SERVER � �������������� �������

                //����������� � SQL server�

                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    //SqlTransaction transact = con.BeginTransaction();

                    //������� ������������
                    string insertHosp = "INSERT INTO ������������ " +
                                        "([������������������������] " +
                                        ",[���������������] " +
                                        ",[����������������] " +
                                        ",[����������������] " +
                                        ",[id_��������] " +
                                        ",[id_�������] " +
                                        ",[������������������������] " +
                                        ",[���] " +
                                        ",[���] " +
                                        ",[���] " +
                                        ",[�����������������] " +
                                        ",[�������������] " +
                                        ",[�����������] " +
                                        ",[�������������] " +
                                        ",[�����������������������] " +
                                        ",[����] " +
                                        ",[�����������������������������] " +
                                        ",[���������������������] " +
                                        ",[�������������] " +
                                        ",[����] " +
                                        ",[�����] " +
                                        ",[Flag] " +
                                        ",[����������������������]) " +
                                        "VALUES " +
                                        "('" + this.txtNameHospital.Text.Trim() + "' " +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",0" +
                                        ",0" +
                                        ",''" +
                                        ",'" + this.txt���.Text.Trim() + "' " +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",'" + DateTime.Today.ToShortDateString() + "' " +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",''" +
                                        ",0" +
                                        ",0" + ")";

                    //�������� ������ �� �������
                    SqlCommand comInsertHosp = new SqlCommand(insertHosp, con);
                    //comInsertHosp.Transaction = transact;
                    comInsertHosp.ExecuteNonQuery();

                    //������� id ���������� ���������
                    string queryTime = "select id_����� from [�����������������] where id_������������� = "+ this.ID_������������� +" ";
                    SqlCommand comInsertTime = new SqlCommand(queryTime, con);
                    //comInsertTime.Transaction = transact;

                    SqlDataReader readTime = comInsertTime.ExecuteReader();
                    while (readTime.Read())
                    {
                        id_time = Convert.ToInt32(readTime["id_�����"]);
                    }
                    readTime.Close();

                }

                //������� ������� ������������
                string querySelectHosp = "select id_������������ from ������������ where ��� = '" + this.txt���.Text.Trim() + "' ";

                SqlConnection conS = new SqlConnection(ConnectDB.ConnectionString());
                conS.Open();
                SqlCommand comSelHosp = new SqlCommand(querySelectHosp, conS);

                SqlDataReader read = comSelHosp.ExecuteReader();

                while (read.Read())
                {
                    id_Hosp = Convert.ToInt32(read["id_������������"]);
                }
                read.Close();
                conS.Close();

                //������� �� SQL Server ������ � ������������� �����
                foreach (DataRow row����� in tab������������.Rows)
                {
                    //������� ������� �������� �������������� ����� � �������
                    string queryIns������� = "INSERT INTO ������������������ " +
                                             "([�������������������] " +
                                             ",[id_������������] " +
                                             ",id_������������� ) " +
                                             "VALUES " +
                                             "('" + row�����["�������������������"].ToString() + "' " +
                                             "," + id_Hosp + " " +
                                             "," + this.ID_������������� + " ) ";

                    SqlConnection con = new SqlConnection(ConnectDB.ConnectionString());
                    SqlCommand comInsert���������� = new SqlCommand(queryIns�������, con);

                    con.Open();
                    comInsert����������.ExecuteNonQuery();
                    con.Close();

                    //������� ������� id ������������� �����
                    string querId����� = "select top 1 id_���������,������������������� from ������������������ order by id_��������� desc";
                    SqlConnection conSel = new SqlConnection(ConnectDB.ConnectionString());

                    conSel.Open();
                    SqlCommand comSelId����� = new SqlCommand(querId�����, conSel);

                    //comSelId�����.Transaction = transact;
                    SqlDataReader readId����� = comSelId�����.ExecuteReader();
                    while (readId�����.Read())
                    {
                        id_����� = Convert.ToInt32(readId�����["id_���������"]);
                        ������������������ = readId�����["�������������������"].ToString();
                    }
                    readId�����.Close();

                    //������� ���� �����
                    OleDbConnection conOLE = new OleDbConnection(connectStringAcess);
                    conOLE.Open();

                    string query�������� = "select * from ��������� where id_��������� in (select id_��������� from ������������������� where ������������������� = '" + ������������������ + "')";
                    //tab��������� = ���������.GetTable(query��������,conOLE, "�������������������");
                    tab��������� = ���������.GetTable(query��������, connectStringAcess, "�������������������");
                    conOLE.Close();


                    //������� ���� ����� �������������� �������� ��������������
                    foreach (DataRow row in tab���������.Rows)
                    {
                        string queryInsert�������� = "INSERT INTO [��������] " +
                                                     "([���������] " +
                                                     ",[����] " +
                                                     ",[id_������������] " +
                                                     ",[��������������] " +
                                                     ",[�������] " +
                                                     ",[id_���������] " +
                                                     ",[�������] " +
                                                     ",id_����� " +
                                                     ",id_������������� ) " +
                                                     "VALUES " +
                                                     "('" + row["���������"].ToString() + "' " +
                                                     "," + Convert.ToDecimal(row["����"]) + " " +
                                                     "," + id_Hosp + " " +
                                                     ",'" + row["��������������"].ToString() + "' " +
                                                     ",'" + Convert.ToBoolean(row["�������"]) + "' " +
                                                     "," + id_����� + " " +
                                                     ",0" +
                                                     ","+ this.id_time +" " +
                                                     "," + this.ID_������������� + " ) ";

                        SqlConnection con�������� = new SqlConnection(ConnectDB.ConnectionString());
                        con��������.Open();

                        SqlCommand com = new SqlCommand(queryInsert��������, con��������);

                        com.ExecuteNonQuery();
                        con��������.Close();
                    }
                }

                //    con.Close();
                //}


            }
            else
            {
                MessageBox.Show("�� ������� �������� ������");
            }
        }

        private void FirstLoadHospital_Load(object sender, EventArgs e)
        {
            //string queryLoad = "select * from dbo.��������������������������";

            //SqlConnection con = new SqlConnection(ConnectDB.ConnectionString());
            //SqlDataAdapter da = new SqlDataAdapter(queryLoad, con);

            //con.Open();
            //DataTable tab = new DataTable("�������������");
            
            //da.Fill(tab);
            //con.Close();

            ////�������� �������������� ������ � ��������������� ��������������
            ////��������������,�����,�����������������
            //this.comboBox1.SelectedIndex = 

        }

        private void btn�������������_Click(object sender, EventArgs e)
        {
            FormSelect������������� form = new FormSelect�������������();
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                this.textBox1.Text = form.�������������;
                this.ID_������������� = form.ID_��������������;
            }
        }
    }
}