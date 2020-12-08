using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ControlDantist.Classes;

namespace ControlDantist
{
    public partial class FormDepartment : Form
    {
        private int id = 0;
        public FormDepartment()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            ��������� parm = new ���������();
            parm.���������������������� = this.textBox1.Text.Trim();

            if (parm.���������������������� != string.Empty)
            {
                string insertQuery = "INSERT INTO �������������������� " +
                                     "([����������������������] " +
                                     ",[���] " +
                                     ",[���������] ) " +
                                     "VALUES " +
                                     "('" + parm.���������������������� + "' " +
                                     ",'"+ this.textBox2.Text.Trim() +"' " +
                                     ",'"+ this.textBox3.Text +"' )";

                ExecuteQuery.Execute(insertQuery);
                LoadUpdate();

                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
            }
            else
            {
                MessageBox.Show("������� ������������ ���� ������");
            }
        }

        private void LoadUpdate()
        {
            string query = "select id_���������,����������������������,���,��������� from ��������������������";
            DataTable dt = ���������.GetTableSQL(query, "��������������������");

            this.dataGridView1.DataSource = dt;

            this.dataGridView1.Columns["id_���������"].Visible = false;
            this.dataGridView1.Columns["����������������������"].HeaderText = "������������ ��";
            this.dataGridView1.Columns["����������������������"].Visible = true;

            this.dataGridView1.Columns["���"].Visible = false;
            this.dataGridView1.Columns["���������"].Visible = false;
            

        }

        struct ���������
        {
            private string ������������������;

            /// <summary>
            /// �������� ���������������� ������
            /// </summary>
            public string ����������������������
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

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDepartment_Load(object sender, EventArgs e)
        {
            LoadUpdate();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ��������� parm = new ���������();
            parm.���������������������� = this.textBox1.Text.Trim();

            string updateQuery = "UPDATE �������������������� " +
                                 "SET [����������������������] = '" + parm.���������������������� + "' " +
                                 ",[���] = '"+ this.textBox2.Text.Trim() +"' " +
                                 ",[���������] = '"+ this.textBox3.Text.Trim() +"' " +
                                 "WHERE id_��������� = "+ id +" ";

            ExecuteQuery.Execute(updateQuery);
            LoadUpdate();

            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            id = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //������� id ���� ������
            id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_���������"].Value);

            //������� �������� ���� ������
            string name = this.dataGridView1.CurrentRow.Cells["����������������������"].Value.ToString();
            this.textBox1.Text = name.Trim();

            //�������� ���
            string fio = this.dataGridView1.CurrentRow.Cells["���"].Value.ToString();
            this.textBox2.Text = fio.Trim();

            //�������� ���������
            string ��������� = this.dataGridView1.CurrentRow.Cells["���������"].Value.ToString();
            this.textBox3.Text = ���������.Trim();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from �������������������� " +
                                 "where id_��������� = "+ id +" ";

            ExecuteQuery.Execute(deleteQuery);
            LoadUpdate();

            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            id = 0;
        }
    }
}