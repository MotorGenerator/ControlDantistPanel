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

using Microsoft.Office.Interop.Word;

using System.Globalization;
using System.Threading;
using System.IO;


//������� DLL
using DantistLibrary;


namespace ControlDantist
{
    public partial class FormDisplayReestr : Form
    {
        private Dictionary<string, DisplayReestr> disp;
        private List<Unload> unload;

        private string _������������ = string.Empty;
        private string _���������������� = string.Empty;
        private string _����������� = string.Empty;
        private string _��������������� = string.Empty;

        private DataTable dtServ;
        private DataTable dtFile;

        //���������� ��� �������� ���������� �����
        private int countAct = 0;


        /// <summary>
        /// ���� ���� �������
        /// </summary>
        public string ���������������
        {
            get
            {
                return _���������������;
            }
            set
            {
                _��������������� = value;
            }
        }


        /// <summary>
        /// ����� ���� �������
        /// </summary>
        public string ����������������
        {
            get
            {
                return _����������������;
            }
            set
            {
                _���������������� = value;
            }
        }


        /// <summary>
        /// ���� �������
        /// </summary>
        public string �����������
        {
            get
            {
                return _�����������;
            }
            set
            {
                _����������� = value;
            }
        }


        /// <summary>
        /// ����� �������
        /// </summary>
        public string ������������
        {
            get
            {
                return _������������;
            }
            set
            {
                _������������ = value;
            }
        }

        /// <summary>
        /// ������ �������� �����
        /// </summary>
        public List<Unload> Unloads
        {
            get
            {
                return unload;
            }
            set
            {
                unload = value;
            }
        }

        /// <summary>
        /// ������ ���������� ����� ����������� �����
        /// </summary>
        public Dictionary<string, DisplayReestr> ���������
        {
            get
            {
                return disp;
            }
            set
            {
                disp = value;
            }
        }

        public FormDisplayReestr()
        {
            InitializeComponent();
        }

        private void FormDisplayReestr_Load(object sender, EventArgs e)
        {
            CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCInfo;

            //�������� ���������� countAct ���������� �����
            countAct = this.���������.Count;

            //List<DisplayReestr> list = new List<DisplayReestr>();
            List<DisplayReestrCheck> list = new List<DisplayReestrCheck>();

            //����� ���������
             decimal sum��������� = 0.0m;

            //�������� ������� ��������� 
            foreach (DisplayReestr dr in this.���������.Values)
            {

                ReestrControl rkTest = new ReestrControl();
                rkTest = dr.��������;

                if (rkTest.��� == "������� ����� ������������")
                {
                    string iTerst = "Test";
                }

                DisplayReestrCheck drck = new DisplayReestrCheck();

                // ������� ������ �� ���������� ����������.
                drck.����������������� = dr.�����������������;

                /*���� FlagError = true - ������ �� ������� ������ ���
                 * ���� ErrorPerson = false �� ������ �� ������������ ������ ������ ���
                 * ���� FlagError = false - ������ �� ������� ������
                 * ���� ErrorPerson = true - �� ������ �� ������������ ������ ������
                 */

                //������ ��� �� � ������� �� � ������������ ������
                if (dr.FlagError == true && dr.ErrorPerson == false)
                {
                    drck.FlagError = true;
                }

                //������ � � ������������ ������ � � �������
                if (dr.FlagError == false && dr.ErrorPerson == true)
                {
                    drck.FlagError = false;
                }

                //
                if (dr.FlagError == true && dr.ErrorPerson == true)
                {
                    drck.FlagError = false;
                }

                if (dr.FlagError == false && dr.ErrorPerson == false)
                {
                    drck.FlagError = false;
                }

                // ���� ������� ��� �����������.
                if (dr.FlagError����������������� == true)
                {
                    drck.FlagError = false;

                    drck.������������������������� = dr.��������������������.Trim();

                    drck.FlagErrorAct������ = true;
                }

                if (dr.FlagError == false && dr.ErrorPrefCategory == true)
                {
                    drck.FlagError = false;
                }
                
                //����� ����������� �����
                //drck.Sum = dr.Sum.ToString("c");
                drck.Sum = Math.Round(dr.������������������,2).ToString("c");

                ReestrControl rck = dr.��������;
                drck.���_�������� = rck.���;

                //�������� ������ � ������� �������� ����� �������� � ���� ��������
                string[] srry���������������� = rck.�����������������.Split(' ');

                //������� ����� ��������
                drck.������������� = srry����������������[0];

                //������� ���� ��������
                drck.������������ = srry����������������[1];

                //������� �������� ����� ���� � ���� ����
                string[] arry������������� = rck.�����������������������.Split(' ');

                //������� ���� ���� 
                drck.��������� = arry�������������[0];

                //������� ���� ����
                drck.�������� =  arry�������������[1];

                drck.������������ = dr.������������;

                //������� ����� ����
                drck.��������� = Math.Round(dr.�������������������������,2).ToString("c");

                //������ ������� ��� ����������
                drck.FlagAddContract = dr.FlagAddContract;

                //drck.��������� = rck.�����������������������;
                list.Add(drck);

                //sum��������� = Math.Round(Math.Round(Math.Round(sum���������, 2) + Math.Round(dr.�������������������������, 2)), 2);
                sum��������� = Math.Round(Math.Round(sum���������, 2) + Math.Round(dr.�������������������������, 2), 2);
            }

            this.dataGridView1.DataSource = list;

            this.dataGridView1.Columns["�������������"].HeaderText = "� ��������";
            this.dataGridView1.Columns["�������������"].DisplayIndex = 1;
            this.dataGridView1.Columns["�������������"].Width = 100;
            this.dataGridView1.Columns["�������������"].ReadOnly = true;

            this.dataGridView1.Columns["������������"].HeaderText = "���� ��������";
            this.dataGridView1.Columns["������������"].DisplayIndex = 2;
            this.dataGridView1.Columns["������������"].Width = 80;
            this.dataGridView1.Columns["������������"].ReadOnly = true;

            this.dataGridView1.Columns["Sum"].HeaderText = "����� ��������";
            this.dataGridView1.Columns["Sum"].DisplayIndex = 3;
            this.dataGridView1.Columns["Sum"].Width = 80;
            this.dataGridView1.Columns["Sum"].ReadOnly = true;

           
             this.dataGridView1.Columns["���������"].HeaderText = "� ����";
             this.dataGridView1.Columns["���������"].DisplayIndex = 4;
             this.dataGridView1.Columns["���������"].Width = 100;
            this.dataGridView1.Columns["���������"].ReadOnly = true;

             this.dataGridView1.Columns["��������"].HeaderText = "���� ����";
             this.dataGridView1.Columns["��������"].DisplayIndex = 5;
             this.dataGridView1.Columns["��������"].Width = 80;
            this.dataGridView1.Columns["��������"].ReadOnly = true;

            this.dataGridView1.Columns["���������"].HeaderText = "����� ����";
            this.dataGridView1.Columns["���������"].DisplayIndex = 6;
            this.dataGridView1.Columns["���������"].Width = 100;
            this.dataGridView1.Columns["���������"].ReadOnly = true;

             this.dataGridView1.Columns["���_��������"].HeaderText = "���";
             this.dataGridView1.Columns["���_��������"].DisplayIndex = 0;
             this.dataGridView1.Columns["���_��������"].Width = 200;
            this.dataGridView1.Columns["���_��������"].ReadOnly = true;

             this.dataGridView1.Columns["FlagError"].HeaderText = "��������� ��������";
             this.dataGridView1.Columns["FlagError"].DisplayIndex = 7;
             this.dataGridView1.Columns["FlagError"].Width = 100;

          
            //������� ��� ��������������
             this.dataGridView1.Columns["FlagError"].ReadOnly = false;

             this.dataGridView1.Columns["FlagAddContract"].HeaderText = "������� ���. ����������";
             this.dataGridView1.Columns["FlagAddContract"].DisplayIndex = 8;
             this.dataGridView1.Columns["FlagAddContract"].Width = 100;
             this.dataGridView1.Columns["FlagAddContract"].ReadOnly = true;

             this.dataGridView1.Columns["�������������������������"].Visible = false;
             this.dataGridView1.Columns["FlagErrorAct������"].Visible = false;

            //�������� �� DataGrid � ��������� ����� ��������� � ����� �����
             decimal sum��������� = 0.0m;

             this.dataGridView1.Columns["Flag�����������������"].Visible = false;

             this.dataGridView1.Columns["�����������������"].Visible = false;
            

             if (this.dataGridView1.Rows.Count != 0)
             {
                 //foreach (DataGridViewRow row in this.dataGridView1.Rows)
                 //{
                 //   //���������� ����� ������� ����������� ��������� (����� �����)
                 //    string s1 = row.Cells["���������"].Value.ToString();
                 //    string sum1 = s1.Replace("�.", " ");

                 //    string summ1 = sum1.Replace(',', '.');
                 //    sum��������� = Math.Round(Math.Round(Math.Round(sum���������, 2) + Math.Round(Convert.ToDecimal(summ1),2)),2);
                 //    //}
                 //}
             }

             //this.labelServ.Text  = "����� ������� ����� " + sum���������.ToString("c");
             this.labelFile.Text = "����� ������� ����������� ��������� " + Math.Round(sum���������, 2).ToString("c") + " ���������� ��������� " + this.���������.Count.ToString() + " ��.";
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["FlagErrorAct������"].Value) == true)
            {
                FormMessageAct form = new FormMessageAct();
                form.��������� = this.dataGridView1.CurrentRow.Cells["�������������������������"].Value.ToString().Trim();
                form.Show();
            }

            //������� ������� 
            this.dataGridView2.DataSource = null;

            //��������� ���������� ������
            this.dataGridView3.DataSource = null;

            //������� ����� ��������
            string ��������� = this.dataGridView1.CurrentRow.Cells["���������"].Value.ToString().Trim();

            this.lblServerLK.Text = "";
            this.lblFileLK.Text = "";

            // ��������� �������� ��������� �� ������� � �� �����.
            PreferentCategory preCat = (PreferentCategory)this.dataGridView1.CurrentRow.Cells["�����������������"].Value;

            // ��������� �������� ���������.
            this.lblServerLK.Text = preCat.PCategoryServer.Trim();
            this.lblFileLK.Text = preCat.PActegoryFile.Trim();

            if (this.���������.ContainsKey(���������))
            {
                //this.iv = �����������������������[numberDog.Trim()];
                DisplayReestr dr = this.���������[���������];

                // ���� �� ������� �������� ��������� ������������� �� ������� � � ��������.
                if (preCat.PCategoryServer.Trim().ToLower() != preCat.PActegoryFile.Trim().ToLower())
                {
                    dr.ErrorPerson = true;
                }

                if (dr.ErrorPerson == true) // || dr.ErrorPerson == false)
                {
                   // MessageBox.Show("�������� ������ � ���������");
                    MyMessage message = new MyMessage();
                    message.ShowDialog();

                    //���������� ��� �������� ������ � ������� � �� ����� ��������
                   

                    if (message.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {

                        //������� id ���������
                        string ������������� = this.dataGridView1.CurrentRow.Cells["�������������"].Value.ToString().Trim();

                        //������� ��� ���������
                        string ��� = this.dataGridView1.CurrentRow.Cells["���_��������"].Value.ToString().Trim();

                        string query�������� = "select * from �������� " +
                                               "where id_�������� in ( " +
                                               "select id_�������� from ������� where ������������� = '" + ������������� + "') ";

                        //������� ������ �� ��������� ������� ��������� � ��� �� �������
                        dtServ = ���������.GetTableSQL(query��������, "��������");

                        //�������
                        string[] ������� = ���.Split(' ');

                        if (�������.Length == 3)
                        {

                            // ������� ������ �� �����.
                            IEnumerable<Unload> un = this.Unloads.Where(u => u.��������.Rows[0]["�������"].ToString().Trim() == �������[0].Trim()
                                &&
                                u.��������.Rows[0]["���"].ToString().Trim() == �������[1].Trim()
                                && u.��������.Rows[0]["��������"].ToString().Trim() == �������[2].Trim()
                                ).Select(u => u).Take(1);

                            if (un != null)
                            {
                                foreach (Unload u in un)
                                {

                                    //list.Add(u);
                                    dtFile = u.��������;

                                    dtFile.Rows[0]["������������"] = Convert.ToDateTime(u.��������.Rows[0]["������������"]).ToLocalTime();
                                }

                                if (un.Count() == 0)
                                {
                                    DataTable dtNull = new DataTable();

                                    dtFile = dtNull;
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                IEnumerable<Unload> un = this.Unloads.Where(u => u.��������.Rows[0]["�������"].ToString().Trim() == �������[0].Trim() &&
                                   u.��������.Rows[0]["���"].ToString().Trim() == �������[0].Trim()).Select(u => u).Take(1);

                                if (un != null)
                                {
                                    foreach (Unload u in un)
                                    {
                                        //list.Add(u);
                                        dtFile = u.��������;

                                        dtFile.Rows[0]["������������"] = Convert.ToDateTime(u.��������.Rows[0]["������������"]).ToLocalTime();
                                    }

                                    if (un.Count() == 0)
                                    {
                                        DataTable dtNull = new DataTable();

                                        dtFile = dtNull;
                                    }

                                }
                            }
                            catch
                            {
                                DataTable dtNull = new DataTable();

                                dtFile = dtNull;
                            }
                        }

                        FormDisplayError ferr = new FormDisplayError();
                        ferr.DtServer = dtServ;
                        ferr.DtFile = dtFile;
                        ferr.LkFile = preCat.PActegoryFile.Trim();
                        ferr.LkServer = preCat.PCategoryServer.Trim();
                        ferr.ShowDialog();
                    }
                    else
                    {
                        message.Close();
                    }
                   
                }

                if (dr.������������ != null)
                {
                    List<ErrorsReestrUnload> listError = dr.������������;

                    //������ ��� �������� ���������� ������
                    List<DateService> listCorrect = new List<DateService>();

                    //������ ��� �������� ��������� ������
                    List<DateService> listErrorD = new List<DateService>();

                    //��������� ������ ������ � ������ �������� ������
                    foreach (ErrorsReestrUnload list in listError)
                    {
                        if (list.������������������ != null)
                        {
                            if (list.������������������.Length != 0)
                            {
                                //������� ���������� ������
                                DateService correct = new DateService();
                                correct.������������ = list.������������������;

                                correct.����� = list.�����.ToString("c");
                                correct.���������� = list.����������.ToString().Trim();

                                correct.���� = list.����.ToString("c");

                                //������� ����� � ���������
                                listCorrect.Add(correct);
                            }
                        }

                        if (list.Error������������������ != null)
                        {
                            if (list.Error������������������.Length != 0)
                            {
                                //������� ��������� ������
                                DateService error = new DateService();
                                error.������������ = list.Error������������������;
                                error.����� = list.Error�����.ToString("c");

                                error.���������� = list.����������������.ToString().Trim();

                                error.���� = list.Error����.ToString("c");
                                listErrorD.Add(error);
                            }
                        }
                    }

                    //��������� ��������� ������
                    this.dataGridView2.DataSource = listErrorD;

                    this.dataGridView2.Columns["������������"].HeaderText = "������������";
                    this.dataGridView2.Columns["������������"].DisplayIndex = 0;
                    
                    this.dataGridView2.Columns["������������"].ReadOnly = true;

                    this.dataGridView2.Columns["����"].HeaderText = "����";
                    this.dataGridView2.Columns["����"].DisplayIndex = 1;
                    
                    this.dataGridView2.Columns["����"].ReadOnly = true;

                    this.dataGridView2.Columns["����������"].HeaderText = "���-��";
                    this.dataGridView2.Columns["����������"].DisplayIndex = 2;
                    this.dataGridView2.Columns["����������"].Width = 50;
                    this.dataGridView2.Columns["����������"].ReadOnly = true;

                    this.dataGridView2.Columns["�����"].HeaderText = "�����";
                    this.dataGridView2.Columns["�����"].DisplayIndex = 3;
                    
                    this.dataGridView2.Columns["�����"].ReadOnly = true;

                    if (listCorrect.Count != 0)
                    {
                        //��������� ���������� ������
                        this.dataGridView3.DataSource = listCorrect;

                        this.dataGridView3.Columns["������������"].HeaderText = "������������";
                        this.dataGridView3.Columns["������������"].DisplayIndex = 0;
                        
                        this.dataGridView3.Columns["������������"].ReadOnly = true;

                        this.dataGridView3.Columns["����"].HeaderText = "����";
                        this.dataGridView3.Columns["����"].DisplayIndex = 1;
                        
                        this.dataGridView3.Columns["����"].ReadOnly = true;

                        this.dataGridView3.Columns["����������"].HeaderText = "���-��";
                        this.dataGridView3.Columns["����������"].DisplayIndex = 2;
                        this.dataGridView3.Columns["����������"].Width = 50;
                        this.dataGridView3.Columns["����������"].ReadOnly = true;

                        this.dataGridView3.Columns["�����"].HeaderText = "�����";
                        this.dataGridView3.Columns["�����"].DisplayIndex = 3;
                        
                        this.dataGridView3.Columns["�����"].ReadOnly = true;
                    }
                }

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //�������� �� DataGridy � ������� � �� ������ �� �������� � ������� ����� ����
            List<DisplayReestrCheck> list = new List<DisplayReestrCheck>();
            List<Unload> unload = this.Unloads; 
          
            //int countContracts = this.���������.Count;- �� ��������
            int countContracts = countAct;
            
            //������� ��������� ��������� ��������� ��������
            int countD = 0;

            //�������� �� DataGridView � ������ ��� �� �������� ������ �������� ���� ��� ������ ��� �� ����� ���������� � ��
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {

                //������ ������� ��������� ������ ��������
                //string ������� = un.�������.Rows[0]["�������������"].ToString().Trim();
                bool fl = Convert.ToBoolean(row.Cells["FlagError"].Value);
                string sumContrControl = row.Cells["Sum"].Value.ToString().Trim();

                if (fl == true && sumContrControl != "0,00�.")
                {
                    countD++;
                }
            }

            //������� ���������� ���������
            if (countContracts == countD)
            {


                //FormReestr formR = new FormReestr();
                //formR.ShowDialog();

                //if (formR.DialogResult == DialogResult.OK)
                //{

                    //������� ����� �������
                string numReestr = this.������������;// formR.������������;

                    //������� ���� �������
                string dateReestr = this.�����������;// formR.�����������;

                    //������� ����� ����-�������
                string num_����������� = this.����������������;// formR.����������������;

                    //������� ���� ���� �������
                string date_����������� = this.���������������;// formR.���������������;

                // �������� ������ ���� ����������� ����� � ������� ������� � ����-������� � ������ ����������.
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    // ��������� ����������.
                    con.Open();

                    // ������� ����������.
                    SqlTransaction transact = con.BeginTransaction();

                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        //��������� ������� ��������
                        CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
                        newCInfo.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = newCInfo;

                        //������� ����� ����
                        string numDog = row.Cells["�������������"].Value.ToString().Trim();


                        //������� ���� ������� ����� � DataGridView 
                        bool flag = Convert.ToBoolean(row.Cells["FlagError"].Value);

                        //������� ������ ��� ������� � ��� ��������
                        foreach (Unload un in this.Unloads)
                        {
                            //����� ����� �������� ������� ������������ ����������� ��� ��������� ��������
                            string ������������� = un.�������.Rows[0]["�������������"].ToString().Trim();

                            if (numDog == ������������� && flag == true)
                            {

                                //������� ������� ������� ����������� ������� �����
                                DataRow rowDog = un.�������.Rows[0];

                                //������� id �������� �������� �������� (������� ������� ��������� ������� � ��)
                                string query = "select top 1 * from dbo.������� " +
                                               "where ������������� = '" + ������������� + "' and ������������ = 'True' order by id_������� desc";

                                DataTable tabID;

                                //�������� �� � ������ ����������
                                tabID = ���������.GetTableSQL(query, "�������", con, transact);

                                //������� id �������� ������� � ��� �� �������  id �������� ���������
                                int id_������� = 0;

                                int id_����������������� = 0;
                                if (tabID.Rows.Count != 0)
                                {
                                    id_������� = Convert.ToInt32(tabID.Rows[0]["id_�������"]);
                                    id_����������������� = Convert.ToInt32(tabID.Rows[0]["id_�����������������"]);
                                }

                                //������� ���� �������� ���������� �� �����.
                                string ������������ = Convert.ToDateTime(rowDog["������������"]).ToShortDateString();

                                // ������� ��� ����������� ����� �� �����.
                                DataTable ������ = un.�������������������;

                                // ������� �� ����� ��������.
                                string �������� = Convert.ToDateTime(������.Rows[0]["��������������"]).ToShortDateString();

                                decimal sumDog = 0.0m;

                                //������� ����� ���� ����������� ����� �� �����.
                                DataTable tab������������� = un.����������������;

                                foreach (DataRow row��� in tab�������������.Rows)
                                {
                                    sumDog = Math.Round(sumDog + Convert.ToDecimal(row���["�����"]), 2);
                                }

                                decimal summ = sumDog;

                                //������� ������ �� ���� ����������� �����
                                DataTable tabAct = un.�������������������;

                                //�������� ���������� �� ��������� ��� ����������� ����� �� �������
                                string countRow = "select COUNT(���������) as '����������' from dbo.������������������� " +
                                                  "where ��������� = '" + tabAct.Rows[0]["���������"].ToString().Trim() + "' ";

                                DataTable tab;
                                //using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                                //{
                                //    con.Open();

                                //    //�������� �� � ������ ����������
                                //    SqlTransaction transact = con.BeginTransaction();
                                    tab = ���������.GetTableSQL(countRow, "���������", con, transact);
                                //}


                                if (Convert.ToInt32(tab.Rows[0]["����������"]) == 0)
                                {

                                    StringBuilder builder = new StringBuilder();

                                    DataRow row����� = un.��������.Rows[0];

                                    //������� ������ � ������� �� ���������
                                    string updateLgot = " UPDATE [��������] " +
                                                        "SET [�������] = '" + row�����["�������"].ToString().Trim() + "' " +
                                                        ",[���] = '" + row�����["���"].ToString().Trim() + "' " +
                                                        ",[��������] = '" + row�����["��������"].ToString().Trim() + "' " +
                                                        ",[������������] = '" + row�����["������������"].ToString().Trim() + "' " +
                                                        ",[�����] = '" + row�����["�����"].ToString().Trim() + "' " +
                                                        ",[���������] = '" + row�����["���������"].ToString().Replace("'",string.Empty).Trim() + "' " +
                                                        ",[������] = '" + row�����["������"].ToString().Trim() + "' " +
                                                        ",[�������������] = '" + row�����["�������������"].ToString().Trim() + "' " +
                                                        ",[�������������] = '" + row�����["�������������"].ToString().Trim() + "' " +
                                                        ",[�������������] = '" + row�����["�������������"].ToString().Trim() + "' " +
                                                        ",[������������������] = '" + row�����["������������������"].ToString().Trim() + "' " +
                                                        ",[���������������] = '" + row�����["���������������"].ToString().Trim() + "' " +
                                                        ",[id_�����������������] = " + id_����������������� + " " +
                                                        ",[id_��������] = " + Convert.ToInt32(row�����["id_��������"]) + " " +
                                                        ",[��������������] = '" + row�����["��������������"].ToString().Trim() + "' " +
                                                        ",[��������������] = '" + row�����["��������������"].ToString().Trim() + "' " +
                                                        ",[�������������������] = '" + row�����["�������������������"].ToString().Trim() + "' " +
                                                        ",[����������������] = '" + row�����["����������������"].ToString().Trim().Replace("'",string.Empty) + "' " +
                                        //",[id_�������] = " + Convert.ToInt32(row�����["id_�������"]) + " " +
                                        //",[id_�����] = " + Convert.ToInt32(row�����["id_�����"]) + " " +
                                        //",[id_��������] = " + Convert.ToInt32(row�����["id_��������"]) + " " +
                                                        "where id_�������� = ( " +
                                                        "select id_�������� from dbo.������� " +
                                                        "where id_������� = " + id_������� + ") ";

                                    builder.Append(updateLgot);

                                    //����� ��������� � ������� �������
                                    string updateQuery = "declare @id int " +
                                           "select @id = id_����������������� from dbo.����������������� " +
                                           "where ����������������� = '" + un.����������������� + "' " +
                                           "UPDATE [�������] " +
                                           //"UPDATE �������Temp " + 
                                           "SET [������������] = '" + ������������.Trim() + "' " +
                                           ",[������������������������] = '" + ��������.Trim() + "' " +
                                           ",������������������������� = " + summ + " " +
                                        //",id_����������������� = @id " + id_�����������������
                                            ",id_����������������� = " + id_����������������� + " " +
                                           ",������������������� = 'True' " +
                                           ",��������������� = 'True' " +
                                        //",������������������ = '"+ DateTime.Today +"' " + // �� ����� ������� ����
                                           ",[������������] = '" + numReestr + "' " +
                                           ",[�����������] = '" + dateReestr + "' " +
                                           ",[�����������������] = '" + num_����������� + "' " +
                                           ",[���������������] = '" + date_����������� + "' " +
                                           ",[logWrite] = '" + MyAplicationIdentity.GetUses() + "' " +
                                           "WHERE id_������� = " + id_������� + " ";

                                    builder.Append(updateQuery);

                                    /*
                                     * �����������, ��� ���� ������������� ��������� ����, ��� ��� ������ ��������
                                     * ������ �� ����������� � ������ ����� ���������� � ������ ����.
                                     * ��� ����� �� ������� ��� ���� ����� ��������� ��� �������� ���� � ���������� ����� ������
                                     * �� �����.
                                     * */

                                    string queryDelete = "delete ���������������� " +
                                                         "where id_������� = " + id_������� + " ";

                                    builder.Append(queryDelete);

                                    //������� ������
                                    int iCount = 0;

                                    //������� ����� ������
                                    foreach (DataRow rowU in un.����������������.Rows)
                                    {
                                        string queryInsert������ = "declare @id�������_" + iCount + " int " +
                                            //"select @id�������_" + iCount + " = id_������� from dbo.������� where ������������� = '" + ������������� + "' " +
                                                  "select @id�������_" + iCount + " = id_������� from dbo.������� where ������������� = '" + ������������� + "' and ������������ = 'True' " +
                                                  //"INSERT INTO ���������������� " +
                                                  "INSERT INTO ���������������� " +
                                                  "([������������������] " +
                                                  ",[����] " +
                                                  ",[����������] " +
                                                  ",[id_�������] " +
                                                  ",[��������������] " +
                                                  ",[�����] " +
                                                  ",[�������]) " +
                                                  "VALUES " +
                                                   "('" + rowU["������������������"].ToString() + "' " +
                                                   "," + Convert.ToDecimal(rowU["����"]) + " " +
                                                   "," + Convert.ToInt32(rowU["����������"]) + " " +
                                                   ",@id�������_" + iCount + " " +
                                                   ",'" + rowU["��������������"].ToString() + "' " +
                                                   "," + Convert.ToDecimal(rowU["�����"]) + " " +
                                                   "," + Convert.ToInt32(rowU["����������"]) + " ) ";

                                        builder.Append(queryInsert������);

                                        iCount++;
                                    }


                                    //try
                                    //{

                                        string insertAct = "INSERT INTO [�������������������] " +
                                                           "([���������] " +
                                                           ",[id_�������] " +
                                                           ",[��������������] " +
                                                           ",[��������������] " +
                                                           ",[��������������] " +
                                                           ",[������������������] " +
                                                           ",[����] " +
                                                           ",[����������] " +
                                                           ",[�����] " +
                                                           ",[�����������������] " +
                                                           ",[������������] " +
                                                           ",[�����������] " +
                                                           ",[����������������] " +
                                                           ",[���������������] " +
                                                           ",[����������] ) " +
                                                           "VALUES " +
                                                           "('" + tabAct.Rows[0]["���������"].ToString().Trim() + "' " +
                                                           "," + id_������� + " " +
                                                           ",'" + tabAct.Rows[0]["��������������"].ToString().Trim() + "' " +
                                                           ",'" + ��������.Trim() + "' " +
                                                           ",'" + tabAct.Rows[0]["��������������"].ToString().Trim() + "' " +
                                                           ",'" + tabAct.Rows[0]["������������������"].ToString().Trim() + "' " +
                                                           ",'" + tabAct.Rows[0]["����"].ToString().Trim() + "' " +
                                                           "," + Convert.ToInt32(tabAct.Rows[0]["����������"]) + " " +
                                                           ",'" + tabAct.Rows[0]["�����"].ToString().Trim() + "' " +
                                                           ",'" + tabAct.Rows[0]["�����������������"].ToString().Trim() + "' " +
                                                           ", '" + this.������������.ToString().Trim() + "' " +
                                                           ", '" + this.����������� + "' " +
                                                           ",'" + this.����������������.ToString().Trim() + "' " +
                                                           ", '" + this.��������������� + "' " +
                                                           ", '' ) ";


                                        builder.Append(insertAct);

                                        //�������� ������ � ������ ����������
                                        //ExecuteQuery.Execute(builder.ToString());

                                        ExecuteQuery.Execute(builder.ToString().Trim(), con, transact);
                                        
                                        break;

                                    //}
                                    //catch
                                    //{
                                    //    MessageBox.Show("��������� �� ��� ����");
                                    //    return;
                                    //}
                                }
                                else
                                {
                                    //MessageBox.Show("��� � " + tabAct.Rows[0]["���������"].ToString().Trim() + " ��� ������� � ���� ������");
                                }


                            }
                        }
                    }

                    // �������� ����������
                    transact.Commit();

                    

                }

                    if (this.������������ != "" && this.���������������� != "" && this.����������� != "" && this.��������������� != "")
                    {
                        this.btnSlRead.Enabled = true;
                        this.btnReturnReestr.Enabled = true;
                    }

                MessageBox.Show("�������� �������");

                //������� �����
                this.Close();
               // }

                //if (formR.DialogResult == DialogResult.No)
                //{
                //    formR.Close();
                //}
            }
            else
            {
                MessageBox.Show("������ �� ������ ��������");
            }
        }

        private void btnSlRead_Click(object sender, EventArgs e)
        {
            //��������� ������� ��������
            CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCInfo;


            //������� ������������ ����������������� ������������
            DataTable tabHosp = this.Unloads[0].������������;
            string hosp = tabHosp.Rows[0]["������������������������"].ToString().Trim();

            //������� ������������ �������� ���������
            string �������������� = this.Unloads[0].�����������������.Trim();

            DataTable tab���������;

            //������� �������� ���������� ������ �������������
            string query��������� = "select ���������,�������,�������� from ���������������������� " +
                                    "where id_��������� = (select id_��������� from dbo.����������������) ";

            using(SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                SqlTransaction transact= con.BeginTransaction();
                
                tab��������� = ���������.GetTableSQL(query���������,"���������",con,transact);

            }

            //try
            //{

                //������� ��������� ���������� ������
                string dolzhnost = tab���������.Rows[0]["���������"].ToString().Trim();

                //������� ��� ���������� ������
                string familija = tab���������.Rows[0]["�������"].ToString().Trim();

                //������� ��������
                string inicialy = tab���������.Rows[0]["��������"].ToString().Trim();


                //������� ���������
                int iCountContr = 0;

                decimal mani = 0.0m;

                //���������� ���������� ���������
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["FlagError"].Value) == true)
                    {
                        iCountContr++;

                        //���������� ����� ������� ����������� ��������� (����� �����)
                        string s1 = row.Cells["���������"].Value.ToString();
                        string sum1 = s1.Replace("�.", " ");

                        string summ1 = sum1.Replace(',', '.');
                        mani = Math.Round(Math.Round(mani, 2) + Math.Round(Convert.ToDecimal(summ1), 2), 2);
                    }
                }

                //��� ����� �������� ��� ���������� �������������-�������������� �����������


                //����������� ��������� �������
                string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ ��������� ������� �� ������.doc";


                //������ ����� Word.Application
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                //��������� ��������
                Microsoft.Office.Interop.Word.Document doc = null;

                object fileName = filName;
                object falseValue = false;
                object trueValue = true;
                object missing = Type.Missing;

                doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing);

                ////������� ���
                //object wdrepl2 = WdReplace.wdReplaceAll;
                ////object searchtxt = "GreetingLine";
                //object searchtxt2 = "fio";
                //object newtxt2 = (object)fio;
                ////object frwd = true;
                //object frwd2 = false;
                //doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                //ref missing, ref missing);

                //������� ���
                object wdrepl2 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt2 = "������������";
                object newtxt2 = (object)hosp;
                //object frwd = true;
                object frwd2 = false;
                doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                ref missing, ref missing);

                object wdrepl3 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt3 = "��������������";
                object newtxt3 = (object)��������������;
                //object frwd = true;
                object frwd3 = false;
                doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
                ref missing, ref missing);

                object wdrepl4 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt4 = "����������";
                object newtxt4 = (object)iCountContr;
                //object frwd = true;
                object frwd4 = false;
                doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
                ref missing, ref missing);

                object wdrepl5 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt5 = "mani";
                object newtxt5 = (object)mani;
                //object frwd = true;
                object frwd5 = false;
                doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
                ref missing, ref missing);

                object wdrepl6 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt6 = "familija";
                object newtxt6 = (object)familija;
                //object frwd = true;
                object frwd6 = false;
                doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
                ref missing, ref missing);

                object wdrepl7 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt7 = "dolzhnost";
                object newtxt7 = (object)dolzhnost;
                //object frwd = true;
                object frwd7 = false;
                doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
                ref missing, ref missing);

                object wdrepl8 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt8 = "inicialy";
                object newtxt8 = (object)inicialy;
                //object frwd = true;
                object frwd8 = false;
                doc.Content.Find.Execute(ref searchtxt8, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd8, ref missing, ref missing, ref newtxt8, ref wdrepl8, ref missing, ref missing,
                ref missing, ref missing);


                object wdrepl9 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt9 = "num";
                object newtxt9 = (object)this.����������������.Trim();
                //object frwd = true;
                object frwd9 = false;
                doc.Content.Find.Execute(ref searchtxt9, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd9, ref missing, ref missing, ref newtxt9, ref wdrepl9, ref missing, ref missing,
                ref missing, ref missing);


                object wdrepl10 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt10 = "date";
                object newtxt10 = (object)this.���������������.Trim();
                //object frwd = true;
                object frwd10 = false;
                doc.Content.Find.Execute(ref searchtxt10, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd10, ref missing, ref missing, ref newtxt10, ref wdrepl10, ref missing, ref missing,
                ref missing, ref missing);

                //��������� ��������
                app.Visible = true;
            //}
            //catch (IndexOutOfRangeException i)
            //{
            //    MessageBox.Show("�������� �� �� ������� ���������� �������������� ������");
            //}

        }

        private void btnReturnReestr_Click(object sender, EventArgs e)
        {
            //�������� ����� ������ ����� �����
            Dictionary<string, DisplayReestr> list��� = this.���������;

            List<string> ����������� = new List<string>();
            //������� �� ���������� ����
            //���������� ���������� ���������
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["FlagError"].Value) == false && (Convert.ToBoolean(row.Cells["FlagAddContract"].Value) == false))
                {
                    �����������.Add(row.Cells["���������"].Value.ToString().Trim());
                }
                else
                {
                    //������ ����� ����
                    string keyAkt = row.Cells["���������"].Value.ToString();
                    list���.Remove(keyAkt);
                }
            }

            //������ ������ ���������� �� ���� ����� � ������� �������
            List<DisplayReestr> listReestr = new List<DisplayReestr>();

            //�������� �� ����� � ������� � ��� ������ ������� ��� ����� ����������
            //foreach (string drKey in this.���������.Keys)
            foreach (string drKey in list���.Keys)
            {
                foreach (string sNUm in �����������)
                {
                    if (drKey == sNUm)
                    {
                        DisplayReestr dr = this.���������[drKey];
                        //dr.��������� = drKey;
                        listReestr.Add(dr);
                    }
                }
            }

            List<DisplayReestr> iListReestr = listReestr;

            //������ ����� � ��� ������������
            DataRow row������������ = this.Unloads[0].������������.Rows[0];
            string ��� = row������������["���"].ToString().Trim();

            string queryHosp = "SELECT [F2],[F3],[���] " +
                               "FROM ����1$ " +
                               "where id in ( " +
                               "SELECT [id_������������] " +
                               "FROM ������������ " +
                               "where ��� = '" + ��� + "' )";
            
            string ���� = string.Empty;
            string obrashhenie = string.Empty;
            string esculap = string.Empty;

            SqlConnection con = new SqlConnection(ConnectDB.ConnectionString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(queryHosp, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "����1$");

            //DataTable tab��������� = ���������.GetTableSQL(queryHosp, "����", con, transact);
            ���� = ds.Tables["����1$"].Rows[0]["F2"].ToString().Trim();// +" " + ds.Tables["����1$"].Rows[0]["���"].ToString().Trim();
            obrashhenie = ds.Tables["����1$"].Rows[0]["F3"].ToString().Trim();
            esculap = ds.Tables["����1$"].Rows[0]["���"].ToString().Trim();

            //���������� ���������� ��������� ���������� � �������
            //������� ���������
            int iCountContr = 0;

            //���������� ���������� ���������
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["FlagError"].Value) == false)
                {
                    iCountContr++;
                }
            }

            //������� �� � WORD
            //����������� ��������� �������
            string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ �� �������.doc";


            //������ ����� Word.Application
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            //��������� ��������
            Microsoft.Office.Interop.Word.Document doc = null;

            object fileName = filName;
            object falseValue = false;
            object trueValue = true;
            object missing = Type.Missing;

            doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing);

            //������� ������� ������
            object wdrepl2 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt2 = "����";
            object newtxt2 = (object)����;
            //object frwd = true;
            object frwd2 = false;
            doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            ref missing, ref missing);

            //������� ��������� ��������� ���� ��������!
            object wdrepl3 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt3 = "obrashhenie";
            object newtxt3 = (object)obrashhenie;
            //object frwd = true;
            object frwd3 = false;
            doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            ref missing, ref missing);


            //������� ���������� ���������
            object wdrepl4 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt4 = "icountcontr";
            object newtxt4 = (object)iCountContr;
            //object frwd = true;
            object frwd4 = false;
            doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
            ref missing, ref missing);

            object wdrepl5 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt5 = "num";
            object newtxt5 = (object)this.����������������;
            //object frwd = true;
            object frwd5 = false;
            doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
            ref missing, ref missing);


            object wdrepl6 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt6 = "date";
            object newtxt6 = (object)this.�����������;
            //object frwd = true;
            object frwd6 = false;
            doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
            ref missing, ref missing);

            //esculap
            object wdrepl7 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt7 = "esculap";
            object newtxt7 = (object)esculap;
            //object frwd = true;
            object frwd7 = false;
            doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
            ref missing, ref missing);


            object bookNaziv = "�������";
            Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 3, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 6;
            table.Columns[1].Width = 180;
            table.Columns[2].Width = 180;
            table.Columns[3].Width = 180;
            table.Borders.Enable = 1; // ����� - �������� �����
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 12;
             //table.Rows[1].Alignment = WdRowAlignment.wdAlignRowCenter;
            table.Rows.First.Alignment = WdRowAlignment.wdAlignRowCenter;
            
            //������� �����
            int i = 1;

            //������ ��� �������� ������ ��� �������
            List<ReestrNoPrintDog> listItem = new List<ReestrNoPrintDog>();

            //������ ������ ������ ��� ��������
            List<TabLetter> list = new List<TabLetter>();

            //���������� ����� �������
            TabLetter ����� = new TabLetter();
            �����.��� = "��� ���������";
            �����.������������� = "������ ��������";
            �����.��������� = "��� ����������� �����";

            list.Add(�����);

  

            //���������� ������ ��� �������
            foreach (DisplayReestr item in listReestr)
            {
                int iCount = 0;

                bool flagError = false;
                if (item.ErrorPerson == true)
                {
                    flagError = true;
                }

                //������� ������ ������
                List<ErrorsReestrUnload> unRest = item.������������;
                foreach (ErrorsReestrUnload eru in unRest)
                {
                    //������ �������� ����������� � �������� � �����
                    TabLetter tLet = new TabLetter();

                    //������� ��� ���������
                    ReestrControl �������� = item.��������;
                    string ��� = ��������.���;
                    //�������� ��� ��������� ������ ��� ������ �������� �� ������ ������
                    if (iCount == 0)
                    {
                        tLet.��� = ���;

                        string ������ = string.Empty;
                        string ���������� = string.Empty;
                        string ����� = string.Empty;

                        //���������� ������ �� �������� ��� ������ � ���� ������
                        if (eru.������������������ != null)
                        {
                            ������ = eru.������������������.ToString().Trim();
                        }
                        else
                        {
                            ������ = "";
                        }

                        if (eru.���������� != null)
                        {
                            ���������� = eru.����������.ToString().Trim();
                        }

                        if (eru.����� != null)
                        {
                            ����� = eru.�����.ToString("c").Trim();
                            if (����� == "0,00�.")
                            {
                                //����� = "_______";
                                ����� = "";
                            }
                        }
                        else
                        {
                            ����� = "";
                        }


                        string ������������� = ������ + " " + �����;

                        //������� ������ �� ��������
                        tLet.������������� = �������������.Trim();

                        string ������2 = string.Empty;
                        string ����������2 = string.Empty;
                        string �����2 = string.Empty;

                        //���������� ������ �� ���� ��� ������
                        if (eru.Error������������������ != null)
                        {
                            ������2 = eru.Error������������������.ToString().Trim();

                        }
                        else
                        {
                            ������2 = "";
                        }

                        if (eru.���������������� != null)
                        {
                            ����������2 = eru.����������������.ToString().Trim();
                        }

                        if (eru.Error����� != null)
                        {
                            �����2 = eru.Error�����.ToString("c").Trim();
                            if (�����2 == "0,00�.")
                            {
                                //�����2 = "_______";
                                �����2 = "";
                            }
                        }
                        else
                        {
                            �����2 = "";
                        }
                        
                        //string �������������2 = ������2 + " " + ����������2 + " " + �����2;
                        string �������������2 = ������2 + " " + �����2;

                        //���� ������ � ������������ ������
                        if (item.ErrorPerson == true)
                        {
                            �������������2 += "�������� ������ �� ���������";
                        }
                                

                        tLet.��������� = �������������2;
                    }
                    else
                    {
                        //� ��� ���������� ������ ������
                        tLet.��� = "";

                        string ������ = string.Empty;
                        string ���������� = string.Empty;
                        string ����� = string.Empty;

                        //���������� ������ �� �������� ��� ������ � ���� ������
                        if (eru.������������������ != null)
                        {
                            ������ = eru.������������������.ToString().Trim();
                        }
                        else
                        {
                            ������ = "";
                        }

                        if (eru.���������� != null)
                        {
                            ���������� = eru.����������.ToString().Trim();
                        }

                        if (eru.����� != null)
                        {
                            ����� = eru.�����.ToString("c").Trim();
                            if (����� == "0,00�.")
                            {
                                //����� = "_______";
                                ����� = "";
                            }

                        }
                        else
                        {
                            ����� = "";
                        }


                        string ������������� = ������ + " " + �����;

                        //������� ������ �� ��������
                        tLet.������������� = �������������.Trim();

                        string ������2 = string.Empty;
                        string ����������2 = string.Empty;
                        string �����2 = string.Empty;

                        //���������� ������ �� ���� ��� ������
                        if (eru.Error������������������ != null)
                        {
                            ������2 = eru.Error������������������.ToString().Trim();
                        }
                        else
                        {
                            ������2 = "";
                        }

                        if (eru.���������������� != null)
                        {
                            ����������2 = eru.����������������.ToString().Trim();
                        }


                        if (eru.Error����� != null)
                        {
                            �����2 = eru.Error�����.ToString("c").Trim();
                            if (�����2 == "0,00�.")
                            {
                                �����2 = "";
                            }
                        }
                        else
                        {
                            �����2 = "";
                        }

                        //string �������������2 = ������2 + " " + ����������2 + " " + �����2;
                        string �������������2 = ������2 + " " + �����2;
                        tLet.��������� = �������������2;
                    }

                    list.Add(tLet);

                    iCount++;
                }
            }

            List<TabLetter> iTest = list;

            int k = 1;

            //������� ������ � �������
            foreach (TabLetter item in list)
            {
                table.Cell(k, 1).Range.Text = item.���.Trim();
                table.Cell(k, 2).Range.Text = item.�������������.Trim();
                table.Cell(k, 3).Range.Text = item.���������.Trim();


                //doc.Words.Count.ToString();
                Object beforeRow1 = Type.Missing;
                table.Rows.Add(ref beforeRow1);

                k++;
            }

            table.Rows[k].Delete();

            //�������� ������ ������ �� ������
            //table.Rows[1].Alignment = WdRowAlignment.wdAlignRowCenter;

            //��������� ��������
            app.Visible = true;

        }

        private void numReestr_Click(object sender, EventArgs e)
        {
                FormReestr formR = new FormReestr();
                formR.ShowDialog();

                if (formR.DialogResult == DialogResult.OK)
                {
                    this.���������������� = formR.����������������.Trim();
                    this.������������ = formR.������������.Trim();
                    this.����������� = formR.�����������.Trim();
                    this.��������������� = formR.���������������.Trim();

                    this.btnSlRead.Enabled = true;
                    this.btnReturnReestr.Enabled = true;
                    this.btnSave.Enabled = true;
                }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Test Message");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder buildNumberContract = new StringBuilder();

            // ������ ���������� ����� � �������.
            int iCountRow = this.dataGridView1.Rows.Count;

            // �������.
            int iCount = 0;

            // ���������� ��� �������� ������� ����������.
            Dictionary<string, ItemLetSumDataAct> dNumContract = new Dictionary<string, ItemLetSumDataAct>();

            // �������� ������ � SQL ���������.
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                iCount++;


                if (iCount < iCountRow)
                {
                    // ������� � ������� ��������� � ������� ��������, ������ ������������� �������.
                    string sItem = "LOWER(LTRIM(RTRIM('" + row.Cells["�������������"].Value.ToString().Trim() + "'))),";

                    buildNumberContract.Append(sItem);
                }
                else
                {
                    // ��������� �������� ����� �������� � ������������ ��� �������.
                    string sItem = "LOWER(LTRIM(RTRIM('" + row.Cells["�������������"].Value.ToString().Trim() + "')))";

                    buildNumberContract.Append(sItem);
                }

                // ������� ����� ���������.
                string numContract = row.Cells["�������������"].Value.ToString().Trim();

                var sumActString = row.Cells["���������"].Value.ToString().Replace(",",".").Replace("�",string.Empty).Replace("P",string.Empty).Replace("�",string.Empty).Replace("p",string.Empty);

                // ������ ��������� ����� � ������ � ������ ����.
                int length = sumActString.Length;

                var summaActStr = sumActString.Substring(0, length - 1);

                // ������� ����� ����.
                decimal sumAct = Convert.ToDecimal(summaActStr);

                // ��������������� ����� ����������� ����� ���� ����������� ����� � ���� ���������� ����.
                ItemLetSumDataAct sumDataAct = new ItemLetSumDataAct();

                // ����� ����.
                sumDataAct.SummAct = sumAct;

                // ���� ���������� ���� ����������� �����.
                sumDataAct.DateAct = row.Cells["��������"].Value.ToString().Trim();

                // ������� ���� � ����� ���� � �����������.
                dNumContract.Add(numContract, sumDataAct);
                
            }

            // ������ ��� �������� ������ � ������.
            //List<ItemLetterToMinistr> listLetter = new List<ItemLetterToMinistr>();

            ContractsForLetter contractLetter = new ContractsForLetter();
            List<ItemLetterToMinistr> listContract = contractLetter.GetPersons(buildNumberContract.ToString());

            ControlDantist.ValidPersonRegion.Region region = new ValidPersonRegion.Region();
            region.GetRegions(listContract);

            var listPerson = listContract;

            string iTest = "";

            //��������� �� ���������� � ��������� ����� ���� � ������ ���������.
            foreach (var sum in dNumContract)
            {
                var item = listContract.Where(w => w.�������������.ToLower().Trim() == sum.Key.ToLower().Trim()).FirstOrDefault();

                if (item != null)
                {

                    item.��������� = sum.Value.SummAct;

                    item.�������� = sum.Value.DateAct.Trim();
                }
            }

            if (Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\��������� VipNet") == false)
            {
                // �������� �����.
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\��������� VipNet");
            }
            else
            {

                DirectoryInfo dirInfo = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\��������� VipNet");

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
            }
                

                DirectoryInfo dirInfoVip = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\��������� VipNet");

                foreach (FileInfo file in dirInfoVip.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch
                    {
                        MessageBox.Show("�� ���� �������� ������ � ����� - " + file.Name);

                        return;
                    }
                }

           // ����������� ������ �� �������.
            var group = listContract.GroupBy(w => w.������������);

            // ������� ������.
            int rCount = 1;

            //������� ���������� �� ������.
            //ClearDirecory();

            // ��������� �� ������� � �������� ��������� ���� VipNet � ���� Excel ��� ������ ������.
            foreach (var listContractFile in group)
            {

                // ���������� ��� �������� ����� ������ �������.
                string nameRegion = string.Empty;

                // ������� ������ ������ �� �������.
                var rtest = listContractFile.Count();

                // ������� �������� �������������
                var filePath = listContractFile.First().������������;

                // �������� ��� ����������� ������������ ����������������� ������ VipNet.
                if(listContractFile.First().IdRegion !=null)
                {
                    int idRegion = (int)listContractFile.First().IdRegion;

                    string NameFile = filePath + ".xls";

                    // ���������� ��������� ���� VipNet.
                    VipNetLetter vipNetLett = new VipNetLetter(idRegion, NameFile);
                    vipNetLett.CreateLetter(out nameRegion);
                }
                else
                {
                    MessageBox.Show(listContractFile.First().������������ + "�� ������� id ������ � ��");
                }


                if (nameRegion.Trim() == "".Trim())
                {
                    nameRegion = "������ ������ " + rCount.ToString();
                }
                
                ////string fileName = @"c:\������ � ������������\" + filePath + ".xls";
                string fileName = System.Windows.Forms.Application.StartupPath + @"\��������� VipNet\" + nameRegion + ".xls";

                GenerateExcelFileLetter file = new GenerateExcelFileLetter(fileName);
                file.GenerateFile(listContractFile.ToList());

                rCount++;
            }

            MessageBox.Show("������ VipNet ���������");
           
        }

        /// <summary>
        /// ������� ���������� �� VipNet ����������
        /// </summary>
        private void ClearDirecory()
        {
            string nameDir = "\\��������� VipNet\\";
            string nsmeDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + nameDir;

            DirectoryInfo dirInfo = new DirectoryInfo(nsmeDirectory);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //if (Convert.ToInt32(this.dataGridView1.Columns["�������������"]) > 0)
            //{

            //}

            if (Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["FlagErrorAct������"].Value) == true)
            {
                FormMessageAct form = new FormMessageAct();
                form.��������� = this.dataGridView1.CurrentRow.Cells["�������������������������"].Value.ToString().Trim();
                form.Show();
            }

        }


    }
}