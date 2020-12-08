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
                DisplayReestrCheck drck = new DisplayReestrCheck();

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

            //�������� �� DataGrid � ��������� ����� ��������� � ����� �����
             decimal sum��������� = 0.0m;

            

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
             this.labelFile.Text = "����� ������� ����������� ��������� " + Math.Round(sum���������,2).ToString("c");
            


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //������� ������� 
            this.dataGridView2.DataSource = null;

            //��������� ���������� ������
            this.dataGridView3.DataSource = null;

            //������� ����� ��������
            string ��������� = this.dataGridView1.CurrentRow.Cells["���������"].Value.ToString().Trim();

            if (this.���������.ContainsKey(���������))
            {
                //this.iv = �����������������������[numberDog.Trim()];
                DisplayReestr dr = this.���������[���������];

                if (dr.ErrorPerson == true)
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
                        

                        //������� ������ �� �����
                        //IEnumerable<Unload> un = this.Unloads.Where(u => u.��������.Rows[0]["�������"] == "���������").Select(u=>u);
                        IEnumerable<Unload> un = this.Unloads.Where(u => u.��������.Rows[0]["�������"].ToString() == �������[0].Trim()).Select(u => u);

                        //List<Unload> list = new List<Unload>();
                        foreach (Unload u in un)
                        {
                            //list.Add(u);
                            dtFile = u.��������;
                        }

                        FormDisplayError ferr = new FormDisplayError();
                        ferr.DtServer = dtServ;
                        ferr.DtFile = dtFile;
                        ferr.ShowDialog();
                    }
                    else
                    {
                        message.Close();
                    }

                    //DataTable adtServ = dtServ;
                    //DataTable adtFile = dtFile;

                    //FormDisplayError ferr = new FormDisplayError();
                    //ferr.DtServer = dtServ;
                    //ferr.DtFile = dtFile;
                    //ferr.ShowDialog();
                    
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
                    //this.dataGridView2.Columns["����������"].Width = 100;
                    this.dataGridView2.Columns["������������"].ReadOnly = true;

                    this.dataGridView2.Columns["����"].HeaderText = "����";
                    this.dataGridView2.Columns["����"].DisplayIndex = 1;
                    //this.dataGridView2.Columns["����������"].Width = 100;
                    this.dataGridView2.Columns["����"].ReadOnly = true;

                    this.dataGridView2.Columns["����������"].HeaderText = "���-��";
                    this.dataGridView2.Columns["����������"].DisplayIndex = 2;
                    this.dataGridView2.Columns["����������"].Width = 50;
                    this.dataGridView2.Columns["����������"].ReadOnly = true;

                    this.dataGridView2.Columns["�����"].HeaderText = "�����";
                    this.dataGridView2.Columns["�����"].DisplayIndex = 3;
                    //this.dataGridView2.Columns["����������"].Width = 100;
                    this.dataGridView2.Columns["�����"].ReadOnly = true;



                    if (listCorrect.Count != 0)
                    {
                        //��������� ���������� ������
                        this.dataGridView3.DataSource = listCorrect;

                        this.dataGridView3.Columns["������������"].HeaderText = "������������";
                        this.dataGridView3.Columns["������������"].DisplayIndex = 0;
                        //this.dataGridView2.Columns["����������"].Width = 100;
                        this.dataGridView3.Columns["������������"].ReadOnly = true;

                        this.dataGridView3.Columns["����"].HeaderText = "����";
                        this.dataGridView3.Columns["����"].DisplayIndex = 1;
                        //this.dataGridView3.Columns["����������"].Width = 100;
                        this.dataGridView3.Columns["����"].ReadOnly = true;

                        this.dataGridView3.Columns["����������"].HeaderText = "���-��";
                        this.dataGridView3.Columns["����������"].DisplayIndex = 2;
                        this.dataGridView3.Columns["����������"].Width = 50;
                        this.dataGridView3.Columns["����������"].ReadOnly = true;

                        this.dataGridView3.Columns["�����"].HeaderText = "�����";
                        this.dataGridView3.Columns["�����"].DisplayIndex = 3;
                        //this.dataGridView2.Columns["����������"].Width = 100;
                        this.dataGridView3.Columns["�����"].ReadOnly = true;
                    }
                }


            }


            //������� ������ � ������� ����
            //DisplayReestr dr =  this.���������[�������������]
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

            string logTest = MyAplicationIdentity.GetUses();
          

             // �������� ��� �� �������� � ������� ������ ��������



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

                        //row.Cells["���������"].Value
                        //row.Cells["�������������"].Value
                        //DisplayReestr act = this.���������[keyAct];

                        //������� ������ ��� ������� � ��� ��������
                        foreach (Unload un in this.Unloads)
                        {
                            //����� ����� �������� ������� ������������ ����������� ��� ��������� ��������
                            string ������������� = un.�������.Rows[0]["�������������"].ToString().Trim();
                            if (numDog == ������������� && flag == true)
                            {
                               
                                //������� ������� ������� ����������� ������� �����
                                DataRow rowDog = un.�������.Rows[0];

                                //������� id �������� �������� ��������
                                string query = "select * from dbo.������� " +
                                               "where ������������� = '" + ������������� + "' and ������������ = 'True' ";

                                DataTable tabID;

                                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                                {
                                    con.Open();

                                    //�������� �� � ������ ����������
                                    SqlTransaction transact = con.BeginTransaction();
                                    tabID = ���������.GetTableSQL(query, "�������", con, transact);
                                }

                                //������� id �������� ������� � ��� �� �������  id �������� ���������
                                int id_������� = 0;

                                int id_����������������� = 0;
                                if (tabID.Rows.Count != 0)
                                {
                                    id_������� = Convert.ToInt32(tabID.Rows[0]["id_�������"]);
                                    id_����������������� = Convert.ToInt32(tabID.Rows[0]["id_�����������������"]);
                                }

                                //������� ���� ��������
                                string ������������ = Convert.ToDateTime(rowDog["������������"]).ToShortDateString();

                                DataTable ������ = un.�������������������;
                                string �������� = Convert.ToDateTime(������.Rows[0]["��������������"]).ToShortDateString();

                                decimal sumDog = 0.0m;

                                //������� ����� ���� ����������� �����
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
                                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                                {
                                    con.Open();

                                    //�������� �� � ������ ����������
                                    SqlTransaction transact = con.BeginTransaction();
                                    tab = ���������.GetTableSQL(countRow, "���������", con, transact);
                                }

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
                                                        ",[���������] = '" + row�����["���������"].ToString().Trim() + "' " +
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
                                                        ",[����������������] = '" + row�����["����������������"].ToString().Trim() + "' " +
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
                                    foreach(DataRow rowU in un.����������������.Rows)
                                    {
                                        string queryInsert������ = "declare @id�������_" + iCount + " int " +
                                                  //"select @id�������_" + iCount + " = id_������� from dbo.������� where ������������� = '" + ������������� + "' " +
                                                  "select @id�������_" + iCount + " = id_������� from dbo.������� where ������������� = '" + ������������� + "' and ������������ = 'True' " +
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


                                    try
                                    {

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
                                        ExecuteQuery.Execute(builder.ToString());
                                        break;

                                    }
                                    catch
                                    {
                                        MessageBox.Show("��������� �� ��� ����");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("��� � " + tabAct.Rows[0]["���������"].ToString().Trim() + " ��� ������� � ���� ������");
                                }

                                //}

                                //������� �� ������ ������ �� ���� ����������� �����

                            }
                        }
                    }

                    if (this.������������ != "" && this.���������������� != "" && this.����������� != "" && this.��������������� != "")
                    {
                        this.btnSlRead.Enabled = true;
                        this.btnReturnReestr.Enabled = true;
                    }

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
    }
}