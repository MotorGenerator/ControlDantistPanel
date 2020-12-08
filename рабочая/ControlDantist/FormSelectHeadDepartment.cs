using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Classes;

namespace ControlDantist
{
    public partial class FormSelectHeadDepartment : Form
    {
        //������� �������� id �� ���������� �� ������� ������ ���������� ���������� �������������
        private DataTable tabCurrentSelect;


        public FormSelectHeadDepartment()
        {
            InitializeComponent();
        }

        private void FormSelectHeadDepartment_Load(object sender, EventArgs e)
        {

             //���������� 
            DataTable tabIdCurrent;
            string item1 = string.Empty;

            //�������� ������� ���� ���������� � �� ����������� ���������� ������������� 
            DataTable tabSelect;
            string query = "select * from [����������������������] ";

            using(SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                SqlTransaction transact = con.BeginTransaction();
                tabSelect = ���������.GetTableSQL(query, "����������������������", con, transact);

                //������ ��� �� ���������� ������ �� ������� ������
                string stringSelect = "select id_��������� from ����������������";
                tabIdCurrent = ���������.GetTableSQL(stringSelect, "����������������������1", con, transact);

                if (tabIdCurrent.Rows.Count != 0)
                {
                    string queryCurrent = "select * from [����������������������] where id_��������� = " + Convert.ToInt32(tabIdCurrent.Rows[0]["id_���������"]) + " ";
                    tabCurrentSelect = ���������.GetTableSQL(queryCurrent, "����������������������2", con, transact);
                    //item1 = tabCurrentSelect.Rows[0]["�������"].ToString().Trim() +" " + tabCurrentSelect.Rows[0]["��������"].ToString().Trim();
                    item1 = tabCurrentSelect.Rows[0]["���_��������"].ToString().Trim(); 

                }
            }

            foreach (DataRow row in tabSelect.Rows)
            {
                string item = row["���_��������"].ToString().Trim();
                this.comboBox1.Items.Add(item);
            }


            this.comboBox1.Text = item1;

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //������ �����
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string stringUpdate = "declare @id int " +
                                  "select @id = id_��������� from dbo.���������������������� where ���_�������� = '" + this.comboBox1.Text.Trim() + "' " +
                                  "declare @count int " +
                                  "select @count = COUNT(*) from ���������������� " +
                                  "if(@count = 0) " +
                                  "begin " +
                                  "INSERT INTO ���������������� " +
                                  "([id_���������]) " +
                                  "VALUES " +
                                  "(@id) " +
                                  "end " +
                                  "else " +
                                  "begin " +
                                  "declare @iid int " +
                                  "select @iid = id_��������� from ���������������� " +
                                  "UPDATE ���������������� " +
                                  "SET [id_���������] = @id " +
                                  "WHERE id_��������� = @iid " +
                                  "end ";

           ExecuteQuery.Execute(stringUpdate);
           this.Close();
        }
    }
}