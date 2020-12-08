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
    public partial class FormHeadDepartment : Form
    {
        private int id_�������� = 0;
        public FormHeadDepartment()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length != 0 && this.textBox2.Text.Length != 0)
            {
                string ��� = this.textBox1.Text.Trim() + " " + this.textBox3.Text.Trim();

                string queryInsert = "INSERT INTO ���������������������� " +
                                     "([���������] " +
                                     ",[�������] " +
                                     ",[��������] " +
                                     ",���_�������� ) " +
                                     "VALUES " +
                                    "('" + this.textBox2.Text.Trim() + "' " +
                                    ",'" + this.textBox1.Text.Trim() + "' " +
                                    ",'"+ this.textBox3.Text.Trim() +"'"  +
                                    ",'" + ���.Trim() + "' )";

                //�������� ����������
                ExecuteQuery.Execute(queryInsert);

                //������� ���������� DataGrid
                UpdateDataGrid();
            }
        }

        private void FormHeadDepartment_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();

            //DataTable tab���������;

            //using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            //{
            //    //������� ����������
            //    con.Open();

            //    //�������� �� � ������ ����������
            //    SqlTransaction transact = con.BeginTransaction();

            //    string queryTable = "select * from ���������������������� ";
            //    tab��������� = ���������.GetTableSQL(queryTable, "����������������������", con, transact);
            //}

            //this.dataGridView1.DataSource = tab���������;
            //this.dataGridView1.Columns["id_���������"].Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //������� id ����������
                id_�������� = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_���������"].Value);
            }
            catch
            {

            }

            //��������� ����������� ���������� ������
            DataGridViewRow rowCurrent = this.dataGridView1.CurrentRow;
            this.textBox1.Text = rowCurrent.Cells["�������"].Value.ToString().Trim();

            this.textBox3.Text = rowCurrent.Cells["��������"].Value.ToString().Trim();
            this.textBox2.Text = rowCurrent.Cells["���������"].Value.ToString().Trim();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ��� = this.textBox1.Text.Trim() + this.textBox3.Text.Trim();
            string updateQuery = "UPDATE ���������������������� " +
                                "SET [���������] = '"+ this.textBox2.Text.Trim() +"' " +
                                ",[�������] = '"+ this.textBox1.Text.Trim() +"' " +
                                ",[��������] = '"+ this.textBox3.Text.Trim() +"' " +
                                ",���_��������  = '"+ ���.Trim() +"' " +
                                "WHERE [id_���������] = "+ id_�������� +" ";

            //�������� ����������
            ExecuteQuery.Execute(updateQuery);

            //������� ���������� DataGrid
            UpdateDataGrid();

        }

        /// <summary>
        /// ��������� ���������� DataGrid
        /// </summary>
        private void UpdateDataGrid()
        {
            //������� ���������� DataGridView
            this.dataGridView1.DataSource = "";

            DataTable tab���������;

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                //������� ����������
                con.Open();

                //�������� �� � ������ ����������
                SqlTransaction transact = con.BeginTransaction();

                string queryTable = "select * from ���������������������� ";
                tab��������� = ���������.GetTableSQL(queryTable, "����������������������", con, transact);
            }

            this.dataGridView1.DataSource = tab���������;
            this.dataGridView1.Columns["id_���������"].Visible = false;
            this.dataGridView1.Columns["���_��������"].Visible = false;

            this.textBox1.Text = "";
            this.textBox3.Text = "";
            this.textBox2.Text = "";
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            StringBuilder build = new StringBuilder();

            string deleteQuery1 = "DELETE FROM [����������������������] " +
                                 "WHERE id_��������� = " + id_�������� + " ";
            build.Append(deleteQuery1);

            string deleteQuery2 = " delete FROM ���������������� " +
                                  "where id_��������� = "+ id_�������� +" ";
            build.Append(deleteQuery2);

            //�������� ����������
            ExecuteQuery.Execute(build.ToString());

            //������� ���������� DataGrid
            UpdateDataGrid();
        }

      
    }
}