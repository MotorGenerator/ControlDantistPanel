using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using Microsoft.Office.Interop.Word;
using System.Runtime.Serialization.Formatters.Binary;

using ControlDantist.Classes;
using DantistLibrary;
using ControlDantist.WriteClassDB;

namespace ControlDantist
{
    public partial class FormValidOut : Form
    {
        private Dictionary<string, ValidateContract> validReestr;
        private Dictionary<string, Unload> unload;
        
        //��������� ������ ��������� ������� ��������� ��� ���������� � ��
        private Dictionary<string,Unload> listSave;

        //��������� ������ ������� �������� ������� �� ������� (�� ������ ��������)
        private Dictionary<string, Unload> listPrintNoSave;

        //��������� ������ ������� ��������� ������� ���������
        private Dictionary<string, Unload> listPrintSave;

        // ��������� ��� �������� ������ ��� ����������.
        private Dictionary<string, Unload> listPrintStatistic;

        //��������� ������ ������� ��������� ����������� � �������������� ��������
        private Dictionary<string, Unload> listPrintAddCheck;
        
        //������ ����� ���������
        string �������� = string.Empty;

        //������ ����� ���������
        string �������������� = string.Empty;

        //������ ���� ������ ���������
        string ���������� = string.Empty;

        //���� ������ ��������� ������� 
        private bool setupTime = false;

        private bool writBD;
        private Unload iv;

        private bool ���������������;

        private List<ErrorReestr> listControl;

        public List<ErrorReestr> ���������������
        {
            get
            {
                return listControl;
            }
            set
            {
                listControl = value;
            }
        }



        /// <summary>
        /// ��������� ������������� ��� ����� ����� ���������� ������ ����������� ���������
        /// </summary>
        public bool ��������������������������
        {
            get
            {
                return ���������������;
            }
            set
            {
                ��������������� = value;
            }
        }

        /// <summary>
        ///��������� ��� ��������� ��� ����
        /// </summary>
        public bool ������������
        {
            get
            {
                return writBD;
            }
            set
            {
                writBD = value;
            }
        }

        //���� �������������
        //private bool flagWrite;

        ///// <summary>
        ///// �������� ������ ���� ����������� ��� ����� ������ �������� �� ������ ���� ���������
        ///// </summary>
        //public bool FlagWrite
        //{
        //    get
        //    {
        //        return flagWrite;
        //    }
        //    set
        //    {
        //        flagWrite = value;
        //    }
        //}

        /// <summary>
        /// ������ � ����������� �������� �������� ���������
        /// </summary>
        public Dictionary<string, ValidateContract> ����������������
        {
            get
            {
                return validReestr;
            }
            set
            {
                validReestr = value;
            }
        }


        /// <summary>
        /// ������ ����������� �������� ��������� ���������� ��������
        /// </summary>
        public Dictionary<string, Unload> �����������������������
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




        public FormValidOut()
        {
            InitializeComponent();
        }

        private void FormValidOut_Load(object sender, EventArgs e)
        {
            if (this.�������������������������� == false)
            {
                //��������� ���� ������ ���� � false
                setupTime = false;

                // �������� ������� ��������� ������� ������ ��������.
                Dictionary<string, ValidateContract> validUnload = this.����������������;

                //�������� ������ ��� ����������� ����������� ��������
                List<ContractDisplay> list = new List<ContractDisplay>();

                //��������� ������ ��������� ��� ���������� � �� ������� ���������
                listSave = new Dictionary<string, Unload>();

                //��������� ������ �������� ��������� ��������
                listPrintSave = new Dictionary<string, Unload>();

                //��������� ������ �������� �� ��������� ��������
                listPrintNoSave = new Dictionary<string, Unload>();

                //��������� ������ ������� ��������� ����������� � �������������� ��������
                listPrintAddCheck = new Dictionary<string, Unload>();

                //���������� ������ ����� �������� ��������� � ������� ������� (������ ���������)
                decimal sumCount = 0.0m;

                // ��������� �� ��������� � ��������� ��������� ������� ������ ��������.
                foreach (string keyVK in validUnload.Keys)
                {
                    ContractDisplay contract = new ContractDisplay();
                    contract.������������� = keyVK.Trim();

                   //// ������� ID ������ � ����� ���������.
                   // if (this.����������������.ContainsKey(keyVK.Trim()))
                   // {
                   //     // ������� id ������ �������.
                   //     contract.IdRegion = this.����������������[keyVK.Trim()].IdRegionEsrn;

                   //     // ������� ����� ���������.
                   //     contract.Sinls = this.����������������[keyVK.Trim()].SnilsPerson;
                   // }


                    //����� �����
                    if (�����������������������.ContainsKey(keyVK.Trim()))
                    {
                        iv = �����������������������[keyVK.Trim()];
                    }

                    //������� ��� ������� ���� ������ ���������
                    //Unload iv = �����������������������[keyVK.Trim()];


                    //������ �� ���� ������� ������ ��������� ��� ���������
                    DataRow rowL = iv.��������.Rows[0];

                    string ������� = rowL["�������"].ToString();
                    string ��� = rowL["���"].ToString();
                    string �������� = rowL["��������"].ToString();

                    //�������� ������ ��������� ��� ���������
                    contract.��� = ������� + " " + ��� + " " + ��������;
                    contract.������������ = validUnload[keyVK].FlagPerson����;
                    contract.������������� = validUnload[keyVK].flagErrorSumm;

                    //������� ����� �� �������� ��������
                    DataTable tabSum = iv.����������������;

                    decimal sum = 0.0m;
                    foreach (DataRow r in tabSum.Rows)
                    {
                        sum = sum + Convert.ToDecimal(r["�����"]);
                    }

                    //�������� 
                    contract.SumService = sum.ToString("c");

                    //������� ����� �� �������
                    sumCount = sumCount + sum;

                    list.Add(contract);
                }

                //��������� ����� ����� � ������� �������� ���������  � ���������� ���������
                this.lblCount.Text += " ����� ����� " + sumCount.ToString("c") + " ���������� ��������� " + list.Count.ToString() + "  ��.";


                //��������� ��������� �������� � �����
                this.dataGridView1.DataSource = list;

                //�������� �������� �������
                this.dataGridView1.Columns["�������������"].HeaderText = "����� ��������";
                this.dataGridView1.Columns["�������������"].ReadOnly = true;
                this.dataGridView1.Columns["�������������"].DisplayIndex = 0;

                this.dataGridView1.Columns["���"].HeaderText = "��� ���������";
                this.dataGridView1.Columns["���"].ReadOnly = true;
                this.dataGridView1.Columns["���"].DisplayIndex = 1;

                this.dataGridView1.Columns["������������"].HeaderText = "��������� �������� � ����";
                this.dataGridView1.Columns["������������"].DisplayIndex = 2;

                this.dataGridView1.Columns["�������������"].HeaderText = "��������� �������� �����";
                this.dataGridView1.Columns["�������������"].DisplayIndex = 3;

                //this.dataGridView1.Columns["����������������"].Visible = false;
                this.dataGridView1.Columns["����������������"].HeaderText = "��������� �������";
                this.dataGridView1.Columns["����������������"].DisplayIndex = 4;

                //�������� ������������� ������� ��������� �������
                this.dataGridView1.Columns["����������������"].ReadOnly = true;
                this.dataGridView1.Columns["SumService"].HeaderText = "����� ��������";
                this.dataGridView1.Columns["SumService"].DisplayIndex = 5;
                this.dataGridView1.Columns["SumService"].ReadOnly = true;

                //this.dataGridView1.Columns["IdRegion"].DisplayIndex = 6;
                //this.dataGridView1.Columns["IdRegion"].Visible = false;

                //this.dataGridView1.Columns["Sinls"].DisplayIndex = 7;
                //this.dataGridView1.Columns["Sinls"].Visible = false;
            }

            if (this.�������������������������� == true)
            {
                // �������� DataGrid ������� � �������� ������� � ����.
                this.dataGridView1.DataSource = this.���������������;

            }

            //�������� �������� �������
            //this.dataGridView1.Columns["�������������"].HeaderText = "����� ��������";
            //this.dataGridView1.Columns["�������������"].ReadOnly = true;
            //this.dataGridView1.Columns["�������������"].DisplayIndex = 0;

            //this.dataGridView1.Columns["���"].HeaderText = "��� ���������";
            //this.dataGridView1.Columns["���"].ReadOnly = true;
            //this.dataGridView1.Columns["���"].DisplayIndex = 1;
            //this.dataGridView1.Columns["������������"].HeaderText = "��������� �������� � ����";
            //this.dataGridView1.Columns["������������"].DisplayIndex = 2;
            //this.dataGridView1.Columns["�������������"].HeaderText = "��������� �������� �����";
            //this.dataGridView1.Columns["�������������"].DisplayIndex = 3;
            ////this.dataGridView1.Columns["����������������"].Visible = false;
            //this.dataGridView1.Columns["����������������"].HeaderText = "��������� �������";
            //this.dataGridView1.Columns["����������������"].DisplayIndex = 4;

            ////�������� ������������� ������� ��������� �������
            //this.dataGridView1.Columns["����������������"].ReadOnly = true;
            //this.dataGridView1.Columns["SumService"].HeaderText = "����� ��������";
            //this.dataGridView1.Columns["SumService"].DisplayIndex = 5;
            //this.dataGridView1.Columns["SumService"].ReadOnly = true;


            ////������ ��������� ������� (����� ������ ���������)
            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
                Dictionary<string, ValidateContract> validUnload = this.����������������;

                //������� ����� �������� 
                string numDog = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

                //if (this.dataGridView1.CurrentRow.Cells[2].Value.ToString() == "True")
                //{
                //    var asd = "True";
                //}
                //else
                //{
                //    //if (this.dataGridView1.Columns[2].HeaderText == "��������� �������� � ����")
                //    if (this.dataGridView1.CurrentCell.ColumnIndex == 2)
                //    {

                //        var dListtCOntractTest = this.listSave;


                //        FormRegions formRegion = new FormRegions();
                //        DialogResult dRezult = formRegion.ShowDialog();

                //        if (dRezult == System.Windows.Forms.DialogResult.OK)
                //        {
                //            var idRegion = formRegion.IdRegion;

                //            // ������� ������� ��� ��������� �������� � ��������� id ������ �������.
                //            unload[numDog].IdRegionEsrn = idRegion.ToString();
                //       }
                //    }
                //}

                

                //if (this.dataGridView1.CurrentCell.ColumnIndex == 2 || this.dataGridView1.CurrentCell.ColumnIndex == 1)
                //{
                    //������� ������ �� ��������� �� ����
                    List<����validate> �������� = validUnload[numDog].����������������;
                    string ��� = string.Empty;

                    try
                    {
                        foreach (����validate ���� in ��������)
                        {

                            //��� = ����.������� + " " + ����.��� + " " + ����.�������� + " ���� �������� - " + ����.������������ + " �������� ��������� - " + ����.����������������� + " " + ����.�������������� + " " + ����.�������������� + " �����: " + ����.�����; 
                            this.txt��������.Text = ����.�������.Trim() + " " + ����.���.Trim() + " " + ����.��������.Trim() + " " + ����.������������.Trim() + "�.�.";
                            this.txt�����.Text = " �����: " + ����.�����.Trim();

                            �������� = string.Empty;

                            //�������� ���� ����� ��������� ����� NULl
                            if (����.�������������� != null)
                            {
                                �������� = ����.��������������.Trim();
                            }
                            else
                            {
                                �������� = "";
                            }

                            �������������� = string.Empty;
                            if (����.�������������� != null)
                            {
                                �������������� = ����.��������������.Trim();
                            }

                            ���������� = string.Empty;
                            if (����.������������������� != null)
                            {
                                ���������� = ����.�������������������.Trim();
                            }
                            else
                            {
                                ���������� = "";
                            }

                            this.txt��������.Text = ����.�����������������.Trim() + " " + ��������.Trim() + " " + �������������� + " ����� " + ����������;
                        }
                    }
                    catch
                    {
                        this.txt��������.Text = "�������� � ��������� ����������� � ���� ���� �����������";
                        this.txt�����.Text = "";
                        this.txt��������.Text = "";
                    }


                    //������� ���������� � ���������
                    //this.txt��������.Text = ���;


                //}


                //if (this.dataGridView1.CurrentCell.ColumnIndex == 2 || this.dataGridView1.CurrentCell.ColumnIndex == 1 || this.dataGridView1.CurrentCell.ColumnIndex == 3)
                //{
                    //������� ������ �� ��������� �� ����
                    List<����validate> ��������1 = validUnload[numDog].����������������;
                    string ���1 = string.Empty;


                    try
                    {

                        foreach (����validate ���� in ��������1)
                        {


                            //��� = ����.������� + " " + ����.��� + " " + ����.�������� + " ���� �������� - " + ����.������������ + " �������� ��������� - " + ����.����������������� + " " + ����.�������������� + " " + ����.�������������� + " �����: " + ����.�����; 
                            this.txt��������.Text = ����.�������.Trim() + " " + ����.���.Trim() + " " + ����.��������.Trim() + " " + ����.������������.Trim() + "�.�.";
                            this.txt�����.Text = " �����: " + ����.�����.Trim();

                            �������� = string.Empty;

                            //�������� ���� ����� ��������� ����� NULl
                            if (����.�������������� != null)
                            {
                                �������� = ����.��������������.Trim();
                            }
                            else
                            {
                                �������� = "";
                            }

                            �������������� = string.Empty;
                            if (����.�������������� != null)
                            {
                                �������������� = ����.��������������.Trim();
                            }

                            ���������� = string.Empty;
                            if (����.������������������� != null)
                            {
                                ���������� = ����.�������������������.Trim();
                            }
                            else
                            {
                                ���������� = "";
                            }

                            this.txt��������.Text = ����.�����������������.Trim() + " " + ��������.Trim() + " " + �������������� + " ����� " + ����������;

                            //this.txt��������.Text = ����.�����������������.Trim() + " " + ����.��������������.Trim() + " " + ����.��������������.Trim() + " ����� " + ����.�������������������;
                        }
                    }
                    catch
                    {
                        this.txt��������.Text = "�������� � ��������� ����������� � ���� ���� �����������";
                        this.txt�����.Text = "";
                        this.txt��������.Text = "";
                    }

                    //���� ������ ����������� �� ����� null

                    if (validUnload[numDog].����������������� != null)
                    {
                        List<ErrorsReestrUnload> errorUbload = validUnload[numDog].�����������������;

                        //������ ��� �������� ���������� ������
                        List<DateService> listCorrect = new List<DateService>();

                        //������ ��� �������� ��������� ������
                        List<DateService> listError = new List<DateService>();

                        //������ ���������� � ��������� ������ 
                        foreach (ErrorsReestrUnload date in errorUbload)
                        {
                            //������� ���������� ������
                            DateService dataCorrect = new DateService();

                            //�� ����� ������ � ��������� ������ ������
                            //if (date.������������������ != "" || date.������������������ != null)
                            if (date.������������������ != string.Empty)
                            {
                                dataCorrect.������������ = date.������������������;

                                dataCorrect.���� = date.����.ToString("c");
                                dataCorrect.����� = date.�����.ToString("c");

                                //������� ����������� ������ � ���������
                                listCorrect.Add(dataCorrect);
                            }

                            DateService dataError = new DateService();

                            //�� ����� ������ � ��������� ������ ������
                            //if (date.Error������������������ != "" || date.Error������������������ != null)
                            if (date.Error������������������ != string.Empty || date.Error������������������ != null)
                            {
                                dataError.������������ = date.Error������������������;

                                dataError.���� = date.Error����.ToString("c");
                                dataError.����� = date.Error�����.ToString("c");

                                //������� ��������� ������ � ���������
                                listError.Add(dataError);
                            }
                        }

                        //��������� ���������� ������
                        this.dataCorrect.DataSource = listCorrect;

                        //��������� ��������� ������
                        this.dataError.DataSource = listError;
                    }
                    else
                    {
                        //��������� ���������� ������
                        this.dataCorrect.DataSource = null;

                        //��������� ��������� ������
                        this.dataError.DataSource = null;
                    }

                //}


                ////���� ������������ ������ ������� � ������� ��������� �������
                ////������� �������� ��� �������� � ������
                string numberDog = string.Empty;
                if (this.dataGridView1.CurrentRow.Cells["����������������"].Selected == true)
                {

                    try
                    {
                        //���� ������� �������
                        numberDog = this.dataGridView1.CurrentRow.Cells["�������������"].Value.ToString().Trim();

                        bool bol����� = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["�������������"].Value);
                        bool bool���� = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["������������"].Value);
                        //bool bool��� = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["���"].Value);

                        //Unload unloadSave = �����������������������[numberDog];

                        if (bol����� == true && bool���� == true)// && bool��� == true)
                        {
                            this.dataGridView1.CurrentRow.Cells["����������������"].ReadOnly = false;
                            //this.listSave.Add(numberDog, unloadSave);
                        }
                        else
                        {
                            MessageBox.Show("������� � " + numberDog + " �� �������� �������� � ���� ������");
                        }
                    }
                    catch
                    {
                        //���� ������������ �������� ������ ������ ������ �� ����� ����� ������ � ������ �� ��������� �������
                        numberDog = this.dataGridView1.CurrentRow.Cells["�������������"].Value.ToString().Trim();
                        //this.listSave.Remove(numberDog);
                    }
                }
            //}
            //catch
            //{
            //    MessageBox.Show("��� �� ������");
            //}

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool flag����� = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["�������������"].Value);
            bool flag���� = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["������������"].Value);

            bool test = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["����������������"].Value);

            if (flag����� == true && flag���� == true)
            {
                this.dataGridView1.CurrentRow.Cells["����������������"].ReadOnly = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //����� ������� �� �������� ��������� ������� ��� ������� ������� ������� ��� ������ ��������.
            //���� ����� ������� ���� �� �� �������� ��������� ������ ���� ��������

            List<PrintContractsValidate> listContract = new List<PrintContractsValidate>();

            // �������� �� ������� DataGridView � �������� � ��������� �� ���������� ������ ��������
            // � ������� ����������������������� ������� ��������� �������
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                // �������� ����� ��������
                string numberDog = row.Cells["�������������"].Value.ToString().Trim();

                // ������ ���������� ��������� ������� ����� ������������ �� �����
                // ������� �������� � ������� � ������� ��������� ������� ����� ������� 
                if (Convert.ToBoolean(row.Cells["����������������"].Value) == true)
                {
                    // ������� ������� �� ��������.
                    Unload unloadSave = �����������������������[numberDog];

                    
                    // ������� ��������� �������� ���������� ��������.
                    ValidateContract unloadValid = this.����������������[numberDog];

                    // ��������� �������� �� ������ ���� ����� �� ������ � ����, � ���������� � ������.
                    if (unloadValid.IdRegionEsrn != null)
                    {
                        // ������� ID ����� �������.
                        unloadSave.IdRegionEsrn = unloadValid.IdRegionEsrn;
                    

                        // �������� ID ������ ������� ��� ������ ������� ������ ��� ������ � ��.
                        //unloadSave.IdRegionEsrn = unloadValid.IdRegionEsrn;

                    }

                    // ������� ����� ��������� ���������� ��������.
                    unloadSave.SnilsPerson = unloadValid.SnilsPerson;

                    // ��������� ���� � TRUE � ��� ��� ������ ������ ��������
                    unloadSave.FalgWrite = true;

                    //������� � �������
                    this.listSave.Add(numberDog, unloadSave);
                }
                else
                {
                    /*
                    * � ����� � ����������� ������ ���������� ��� ������� ��������� � ���� � � ��� ����� � �� ��������� ��������
                    */

                    if (�����������������������.ContainsKey(numberDog.Trim()))
                    {
                        // ������� �� ���������� ������ ��������.
                        this.iv = �����������������������[numberDog.Trim()];
                    }

                    // ��������� ���� � FALSE ��������� � ��� ��� ������ �������� �� ������ ��������
                    //unloadSave.FalgWrite = false;
                    this.iv.FalgWrite = false;

                    // ������� � ������� ��� ������� ���������
                    //this.listSave.Add(numberDog, unloadSave);
                    this.listSave.Add(numberDog, this.iv);
                }
            }

            //���� ������ �������� ��������� �� ����� 0
            if (listSave.Count != 0)
            {
                //����������� ������� � ������
                Dictionary<string, Unload> iTest = this.listSave;

                //�������� ������ ��������� ���� Unload
                List<Unload> listUnload = new List<Unload>();

                //������ ���������� �� ��� �������� � �������� ���������
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    SqlTransaction transact = con.BeginTransaction();

                    foreach (Unload unload in listSave.Values)
                    {

                        //������� ����� �������� �������� ���������� � �����
                        string numDogFile = unload.�������.Rows[0]["�������������"].ToString().Trim();

                        if (numDogFile == "8/5624")
                        {
                            var dTest = "";
                        }

                        // ������ ���� �� �� ������� ����� ����� �������� ������� ������ ��������
                        string queryNumDogServ = "select count(�������������) from ������� where LOWER(RTRIM(LTRIM(�������������))) = LOWER(RTRIM(LTRIM('" + numDogFile + "'))) and  ������������ = 'True' ";

                        // ������� � ������������ ��������.
                        DataTable tab = ���������.GetTableSQL(queryNumDogServ, "����������������", con, transact);

                        // ���� ���������� ����� 24,09,2018 ����.
                        if (Convert.ToInt32(tab.Rows[0][0]) != 0)
                        {
                            // ������� ��������� ��� ����� �������� ��� ����������.
                            FormModalPerson fmp = new FormModalPerson();
                            fmp.NumContract = numDogFile;
                            fmp.ShowDialog();

                            if (fmp.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                //���� ����� ������� ��� � �� ���������� �� ����� ��������� � ������ ��������
                                //continue;
                            }
                        }
                 

                        listUnload.Add(unload);

                        //������� ��� ���������
                        DataRow rowL = unload.��������.Rows[0];

                        //������� ������������
                        DataRow rowH = unload.������������.Rows[0];

                        // �����c ���������� ��� � ������ ������������� ������ �������� �� ������� ������
                        string query = "select �������������,������������ from dbo.������� " +
                                              "where id_�������� in ( " +
                                              "SELECT [id_��������] FROM [��������] " +
                                              "where [�������] = '" + rowL["�������"].ToString() + "' and ��� = '" + rowL["���"].ToString() + "' and �������� = '" + rowL["��������"].ToString() + "' and ������������ = '" + rowL["������������"].ToString().Trim() + "') ";// and id_������������ <> (select top 1 id_������������ from dbo.������������ where ��� = '" + rowH["���"].ToString().Trim() + "' order by id_������������ desc) ";

 
                        DataTable tabUnload = ���������.GetTableSQL(query, "�������", con, transact);
                        

                        //������ ������ ��� �������� ���������
                        PrintContractsValidate contr = new PrintContractsValidate();

                        string ������� = rowL["�������"].ToString();
                        string ��� = rowL["���"].ToString();

                        string �������� = rowL["��������"].ToString();
                        string ������������ = Convert.ToDateTime(rowL["������������"]).ToShortDateString();

                        //�������� ������ ��������� ��� ���������
                        string ��� = ������� + " " + ��� + " " + ��������;

                        //������� ������� ����� ��������
                        DataRow rowDog = unload.�������.Rows[0];
                        string numDog = rowDog["�������������"].ToString().Trim();

                     
                        contr.���_�����_�������������� = ���.Trim();
                        contr.������������������� = numDog.Trim();

                        //������������ ������ ��� �������� 
                        StringBuilder builder = new StringBuilder();

                        // ������� �������� ������� ��� ���� � ���������
                        foreach(DataRow row in tabUnload.Rows)
                        {
                            //������� ����� ��������
                            string ������ = row["�������������"].ToString().Trim();
                            string ������� = Convert.ToDateTime(row["������������"]).ToShortDateString();

                            string ������� = ������ + " �� " + ������� + " ; ";

                            //���� ������� �� �������� �� � ���� �� �������
                            if (�������.Trim() == "01.01.1900")
                            {
                                ������� = ������ + " ;";
                            }
                            
                            builder.Append(�������);
                        }

                        //���� ��������� ���
                        if (builder.Length == 0)
                        {
                            contr.��������������� = "���";
                        }
                        else
                        {
                            contr.��������������� = "����������� �������� - " + builder.ToString().Trim();
                        }

                         //string querDubl = "select dbo.�������.id_�������, �������������,������������, dbo.�������������������.���������,dbo.�������������������.��������������  from dbo.������� " +
                         //              " INNER  JOIN dbo.������������������� " +
                         //              " ON dbo.�������.id_������� = dbo.�������������������.id_������� " +
                         //               " where id_��������  " +
                         //               " in ( SELECT [id_��������] FROM [��������] " +
                         //               "where [�������] = '" + rowL["�������"].ToString() + "' and ��� = '" + rowL["���"].ToString() + "' and �������� = '" + rowL["��������"].ToString() + "' and ������������ = '" + rowL["������������"].ToString().Trim() + "') ";

                        string querDubl = " select dbo.�������.id_�������, �������������,�������.������������������,������������, dbo.�������������������.���������, " +
                                          " dbo.�������������������.��������������  from dbo.������� left outer JOIN dbo.������������������� " +
                                          " ON dbo.�������.id_������� = dbo.�������������������.id_������� " +
                                          " where id_��������  " +
                                          " in ( SELECT [id_��������] FROM [��������] " +
                                          " where [�������] = '" + rowL["�������"].ToString() + "' and ��� = '" + rowL["���"].ToString() + "' and �������� = '" + rowL["��������"].ToString() + "' and ������������ = '" + rowL["������������"].ToString().Trim() + "') " +
                                          " and �������.������������ = 'True' ";
                  

                        DataTable tabContr = ���������.GetTableSQL(querDubl, "�����", con, transact);
                        
                        // ������ � �������� ����� ����������� ��������� ��� �������� ��������� (��� � ������� ������������ ��� � � ������ �������������)
                        StringBuilder listNumDog = new StringBuilder();

                        foreach (DataRow row in tabContr.Rows)
                        {
                            //listNumDog.Append(row["�������������"].ToString().Trim() + " �� - " + Convert.ToDateTime(row["��������������"]).ToShortDateString() + "; ");

                            if (DBNull.Value != row["�������������"])
                            {
                                listNumDog.Append('\n' + " " + row["�������������"].ToString().Trim());

                                //if (DBNull.Value != row["������������������"])
                                //{
                                //    listNumDog.Append("���� ������ �������� - " + Convert.ToDateTime(row["������������������"]).ToShortDateString());
                                //}
                            }

                            if(DBNull.Value != row["���������"])
                            {

                                //if (DBNull.Value != row["������������"])
                                //{
                                //    listNumDog.Append(" ���� �������� - " + Convert.ToDateTime(row["������������"]).ToShortDateString());
                                //}

                                //listNumDog.Append(" ��� � " + row["���������"].ToString().Trim());// + " �� - " + Convert.ToDateTime(row["��������������"]).ToShortDateString());

                                if (DBNull.Value != row["��������������"])
                                {

                                    listNumDog.Append(" �� - " + Convert.ToDateTime(row["��������������"]).ToShortDateString() + "; ");
                                }
                            }
                            
                        }

                        contr.��������������� = listNumDog.ToString();

                        // ������� �������� � ���������
                        listContract.Add(contr);
                    }
                }


                List<Unload> test = listUnload;
                // ������� � ��
                //MessageBox.Show("���������� � ��");


                //������� ������ � �������� word
                List<PrintContractsValidate> lTest = listContract;

                string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ ���������.doc";

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

                //�������������� �������������� �����
                //doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;


                object bookNaziv = "�������";
                Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

                object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 4, ref behavior, ref autobehavior);
                table.Range.ParagraphFormat.SpaceAfter = 6;
                table.Columns[1].Width = 40;
                table.Columns[2].Width = 140;
                table.Columns[3].Width = 80;
                table.Columns[4].Width = 200;
                table.Borders.Enable = 1; // ����� - �������� �����
                table.Range.Font.Name = "Times New Roman";
                table.Range.Font.Size = 10;
                //������� �����
                int i = 1;

                //������ � ������� ��� �������
                List<ContractListItem> list = new List<ContractListItem>();

                //���������� ����� �������
                ContractListItem ����� = new ContractListItem();
                �����.Num = "� �/�";
                �����.FIO = "��� ���������";
                �����.NumCurrentContract = "����� �������� ��������";
                //�����.NumContracts = "����� ����������� �������� � ������ �������������";
                �����.NumContr = "����� ����������� ��������";

                list.Add(�����);

                //�������
                int iCount = 1;

                //�������� ������� �������
                foreach (PrintContractsValidate un in listContract)
                {
                    //�������� ������� �������
                    ContractListItem item = new ContractListItem();

                    //���������� �����
                    item.Num = iCount.ToString().Trim();

                    //������� ��� ���������
                    item.FIO = un.���_�����_��������������.Trim();

                    //����� �������� ��������
                    item.NumCurrentContract = un.�������������������.Trim();

                    //������� ������ ���������
                    //item.NumContracts = un.���������������.Trim();
                    item.NumContr = un.���������������.Trim();

                    list.Add(item);

                    //�������� ������� �� 1
                    iCount++;
                }

                //�������� �������
                int k = 1;
                //������� ������ � �������
                foreach (ContractListItem item in list)
                {
                    //table.Cell(i, 1).Range.Text = i.ToString();//item.���������������;
                    table.Cell(k, 1).Range.Text = item.Num;

                    table.Cell(k, 2).Range.Text = item.FIO;

                    table.Cell(k, 3).Range.Text = item.NumCurrentContract;

                    //table.Cell(k, 4).Range.Text = item.NumContracts;
                    table.Cell(k, 4).Range.Text = item.NumContr;


                    //doc.Words.Count.ToString();
                    Object beforeRow1 = Type.Missing;
                    table.Rows.Add(ref beforeRow1);

                    k++;
                }

                table.Rows[k].Delete();

                //��������� ��������
                app.Visible = true;

                List<Unload> listTest = listUnload;

                foreach (var iTestW in listUnload)
                {
                    var asd = iTestW;
                }

                //��������� ������ � �� 
                FormDalogWriteBD formDialog = new FormDalogWriteBD();
                formDialog.ShowDialog();

                if (formDialog.DialogResult == System.Windows.Forms.DialogResult.OK)
                {


                    WriteBD writBD = new WriteBD(listUnload);

                    // ������� � ��.
                    writBD.Write();

                    // 
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("�������� �������� ��� ������ � ���� ������");
            }
        }

        private void btnDist_Click(object sender, EventArgs e)
        {
            //������� ��������� ��� ������� � DataGridView � �������� true

            //��������� ����� �������
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {

                //// ������� ����� �������.
                //row.Cells["�������������"].Value = true;
                //row.Cells["������������"].Value = true;

                DataGridViewCheckBoxCell check1 = (DataGridViewCheckBoxCell)row.Cells["�������������"];
                DataGridViewCheckBoxCell check2 = (DataGridViewCheckBoxCell)row.Cells["������������"];
                if (Convert.ToBoolean(check1.FormattedValue) == true && Convert.ToBoolean(check2.FormattedValue) == true)
                {
                    row.Cells["����������������"].Value = true;

                }
                else
                {
                    //string message = "�������� " + row.Cells["���"].Value.ToString().Trim() + "  �� ����� ���� ������� �� ������";
                    //string caption = "������";
                    //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    //DialogResult result;

                    //// Displays the MessageBox.

                    //result = MessageBox.Show(this, message, caption, buttons);

                    //if (result == DialogResult.Yes)
                    //{
                    //    row.Cells["����������������"].Value = true;
                    //}

                    MessageBox.Show("�������� " + row.Cells["���"].Value.ToString().Trim() + "  �� ����� ���� �������");
                }
                
            }


            //DataGridViewCheckBoxColumn col1 = (DataGridViewCheckBoxColumn)this.dataGridView1.Columns[2];
            //DataGridViewCheckBoxColumn col2 = (DataGridViewCheckBoxColumn)this.dataGridView1.Columns[3];
            //DataGridViewCheckBoxColumn col3 = (DataGridViewCheckBoxColumn)this.dataGridView1.Columns[4];
            //col1.TrueValue = false;
            //col2.TrueValue = false;
            //col3.TrueValue = false;
            

        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            //���� ������������ ������ ����
            //this.setupTime = true;

            this.button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //������� ������
            this.listPrintSave.Clear();
            this.listPrintNoSave.Clear();

            ListLoad();

            if (listPrintSave.Count != 0)
            {
                Dictionary<string, Unload> iTest = listPrintSave;

                //����������� �������� word
                //������ ��� ������������
                string ��� = string.Empty;

                //������ �������
                string ����� = string.Empty;

                //������ ��������� (��������� ������ �������)
                string ��������� = string.Empty;

                //������ ��������
                string fio = string.Empty;

                //����������� �������� Word
                //string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ �������.doc";
                //string filName = @"D:\�������\ControlDantist\ControlDantist\bin\Debug\������\������  �������.doc";
                string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ � ������.doc";


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

                //�������������� �������������� �����
                //doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;


                object bookNaziv = "�������";
                Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

                object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 3, ref behavior, ref autobehavior);
                table.Range.ParagraphFormat.SpaceAfter = 6;
                table.Columns[1].Width = 40;
                table.Columns[2].Width = 140;
                table.Columns[3].Width = 300;
                table.Borders.Enable = 1; // ����� - �������� �����
                table.Range.Font.Name = "Times New Roman";
                table.Range.Font.Size = 10;
                //������� �����
                int i = 1;

                //������ ��� �������� ������ ��� �������
                List<ReestrNoPrintDog> listItem = new List<ReestrNoPrintDog>();

                //���������� ����� �������
                ReestrNoPrintDog ����� = new ReestrNoPrintDog();
                �����.Number = "� �/�";
                �����.NumDog = "� ��������";
                �����.Fio = "���";

                listItem.Add(�����);

                //�������
                int iCount = 1;

                //�������� ������� �������
                foreach (Unload un in listPrintSave.Values)
                {
                    //������ �������
                    ReestrNoPrintDog item = new ReestrNoPrintDog();
                    item.Number = iCount.ToString();

                    //������� ��� ������������
                    ��� = un.������������.Rows[0]["���"].ToString().Trim();

                    DataRow rowDog = un.�������.Rows[0];
                    item.NumDog = rowDog["�������������"].ToString().Trim();

                    //������� ��� ���������
                    DataRow row�������� = un.��������.Rows[0];

                    string ������� = row��������["�������"].ToString().Trim();
                    string ��� = row��������["���"].ToString().Trim();
                    string �������� = row��������["��������"].ToString().Trim();

                    //
                    string ��� = ������� + " " + ��� + " " + ��������;
                    item.Fio = ���.Trim();

                    listItem.Add(item);

                    iCount++;
                }


                //�������� �������
                int k = 1;
                //������� ������ � �������
                foreach (ReestrNoPrintDog item in listItem)
                {
                    //table.Cell(i, 1).Range.Text = i.ToString();//item.���������������;
                    table.Cell(k, 1).Range.Text = item.Number;

                    table.Cell(k, 2).Range.Text = item.NumDog;
                    table.Cell(k, 3).Range.Text = item.Fio;


                    //doc.Words.Count.ToString();
                    Object beforeRow1 = Type.Missing;
                    table.Rows.Add(ref beforeRow1);

                    k++;
                }

                table.Rows[k].Delete();

                //�������� ����� � ������ � ������������
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();

                    SqlTransaction transact = con.BeginTransaction();

                    //������� id ������������
                    string queryINN = "SELECT [F2],[F3],��� FROM [����1$] where id in (select id_������������ from dbo.������������ where ��� = " + ��� + ") ";
                    DataRow row = ���������.GetTableSQL(queryINN, "����1$", con, transact).Rows[0];

                    //������� �����
                    ����� = row["F2"].ToString().Trim();

                    //������� ���������
                    ��������� = row["F3"].ToString().Trim();

                    //������� ���
                    fio = row["���"].ToString().Trim();

                }

                //�������� �������� WORD �������
                ////����� ��������

                object wdrepl = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt = "�������";
                object newtxt = (object)�����;
                //object frwd = true;
                object frwd = false;
                doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                ref missing, ref missing);

                //������� ����
                object wdrepl1 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt1 = "����";
                object newtxt1 = (object)���������;
                //object frwd = true;
                object frwd1 = false;
                doc.Content.Find.Execute(ref searchtxt1, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd1, ref missing, ref missing, ref newtxt1, ref wdrepl1, ref missing, ref missing,
                ref missing, ref missing);

                //������� ���
                object wdrepl2 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt2 = "fio";
                object newtxt2 = (object)fio;
                //object frwd = true;
                object frwd2 = false;
                doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                ref missing, ref missing);

                //��������� ��������
                app.Visible = true;
            }
            else
            {
                MessageBox.Show("��� ������ ��� ������.");
            }
        }

        
        /// <summary>
        /// �������� ������ ��������� ������� ������ � �� ������ ��������
        /// </summary>
        private void ListLoad()
        {
            //������� ������
            this.listPrintSave.Clear();
            this.listPrintNoSave.Clear();
            this.listPrintAddCheck.Clear();


            //�������� �� ������� DataGridView � �������� � ��������� �� ���������� ������ ��������
            //� ������� ����������������������� ������� ��������� �������
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                //������ ���������� ��������� ������� ����� ������������ �� �����
                //������� �������� � ������� � ������� ��������� ������� ����� ������� 
                //if (Convert.ToBoolean(row.Cells["����������������"].Value) == true)
                //{
                //    string numberDog = row.Cells["�������������"].Value.ToString().Trim();
                //    Unload unloadSave = �����������������������[numberDog];

                //    //������� � �������
                //    //this.listPrintSave.Add(numberDog, unloadSave);
                //    this.listPrintSave.Add(numberDog, unloadSave);
                //}
                //else
                //{
                //    string numberDog = row.Cells["�������������"].Value.ToString().Trim();
                //    Unload unloadSave = �����������������������[numberDog];

                //    //������� � �������
                //    this.listPrintNoSave.Add(numberDog, unloadSave);
                //}

                    //��������� ��������
                    if (Convert.ToBoolean(row.Cells["����������������"].Value) == true && Convert.ToBoolean(row.Cells["������������"].Value) == true)
                    {
                        string numberDog = row.Cells["�������������"].Value.ToString().Trim();
                        Unload unloadSave = �����������������������[numberDog];

                        //������� � �������
                        //this.listPrintSave.Add(numberDog, unloadSave);
                        this.listPrintSave.Add(numberDog, unloadSave);
                    }
                   
                    ////�� ��������� ��������
                    if (Convert.ToBoolean(row.Cells["����������������"].Value) == false && Convert.ToBoolean(row.Cells["������������"].Value) == false)
                    {
                        string numberDog = row.Cells["�������������"].Value.ToString().Trim();
                        Unload unloadSave = �����������������������[numberDog];

                        //������� � �������
                        this.listPrintNoSave.Add(numberDog, unloadSave);
                    }

                    //�������������� ��������
                    if (Convert.ToBoolean(row.Cells["����������������"].Value) == false && Convert.ToBoolean(row.Cells["������������"].Value) == true)
                    {
                        string numberDog = row.Cells["�������������"].Value.ToString().Trim();
                        Unload unloadSave = �����������������������[numberDog];

                        //������� � �������
                        this.listPrintAddCheck.Add(numberDog, unloadSave);
                    }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //������� ������
            this.listPrintSave.Clear();
            this.listPrintNoSave.Clear();

            //������ ��� ������������
            string ��� = string.Empty;

            //������ �������
            string ����� = string.Empty;

            //������ ��������� (��������� ������ �������)
            string ��������� = string.Empty;

            //������ ��������
            string fio = string.Empty;


            ListLoad();

            Dictionary<string, Unload> iTest = listPrintNoSave;

            if (listPrintNoSave.Count != 0)
            {
                //����������� �������� Word
                //string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ �������.doc";
                //string filName = @"D:\�������\ControlDantist\ControlDantist\bin\Debug\������\������  �������.doc";
                string filName = System.Windows.Forms.Application.StartupPath + @"\������\������  �������.doc";


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

                //�������������� �������������� �����
                //doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;


                object bookNaziv = "�������";
                Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

                object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 4, ref behavior, ref autobehavior);
                table.Range.ParagraphFormat.SpaceAfter = 6;
                table.Columns[1].Width = 40;
                table.Columns[2].Width = 80;
                table.Columns[3].Width = 250;
                table.Columns[4].Width = 150;
                table.Borders.Enable = 1; // ����� - �������� �����
                table.Range.Font.Name = "Times New Roman";
                table.Range.Font.Size = 10;
                //������� �����
                int i = 1;

                //������ ��� �������� ������ ��� �������
                List<ReestrNoPrintDog> listItem = new List<ReestrNoPrintDog>();

                //���������� ����� �������
                ReestrNoPrintDog ����� = new ReestrNoPrintDog();
                �����.Number = "� �/�";
                �����.NumDog = "� ��������";
                �����.Fio = "���";
                �����.���������� = "����������";

                listItem.Add(�����);

                //�������
                int iCount = 1;

                //�������� ������� �������
                foreach (Unload un in listPrintNoSave.Values)
                {
                    //������ �������
                    ReestrNoPrintDog item = new ReestrNoPrintDog();
                    item.Number = iCount.ToString();

                    //������� ��� ������������
                    ��� = un.������������.Rows[0]["���"].ToString().Trim();

                    DataRow rowDog = un.�������.Rows[0];
                    item.NumDog = rowDog["�������������"].ToString().Trim();

                    //������� ��� ���������
                    DataRow row�������� = un.��������.Rows[0];

                    string ������� = row��������["�������"].ToString().Trim();
                    string ��� = row��������["���"].ToString().Trim();
                    string �������� = row��������["��������"].ToString().Trim();

                    //
                    string ��� = ������� + " " + ��� + " " + ��������;
                    item.Fio = ���.Trim();

                    item.���������� = "";

                    listItem.Add(item);

                    iCount++;
                }


                //�������� �������
                int k = 1;
                //������� ������ � �������
                foreach (ReestrNoPrintDog item in listItem)
                {

                    table.Cell(k, 1).Range.Text = item.Number;

                    table.Cell(k, 2).Range.Text = item.NumDog;
                    table.Cell(k, 3).Range.Text = item.Fio;
                    
                    //if (k == 1)
                    //{

                    //}

                    //if (k > 1)
                    //{
                    //    table.Cell(k, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    //}
                    
                    table.Cell(k, 4).Range.Text = item.����������.Trim();

                    //doc.Words.Count.ToString();
                    Object beforeRow1 = Type.Missing;
                    table.Rows.Add(ref beforeRow1);

                    k++;
                }

                table.Rows[k].Delete();

                //�������� ����� � ������ � ������������
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();

                    SqlTransaction transact = con.BeginTransaction();

                    //������� id ������������
                    string queryINN = "SELECT [F2],[F3],��� FROM [����1$] where id in (select id_������������ from dbo.������������ where ��� = " + ��� + ") ";
                    DataRow row = ���������.GetTableSQL(queryINN, "����1$", con, transact).Rows[0];

                    //������� �����
                    ����� = row["F2"].ToString().Trim();

                    //������� ���������
                    ��������� = row["F3"].ToString().Trim();

                    //������� ���
                    fio = row["���"].ToString().Trim();

                }

                //�������� �������� WORD �������
                ////����� ��������

                object wdrepl = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt = "�������";
                object newtxt = (object)�����;
                //object frwd = true;
                object frwd = false;
                doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                ref missing, ref missing);

                //������� ����
                object wdrepl1 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt1 = "����";
                object newtxt1 = (object)���������;
                //object frwd = true;
                object frwd1 = false;
                doc.Content.Find.Execute(ref searchtxt1, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd1, ref missing, ref missing, ref newtxt1, ref wdrepl1, ref missing, ref missing,
                ref missing, ref missing);

                //������� ���
                object wdrepl2 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt2 = "fio";
                object newtxt2 = (object)fio;
                //object frwd = true;
                object frwd2 = false;
                doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                ref missing, ref missing);

                //��������� ��������
                app.Visible = true;
            }
            else
            {
                MessageBox.Show("�� ������������� ��������� ���");
            }

        }

        //
        private void btnExel_Click(object sender, EventArgs e)
        {
            ListLoad();
            Dictionary<string, Unload> iTest = listPrintSave;

            //������ ������ ����������
            List<RowExcel> list = new List<RowExcel>();

            if (listPrintSave.Count != 0)
            {
                using(SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    SqlTransaction transact = con.BeginTransaction();

                    //��������� �� ���������� ������� ������ ��������
                    foreach (Unload un in listPrintSave.Values)//this.����������������
                    {
                        //������� ��� ���������
                        DataRow row�������� = un.��������.Rows[0];
                        string ������� = row��������["�������"].ToString().Trim();

                        string ��� = row��������["���"].ToString().Trim();
                        string �������� = row��������["��������"].ToString().Trim();

                        string queryFIO = "SELECT [�������] " +
                                          ",[�������] " +
                                          ",[���] " +
                                          ",[��������] " +
                                          "FROM [�������Excel] " +
                                          "where ������� like '%" + ������� + "%' and ��� like '%" + ��� + "%' and �������� like '%" + �������� + "%' ";

                        //������� ������� ����������
                        DataTable tab�������� = ���������.GetTableSQL(queryFIO, "Excel", con, transact);

                        //������� ���������� � ������
                        foreach (DataRow row in tab��������.Rows)
                        {
                            RowExcel exe = new RowExcel();
                            //exe.Num = row["�������������"].ToString().Trim();
                            exe.Num = row[0].ToString().Trim();

                            exe.������� = row["�������"].ToString().Trim();
                            exe.��� = row["���"].ToString().Trim();

                            exe.�������� = row["��������"].ToString().Trim();
                            list.Add(exe);
                        }
                    }
                }
            }

            //���� ������ � ���������� ���������� ������
            if (list.Count != 0)
            {
                //��������� � ����� ������ 
                FormExcel fexe = new FormExcel();
                fexe.TopMost = true;

                //��������� � ����� ������
                fexe.ListExcel = list;
                fexe.Show();
            }
            else
            {
                MessageBox.Show("���������� �� �������");
            }


        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{

            //    this.dataGridView1.ClearSelection();
            //    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            //    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

            //    DataGridViewRow rows = this.dataGridView1.Rows[e.RowIndex];

            //    //������� ����� ��������
            //    //this.����������������;
            //    string numDog = rows.Cells[0].Value.ToString().Trim();

            //    Unload vr = this.�����������������������[numDog];

            //    //��������� �����
            //    FormInfo�������� info = new FormInfo��������();

            //    //��������� ������ � �����
            //    info.Unloads = vr;
                
            //    //��������� �����
            //    info.Show();

            // }
        }

        private void ��������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

            DataGridViewRow rows = this.dataGridView1.Rows[e.RowIndex];

            //������� ����� ��������
            //this.����������������;
            string numDog = rows.Cells[0].Value.ToString().Trim();

            Unload vr = this.�����������������������[numDog];

            //��������� �����
            FormInfo�������� info = new FormInfo��������();

            //��������� ������ � �����
            info.Unloads = vr;

            //��������� �����
            info.Show();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //������� ������
            this.listPrintSave.Clear();
            this.listPrintNoSave.Clear();
            this.listPrintAddCheck.Clear();

            //������ ��� ������������
            string ��� = string.Empty;

            //������ �������
            string ����� = string.Empty;

            //������ ��������� (��������� ������ �������)
            string ��������� = string.Empty;

            //������ ��������
            string fio = string.Empty;


            ListLoad();

            if (this.listPrintAddCheck.Count != 0)
            {

            }
            else
            {
                MessageBox.Show("��� ������ ��� ������");
            }

            //Dictionary<string, Unload> lTest = this.listPrintAddCheck;

            //string iTest = "Test";
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            //���������� ��� �������� ����� ��������� ��������� ��������
            decimal sum��������� = 0.0m;

            //���������� ��� �������� ����� �� ��������� ��������
            decimal sum�������� = 0.0m;

            foreach(DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["����������������"].Value) == true)
                {
                   string s =  row.Cells["SumService"].Value.ToString();
                   string sum = s.Replace("�.", " ");

                   decimal sumTest = decimal.Parse(sum.Trim());
                   sum��������� = Math.Round(Math.Round(sum���������, 2) + sumTest);
                }
                else
                {
                    string s = row.Cells["SumService"].Value.ToString();
                    string sum = s.Replace("�.", " ");

                    decimal sumTest = decimal.Parse(sum.Trim());
                    sum�������� = Math.Round(Math.Round(sum��������, 2) + sumTest);
                }
            }

            FormSum fSum = new FormSum();
            fSum.Sum��������� = sum���������;
            
            fSum.Sum����������� = sum��������;
            fSum.ShowDialog();


        }

        private void �������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //��������� ����� �������
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells["�������������"].Value = false;
            }
        }

        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //��������� ����� �������
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells["������������"].Value = false;
            }
        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //��������� ����� �������
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells["����������������"].Value = false;
            }
        }

        private void btnLK_Click(object sender, EventArgs e)
        {

            // ������ ��� ����������.
            listPrintStatistic = new Dictionary<string, Unload>();

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {

                //��������� ��������
                if (Convert.ToBoolean(row.Cells["����������������"].Value) == true && Convert.ToBoolean(row.Cells["������������"].Value) == true)
                {
                    string numberDog = row.Cells["�������������"].Value.ToString().Trim();
                    Unload unloadSave = �����������������������[numberDog];

                    //������� � �������
                    //this.listPrintSave.Add(numberDog, unloadSave);
                    this.listPrintStatistic.Add(numberDog, unloadSave);
                }
            }

            // ������ ��� �������� �������� ���������.
            List<��������������������������> listStat = new List<��������������������������>();

            //Dictionary<string, Unload> list = this.listPrintStatistic;

            //foreach (Unload item in list.Values)
            foreach (Unload item in listPrintStatistic.Values)
            {
                �������������������������� it = new ��������������������������();
                it.����������������� = item.�����������������.Trim();

                it.������������� = item.�������.Rows[0]["�������������"].ToString().Trim();

                it.������� = item.��������.Rows[0]["�������"].ToString().Trim();
                it.��� = item.��������.Rows[0]["���"].ToString().Trim();
                it.�������� = item.��������.Rows[0]["��������"].ToString().Trim();

                decimal sum = 0.0m;

                foreach(DataRow rSum in item.����������������.Rows)
                {
                    sum += Convert.ToDecimal(rSum["�����"]);
                }

                it.������������������������ = sum;

                listStat.Add(it);
            }

            // ������� �� ������.
            WordLetterStatistic word = new WordLetterStatistic(listStat);
            word.PrintDoc();


            var asd = "asd";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                // ������� ����� �������.
                row.Cells["�������������"].Value = true;
                row.Cells["������������"].Value = true;
                row.Cells["����������������"].Value = true;
            }
        }

       
                
    }
}