using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using DantistLibrary;
using ControlDantist.Classes;


namespace ControlDantist
{
    public partial class MainForm : Form
    {
        //���������� ������ id ������������ ������� ����������� ���� �������
        private int idHosp;
        private string �������� = string.Empty;
        private decimal ���� = 0.0m;

        //���������� ������ ����� � ��� ��� ������ �������� ������
        //���� false �� ������ ���, ���� true �� ������ �������� ������
        private bool errorFlag�������� = false;
        private bool errorFlag���� = false;
        private bool errorFlag��������������� = false;

        //���� ������ ��������� ������ � ������� �������
        private bool error������ = false;

        //��������� ������ �������� ����� ����� ��� ������� ���������
        private decimal ������������������� = 0.0m;

        //���������� ������ ��������� ��������� ����� �� ������� ���������
        private decimal error������������������� = 0.0m;

        //���������� ������ �������� ����� �� �������
        private decimal ���������������� = 0.0m;

        //���������� ������ �������� �������� ���������
        private string ����������������� = string.Empty;

        //���������� ��� �������� ��������� �������� ���������
        Dictionary<string, Unload> unload;

        public MainForm()
        {
            InitializeComponent();
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.r";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

               //SerializableObject objToSerialize = null;
                FileStream fstream = File.Open(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                List<Unload> unload = (List<Unload>)binaryFormatter.Deserialize(fstream);

                ////�������� ������ ������� �������� ����������� � �������
                List<ErrorReestr> list = new List<ErrorReestr>();

                //�������� ������ ������� �������� ������ ������� ������ �������� 
                List<ReestrControl> listControlReestr = new List<ReestrControl>();

                //������� ���������� � �� �� �������
                using(SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    //�������� �� � ������ ����������
                    SqlTransaction transact = con.BeginTransaction();

                    //�������� ���������� �� ������� ����������� ��������� � ����������� ���������� � ���� ������
                    //����� ���������� �������� ����� ��������� �������� ���� ����� � �� ���������, ��� � ������ �������
                    //����������� �������� ���������

                    //������� ���������� ������� � �������� � ���� ������
                    foreach (Unload un in unload)
                    {
                        //������� ��� ���������
                        DataRow row�������� = un.��������.Rows[0];

                        string ������� = row��������["�������"].ToString();
                        string ��� = row��������["���"].ToString();
                        string �������� = row��������["��������"].ToString();

                        //
                        string ��� = ������� + " " + ��� + " " + ��������;

                        //������� ������ �� �������� ��������
                        DataRow rowContr = un.�������.Rows[0];

                        //������� ����� ��������
                        string numContr = rowContr["�������������"].ToString().Trim();

                        //������� ���� �� ����� ������� � ����� ��
                        string queryDog = "select ������������� from dbo.������� where ������������� = '" + numContr + "' ";

                        //������� ����� �������� � ����� ����
                        DataTable TabDogS = ���������.GetTableSQL(queryDog, "�������", con, transact);

                        //���� ������� ������� ���������� � ��
                        if (TabDogS.Rows.Count != 0)
                        {
                            //������� ������ � ������� �������������������
                            DataTable tab = un.�������������������;

                            //������� ������ �� ���� ����������� ����� � �� �� ������

                        }
                        //else
                        //{
                        //    //���� ������� ������� � �� �����������
                        //    MessageBox.Show("������� � = " + numContr + " � ���� ������ ����������");
                        //}



                    }

                    #region ������ ����������
                    //������� ���������� ������� � �������� � ���� ������
                    //foreach (Unload un in unload)
                    //{
                    //    ////�������� ��������� ������� ��� �������� ��������� ����������
                    //    //ErrorsReestrUnload error = new ErrorsReestrUnload();

                    //    ErrorReestr errorReestr = new ErrorReestr();

                    //    //�������� ��������� ������� ��� �������� ������ ������� �� ��������� ������ ������� ��������� ��������
                    //    ReestrControl rControl = new ReestrControl();

                    //    //������� ��� ��������� �������� ������� � ������ ������ � ������
                    //    DataRow row�������� = un.��������.Rows[0];

                    //    string ������� = row��������["�������"].ToString();
                    //    string ��� = row��������["���"].ToString();
                    //    string �������� = row��������["��������"].ToString();

                    //    //������� � ������ ��� �������� ���������
                    //    errorReestr.��� = ������� + " " + ��� + " " + ��������;

                    //    //������� ��� ���������
                    //    rControl.��� = ������� + " " + ��� + " " + ��������;

                    //    //������� ���� � ����� �������� �� �������� �����
                    //    DataRow rowControlReestr������� = un.�������.Rows[0];

                    //    //������� ����� ������������ � ����� ��������
                    //    string ������������� = rowControlReestr�������["�������������"].ToString();

                    //    //������� ���� ��������
                    //    string ������������ = Convert.ToDateTime(rowControlReestr�������["������������"]).ToShortDateString();

                    //    //������� ���� � ����� �������� � ������
                    //    rControl.����������������� = ������������� + " " + ������������;

                    //    //������� ���� � ����� ���� ��������� �����
                    //    DataRow rowControlReestr��� = un.�������������������.Rows[0];

                    //    //������� ����� ���� 
                    //    string ��������� = rowControlReestr���["���������"].ToString();

                    //    //������� ���� ���� ��������� �����
                    //    string �������� = Convert.ToDateTime(rowControlReestr���["��������������"]).ToShortDateString();

                    //    //������� � ������ ����� � ���� ���� ��������� ����� 
                    //    rControl.����������������������� = ��������� + " " + ��������;

                    //    //������� ����� � ����� ��������� � ����� �� ������
                    //    DataRow row����������� = un.��������.Rows[0];

                    //    //����� ���������
                    //    string ����� = row�����������["��������������"].ToString();

                    //    //������� ����� ������������ � ����� ��������
                    //    string �������������� = row�����������["��������������"].ToString();

                    //    //������� ���� ��������
                    //    string ������������� = Convert.ToDateTime(row�����������["�������������������"]).ToShortDateString();

                    //    rControl.�������������� = ����� + " " + �������������� + " " + �������������;

                    //    //������� �������� �������� ���������
                    //    ����������������� = un.�����������������;


                    //    //�������� ������ ������� �������� ����������� � �������
                    //    List<ErrorsReestrUnload> listError = new List<ErrorsReestrUnload>();

                    //    //������ ����� ������������ ����������� ���� �������
                    //    DataRow rowHosp = un.������������.Rows[0];

                    //    //������� id ������������
                    //    string queryIdHosp = "select id_������������ from dbo.������������ where ��� = " + rowHosp["���"].ToString() + " ";

                    //    SqlCommand com = new SqlCommand(queryIdHosp, con);
                    //    com.Transaction = transact;
                    //    SqlDataReader read = com.ExecuteReader();

                    //    while (read.Read())
                    //    {
                    //        idHosp = Convert.ToInt32(read["id_������������"]);
                    //    }

                    //    read.Close();

                    //    //������� ������ ����� �� �������� ��������
                    //    DataTable tab������� = un.����������������;
                    //    foreach (DataRow rowDog in tab�������.Rows)
                    //    {
                    //        //�������� ��������� ������� ��� �������� ��������� ���������� ��� ����������� ���������
                    //        ErrorsReestrUnload error = new ErrorsReestrUnload();

                    //        string linkText = "'%" + rowDog["������������������"].ToString() + "%'"; 
                    //        //�������� �������� ���� ����� � ��������� ������� ������� � ����� �������
                    //        string queryViewServices = "select ���������,���� from dbo.�������� " +
                    //                                   "where id_������������ = " + idHosp + " and ��������� like "+ linkText +" ";

                    //        SqlCommand comViewServ = new SqlCommand(queryViewServices, con);
                    //        comViewServ.Transaction = transact;
                    //        SqlDataReader readViewServ = comViewServ.ExecuteReader();

                    //        //������� �������� ������ � ��������� ������� ��������� � ��� �� �������
                    //        while (readViewServ.Read())
                    //        {
                    //            �������� = readViewServ["���������"].ToString().Trim();
                    //            ���� = Convert.ToDecimal(readViewServ["����"]);
                    //        }
                            
                    //        //������� DataReader
                    //        readViewServ.Close();

                    //        //������ ������� ��� ����� � ���� � ��� ����� � ����� �������
                    //        if (rowDog["������������������"].ToString().Trim() == ��������.Trim())
                    //        {
                    //            //������ ���
                    //            errorFlag�������� = false;

                    //        }
                    //        else
                    //        {
                    //            //������
                    //            errorFlag�������� = true;

                    //            //������� ���������� ������������
                    //            error.������������������ = ��������.Trim();

                    //            //������� ������
                    //            error.Error������������������ = rowDog["������������������"].ToString().Trim();

                    //        }

                    //        //������ ������� ��������� ������
                    //        if (Convert.ToDecimal(rowDog["����"]) == ����)
                    //        {
                    //            //������ ���
                    //            errorFlag���� = false;
                    //        }
                    //        else
                    //        {
                    //            //������
                    //            errorFlag���� = true;

                    //            //������� ������������ ������ 
                    //            error.������������������ = ��������.Trim();


                    //            //������� �������
                    //            error.���� = ����;
                    //            error.Error���� = Convert.ToDecimal(rowDog["����"]);
                    //        }

                    //        //������ �������� ��������� �� ��������� ����� ��������� ����� �� ������� ���� ������
                    //        int ���������� = Convert.ToInt32(rowDog["����������"]);

                    //        //���������� ����������� ����� ��������� �����
                    //        decimal ����� = Math.Round((Math.Round(����, 2) * ����������), 2);

                    //        //���������� �������� ����� ����� ��� ����������� ���������
                    //        ������������������� = Math.Round((������������������� + �����), 2);

                    //        //���������� ����� � ����� �������� ��� ����������� �������
                    //        error������������������� = Math.Round((error������������������� + Convert.ToDecimal(rowDog["�����"])), 2);

                    //        //������ ����� �� ������
                    //        if (Convert.ToDecimal(rowDog["�����"]) == �����)
                    //        {
                    //            //������ ���
                    //            errorFlag��������������� = false;
                    //        }
                    //        else
                    //        {
                    //            //������
                    //            errorFlag��������������� = true;

                    //            //������� �������
                    //            error.����� = �����;
                    //            error.Error����� = Convert.ToDecimal(rowDog["�����"]);

                    //        }

                    //        //������� ��������� ������� � ��� ���������
                    //        if (errorFlag�������� == false && errorFlag���� == false && errorFlag��������������� == false)
                    //        {
                    //            //������ � ������ ���� ����� �� ���������
                                
                    //        }
                    //        else
                    //        {
                    //            //��������� ������ � �� �������� ���� ������� � ������ 
                    //            error������ = true;

                    //            //������� ��������� �������� ������� ������ � ������ ������
                    //            listError.Add(error);
                    //        }

                            
                    //    }

                       

                    //    //������� ��������� ����� �� ����� � �� ������� � ��
                    //    if (������������������� == error�������������������)
                    //    {
                    //         //������� ��������� �����
                    //        rControl.�������������� = �������������������.ToString();
                    //    }
                    //    else
                    //    {
                    //        errorReestr.������������������������ = �������������������;
                    //        errorReestr.Error������������������������ = error�������������������;
                    //    }

                    //    //������� � ������ ������ ���������� ������ � ������� ���������� �����������
                    //    errorReestr.ErrorList������ = listError;

                    //    if (error������ == true)
                    //    {
                    //        //������� � ������ � �������� �������� ��������� �� ����� �������������
                    //        list.Add(errorReestr);
                    //    }

                    //    listControlReestr.Add(rControl);
                    //}
                    #endregion

                }
                //fstream.Close();

                //sr.Close();
               // string iTest = "test";

                if (error������ == true)
                {
                    //������� ������ 
                    //MessageBox.Show("������� ���������� �� �������");

                    List<ErrorReestr> listTest = list;

                    //���������� ������ ��������� ���������� ������
                    PrintReestrError printReest = new PrintReestrError(list, �����������������);
                    printReest.Print();
                    
                }
                else
                {
                    //������� ���������� �� ����� � ���� ��������� word
                    List<ReestrControl> lTest = listControlReestr;

                    //����������� ������ ���������� ����������� ����������
                    PrintReestrControl printReestr = new PrintReestrControl(listControlReestr, fileName, �������������������.ToString(), �����������������);
                    printReestr.Print();

                    //������� � ��
                    //MessageBox.Show("���������� � ��");
                    WriteBD writBD = new WriteBD(unload);
                    writBD.Write();
                }


            }

        }

        private void ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FirstLoadHospital flHosp = new FirstLoadHospital();
            flHosp.ShowDialog();

            if (flHosp.DialogResult == DialogResult.OK)
            {

            }

            if (flHosp.DialogResult == DialogResult.Cancel)
            {
                flHosp.Close();
            }
        }

        private void �������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form������������� form = new Form�������������();
            form.MdiParent = this;
            form.Show();
        }

        private void �������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //������� �� ����� ������� � ����������
            //Dictionary<string, Unload> unload;

            //���������� ������������
            int iConfig;
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.r";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                //SerializableObject objToSerialize = null;
                FileStream fstream = File.Open(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //������� �� ����� ������� � ����������
                unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);

                //������� �������� �����
                fstream.Close();

                //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
                Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;


                ////�������� ������ �������� ��� �������� ����������� �������� �������� ���������
                //Dictionary<string, ValidateContract> listValContr = new Dictionary<string, ValidateContract>();

                //Validate���� ���� = new Validate����(unload,listValContr);
                //Dictionary<string, ValidateContract> validReestr = ����.Validate();

                //������� ��������� �������� �� �����

                //������� ���������� ����
                openFileDialog1.Dispose();

            }
            else
            {
                //���� ������������ ����� ������ ������ �� ������� �� ������
                return;
            }


            ////������� ���������� ����
            //openFileDialog1.Dispose();

            ////��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
            //Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;


            //�������� ������ �����
            //Thread t = new Thread(Display);
            //t.Start();
            //t.Join();

            //�������� ������ �������� ��� �������� ����������� �������� �������� ���������
            Dictionary<string, ValidateContract> listValContr = new Dictionary<string, ValidateContract>();

            //������� ������������ ������ ��������� � ���� ������ ����
            using (FileStream fs = File.OpenRead("Config.dll"))
            using (TextReader read = new StreamReader(fs))
            {
                iConfig = Convert.ToInt32(read.ReadLine().Trim());
                fs.Close();
                read.Close();
            }

            
            ////string querySelect = "select configSearch from ConfgSearch";

            ////SqlConnection con = new SqlConnection(ConnectDB.ConnectionString());
            ////SqlCommand com = new SqlCommand(querySelect,con);

            ////int iConfig = 0;

            ////con.Open();
            ////SqlDataReader read = com.ExecuteReader();
            ////while(read.Read())
            ////{
            ////   iConfig = Convert.ToInt32(read["configSearch"]);  
            ////}
            ////con.Close();

            //this.progressBar1.Value = 15;

            //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;


            //�������� ���������� �� ���� ������ � ����
            Validate���� ���� = new Validate����(unload, listValContr, iConfig);
            Dictionary<string, ValidateContract> validReestr = ����.Validate();

            //������� ��������� �������� �� �����
            FormValidOut formOutVal = new FormValidOut();

            //��������� � ����� ����������� ��������
            formOutVal.���������������� = validReestr;

            //��������� � ���������� �������� ���������
            formOutVal.����������������������� = unload;

            //������� ����� �� ������ �����
            //formOutVal.TopMost = true;
           
            
            formOutVal.Show();

        }

        private void ��������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfigSearch formCS = new FormConfigSearch();
            formCS.MdiParent = this;
            formCS.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form�������� f = new Form��������();
            f.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //������� ���������� �� ���� ���������
            FileStream fs = File.Open("Config.dll", FileMode.OpenOrCreate);
            if (fs.Length != 0)
            {
                //��������� �� ����� ��������� �� ��������
                ;
            }
            else
            {
                //������ ���� � ���������� ��������� �������� ��������
                using (TextWriter writ = new StreamWriter(fs))
                {
                    writ.WriteLine("4");
                }
            }
            fs.Close();

        }

        //private void Display()
        //{
        //    MessageBox.Show("����������� ��������");
        //}
    }
}