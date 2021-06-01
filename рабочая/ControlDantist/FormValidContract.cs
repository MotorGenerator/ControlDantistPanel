using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Classes;
using DantistLibrary;
using ControlDantist.Find;
using System.Linq;
using ControlDantist.ValidPersonContract;


namespace ControlDantist
{
    public partial class FormValidContract : Form
    {
        //������ �������� ����������� ��������
        private bool flagValid;
        private bool flagCheck;

        // ������ ��� �������� ����������� ������.
        private List<FindPersonNumContractItem> listPerson = new List<FindPersonNumContractItem>();

        /// <summary>
        /// ������ ��������� ����� ������ ���������� ������ �����. ��������� �������� = TRUE � �� ��������� �������� = FALSE
        /// </summary>
        public bool FlagValid
        {
            get
            {
                return flagValid;
            }
            set
            {
                flagValid = value;
            }
        }

        /// <summary>
        /// ������ ���� ����������� ��� ������� ��������� �� �������� (true) ���� (false ��� null = �� ������� ������ �������� ��� �������)
        /// </summary>
        public bool FlagCheck
        {
            get
            {
                return flagCheck;
            }
            set
            {
                flagCheck = value;
            }
        }

        public FormValidContract()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //������� �����
            this.Close();
        }

        private void FormValidContract_Load(object sender, EventArgs e)
        {
            //�� ����� ��� �������� ���������� ���������� (����� �� ��������� ���������)
            //string query = string.Empty;

            ////���� ������ ��������
            //if (this.FlagValid == true && this.FlagCheck == false)
            //{
            //    query = "SELECT �������.id_�������, �������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite  " +
            //              "FROM ������� INNER JOIN �������� " +
            //              "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
            //              "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
            //              "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
            //              "where �������.������������ = 'True' " +
            //              "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite  ";
            //}
            ////else

            ////���� �� ������ ��������
            //if (this.FlagValid == false && this.FlagCheck == false)
            //{
            //    query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite  " +
            //              "FROM ������� INNER JOIN �������� " +
            //              "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
            //              "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
            //              "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
            //              "where �������.������������ = 'False' " +
            //              "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite  ";

            //}

            ////��������� ������
            ////���� �� ������ ��������
            //if (this.FlagValid == false && this.FlagCheck == true)
            //{
            //    query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite  " +
            //          "FROM ������� INNER JOIN �������� " +
            //          "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
            //          "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
            //          "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
            //          "where �������.������������ = 'False' and �������.���������� = 'True' " +
            //          "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite  ";

            //}


            //using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            //{
            //    con.Open();
            //    SqlTransaction transact = con.BeginTransaction();

            //    //DataTable dtContract = ���������.GetTableSQL(query, "����������������", con, transact);
            //    SqlCommand com = new SqlCommand(query, con);
            //    com.Transaction = transact;

            //    DataTable tab = new DataTable();
            //    DataColumn col0 = new DataColumn("id_�������", typeof(int));
            //    tab.Columns.Add(col0);
            //    DataColumn col1 = new DataColumn("�������������",typeof(string));
            //    tab.Columns.Add(col1);
            //    DataColumn col2 = new DataColumn("�������", typeof(string));
            //    tab.Columns.Add(col2);
            //    DataColumn col3 = new DataColumn("���", typeof(string));
            //    tab.Columns.Add(col3);
            //    DataColumn col4 = new DataColumn("��������", typeof(string));
            //    tab.Columns.Add(col4);
            //    DataColumn col5 = new DataColumn("�����������������", typeof(string));
            //    tab.Columns.Add(col5);
            //    DataColumn col6 = new DataColumn("�����", typeof(string));
            //    tab.Columns.Add(col6);
            //    DataColumn col7 = new DataColumn("����", typeof(string));
            //    tab.Columns.Add(col7);
            //    DataColumn col8 = new DataColumn("����������", typeof(string));
            //    tab.Columns.Add(col8);


            //    SqlDataReader read = com.ExecuteReader();
            //    while (read.Read())
            //    {
            //        DataRow row = tab.NewRow();
            //        row["id_�������"] = read["id_�������"].ToString().Trim();
            //        row["�������������"] = read["�������������"].ToString().Trim();
            //        row["�������"] = read["�������"].ToString().Trim();
            //        row["���"] = read["���"].ToString().Trim();
            //        row["��������"] = read["��������"].ToString().Trim();
            //        row["�����������������"] = read["�����������������"].ToString().Trim();
            //        row["�����"] = Convert.ToDecimal(read["�����"]).ToString("c").Trim();
            //        if (read["������������������"] != DBNull.Value)
            //        {
            //            row["����"] = Convert.ToDateTime(read["������������������"]).ToShortDateString().Trim();
            //        }
            //        if (read["logWrite"] != DBNull.Value)
            //        {
            //            row["����������"] = read["logWrite"].ToString().Trim();
            //        }
            //        tab.Rows.Add(row);
            //    }

            //    this.dataGridView1.DataSource = tab;

            //    this.dataGridView1.Columns["id_�������"].Width = 100;
            //    this.dataGridView1.Columns["id_�������"].Visible = false;

            //    this.dataGridView1.Columns["�������������"].Width = 100;
            //    this.dataGridView1.Columns["�������������"].DisplayIndex = 0;

            //    this.dataGridView1.Columns["�������"].Width = 200;
            //    this.dataGridView1.Columns["�������"].DisplayIndex = 1;

            //    this.dataGridView1.Columns["���"].Width = 200;
            //    this.dataGridView1.Columns["���"].DisplayIndex = 2;

            //    this.dataGridView1.Columns["��������"].Width = 200;
            //    this.dataGridView1.Columns["��������"].DisplayIndex = 3;

            //    this.dataGridView1.Columns["�����������������"].Width = 300;
            //    this.dataGridView1.Columns["�����������������"].DisplayIndex = 4;

            //    this.dataGridView1.Columns["�����"].Width = 200;
            //    this.dataGridView1.Columns["�����"].DisplayIndex = 5;

            //    this.dataGridView1.Columns["����"].Width = 70;
            //    this.dataGridView1.Columns["����"].DisplayIndex = 6;

            //    this.dataGridView1.Columns["����������"].Width = 70;
            //    this.dataGridView1.Columns["����������"].DisplayIndex = 7;
            //}

            // ������� ���������� ���.
            string query = "select YEAR(������������������) from ������� " +
                                    "group by ������������������ " +
                                    "having ������������������ is not null";


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = string.Empty;

            if (this.textBox1.Text.Trim() == "5/8785")
            {
                var asd = "";
            }

            ShowResultPerson showResultPerson;

            // ������ ��� ����������� ����������� ������.
            List<ValideContract> listDisplay = new List<ValideContract>();

            //���� ������ ��������
            if (this.FlagValid == true && this.FlagCheck == false)
            {
                // ��������� c����� ���������� ��� ��������� ��������.
                List<ValideContract> listTempDisplay = new List<ValideContract>();

                // ���������� ��� �������� ������ �������� ������� ���������� �����.
                string numContract = this.textBox1.Text;

                // ����� ��������� ���������� �������� �� ������ ��������.
                IFindPerson findPerson = new FindContractTo2019(this.textBox1.Text);
                string queryTo2019 = findPerson.Query();

                StringParametr stringParametr = new StringParametr();
                stringParametr.Query = queryTo2019;

                // ����� ������ �������� �� 2019 ��� �� �������� TableAdd.
                IFindPerson fintPerson2019Add = new FindContract2019Add(numContract);
                string query2019Add = fintPerson2019Add.Query();

                StringParametr stringParametr2019Add = new StringParametr();
                stringParametr2019Add.Query = query2019Add;

                // ���� ������ ����� ���������� � �������� ������� �� 2019 ���.
                //  ����� ������ �������� �� 2019 ��� �� ������� ��������.
                IFindPerson findPerson2019 = new FindContract2019(numContract);
                string query2019 = findPerson2019.Query();


                StringParametr stringParametr2019 = new StringParametr();
                stringParametr2019.Query = query2019;

                // ����� ������ �������� ����� 2019 ����.
                IFindPerson fintPersonAftar2019 = new FindPersonAftar2019(numContract);
                string query2019Aftar = fintPersonAftar2019.Query();

                StringParametr stringParametr2019Aftar = new StringParametr();
                stringParametr2019Aftar.Query = query2019Aftar;

                // ����� �������� �� �������� 2021 ����.
                IFindPerson findPerson2021 = new FindPersonAftar(numContract);
                string query2021 = findPerson2021.Query();

                StringParametr stringParametr2021 = new StringParametr();
                stringParametr2021.Query = query2021;


                // �������� ������ �� 2019 ����.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr));

                // �������� ������ 2019 ���� ������� Add.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2019Add));

                // �������� ������ 2019 ����.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2019));

                // �������� ������ ������ 2019 ����.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2019Aftar));

                // �������� ������ 2021.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2021));

                var listRes = CompareContractForNumber.Compare(listTempDisplay);

                // ��������� ������ ���������.
                //List<ValideContract> listDisplayTemp = new List<ValideContract>();

                //listDisplayTemp.AddRange(listRes);

                listDisplay.AddRange(listRes);

                

            }
            //else
            if (this.FlagValid == false && this.FlagCheck == false)
            {
                // ��������� c����� ���������� ��� ��������� ��������.
                List<ValideContract> listTempDisplay = new List<ValideContract>();

                // ����� ��������� ���������� �������� �� ������ ��������.
                IFindPerson findPerson = new FindContractTo2019NoValid(this.textBox1.Text);
                string queryTo2019 = findPerson.Query();

                StringParametr stringParametr = new StringParametr();
                stringParametr.Query = queryTo2019;

                // ����� ������ �������� �� 2019 ��� �� �������� TableAdd.
                IFindPerson fintPerson2019Add = new FindContract2019AddNoValid(this.textBox1.Text);
               string query2019Add = fintPerson2019Add.Query();

                StringParametr stringParametr2019Add = new StringParametr();
                stringParametr2019Add.Query = query2019Add;

                //IFindPerson findPerson2019 = new FindContract2019NoValid(this.textBox1.Text);
                //string query2019 = findPerson2019.Query();

                //StringParametr stringParametr2019 = new StringParametr();
                //stringParametr2019.Query = query2019;

                // ����� ������ �������� ����� 2019 ����.
                IFindPerson fintPersonAftar2019 = new FindPersonAftar2019NoValid(this.textBox1.Text);
                string query2019Aftar = fintPersonAftar2019.Query();

                StringParametr stringParametr2019Aftar = new StringParametr();
                stringParametr2019Aftar.Query = query2019Aftar;

                // ����� ���������� �� �������� 2021 ����.
                IFindPerson findPersonNoValid = new FindPersonNoValid(this.textBox1.Text);
                string queryNoValid2021 = findPersonNoValid.Query();

                StringParametr stringParametrFindNoValid2021 = new StringParametr();
                stringParametrFindNoValid2021.Query = queryNoValid2021;

                // �������� ������ �� 2019 ����.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr));

                // �������� ������ 2019 ���� ������� Add.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2019Add));

                // �������� ������ 2019 ����.
                //listTempDisplay.AddRange(ExecuteQuery(stringParametr2019));

                // �������� ������ ������ 2019 ����.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2019Aftar));

                // �������� ������ 2021 ����.
                listTempDisplay.AddRange(ExecuteQuery(stringParametrFindNoValid2021));

                var listRes = CompareContractForNumber.Compare(listTempDisplay);

                listDisplay.AddRange(listRes);
            }

            if (this.FlagValid == false && this.FlagCheck == true)
            {

                IFindPerson findPersonNumberDoc = new FindPersonNumContract(this.textBox1.Text);

                showResultPerson = new ShowResultPerson(findPersonNumberDoc);

                // �������� ������ ������������ ������.  
                listDisplay.AddRange(showResultPerson.DisplayDate());

                //var test = listDisplay.GroupBy(w => w.�������������);

                var test = listDisplay;

            }

            this.dataGridView1.DataSource = listDisplay;

            this.dataGridView1.Columns["id_�������"].Width = 100;
            this.dataGridView1.Columns["id_�������"].Visible = false;
            this.dataGridView1.Columns["�������������"].Width = 100;
            this.dataGridView1.Columns["�������������"].DisplayIndex = 0;

            this.dataGridView1.Columns["�������"].Width = 150;
            this.dataGridView1.Columns["�������"].DisplayIndex = 1;

            this.dataGridView1.Columns["���"].Width = 150;
            this.dataGridView1.Columns["���"].DisplayIndex = 2;

            this.dataGridView1.Columns["��������"].Width = 150;
            this.dataGridView1.Columns["��������"].DisplayIndex = 3;

            this.dataGridView1.Columns["�����������������"].Width = 300;
            this.dataGridView1.Columns["�����������������"].DisplayIndex = 4;
            this.dataGridView1.Columns["�����������������"].HeaderText = "�������� ���������";

            this.dataGridView1.Columns["�����"].Width = 100;
            this.dataGridView1.Columns["�����"].DisplayIndex = 5;

            this.dataGridView1.Columns["����"].Width = 100;
            this.dataGridView1.Columns["����"].DisplayIndex = 6;
            this.dataGridView1.Columns["����"].HeaderText = "���� ������ ������� �������� � ���� ��";

            this.dataGridView1.Columns["���������"].Width = 100;
            this.dataGridView1.Columns["���������"].DisplayIndex = 7;
            this.dataGridView1.Columns["���������"].HeaderText = "����� ����";

            this.dataGridView1.Columns["��������������"].Width = 100;
            this.dataGridView1.Columns["��������������"].DisplayIndex = 8;
            this.dataGridView1.Columns["��������������"].HeaderText = "���� ���������� ����";

            this.dataGridView1.Columns["����������"].Width = 150;
            this.dataGridView1.Columns["����������"].DisplayIndex = 9;

            this.dataGridView1.Columns["flag2019AddWrite"].Width = 150;
            this.dataGridView1.Columns["flag2019AddWrite"].DisplayIndex = 10;
            this.dataGridView1.Columns["flag2019AddWrite"].Visible = false;

            this.dataGridView1.Columns["flag����������"].Width = 150;
            this.dataGridView1.Columns["flag����������"].DisplayIndex = 11;
            this.dataGridView1.Columns["flag����������"].Visible = true;// false;



            // ������� ������ � ������� ����.
            for (int i = 0; i <= this.dataGridView1.Rows.Count - 1; i++)
            {
                if(Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["flag����������"].Value) == true)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private List<ValideContract> ExecuteQuery(object obj)
        {
            StringParametr stringParametr = (StringParametr)obj;

            // ������ ��� �������� ���������� ������.
            List<ValideContract> listDisplay = new List<ValideContract>();

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                //SqlTransaction transact = con.BeginTransaction();

                //DataTable dtContract = ���������.GetTableSQL(query, "����������������", con, transact);
                SqlCommand com = new SqlCommand(stringParametr.Query, con);
                //com.Transaction = transact;

                SqlDataReader read = com.ExecuteReader();

                IFindContract findContract = new FindContractForNumber(read);

                var list = findContract.Adapter();

                listDisplay.AddRange(list);

            }

            return listDisplay;


        }

        private List<ValideContract> FIltrDowbel(IEnumerable<IGrouping<string, ValideContract>> listG)
        {
            // ������ ��� ����������� �� ������.
            List<ValideContract> listDisplay = new List<ValideContract>();

            foreach (var itms in listG)
            {
                if (itms.All(w => w.flag2019AddWrite == false) == true)
                {
                    foreach (var itm in itms)
                    {
                        listDisplay.Add(itm);
                    }
                }
                else
                {
                    foreach (var itm in itms)
                    {
                        if (itm.flag2019AddWrite == true)
                        {
                            listDisplay.Add(itm);
                        }
                    }
                }
            }

            return listDisplay;
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           // string query = string.Empty;

           // //���� ������ ��������
           //if (this.FlagValid == true && this.FlagCheck == false)
           // {
           //     query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.����������������� " +
           //               "FROM ������� INNER JOIN �������� " +
           //               "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
           //               "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
           //               "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
           //               //"where �������.������������ = 'True' and ��������.������� like '" + this.textBox2.Text + "%' " +
           //               //"where �������.������������ = 'True' and ��������.������� = '" + this.textBox2.Text + "' " + ==== �������� ���������
           //               "where �������.������������ = 'True' and ��������.������� = '" + this.textBox2.Text + "' " + //and logWrite <>  '������ ������ ������������' " +
           //               "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.����������������� ";
           // }
           // //else

           // if (this.FlagValid == false && this.FlagCheck == false)
           // {
           //     query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.����������������� " +
           //               "FROM ������� INNER JOIN �������� " +
           //               "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
           //               "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
           //               "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
           //               //"where �������.������������ = 'False' and ��������.������� like '" + this.textBox2.Text + "%' " +
           //               //========="where �������.������������ = 'False' and ��������.������� = '" + this.textBox2.Text + "' " + // �������� ���������
           //               "where �������.������������ = 'False' and ��������.������� = '" + this.textBox2.Text + "' " + //and logWrite <> '������ ������ ������������' " +
           //               "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.����������������� ";

           // }

           // if (this.FlagValid == false && this.FlagCheck == true)
           // {
           //     query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.����������������� " +
           //               "FROM ������� INNER JOIN �������� " +
           //               "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
           //               "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
           //               "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
           //               //"where �������.������������ = 'False' and ��������.������� like '" + this.textBox2.Text + "%' " +
           //               "where �������.������������ = 'False' and ��������.������� = '" + this.textBox2.Text + "' " + //and logWrite <> '������ ������ ������������' " +
           //               "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.����������������� ";

           // }

           // using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
           // {
           //     con.Open();
           //     SqlTransaction transact = con.BeginTransaction();

           //     //DataTable dtContract = ���������.GetTableSQL(query, "����������������", con, transact);
           //     SqlCommand com = new SqlCommand(query, con);
           //     com.Transaction = transact;

           //     DataTable tab = new DataTable();
           //     DataColumn col0 = new DataColumn("id_�������", typeof(int));
           //     tab.Columns.Add(col0);
           //     DataColumn col1 = new DataColumn("�������������", typeof(string));
           //     tab.Columns.Add(col1);
           //     DataColumn col2 = new DataColumn("�������", typeof(string));
           //     tab.Columns.Add(col2);

           //     DataColumn col3 = new DataColumn("���", typeof(string));
           //     tab.Columns.Add(col3);
           //     DataColumn col4 = new DataColumn("��������", typeof(string));
           //     tab.Columns.Add(col4);

           //     DataColumn col5 = new DataColumn("�����������������", typeof(string));
           //     tab.Columns.Add(col5);
           //     DataColumn col6 = new DataColumn("�����", typeof(string));
           //     tab.Columns.Add(col6);

           //     DataColumn col7 = new DataColumn("����", typeof(string));
           //     tab.Columns.Add(col7);
           //     DataColumn col8 = new DataColumn("����������", typeof(string));
           //     tab.Columns.Add(col8);

           //     // ��������� ����� ���� ������� � ����� �������
           //     DataColumn col9 = new DataColumn("������������", typeof(string));
           //     tab.Columns.Add(col9);

           //     DataColumn col10 = new DataColumn("�����������������", typeof(string));
           //     tab.Columns.Add(col10);


           //     SqlDataReader read = com.ExecuteReader();
           //     while (read.Read())
           //     {
           //         DataRow row = tab.NewRow();
           //         row["id_�������"] = read["id_�������"].ToString().Trim();
           //         row["�������������"] = read["�������������"].ToString().Trim();
           //         row["�������"] = read["�������"].ToString().Trim();
           //         row["���"] = read["���"].ToString().Trim();
           //         row["��������"] = read["��������"].ToString().Trim();
           //         row["�����������������"] = read["�����������������"].ToString().Trim();
           //         row["�����"] = Convert.ToDecimal(read["�����"]).ToString("c").Trim();
           //         //string sComp = row["����"].ToString();
           //         if (read["������������������"] != DBNull.Value)//
           //         {
           //             row["����"] = Convert.ToDateTime(read["������������������"]).ToShortDateString().Trim();
           //         }
           //         else
           //         {

           //         }
           //         if (read["logWrite"] != DBNull.Value)
           //         {
           //             row["����������"] = read["logWrite"].ToString().Trim();
           //         }

           //         // ��������� ����� ���� �������
           //         if (read["������������"] != DBNull.Value && read["������������"].ToString().Trim() != "NULL")
           //         {
           //             row["������������"] = read["������������"].ToString().Trim();
           //         }

           //         // ��������� ����� �������
           //         if (read["�����������������"] != DBNull.Value && read["�����������������"].ToString().Trim() != "NULL" )
           //         {
           //             row["�����������������"] = read["�����������������"].ToString().Trim();
           //         }


           //         tab.Rows.Add(row);
           //     }

           //     this.dataGridView1.DataSource = tab;

           //     this.dataGridView1.Columns["id_�������"].Width = 100;
           //     this.dataGridView1.Columns["id_�������"].Visible = false;
           //     this.dataGridView1.Columns["�������������"].Width = 100;
           //     this.dataGridView1.Columns["�������������"].DisplayIndex = 0;

           //     this.dataGridView1.Columns["�������"].Width = 200;
           //     this.dataGridView1.Columns["�������"].DisplayIndex = 1;

           //     this.dataGridView1.Columns["���"].Width = 200;
           //     this.dataGridView1.Columns["���"].DisplayIndex = 2;

           //     this.dataGridView1.Columns["��������"].Width = 200;
           //     this.dataGridView1.Columns["��������"].DisplayIndex = 3;

           //     this.dataGridView1.Columns["�����������������"].Width = 300;
           //     this.dataGridView1.Columns["�����������������"].DisplayIndex = 4;

           //     this.dataGridView1.Columns["�����"].Width = 200;
           //     this.dataGridView1.Columns["�����"].DisplayIndex = 5;

           //     this.dataGridView1.Columns["����"].Width = 70;
           //     this.dataGridView1.Columns["����"].DisplayIndex = 6;

           //     this.dataGridView1.Columns["����������"].Width = 70;
           //     this.dataGridView1.Columns["����������"].DisplayIndex = 7;

           //     this.dataGridView1.Columns["������������"].Width = 70;
           //     this.dataGridView1.Columns["������������"].DisplayIndex = 7;

           //     this.dataGridView1.Columns["�����������������"].Width = 70;
           //     this.dataGridView1.Columns["�����������������"].DisplayIndex = 7;


           // }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //������� ������ �������
            int i = e.ColumnIndex;

            //���������� ������ ������� � ������� � ���������
            DataTable tabContract;

            //���������� ������ ������ �� ���������
            DataTable tabPerson;

            int id_������� = 0;

            // ���������, ��� ������� (� ��������� true) �������� ������ 2019 ����.
            bool flag2019AddWrite = false;

            // ������� id ��������.
            id_������� = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_�������"].Value);

            //flag2019AddWrite = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);
            int flarYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["���"].Value);

            // ���������� ������ ����� ������� �� �������.
            string queruContract = string.Empty;

            // ���������� ������ ����� ������� �� ��������.
            string queruPerson = string.Empty;

            // ���� ������� ������ 2019 ���� ��� ����� 2020 ����.
            if (flarYear < 2019 || flarYear == 2020)
            {
                // ����� �� ������ ��������.
                if (this.textBox2.Text == "" && this.txt���.Text == "")
                {
                    //// ������� ������ �� ������� � ��������.
                    //queruContract = "SELECT ���������������������.������������������, ���������������������.����, " +
                    //                       "���������������������.����������, ���������������������.����� " +
                    //                       "FROM ������������ INNER JOIN " +
                    //                       "��������������������� ON ������������.id_������� = ���������������������.id_������� " +
                    //                       "where ������������.id_������� = " + id_������� + " ";

                    ////������� ������ �� ��������.
                    //queruPerson = "SELECT top 1 ������������.�������������, ������������.������������, �������������.�������, �������������.���, �������������.��������, " +
                    //                       "�������������.������������, �������������.�����, �������������.���������, �������������.������, �������������.�������������, " +
                    //                       "�������������.�������������, �������������.�������������, �������������.��������������, �������������.��������������, " +
                    //                       "�������������.�������������������, �������������.������������������,������������.������������ " +
                    //                       "FROM ������������ INNER JOIN " +
                    //                       "��������������������� ON ������������.id_������� = ���������������������.id_������� " +
                    //                       "INNER JOIN ������������� ON dbo.������������.id_�������� = dbo.�������������.id_�������� " +
                    //                       "where ������������.id_������� = " + id_������� + " ";

                    // ������� ������ �� ������� � ��������.
                    IQueryFind queryFindId2019 = new QueryFindContractBefor2019(id_�������);

                    queruContract = queryFindId2019.Query();

                    // ������� ������ �� ��������.
                    IQueryFind queryFindBefor2019 = new QueryFindIdContractBefore2019(id_�������);

                    queruPerson = queryFindBefor2019.Query();
                }
                else if(this.textBox2.Text != "" && this.txt���.Text == "")
                {
                    // ������� ������ �� ������� � ��������.
                    IQueryFind queryFindId2019 = new QueryFindContractBefor2019(id_�������);

                    queruContract = queryFindId2019.Query();

                    // ������� ������ �� ��������.
                    IQueryFind queryFindBefor2019 = new QueryFindIdContractBefore2019(id_�������);

                    queruPerson = queryFindBefor2019.Query();
                }
                else if (this.textBox2.Text != "" && this.txt���.Text != "")
                {
                    // ������� ������ �� ������� � ��������.
                    IQueryFind queryFindId2019 = new QueryFindContractBefor2019(id_�������);

                    queruContract = queryFindId2019.Query();

                    // ������� ������ �� ��������.
                    IQueryFind queryFindBefor2019 = new QueryFindIdContractBefore2019(id_�������);

                    queruPerson = queryFindBefor2019.Query();
                }
            }

            if(flarYear == 2019)
            {
                //flag2019AddWrite = true;
                // ����� �� ������ �������� � 2019 ����.
                if (this.textBox2.Text == "" && this.txt���.Text == "")
                {
                    IQueryFind queryPersonAdd = new QueryPersonAdd(id_�������);

                    // ������� ������ �� ��������.
                    queruPerson = queryPersonAdd.Query();

                    IQueryFind queryFindContractAdd = new QueryContractAdd(id_�������);

                    // ������� ������ �� ������� � ��������.
                    queruContract = queryFindContractAdd.Query();
                }

                if (this.textBox2.Text != "" && this.txt���.Text == "")
                {
                    IQueryFind queryPersonAdd = new QueryPersonAdd(id_�������);

                    // ������� ������ �� ��������.
                    queruPerson = queryPersonAdd.Query();

                    IQueryFind queryFindContractAdd = new QueryContractAdd(id_�������);

                    // ������� ������ �� ������� � ��������.
                    queruContract = queryFindContractAdd.Query();
                }

                if (this.textBox2.Text != "" && this.txt���.Text != "")
                {
                    IQueryFind queryPersonAdd = new QueryPersonAdd(id_�������);

                    // ������� ������ �� ��������.
                    queruPerson = queryPersonAdd.Query();

                    IQueryFind queryFindContractAdd = new QueryContractAdd(id_�������);

                    // ������� ������ �� ������� � ��������.
                    queruContract = queryFindContractAdd.Query();
                }

            }

            if(flarYear > 2020)
            {
                if (this.textBox2.Text == "" && this.txt���.Text == "")
                {
                    IQueryFind queryPerson2021 = new QueryPerson2021(id_�������);

                    // ������� ������ �� ��������.
                    queruPerson = queryPerson2021.Query();

                    IQueryFind queryContract2021 = new QueryContract2021(id_�������);

                    queruContract = queryContract2021.Query();
                }
                else if (this.textBox2.Text != "" && this.txt���.Text == "")
                {
                    QueryPerson2021 queryPerson2021 = new QueryPerson2021(id_�������);

                    // ������� ������ �� ��������.
                    queruPerson = queryPerson2021.Query();

                    QueryContract2021 queryContract2021 = new QueryContract2021(id_�������);

                    queruContract = queryContract2021.Query();
                }
                else if (this.textBox2.Text != "" && this.txt���.Text != "")
                {
                    QueryPerson2021 queryPerson2021 = new QueryPerson2021(id_�������);

                    // ������� ������ �� ��������.
                    queruPerson = queryPerson2021.Query();

                    QueryContract2021 queryContract2021 = new QueryContract2021(id_�������);

                    queruContract = queryContract2021.Query();
                }
            }




            //else if(this.textBox2.Text != "" && this.txt���.Text != "" && flag2019AddWrite == true)
            //{
            //    // ������� ������ �� ��������.
            //    queruPerson = @" SELECT top 1 �������Add.�������������, �������Add.������������, ��������Add.�������, ��������Add.���, 
            //                                    ��������Add.��������, ��������Add.������������, ��������Add.�����, ��������Add.���������, 
            //                                    ��������Add.������, ��������Add.�������������, ��������Add.�������������, ��������Add.�������������, 
            //                                    ��������Add.��������������, ��������Add.��������������, ��������Add.�������������������, 
            //                                    ��������Add.������������������,�������Add.������������ FROM �������Add
            //                                    INNER JOIN ����������������
            //                                    ON �������Add.id_������� = ����������������.id_�������
            //                                    INNER JOIN ��������Add
            //                                    ON dbo.�������Add.id_�������� = dbo.��������Add.id_�������� " +
            //                                   "where �������Add.id_������� = " + id_������� + " ";

            //    // ������� ������ �� ������� � ��������.
            //    queruContract = "SELECT ����������������Add.������������������, ����������������Add.����, " +
            //                           "����������������Add.����������, ����������������Add.����� " +
            //                           "FROM �������Add INNER JOIN " +
            //                           "����������������Add ON �������Add.id_������� = ����������������Add.id_������� " +
            //                           "where �������Add.id_������� = " + id_������� + " ";
            //}
            //else if(this.textBox2.Text != "" && this.txt���.Text == "" && flag2019AddWrite == true)
            //{
            //    // ������� ������ �� ��������.
            //    queruPerson = @" SELECT top 1 �������Add.�������������, �������Add.������������, ��������Add.�������, ��������Add.���, 
            //                                    ��������Add.��������, ��������Add.������������, ��������Add.�����, ��������Add.���������, 
            //                                    ��������Add.������, ��������Add.�������������, ��������Add.�������������, ��������Add.�������������, 
            //                                    ��������Add.��������������, ��������Add.��������������, ��������Add.�������������������, 
            //                                    ��������Add.������������������,�������Add.������������ FROM �������Add
            //                                    INNER JOIN ����������������Add
            //                                    ON �������Add.id_������� = ����������������Add.id_�������
            //                                    INNER JOIN ��������Add
            //                                    ON dbo.�������Add.id_�������� = dbo.��������Add.id_�������� " +
            //                                   "where �������Add.id_������� = " + id_������� + " ";

            //    // ������� ������ �� ������� � ��������.
            //    queruContract = "SELECT ����������������Add.������������������, ����������������Add.����, " +
            //                           "����������������Add.����������, ����������������Add.����� " +
            //                           "FROM �������Add INNER JOIN " +
            //                           "����������������Add ON �������Add.id_������� = ����������������Add.id_������� " +
            //                           "where �������Add.id_������� = " + id_������� + " ";
            //}
            //else if (this.textBox2.Text != "" && this.txt���.Text == "" && flag2019AddWrite == false)
            //{
            //    // ������� ������ �� ��������.
            //    queruPerson = @" SELECT top 1 �������.�������������, �������.������������, ��������.�������, ��������.���, 
            //                                    ��������.��������, ��������.������������, ��������.�����, ��������.���������, 
            //                                    ��������.������, ��������.�������������, ��������.�������������, ��������.�������������, 
            //                                    ��������.��������������, ��������.��������������, ��������.�������������������, 
            //                                    ��������.������������������,�������.������������ FROM �������
            //                                    INNER JOIN ����������������
            //                                    ON �������.id_������� = ����������������.id_�������
            //                                    INNER JOIN ��������
            //                                    ON dbo.�������.id_�������� = dbo.��������.id_�������� " +
            //                                   "where �������.id_������� = " + id_������� + " ";

            //    // ������� ������ �� ������� � ��������.
            //    queruContract = "SELECT ����������������.������������������, ����������������.����, " +
            //                           "����������������.����������, ����������������.����� " +
            //                           "FROM ������� INNER JOIN " +
            //                           "���������������� ON �������.id_������� = ����������������.id_������� " +
            //                           "where �������.id_������� = " + id_������� + " ";
            //}
            //else if(flag2019AddWrite == true && this.textBox1.Text != "")
            //{
            //    // ������� ������ �� ������� � ��������.
            //    queruContract = "SELECT ����������������Add.������������������, ����������������Add.����, " +
            //                           "����������������Add.����������, ����������������Add.����� " +
            //                           "FROM �������Add INNER JOIN " +
            //                           "����������������Add ON �������Add.id_������� = ����������������Add.id_������� " +
            //                           "where �������Add.id_������� = " + id_������� + " ";


            //    queruPerson = @" SELECT top 1 �������Add.�������������, �������Add.������������, ��������Add.�������, ��������Add.���, 
            //                           ��������Add.��������, ��������Add.������������, ��������Add.�����, ��������Add.���������, 
            //                           ��������Add.������, ��������Add.�������������, ��������Add.�������������, ��������Add.�������������, 
            //                           ��������Add.��������������, ��������Add.��������������, ��������Add.�������������������, 
            //                           ��������Add.������������������,�������Add.������������ FROM �������Add
            //                           INNER JOIN ����������������Add
            //                           ON �������Add.id_������� = ����������������Add.id_�������
            //                           INNER JOIN ��������Add
            //                           ON dbo.�������Add.id_�������� = dbo.��������Add.id_�������� " +
            //                           " where �������Add.id_������� = " + id_������� + " and �������Add.������������� = '" + this.textBox1.Text + "' ";
            //}
            //else if(flag2019AddWrite == false &&  this.textBox1.Text != "")
            //{
            //    //������� ������ �� �������� 
            //    queruContract = "SELECT ����������������.������������������, ����������������.����, " +
            //                           "����������������.����������, ����������������.����� " +
            //                           "FROM ������� INNER JOIN " +
            //                           "���������������� ON �������.id_������� = ����������������.id_������� " +
            //                           "where �������.id_������� = " + id_������� + " ";

            //    queruPerson = "SELECT top 1 �������.�������������, �������.������������, ��������.�������, ��������.���, ��������.��������, " +
            //                           "��������.������������, ��������.�����, ��������.���������, ��������.������, ��������.�������������, " +
            //                           "��������.�������������, ��������.�������������, ��������.��������������, ��������.��������������, " +
            //                           "��������.�������������������, ��������.������������������,�������.������������ " +
            //                           "FROM ������� INNER JOIN " +
            //                           "���������������� ON �������.id_������� = ����������������.id_������� " +
            //                           "INNER JOIN �������� ON dbo.�������.id_�������� = dbo.��������.id_�������� " +
            //                           "where �������.id_������� = " + id_������� + " and �������.������������� = '" + this.textBox1.Text + "' ";
            //}





            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                  con.Open();
                  SqlTransaction transact = con.BeginTransaction();

                  tabContract = ���������.GetTableSQL(queruContract, "�������", con, transact);

                  tabPerson = ���������.GetTableSQL(queruPerson, "��������", con, transact);
            }
                //}

                FormDispContract fdc = new FormDispContract();
                fdc.����������������� = tabContract;
                fdc.����������������� = tabPerson;
               
                fdc.TopMost = true;
                fdc.Id������� = id_�������;
                fdc.Show();


            //}
            //MessageBox.Show(i.ToString());
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow rows = this.dataGridView1.Rows[e.RowIndex];

                    //������� id ���������� ���������

                    //id_��������������� = Convert.ToInt32(rows.Cells["id_��������"].Value);
                    //������������������ = rows.Cells["�������"].Value.ToString().Trim();

                    this.dataGridView1.ClearSelection();
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];


                }
            }
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id_������� = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_�������"].Value);

            string sTest = "update dbo.������� " +
                           "set ������������������ = '"+ DateTime.Today.ToShortDateString() +"' " +
                           ",������������ = 'True' " +
                           ",logWrite = '"+ MyAplicationIdentity.GetUses() +"' " +
                           ", ���������������������� = 0 " +
                           ", flag���������� = 0" +
                           ", �������������� = 0 " +
                           ", flag2020 = 0 " +
                           ", flag2019AddWrite = 0 " + 
                           "where id_������� = " + id_������� +" ";

            Classes.ExecuteQuery.Execute(sTest);
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            // ������� ������ �������.
            listPerson.Clear();

            // �������� �����.
            LoadAfterClickFind();

            #region ������ �������� ���� �������
            //string query = string.Empty;

            ////���� ������ ��������
            //if (this.FlagValid == true && this.FlagCheck == false)
            //{
            //    if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length == 0)
            //    {
            //        query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.�����������������, �������������������.���������, �������������������.�������������� " +
            //                  "FROM ������� INNER JOIN �������� " +
            //                  "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
            //                  "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
            //                  "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
            //                  "inner join dbo.������������������� " +
            //                  "on dbo.�������������������.id_������� = �������.id_������� " +
            //                  "where �������.������������ = 'True' and ��������.������� = '" + this.textBox2.Text + "' " +
            //                  "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.�����������������,�������������������.���������, �������������������.�������������� ";
            //    }
            //    else if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length > 0)
            //    {
            //        query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.�����������������, �������������������.���������, �������������������.�������������� " +
            //                 "FROM ������� INNER JOIN �������� " +
            //                 "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
            //                 "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
            //                 "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
            //                 "inner join dbo.������������������� " +
            //                 "on dbo.�������������������.id_������� = �������.id_������� " +
            //                 "where �������.������������ = 'True' and  (��������.������� = '"+ this.textBox2.Text.Trim() +"' and ��������.��� = '"+ this.txt���.Text.Trim() +"') " +
            //                 "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.�����������������,�������������������.���������, �������������������.�������������� ";
            //    }
                
            //}
            ////else

            //if (this.FlagValid == false && this.FlagCheck == false)
            //{
            //    query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.����������������� " +
            //              "FROM ������� INNER JOIN �������� " +
            //              "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
            //              "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
            //              "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
            //               "where �������.������������ = 'False' and ��������.������� = '" + this.textBox2.Text + "' and ��������.��� = '" + this.txt���.Text.Trim() + "' " +
            //              "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.����������������� ";
            //}

            //if (this.FlagValid == false && this.FlagCheck == true)
            //{
            //    if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length == 0)
            //    {
            //        query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.����������������� " +
            //                  "FROM ������� INNER JOIN �������� " +
            //                  "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
            //                  "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
            //                  "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
            //                  "where �������.������������ = 'False' and ��������.������� = '" + this.textBox2.Text + "' " + // and ��������.��� = '" + this.txt���.Text.Trim() + "' " +
            //                  "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.����������������� ";
            //    }
            //    else if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length > 0)
            //    {
            //        query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.����������������� " +
            //                  "FROM ������� INNER JOIN �������� " +
            //                  "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
            //                  "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
            //                  "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
            //                  "where �������.������������ = 'False' and ��������.������� = '" + this.textBox2.Text + "'  and ��������.��� = '" + this.txt���.Text.Trim() + "' " +
            //                  "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.����������������� ";
            //    }

            //}

            //using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            //{
            //    con.Open();
            //    SqlTransaction transact = con.BeginTransaction();

            //    //DataTable dtContract = ���������.GetTableSQL(query, "����������������", con, transact);
            //    SqlCommand com = new SqlCommand(query, con);
            //    com.Transaction = transact;

            //    // �������� ��������� ������� ��� ����������� ������ � DataGrid.
            //    DataTable tab = new DataTable();
            //    DataColumn col0 = new DataColumn("id_�������", typeof(int));
            //    tab.Columns.Add(col0);
            //    DataColumn col1 = new DataColumn("�������������", typeof(string));
            //    tab.Columns.Add(col1);
            //    DataColumn col2 = new DataColumn("�������", typeof(string));
            //    tab.Columns.Add(col2);

            //    DataColumn col3 = new DataColumn("���", typeof(string));
            //    tab.Columns.Add(col3);
            //    DataColumn col4 = new DataColumn("��������", typeof(string));
            //    tab.Columns.Add(col4);

            //    DataColumn col5 = new DataColumn("�����������������", typeof(string));
            //    tab.Columns.Add(col5);
            //    DataColumn col6 = new DataColumn("�����", typeof(string));
            //    tab.Columns.Add(col6);

            //    DataColumn col7 = new DataColumn("����", typeof(string));
            //    tab.Columns.Add(col7);
            //    DataColumn col8 = new DataColumn("����������", typeof(string));
            //    tab.Columns.Add(col8);

            //    // ��������� ����� ���� ������� � ����� �������
            //    DataColumn col9 = new DataColumn("������������", typeof(string));
            //    tab.Columns.Add(col9);

            //    DataColumn col10 = new DataColumn("�����������������", typeof(string));
            //    tab.Columns.Add(col10);

            //    if (this.FlagValid == true && this.FlagCheck == false)
            //    {
            //        DataColumn col11 = new DataColumn("���������", typeof(string));
            //        tab.Columns.Add(col11);

            //        DataColumn col12 = new DataColumn("��������������", typeof(string));
            //        tab.Columns.Add(col12);
            //    }


            //    SqlDataReader read = com.ExecuteReader();
            //    while (read.Read())
            //    {
            //        DataRow row = tab.NewRow();
            //        row["id_�������"] = read["id_�������"].ToString().Trim();
            //        row["�������������"] = read["�������������"].ToString().Trim();
            //        row["�������"] = read["�������"].ToString().Trim();
            //        row["���"] = read["���"].ToString().Trim();
            //        row["��������"] = read["��������"].ToString().Trim();
            //        row["�����������������"] = read["�����������������"].ToString().Trim();
            //        row["�����"] = Convert.ToDecimal(read["�����"]).ToString("c").Trim();
            //        //string sComp = row["����"].ToString();
            //        if (read["������������������"] != DBNull.Value)//
            //        {
            //            row["����"] = Convert.ToDateTime(read["������������������"]).ToShortDateString().Trim();
            //        }
            //        else
            //        {

            //        }
            //        if (read["logWrite"] != DBNull.Value)
            //        {
            //            row["����������"] = read["logWrite"].ToString().Trim();
            //        }

            //        // ��������� ����� ���� �������
            //        if (read["������������"] != DBNull.Value && read["������������"].ToString().Trim() != "NULL")
            //        {
            //            row["������������"] = read["������������"].ToString().Trim();
            //        }

            //        // ��������� ����� �������
            //        if (read["�����������������"] != DBNull.Value && read["�����������������"].ToString().Trim() != "NULL")
            //        {
            //            row["�����������������"] = read["�����������������"].ToString().Trim();
            //        }

            //        if (this.FlagValid == true && this.FlagCheck == false)
            //        {
            //            if (read["���������"] != DBNull.Value)// && read["���������"] != null)
            //            {
            //                var asd = read["���������"].Do(x => x, "").ToString().Trim();

            //                row["���������"] = read["���������"].ToString().Trim();
            //            }

            //            if (read["��������������"] != DBNull.Value)// && read["���������"] != null)
            //            {
            //                var asd = read["��������������"].Do(x => x, "").ToString().Trim();

            //                row["��������������"] = Convert.ToDateTime(read["��������������"]).ToShortDateString().Trim();
            //            }
            //        }


            //        tab.Rows.Add(row);
            //    }

            //    this.dataGridView1.DataSource = tab;

            //    this.dataGridView1.Columns["id_�������"].Width = 100;
            //    this.dataGridView1.Columns["id_�������"].Visible = false;
            //    this.dataGridView1.Columns["�������������"].Width = 100;
            //    this.dataGridView1.Columns["�������������"].DisplayIndex = 0;

            //    this.dataGridView1.Columns["�������"].Width = 150;
            //    this.dataGridView1.Columns["�������"].DisplayIndex = 1;

            //    this.dataGridView1.Columns["���"].Width = 150;
            //    this.dataGridView1.Columns["���"].DisplayIndex = 2;

            //    this.dataGridView1.Columns["��������"].Width = 150;
            //    this.dataGridView1.Columns["��������"].DisplayIndex = 3;

            //    this.dataGridView1.Columns["�����������������"].Width = 200;
            //    this.dataGridView1.Columns["�����������������"].DisplayIndex = 4;
            //    this.dataGridView1.Columns["�����������������"].HeaderText = "�������� ���������";

            //    this.dataGridView1.Columns["�����"].Width = 100;
            //    this.dataGridView1.Columns["�����"].DisplayIndex = 5;

            //    this.dataGridView1.Columns["����"].Width = 120;
            //    this.dataGridView1.Columns["����"].DisplayIndex = 6;
            //    this.dataGridView1.Columns["����"].HeaderText = "���� ������ ������� �������� � ���� ��";
               
            //    this.dataGridView1.Columns["������������"].Width = 80;
            //    this.dataGridView1.Columns["������������"].DisplayIndex = 7;
            //    this.dataGridView1.Columns["������������"].HeaderText = "����� �������";

            //    this.dataGridView1.Columns["�����������������"].Width = 100;
            //    this.dataGridView1.Columns["�����������������"].DisplayIndex = 8;
            //    this.dataGridView1.Columns["�����������������"].HeaderText = "����� ���� �������";

            //    if (this.FlagValid == true && this.FlagCheck == false)
            //    {
            //        this.dataGridView1.Columns["���������"].Width = 100;
            //        this.dataGridView1.Columns["���������"].DisplayIndex = 9;
            //        this.dataGridView1.Columns["���������"].HeaderText = "����� ����";
            //        this.dataGridView1.Columns["���������"].Visible = false;

            //        this.dataGridView1.Columns["��������������"].Width = 100;
            //        this.dataGridView1.Columns["��������������"].DisplayIndex = 10;
            //        this.dataGridView1.Columns["��������������"].HeaderText = "���� ���������� ����";

            //        this.dataGridView1.Columns["����������"].Width = 100;
            //        this.dataGridView1.Columns["����������"].DisplayIndex = 11;

            //    }
            //    else
            //    {
            //    this.dataGridView1.Columns["����������"].Width = 100;
            //    this.dataGridView1.Columns["����������"].DisplayIndex = 9;
            //    }


            //}
            #endregion
        }

        private void �����������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flagAct = false;
            // ������� ������� �������.
            int id������� = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_�������"].Value);

            bool flagWrite2019 = false;

            flagWrite2019 = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);

            string queryValidAct = string.Empty;

            if(flagWrite2019 == false)
            {
                queryValidAct = "select COUNT(id_���) as '���������������' from ������������������� " +
                                   "where id_������� in ( " +
                                    "select id_�������  from ������� " +
                                    "where id_������� = " + id������� + " ) ";
            }
            else
            {
                queryValidAct = @"select  COUNT(id_���) as '���������������' from �������������������Add 
                                        inner join �������Add
                                        on �������������������Add.id_������� = �������Add.id_����������
                                         where �������Add.id_������� = " + id������� + " ";
            

            //queryValidAct = "select COUNT(id_���) as '���������������' from �������������������Add " +
            //                       "where id_������� in ( " +
            //                        "select id_�������  from �������Add " +
            //                        "where id_������� = " + id������� + " ) ";
            }

            DataTable tabAct = ���������.GetTableSQL(queryValidAct, "����������");

            StringBuilder build = new StringBuilder();

            if (Convert.ToInt32(tabAct.Rows[0]["���������������"]) > 0)
            {
                flagAct = true;

                string queryValidNumAct = string.Empty;

                if (flagWrite2019 == false)
                {
                    //queryValidNumAct = "select ���������,�������������� from ������������������� " +
                    //                      "where id_������� in ( " +
                    //                      "select id_�������  from ������� " +
                    //                      "where id_������� = " + id������� + " ) ";

                    queryValidNumAct = @"select ���������,��������������,������������������������� from ������������������� 
                                        inner join �������
                                        on �������������������.id_������� = �������.id_�������
                                         where �������Add.id_������� = " + id������� + " ";
                }
                else
                {
                    //queryValidNumAct = "select ���������,�������������� from �������������������Add " +
                    //                      "where id_������� in ( " +
                    //                      "select id_�������  from �������Add " +
                    //                      "where id_������� = " + id������� + " ) ";

                    queryValidNumAct = @"select ���������,��������������,������������������������� from �������������������Add 
                                        inner join �������Add
                                        on �������������������Add.id_������� = �������Add.id_����������
                                         where �������Add.id_������� = " + id������� + " ";
                }

                DataTable tabNumAct = ���������.GetTableSQL(queryValidNumAct, "���������������");

                // ������� ����� ����
                build.Append("����� ���� - " + tabNumAct.Rows[0]["���������"].ToString().Trim());
                build.Append(" �� " + Convert.ToDateTime(tabNumAct.Rows[0]["��������������"]).ToShortDateString());

                // �������� ���� �� ���.
                // ��� ��� �� �������� 2019 ��� ��� ������� ��� �� �������� �� ������� ����� ���� � ������� �������
                if (Convert.ToDecimal(tabNumAct.Rows[0]["�������������������������"]) != 0.0m)
                {
                    flagAct = true;
                }
                else
                {
                    flagAct = false;
                }
            }

            //// ������� ���������� ����.
            //FormMessageAct formMessAct = new FormMessageAct();
            //formMessAct.��������� = build.ToString();
            //formMessAct.ShowDialog();
            DialogResult dialogResult = MessageBox.Show("������� ��� ����������� ����� " + build.ToString().Trim(), "��������", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            string test = "test";

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {

                string query = string.Empty;

                string user = MyAplicationIdentity.GetUses();

                if (flagWrite2019 == false)
                {
                        query = " declare @id int " +
                                "set @id = " + id������� + " " +
                                "delete ������������������� " +
                                "where id_������� in ( " +
                                "select id_�������  from ������� " +
                                "where id_������� = @id " +
                                ") " +
                                "update ������� " +
                                "set ������������ = 'True', " +
                                "������������������������ = '19000101', " +
                                "������������������������� = 0.0, " +
                                "������������ =  null, " +
                                "����������� = null, " +
                                "����������������� = null, " +
                                "��������������� = null, " +
                                "logWrite = '" + user + "',  " +
                                " ���������������������� = 1 " +
                                "where id_������� in ( " +
                                "select id_�������  from ������� " +
                                "where id_������� = @id) ";
                }
                else
                {
                    query = "declare @id int " +
                               "set @id = " + id������� + " " +
                               @" delete Act
                                from �������������������Add as Act
                                inner join �������Add
                                on �������Add.id_������� = Act.id_�������
                                where �������Add.id_������� = @id
                                delete Act2
                                from ������������������� as Act2
                                inner join �������Add
                                on �������Add.id_���������� = Act2.id_�������
                                where �������Add.id_������� = @id " +
                               "update �������Add " +
                               "set ������������ = 'True', " +
                               " ��������������� = 1 " + 
                               "������������������������ = '19000101', " +
                               "������������������������� = 0.0, " +
                               "������������ =  null, " +
                               "����������� = null, " +
                               "����������������� = null, " +
                               "��������������� = null, " +
                               "logWrite = '" + user + "',  " +
                                " ���������������������� = 1 " +
                               "where id_������� in ( " +
                               "select id_�������  from �������Add " +
                               "where id_������� = @id) ";
                }

                // �������� ������.
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    SqlTransaction transact = con.BeginTransaction();
                    con.Open();

                    Classes.ExecuteQuery.Execute(query, con, transact);
                }

                // �������� ��������� � ������������ ��������� ��������.
                this.FlagValid = true;

                // �������� ������ �����, ��� �� ���������� ������� ����������� ���������� ��� ������.
                this.textBox2.Text = "����������";

                // ������� DataGrid.
                LoadAfterClickFind();

                // ������� ���� ����� �������.
                this.textBox2.Text = string.Empty;

                // 
                this.FlagValid = false;

            }

        }


        /// <summary>
        /// ����� ���������� ������ ���������.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private void ExecuteFind(object query)
        {
            StringParametr stringParametr = (StringParametr)query;

            //// ������ ��� �������� ����������� ������.
            //List<FindPersonNumContractItem> listPerson = new List<FindPersonNumContractItem>();

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                //SqlTransaction transact = con.BeginTransaction();

                //DataTable dtContract = ���������.GetTableSQL(query, "����������������", con, transact);
                SqlCommand com = new SqlCommand(stringParametr.Query, con);
                //com.Transaction = transact;

                // �������� ��������� ������� ��� ����������� ������ � DataGrid.
                DataTable tab = new DataTable();
                DataColumn col0 = new DataColumn("id_�������", typeof(int));
                tab.Columns.Add(col0);
                DataColumn col1 = new DataColumn("�������������", typeof(string));
                tab.Columns.Add(col1);
                DataColumn col2 = new DataColumn("�������", typeof(string));
                tab.Columns.Add(col2);

                DataColumn col3 = new DataColumn("���", typeof(string));
                tab.Columns.Add(col3);
                DataColumn col4 = new DataColumn("��������", typeof(string));
                tab.Columns.Add(col4);

                DataColumn col5 = new DataColumn("�����������������", typeof(string));
                tab.Columns.Add(col5);
                DataColumn col6 = new DataColumn("�����", typeof(string));
                tab.Columns.Add(col6);

                DataColumn col7 = new DataColumn("����", typeof(string));
                tab.Columns.Add(col7);
                DataColumn col8 = new DataColumn("����������", typeof(string));
                tab.Columns.Add(col8);

                // ��������� ����� ���� ������� � ����� �������
                DataColumn col9 = new DataColumn("������������", typeof(string));
                tab.Columns.Add(col9);

                DataColumn col10 = new DataColumn("�����������������", typeof(string));
                tab.Columns.Add(col10);

                if (this.FlagValid == true && this.FlagCheck == false)
                {
                    DataColumn col11 = new DataColumn("���������", typeof(string));
                    tab.Columns.Add(col11);

                    DataColumn col12 = new DataColumn("��������������", typeof(string));
                    tab.Columns.Add(col12);

                    DataColumn col13 = new DataColumn("flag2019AddWrite", typeof(bool));
                    tab.Columns.Add(col13);
                }
                else
                {
                    DataColumn col14 = new DataColumn("flag2019AddWrite", typeof(bool));
                    tab.Columns.Add(col14);
                }

                DataColumn col15 = new DataColumn("���", typeof(int));
                tab.Columns.Add(col15);

                DataColumn col16 = new DataColumn("flag����������", typeof(bool));
                tab.Columns.Add(col16);

                SqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    DataRow row = tab.NewRow();

                    // ����� ���������� ������.
                    FindPersonNumContractItem it = new FindPersonNumContractItem();

                    it.id_������� = read["id_�������"].ToString().Trim();
                    it.������������� = read["�������������"].ToString().Trim();
                    it.������� = read["�������"].ToString().Trim();
                    it.��� = read["���"].ToString().Trim();
                    it.�������� = read["��������"].ToString().Trim();
                    it.����������������� = read["�����������������"].ToString().Trim();
                    it.����� = Convert.ToDecimal(read["�����"]).ToString("c").Trim();
                    if (read["������������������"] != DBNull.Value)//
                    {
                        it.���� = Convert.ToDateTime(read["������������������"]).ToShortDateString().Trim();
                    }
                    if (read["logWrite"] != DBNull.Value)
                    {
                        it.���������� = read["logWrite"].ToString().Trim();
                    }
                    if (read["������������"] != DBNull.Value && read["������������"].ToString().Trim() != "NULL")
                    {
                        it.������������ = read["������������"].ToString().Trim();
                    }
                    if (read["�����������������"] != DBNull.Value && read["�����������������"].ToString().Trim() != "NULL")
                    {
                        it.����������������� = read["�����������������"].ToString().Trim();
                    }

                    if (this.FlagValid == true && this.FlagCheck == false)
                    {
                        if (read["���������"] != DBNull.Value)// && read["���������"] != null)
                        {
                            it.��������� = read["���������"].ToString().Trim();
                        }

                        if (read["��������������"] != DBNull.Value)// && read["���������"] != null)
                        {
                            it.�������������� = Convert.ToDateTime(read["��������������"]).ToShortDateString().Trim();
                        }
                    }

                    if (read["flag2019AddWrite"] != DBNull.Value)
                    {
                        it.flag2019AddWrite = Convert.ToBoolean(read["flag2019AddWrite"]);
                    }

                    if (read["���"] != DBNull.Value && read["���"].ToString().Trim() != "NULL")
                    {
                        it.��� = Convert.ToUInt16(read["���"]);
                    }

                    if (read["flag����������"] != DBNull.Value && read["flag����������"].ToString().Trim() != "NULL")
                    {
                        var test = Convert.ToBoolean(read["flag����������"]);

                        it.flag���������� = Convert.ToBoolean(read["flag����������"]);
                    }

                    listPerson.Add(it);
                }

            }

            //return listPerson;
        }

        private void LoadAfterClickFind()
        {
            // ������� ��������� ������.
            this.dataGridView1.DataSource = null;

            // ���������� ��� �������� SQL ������� ��� ������ ���������.
            string query = string.Empty;

            // ��������������� ���� ����������� ������ ������ ���.
            bool flagExecute = false;

            //���� ������ ��������
            if (this.FlagValid == true && this.FlagCheck == false)
            {
               
                // ����� ���������� ��������� �� �������.
                if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length == 0)
                {
                    // ���������� ��� �������� ������� ��������� �������� ����� ����� � ��.
                    string personFamili = string.Empty;

                    personFamili = this.textBox2.Text.Trim();

                    // ����� ���������� �� 2019 ����.
                    //IFindPerson findPersonTo2019 = new FindPersonFamiliTo2019(personFamili);
                    //string queryTo2019 = findPersonTo2019.Query();

                    //StringParametr stringParametr = new StringParametr();
                    //stringParametr.Query = queryTo2019;

                    //// ����� ���������� �� �������� NameTableAdd.
                    //IFindPerson findPerson2019Add = new FindPersonFamiliTableAdd(personFamili);
                    //string query2019Add = findPerson2019Add.Query();

                    //StringParametr stringParametr2019Add = new StringParametr();
                    //stringParametr2019Add.Query = query2019Add;

                    //// ����� ���������� �� �������� �� 2019 ���.
                    //IFindPerson findPerson2019 = new FindPerson2019(personFamili);
                    //string query2019 = findPerson2019.Query();

                    //StringParametr stringParametr2019 = new StringParametr();
                    //stringParametr2019.Query = query2019;

                    // ����� ����� ���� �� ����� 2019 ����.
                    IFindPerson findPerson2019Afftar = new FIndPersonFamili2019Aftar(personFamili);
                    string query2019Aftar = findPerson2019Afftar.Query();

                    StringParametr stringParametr2019Aftar = new StringParametr();
                    stringParametr2019Aftar.Query = query2019Aftar;

                    // ����� ��������� ���������� �������� �� ������� � 2021 ����.
                    FindPersonFamilyValidTrue2021 findPersonFamilyValidTrue2021 = new FindPersonFamilyValidTrue2021(personFamili);
                    string queryFindPerson2021FamilyTrue = findPersonFamilyValidTrue2021.Query();

                    StringParametr stringfindPersonFamilyValidTrue2021 = new StringParametr();
                    stringfindPersonFamilyValidTrue2021.Query = queryFindPerson2021FamilyTrue;

                    // ��������� ����� ���������� �� 2019 ����.
                    //ExecuteFind(stringParametr);

                    //// ��������� ����� ���������� 2019 �� �������� TableName2019 ����.
                    //ExecuteFind(stringParametr2019Add);

                    // ��������� ����� ���������� 2019 �� �������� ���� ������ ����.
                    //ExecuteFind(stringParametr2019);

                    // ��������� ����� ���������� ����� 2019 �� �������� ���� ������ ����.
                    ExecuteFind(stringParametr2019Aftar);

                    ExecuteFind(stringfindPersonFamilyValidTrue2021);

                    flagExecute = true;

                    //IFindPerson findPerson = new FindPersonFamili(this.textBox2.Text.Trim());
                    //query = findPerson.Query();
                }
                // ����� ���������� ��������� �������� �� ������� � �����.
                else if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length > 0)
                {
                    //// ����� ��������� �� ������� � �����.
                    //IFindPerson findPerson = new FindPersonByFamiliName(this.textBox2.Text.Trim(), this.txt���.Text.Trim());
                    //query = findPerson.Query();

                    // ���������� ��� �������� ������� ��������� �������� ����� ����� � ��.
                    string personFamili = string.Empty;

                    string personName = string.Empty;

                    personFamili = this.textBox2.Text.Trim();

                    personName = this.txt���.Text.Trim();

                    // ����� ���������� �� 2019 ����.
                    //IFindPerson findPersonTo2019 = new FindPersonFioTo2019(personFamili,personName);
                    //string queryTo2019 = findPersonTo2019.Query();

                    //StringParametr stringParametr = new StringParametr();
                    //stringParametr.Query = queryTo2019;

                    // ��� ��� ������� 2020 ���� ������� �� ������.
                    //// ����� ���������� �� �������� NameTableAdd.
                    //IFindPerson findPerson2019Add = new FindPersonFioTableAdd(personFamili, personName);
                    //string query2019Add = findPerson2019Add.Query();

                    //StringParametr stringParametr2019Add = new StringParametr();
                    //stringParametr2019Add.Query = query2019Add;

                    //// ����� ���������� �� �������� �� 2019 ���.
                    //IFindPerson findPerson2019 = new FindPersonFio2019(personFamili, personName);
                    //string query2019 = findPerson2019.Query();

                    //StringParametr stringParametr2019 = new StringParametr();
                    //stringParametr2019.Query = query2019;

                    //// ����� ����� ���� �� ����� 2019 ����.
                    IFindPerson findPerson2019Afftar = new FIndPersonFio2019Aftar(personFamili, personName);
                    string query2019Aftar = findPerson2019Afftar.Query();

                    StringParametr stringParametr2019Aftar = new StringParametr();
                    stringParametr2019Aftar.Query = query2019Aftar;

                    IFindPerson findPersonFio2021 = new FindPersonFioValidate2021(personFamili, personName);
                    string query2021Fio = findPersonFio2021.Query();

                    StringParametr stringParametrfindPersonFio2021 = new StringParametr();
                    stringParametrfindPersonFio2021.Query = query2021Fio;
                    

                    // ��������� ����� ���������� �� 2019 ����.
                    //ExecuteFind(stringParametr);

                    // ��������� ����� ���������� 2019 �� �������� TableName2019 ����.
                    //ExecuteFind(stringParametr2019Add);

                    // ��������� ����� ���������� 2019 �� �������� ���� ������ ����.
                    //ExecuteFind(stringParametr2019);

                    // ��������� ����� ���������� ����� 2019 �� �������� ���� ������ ����.
                    //ExecuteFind(stringParametr2019Aftar);

                    ExecuteFind(stringParametrfindPersonFio2021);

                    flagExecute = true;
                }

            }
            //else
            
            if (this.FlagValid == false && this.FlagCheck == false)
            {
                // �� ������ ��������. ����� �� �������.
                if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length == 0)
                {
                    // ���������� ��� �������� ������� ��������� �������� ����� ����� � ��.
                    string personFamili = string.Empty;

                    personFamili = this.textBox2.Text.Trim();

                    //// ����� ���������� �� 2019 ����.
                    //IFindPerson findPersonTo2019 = new FindPersonFamiliTo2019NoValid(personFamili);
                    //string queryTo2019 = findPersonTo2019.Query();

                    //StringParametr stringParametr = new StringParametr();
                    //stringParametr.Query = queryTo2019;

                    //// ����� ���������� �� �������� NameTableAdd 2019 ���.
                    //IFindPerson findPerson2019Add = new FindPersonFamiliTableAddNoValid(personFamili);
                    //string query2019Add = findPerson2019Add.Query();

                    //StringParametr stringParametr2019Add = new StringParametr();
                    //stringParametr2019Add.Query = query2019Add;

                    //// ����� ���������� �� �������� �� 2019 ���.
                    //IFindPerson findPerson2019 = new FindPersonFamili2019NoValid(personFamili);
                    //string query2019 = findPerson2019.Query();

                    //StringParametr stringParametr2019 = new StringParametr();
                    //stringParametr2019.Query = query2019;

                    // ����� ����� ���� �� ����� 2019 ����.
                    IFindPerson findPerson2019Afftar = new FIndPersonFamili2019AftarNoValid(personFamili);
                    string query2019Aftar = findPerson2019Afftar.Query();

                    StringParametr stringParametr2019Aftar = new StringParametr();
                    stringParametr2019Aftar.Query = query2019Aftar;

                    IFindPerson findPersonFamiliNoValid2021 = new IFindPersonFamiliNoValid2021(personFamili);
                    string queryFamiliNoValid2021 = findPersonFamiliNoValid2021.Query();

                    StringParametr stringParametrFamiliNoValid2021 = new StringParametr();
                    stringParametrFamiliNoValid2021.Query = queryFamiliNoValid2021;

                    // ��������� ����� ���������� �� 2019 ����.
                    //ExecuteFind(stringParametr);

                    //��������� ����� ���������� 2019 �� �������� TableName2019 ����.
                    //ExecuteFind(stringParametr2019Add);

                    // ��������� ����� ���������� 2019 �� �������� ���� ������ ����.
                    //ExecuteFind(stringParametr2019);

                    // ��������� ����� ���������� ����� 2019 �� �������� ���� ������ ����.
                    ExecuteFind(stringParametr2019Aftar);

                    ExecuteFind(stringParametrFamiliNoValid2021);

                    flagExecute = true;

                }
                else if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length > 0)
                {
                    //IFindPerson findPerson = new FindPersonFirstNameNameNoValid(this.textBox2.Text.Trim(), this.txt���.Text.Trim());
                    //query = findPerson.Query();

                    // ����� ���������� �� ��������� �������� �� ���.
                    string personFamili = string.Empty;

                    string personName = string.Empty;

                    personFamili = this.textBox2.Text.Trim();

                    personName = this.txt���.Text.Trim();

                    // ����� ���������� �� 2019 ����.
                    //IFindPerson findPersonTo2019 = new FindPersonFioTo2019NoValid(personFamili, personName);
                    //string queryTo2019 = findPersonTo2019.Query();

                    //StringParametr stringParametr = new StringParametr();
                    //stringParametr.Query = queryTo2019;

                    //// ����� ���������� �� �������� NameTableAdd.
                    //IFindPerson findPerson2019Add = new FindPersonFioTableAddNoValid(personFamili, personName);
                    //string query2019Add = findPerson2019Add.Query();

                    //StringParametr stringParametr2019Add = new StringParametr();
                    //stringParametr2019Add.Query = query2019Add;

                    //// ����� ���������� �� �������� �� 2019 ���.
                    //IFindPerson findPerson2019 = new FindPersonFio2019NoValid(personFamili, personName);
                    //string query2019 = findPerson2019.Query();

                    //StringParametr stringParametr2019 = new StringParametr();
                    //stringParametr2019.Query = query2019;

                    //// ����� ����� ���� �� ����� 2019 ����.
                    IFindPerson findPerson2019Afftar = new FIndPersonFio2019AftarNoValid(personFamili, personName);
                    string query2019Aftar = findPerson2019Afftar.Query();

                    StringParametr stringParametr2019Aftar = new StringParametr();
                    stringParametr2019Aftar.Query = query2019Aftar;

                    // ����� ��������� �� ������� � ����� � �������� 2021.
                    IFindPerson findPerson2021FioNoValid = new FindPersonFioNoValid2021(personFamili, personName);
                    string queryFindPersonFio2021NoValid = findPerson2021FioNoValid.Query();

                    StringParametr stringfindPerson2021FioNoValid = new StringParametr();
                    stringfindPerson2021FioNoValid.Query = queryFindPersonFio2021NoValid;

                    // ��������� ����� ���������� �� 2019 ����.
                    //ExecuteFind(stringParametr);

                    // ��������� ����� ���������� 2019 �� �������� TableName2019 ����.
                    //ExecuteFind(stringParametr2019Add);

                    // ��������� ����� ���������� 2019 �� �������� ���� ������ ����.
                    //ExecuteFind(stringParametr2019);

                    // ��������� ����� ���������� ����� 2019 �� �������� ���� ������ 2020 ����.
                    // ExecuteFind(stringParametr2019Aftar);

                    // ��������� ����� ���������� ����� 2020 �� �������� ���� ������ 2021 ����.
                    ExecuteFind(stringfindPerson2021FioNoValid);

                    flagExecute = true;

                }
            }

            if (this.FlagValid == false && this.FlagCheck == true)
            {
                if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length == 0)
                {
                    query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.����������������� " +
                              "FROM ������� INNER JOIN �������� " +
                              "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
                              "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
                              "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
                              "where �������.������������ = 'False' " + " or (�������.������������ = 'False' and flag���������� = 0)  " +
                              "and ��������.������� = '" + this.textBox2.Text + "' " + // and ��������.��� = '" + this.txt���.Text.Trim() + "' " +
                              "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.����������������� ";
                }
                else if (this.textBox2.Text.Trim().Length > 0 && this.txt���.Text.Trim().Length > 0)
                {
                    query = "SELECT �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������, sum(����������������.�����) as '�����',�������.������������������,�������.logWrite,�������.������������,�������.����������������� " +
                              "FROM ������� INNER JOIN �������� " +
                              "ON dbo.�������.id_�������� = dbo.��������.id_�������� INNER JOIN " +
                              "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� INNER JOIN " +
                              "dbo.���������������� ON dbo.�������.id_������� = dbo.����������������.id_������� " +
                              "where �������.������������ = 'False' " + " or (�������.������������ = 'False' and flag���������� = 0)  " +
                              "and ��������.������� = '" + this.textBox2.Text + "'  and ��������.��� = '" + this.txt���.Text.Trim() + "' " +
                              "Group by �������.id_�������,�������.�������������, ��������.�������, ��������.���, ��������.��������, �����������������.�����������������,�������.������������������,�������.logWrite,�������.������������,�������.����������������� ";
                }

            }

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                /*

                if (flagExecute == false)
                {
                    con.Open();
                    SqlTransaction transact = con.BeginTransaction();

                    var queryScript = query;

                    SqlCommand com = new SqlCommand(query, con);
                    com.Transaction = transact;

                    // �������� ��������� ������� ��� ����������� ������ � DataGrid.
                    DataTable tab = new DataTable();
                    DataColumn col0 = new DataColumn("id_�������", typeof(int));
                    tab.Columns.Add(col0);
                    DataColumn col1 = new DataColumn("�������������", typeof(string));
                    tab.Columns.Add(col1);
                    DataColumn col2 = new DataColumn("�������", typeof(string));
                    tab.Columns.Add(col2);

                    DataColumn col3 = new DataColumn("���", typeof(string));
                    tab.Columns.Add(col3);
                    DataColumn col4 = new DataColumn("��������", typeof(string));
                    tab.Columns.Add(col4);

                    DataColumn col5 = new DataColumn("�����������������", typeof(string));
                    tab.Columns.Add(col5);
                    DataColumn col6 = new DataColumn("�����", typeof(string));
                    tab.Columns.Add(col6);

                    DataColumn col7 = new DataColumn("����", typeof(string));
                    tab.Columns.Add(col7);
                    DataColumn col8 = new DataColumn("����������", typeof(string));
                    tab.Columns.Add(col8);

                    // ��������� ����� ���� ������� � ����� �������
                    DataColumn col9 = new DataColumn("������������", typeof(string));
                    tab.Columns.Add(col9);

                    DataColumn col10 = new DataColumn("�����������������", typeof(string));
                    tab.Columns.Add(col10);

                    if (this.FlagValid == true && this.FlagCheck == false)
                    {
                        DataColumn col11 = new DataColumn("���������", typeof(string));
                        tab.Columns.Add(col11);

                        DataColumn col12 = new DataColumn("��������������", typeof(string));
                        tab.Columns.Add(col12);

                        DataColumn col13 = new DataColumn("flag2019AddWrite", typeof(bool));
                        tab.Columns.Add(col13);
                    }
                    else
                    {
                        DataColumn col14 = new DataColumn("flag2019AddWrite", typeof(bool));
                        tab.Columns.Add(col14);
                    }

                    DataColumn col15 = new DataColumn("flag����������", typeof(bool));
                    tab.Columns.Add(col15);

                    // ������ ��� �������� ����������� ������.
                    List<FindPersonNumContractItem> listPerson = new List<FindPersonNumContractItem>();

                    SqlDataReader read = com.ExecuteReader();
                    while (read.Read())
                    {
                        DataRow row = tab.NewRow();

                        // ����� ���������� ������.
                        FindPersonNumContractItem it = new FindPersonNumContractItem();

                        it.id_������� = read["id_�������"].ToString().Trim();
                        it.������������� = read["�������������"].ToString().Trim();
                        it.������� = read["�������"].ToString().Trim();
                        it.��� = read["���"].ToString().Trim();
                        it.�������� = read["��������"].ToString().Trim();
                        it.����������������� = read["�����������������"].ToString().Trim();
                        it.����� = Convert.ToDecimal(read["�����"]).ToString("c").Trim();
                        if (read["������������������"] != DBNull.Value)//
                        {
                            it.���� = Convert.ToDateTime(read["������������������"]).ToShortDateString().Trim();
                        }
                        if (read["logWrite"] != DBNull.Value)
                        {
                            it.���������� = read["logWrite"].ToString().Trim();
                        }
                        if (read["������������"] != DBNull.Value && read["������������"].ToString().Trim() != "NULL")
                        {
                            it.������������ = read["������������"].ToString().Trim();
                        }
                        if (read["�����������������"] != DBNull.Value && read["�����������������"].ToString().Trim() != "NULL")
                        {
                            it.����������������� = read["�����������������"].ToString().Trim();
                        }

                        if (this.FlagValid == true && this.FlagCheck == false)
                        {
                            if (read["���������"] != DBNull.Value)// && read["���������"] != null)
                            {
                                it.��������� = read["���������"].ToString().Trim();
                            }

                            if (read["��������������"] != DBNull.Value)// && read["���������"] != null)
                            {
                                it.�������������� = Convert.ToDateTime(read["��������������"]).ToShortDateString().Trim();
                            }
                        }

                        if (read["flag2019AddWrite"] != DBNull.Value)
                        {
                            it.flag2019AddWrite = Convert.ToBoolean(read["flag2019AddWrite"]);
                        }

                        if (read["flag����������"] != DBNull.Value)
                        {
                            it.flag���������� = Convert.ToBoolean(read["flag����������"]);
                        }
       
                        listPerson.Add(it);
                    }

                }
                */

                var listCount = listPerson.ToList();

                var listCountA = listPerson.Where(w=>w.flag���������� == true).ToList();

                if (flagExecute == false)
                {
                    this.dataGridView1.DataSource = listPerson;

                    this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
                }
                else
                {
                    this.dataGridView1.DataSource = CompareContractPerson.Compare(listPerson);
                }

                this.dataGridView1.Columns["id_�������"].Width = 100;
                this.dataGridView1.Columns["id_�������"].Visible = false;
                this.dataGridView1.Columns["�������������"].Width = 100;
                this.dataGridView1.Columns["�������������"].DisplayIndex = 0;

                this.dataGridView1.Columns["�������"].Width = 150;
                this.dataGridView1.Columns["�������"].DisplayIndex = 1;
                this.dataGridView1.Columns["�������"].SortMode = DataGridViewColumnSortMode.Automatic;

                this.dataGridView1.Columns["���"].Width = 150;
                this.dataGridView1.Columns["���"].DisplayIndex = 2;

                this.dataGridView1.Columns["��������"].Width = 150;
                this.dataGridView1.Columns["��������"].DisplayIndex = 3;

                this.dataGridView1.Columns["�����������������"].Width = 200;
                this.dataGridView1.Columns["�����������������"].DisplayIndex = 4;
                this.dataGridView1.Columns["�����������������"].HeaderText = "�������� ���������";

                this.dataGridView1.Columns["�����"].Width = 100;
                this.dataGridView1.Columns["�����"].DisplayIndex = 5;

                this.dataGridView1.Columns["����"].Width = 120;
                this.dataGridView1.Columns["����"].DisplayIndex = 6;
                this.dataGridView1.Columns["����"].HeaderText = "���� ������ ������� �������� � ���� ��";

                this.dataGridView1.Columns["������������"].Width = 80;
                this.dataGridView1.Columns["������������"].DisplayIndex = 7;
                this.dataGridView1.Columns["������������"].HeaderText = "����� �������";

                this.dataGridView1.Columns["�����������������"].Width = 100;
                this.dataGridView1.Columns["�����������������"].DisplayIndex = 8;
                this.dataGridView1.Columns["�����������������"].HeaderText = "����� ���� �������";

                if (this.FlagValid == true && this.FlagCheck == false)
                {
                    this.dataGridView1.Columns["���������"].Width = 100;
                    this.dataGridView1.Columns["���������"].DisplayIndex = 9;
                    this.dataGridView1.Columns["���������"].HeaderText = "����� ����";
                    this.dataGridView1.Columns["���������"].Visible = false;

                    this.dataGridView1.Columns["��������������"].Width = 100;
                    this.dataGridView1.Columns["��������������"].DisplayIndex = 10;
                    this.dataGridView1.Columns["��������������"].HeaderText = "���� ���������� ����";

                    this.dataGridView1.Columns["����������"].Width = 100;
                    this.dataGridView1.Columns["����������"].DisplayIndex = 11;
                    this.dataGridView1.Columns["flag2019AddWrite"].DisplayIndex = 12;
                    this.dataGridView1.Columns["flag2019AddWrite"].Visible = false;
                }
                else
                {
                    this.dataGridView1.Columns["����������"].Width = 100;
                    this.dataGridView1.Columns["����������"].DisplayIndex = 9;
                    this.dataGridView1.Columns["flag2019AddWrite"].DisplayIndex = 10;
                    this.dataGridView1.Columns["flag2019AddWrite"].Visible = false;
                }

                // ������� ������ � ������� ����.
                for (int i = 0; i <= this.dataGridView1.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["flag����������"].Value) == true)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }


                //this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }


            }
        }

        private void ValidateActForContract(int id�������)
        {
            // ��������� ���� �� � ������� �������� ��� ����������� �����.
            string queryValidAct = "select COUNT(id_���) as '���������������' from ������������������� " +
                                   "where id_������� in ( " +
                                    "select id_�������  from ������� " +
                                    "where id_������� = " + id������� + " ) ";

            DataTable tabAct = ���������.GetTableSQL(queryValidAct, "����������");

            StringBuilder build = new StringBuilder();

            if (Convert.ToInt32(tabAct.Rows[0]["���������������"]) > 0)
            {
                string queryValidNumAct = "select ���������,�������������� from ������������������� " +
                                          "where id_������� in ( " +
                                          "select id_�������  from ������� " +
                                          "where id_������� = " + id������� + " ) ";

                DataTable tabNumAct = ���������.GetTableSQL(queryValidNumAct, "���������������");

                // ������� ����� ����
                build.Append("����� ���� - " + tabNumAct.Rows[0]["���������"].ToString().Trim());
                build.Append(" �� " + Convert.ToDateTime(tabNumAct.Rows[0]["��������������"]).ToShortDateString());

                DialogResult dialogResult = MessageBox.Show("�������� ������ �������� ������, ������� ������ � ����� -  " + build.ToString().Trim(), "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }


            }
        }

        private void ����������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // ������� ������� �������.
            int id������� = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_�������"].Value);

            bool flagWrite2019 = false;

            flagWrite2019 = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);

            string queryValidAct = string.Empty;
            

            if (flagWrite2019 == false)
            {
                // ��������� ���� �� � ������� �������� ��� ����������� �����.
                queryValidAct = "select COUNT(id_���) as '���������������' from ������������������� " +
                                   "where id_������� in ( " +
                                    "select id_�������  from ������� " +
                                    "where id_������� = " + id������� + " ) ";
            }
            else
            {
                queryValidAct = @"select  COUNT(id_���) as '���������������' from �������������������Add 
                                        inner join �������Add
                                        on �������������������Add.id_������� = �������Add.id_����������
                                         where �������Add.id_������� = " + id������� + " ";
            }

            DataTable tabAct = ���������.GetTableSQL(queryValidAct, "����������");

            StringBuilder build = new StringBuilder();

            string queryValidNumAct = string.Empty;

            if (Convert.ToInt32(tabAct.Rows[0]["���������������"]) > 0)
            {
                if (flagWrite2019 == false)
                {

                    queryValidNumAct = "select ���������,�������������� from ������������������� " +
                                          "where id_������� in ( " +
                                          "select id_�������  from ������� " +
                                          "where id_������� = " + id������� + " ) ";
                }
                else
                {
                    queryValidNumAct = @"select ���������,��������������,������������������������� from �������������������Add 
                                        inner join �������Add
                                        on �������������������Add.id_������� = �������Add.id_����������
                                         where �������Add.id_������� = " + id������� + " ";
                }

                DataTable tabNumAct = ���������.GetTableSQL(queryValidNumAct, "���������������");

                // ������� ����� ����
                build.Append("����� ���� - " + tabNumAct.Rows[0]["���������"].ToString().Trim());
                build.Append(" �� " + Convert.ToDateTime(tabNumAct.Rows[0]["��������������"]).ToShortDateString());

                //// �������� ���� �� ���.
                //// ��� ��� �� �������� 2019 ��� ��� ������� ��� �� �������� �� ������� ����� ���� � ������� �������
                //if (Convert.ToDecimal(tabNumAct.Rows[0]["�������������������������"]) != 0.0m)
                //{
                //    flagAct = true;
                //}
                //else
                //{
                //    flagAct = false;
                //}

                DialogResult dialogResult = MessageBox.Show("�������� ������ �������� ������, ������� ������ � ����� -  " + build.ToString().Trim(), "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

            }


            DialogResult dialogResult2 = MessageBox.Show("�������� ������ �������� ��� ����������� �������� " + build.ToString().Trim(), "��������", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (dialogResult2 == System.Windows.Forms.DialogResult.OK)
            {
                string query = string.Empty;

                string user = MyAplicationIdentity.GetUses();

                if (flagWrite2019 == false)
                {
                    query = "declare @id int " +
                               "set @id = " + id������� + " " +
                               "delete ������������������� " +
                               "where id_������� in ( " +
                               "select id_�������  from ������� " +
                               "where id_������� = @id " +
                               ") " +
                               "update ������� " +
                               "set ������������ = 'False', " +
                               "������������������������ = '19000101', " +
                               "������������������������� = 0.0, " +
                               "������������ =  null, " +
                               "����������� = null, " +
                               "����������������� = null, " +
                               "��������������� = null, " +
                               " �������������� = 0, " +
                               " flag���������� = 0, " +
                               " logWrite = '" + MyAplicationIdentity.GetUses() + "' , " +
                               " ���������������������� = 1 " +
                               "where id_������� in ( " +
                               "select id_�������  from ������� " +
                               "where id_������� = @id) ";
                }
                else
                {
                    query = "declare @id int " +
                               "set @id = " + id������� + " " +
                               @" delete Act
                                from �������������������Add as Act
                                inner join �������Add
                                on �������Add.id_������� = Act.id_�������
                                where �������Add.id_������� = @id
                                delete Act2
                                from ������������������� as Act2
                                inner join �������Add
                                on �������Add.id_���������� = Act2.id_�������
                                where �������Add.id_������� = @id " +
                               "update �������Add " +
                               "set ������������ = 'False', " +
                               " ��������������� = 0 ," +
                               "������������������������ = '19000101', " +
                               "������������������������� = 0.0, " +
                               "������������ =  null, " +
                               "����������� = null, " +
                               "����������������� = null, " +
                               "��������������� = null, " +
                               "logWrite = '" + user + "' , " +
                               " ���������������������� = 1 " +
                               "where id_������� in ( " +
                               "select id_�������  from �������Add " +
                               "where id_������� = @id) ";
                }

                // �������� ������.
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    SqlTransaction transact = con.BeginTransaction();

                    // �������� Sql ������.
                    Classes.ExecuteQuery.Execute(query, con, transact);

                    // �������� ����������.
                    transact.Commit();
                }

                this.textBox2.Text = "����������";

                this.FlagValid = true;

                // ������� DataGrid.
                LoadAfterClickFind();

                this.FlagValid = false;

                this.textBox2.Text = string.Empty;


            }
        }

        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ������� ������� �������.
            int id������� = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_�������"].Value);

            string numContract = this.dataGridView1.CurrentRow.Cells["�������������"].Value.ToString();

            // id �������� �� 
           // int idContract = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["idContract"].Value);

            int flag��� =  Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["���"].Value);

            bool flagWrite2019 = false;

            flagWrite2019 = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);

            string queryValidAct = string.Empty;

            if (flag��� != 2019 && flag��� != 1)
            {
                queryValidAct = "select COUNT(id_���) as '���������������' from ������������������� " +
                                   "where id_������� in ( " +
                                    "select id_�������  from ������� " +
                                    "where id_������� = " + id������� + " ) ";
            }
            //else if(flag��� != 1)
            //{
            //    queryValidAct = "select COUNT(id_���) as '���������������' from ������������������� " +
            //                       "where id_������� in ( " +
            //                        "select id_�������  from ������� " +
            //                        "where id_������� = " + id������� + " ) ";
            //}
            else if(flag��� == 2019 || flag��� == 1)
            {
                queryValidAct = @"select  COUNT(id_���) as '���������������' from �������������������Add 
                                        inner join �������Add
                                        on �������������������Add.id_������� = �������Add.id_����������
                                         where �������Add.id_������� = " + id������� + " ";
            }
            //else if(flag��� == 1)
            //{
            //    queryValidAct = @"select  COUNT(id_���) as '���������������' from �������������������Add 
            //                            inner join �������Add
            //                            on �������������������Add.id_������� = �������Add.id_����������
            //                             where �������Add.id_������� = " + id������� + " ";
            //}

            DataTable tabAct = ���������.GetTableSQL(queryValidAct, "����������");

            StringBuilder build = new StringBuilder();

            string queryValidNumAct = string.Empty;

            // �������� ���� �� � �������� ���.
            if (Convert.ToInt32(tabAct.Rows[0]["���������������"]) > 0)
            {
                if (flag��� != 2019)
                {
                    queryValidNumAct = "select ���������,�������������� from ������������������� " +
                                          "where id_������� in ( " +
                                          "select id_�������  from ������� " +
                                          "where id_������� = " + id������� + " ) ";
                }
                else if(flag��� != 1)
                {
                    queryValidNumAct = "select ���������,�������������� from ������������������� " +
                                          "where id_������� in ( " +
                                          "select id_�������  from ������� " +
                                          "where id_������� = " + id������� + " ) ";
                }
                else if(flag��� == 2019)
                {
                    queryValidNumAct = @"select ���������,��������������,������������������������� from �������������������Add 
                                        inner join �������Add
                                        on �������������������Add.id_������� = �������Add.id_����������
                                         where �������Add.id_������� = " + id������� + " ";
                }
                else if(flag��� == 1)
                {
                    queryValidNumAct = @"select ���������,��������������,������������������������� from �������������������Add 
                                        inner join �������Add
                                        on �������������������Add.id_������� = �������Add.id_����������
                                         where �������Add.id_������� = " + id������� + " ";
                }

                DataTable tabNumAct = ���������.GetTableSQL(queryValidNumAct, "���������������");

                // ������� ����� ����
                build.Append("����� ���� - " + tabNumAct.Rows[0]["���������"].ToString().Trim());
                build.Append(" �� " + Convert.ToDateTime(tabNumAct.Rows[0]["��������������"]).ToShortDateString());


                DialogResult dialogResult = MessageBox.Show("�������� ������ �������� ������, ������� ������ � ����� -  " + build.ToString().Trim(), "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

            }

            DialogResult dialogResult2 = MessageBox.Show("�������� ������ �������� ������������� " + build.ToString().Trim(), "��������", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (dialogResult2 == System.Windows.Forms.DialogResult.OK)
            {
                string query = string.Empty;

                string user = Environment.UserName;// MyAplicationIdentity.GetUses();

                string user2 = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                //string user3 = MyAplicationIdentity.GetUses();

                if (flag��� != 2019 && flag��� != 1)
                {
                    query = @"declare @id int 
                            set @id = 150106
                            declare @numContract nvarchar(50)
                            set @numContract = '"+ numContract + "' " +
                            @" delete �������������������
                            where id_������� in (select id_������� from ������� where ������������� = @numContract ) 
                            update �������
                            set ������������ = 'False',
                            ������������������������ = '19000101', ������������������������� = 0.0,
                            ������������ = null, ����������� = null, ����������������� = null,
                            ��������������� = null, �������������� = 1, flag���������� = 1,
                            logWrite = '" + user + "', " +
                            @" ���������������������� = 1
                            where ������������� = @numContract
                            delete �������������������Add
                            where �������������������Add.id_������� in (select id_������� from �������Add where ������������� = @numContract) 
                            update �������Add set ������������ = 'False', ��������������� = 0,
                            ������������������������ = '19000101', ������������������������� = 0.0, " +
                            //������������ = null,
                            //����������� = null, ����������������� = null, ��������������� = null
                            @" �������������� = 1, flag���������� = 1,
                            logWrite = '"+ user +"', ���������������������� = 1 " +
                            " where �������Add.������������� = @numContract  ";

                    #region ������ ���
                    //query = "declare @id int " +
                    //           "set @id = " + id������� + " " +
                    //           " declare @numContract nvarchar(50) " +
                    //           "delete ������������������� " +
                    //           "where id_������� in ( " +
                    //           "select id_�������  from ������� " +
                    //           "where id_������� = @id " +
                    //           ") " +
                    //           "update ������� " +
                    //           "set ������������ = 'False', " +
                    //           "������������������������ = '19000101', " +
                    //           "������������������������� = 0.0, " +
                    //           "������������ =  null, " +
                    //           "����������� = null, " +
                    //           "����������������� = null, " +
                    //           "��������������� = null, " +
                    //           " �������������� = 1, " +
                    //           " flag���������� = 1, " +
                    //           //" logWrite = '" + MyAplicationIdentity.GetUses() + "' , " +
                    //           " logWrite = '" + user + "' , " +
                    //           " ���������������������� = 1 " +
                    //           "where id_������� in (select �������.id_������� from ������� " +
                    //            " inner join ������� as T1 " +
                    //            " on T1.������������� = �������.������������� " +
                    //            " where �������.id_������� = @id)  " +
                    //            " select @numContract = �������.������������� from �������  " +
                    //            " inner join ������� as T1  on T1.������������� = �������.������������� " +
                    //            " where �������.id_������� = @id ";
                    //query += " delete �������������������Add " +
                    //         " where �������������������Add.id_������� in (select id_������� from �������Add " +
                    //          " where ������������� = @numContract) " +
                    //           "update �������Add " +
                    //           "set ������������ = 'False', " +
                    //           " ��������������� = 0 ," +
                    //           "������������������������ = '19000101', " +
                    //           "������������������������� = 0.0, " +
                    //           "������������ =  null, " +
                    //           "����������� = null, " +
                    //           "����������������� = null, " +
                    //           "��������������� = null, " +
                    //            " �������������� = 1, " +
                    //           " flag���������� = 1, " +
                    //           "logWrite = '" + user + "' , " +
                    //           " ���������������������� = 1 " +
                    //           "where �������Add.������������� =  @numContract  ";
                    #endregion
                }
                else if(flag��� == 2019 || flag��� == 1)
                {
                    query = @"declare @id int 
                            set @id = 150106
                            declare @numContract nvarchar(50)
                            set @numContract = '" + numContract + "' " +
                            @" delete �������������������
                            where id_������� in (select id_������� from ������� where ������������� = @numContract ) 
                            update �������
                            set ������������ = 'False',
                            ������������������������ = '19000101', ������������������������� = 0.0,
                            ������������ = null, ����������� = null, ����������������� = null,
                            ��������������� = null, �������������� = 1, flag���������� = 1,
                            logWrite = '" + user + "', " +
                            @" ���������������������� = 1
                            where ������������� = @numContract
                            delete �������������������Add
                            where �������������������Add.id_������� in (select id_������� from �������Add where ������������� = @numContract) 
                            update �������Add set ������������ = 'False', ��������������� = 0,
                            ������������������������ = '19000101', ������������������������� = 0.0, ������������ = null,
                            ����������� = null, ����������������� = null, ��������������� = null, �������������� = 1, flag���������� = 1,
                            logWrite = '" + user + "', ���������������������� = 1 " +
                            " where �������Add.������������� = @numContract  ";

                    #region ������ ���
                    //query = "declare @id int " +
                    //           "set @id = " + id������� + " " +
                    //           " declare @idContract int  " +
                    //           @" delete Act
                    //            from �������������������Add as Act
                    //            inner join �������Add
                    //            on �������Add.id_������� = Act.id_�������
                    //            where �������Add.id_������� = @id
                    //            delete Act2
                    //            from ������������������� as Act2
                    //            inner join �������Add
                    //            on �������Add.id_���������� = Act2.id_�������
                    //            where �������Add.id_������� = @id " +
                    //           "update �������Add " +
                    //           "set ������������ = 'False', " +
                    //           " ��������������� = 0 ," +
                    //           "������������������������ = '19000101', " +
                    //           "������������������������� = 0.0, " +
                    //           "������������ =  null, " +
                    //           "����������� = null, " +
                    //           "����������������� = null, " +
                    //           "��������������� = null, " +
                    //            " �������������� = 1, " +
                    //           " flag���������� = 1, " +
                    //           "logWrite = '" + user + "' , " +
                    //           " ���������������������� = 1 " +
                    //           "where id_������� in ( " +
                    //           "select id_�������  from �������Add " +
                    //           "where id_������� = @id) " +
                    //           " select @idContract = id_���������� from �������Add where id_������� = @id ";

                    //query += @" delete �������������������  
                    //            where id_������� in (
                    //            select id_������� from �������
                    //            where ������������� = '"+ this.textBox1.Text.Trim() + "' ) " +
                    //            @" update �������
                    //            set ������������ = 'False', ������������������������ = '19000101',
                    //            ������������������������� = 0.0, ������������ = null, ����������� = null,
                    //            ����������������� = null, ��������������� = null, �������������� = 1,
                    //            flag���������� = 1, logWrite = 'dugin', ���������������������� = 1
                    //            where ������������� = '" + this.textBox1.Text.Trim() + "' ";
                    #endregion
                }

                var testQuery = query;

                // �������� ������.
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    SqlTransaction transact = con.BeginTransaction();


                    Classes.ExecuteQuery.Execute(query, con, transact);

                    // �������� ����������.
                    transact.Commit();
                }

                this.textBox2.Text = "����������";

                this.FlagValid = true;

                // ������� DataGrid.
                LoadAfterClickFind();

                this.FlagValid = false;

                this.textBox2.Text = string.Empty;


            }

            //ValidateActForContract(id�������);


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}