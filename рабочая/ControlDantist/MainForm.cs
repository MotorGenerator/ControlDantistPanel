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
using System.Linq;
using System.Data.OleDb;

using Microsoft.Office.Interop.Word;
using DantistLibrary;
using ControlDantist.Classes;

using System.Globalization;
using System.Threading;

using ControlDantist.ClassValidRegions;
using ControlDantist.Repozirories;

using ControlDantist.Repository;
using ControlDantist.ReceptionDocuments;
using ControlDantist.ValidateRegistrProject;

using ControlDantist.WriteClassDB;
using ControlDantist.Find;
using ControlDantist.FindEsrnWoW;
using ControlDantist.DataBaseContext;
using ControlDantist.ReadRegistrProject;
using ControlDantist.ValidPersonContract;
using ControlDantist.RenameFile;
using MyTask = System.Threading.Tasks;
using ControlDantist.MedicalServices;


namespace ControlDantist
{
    public partial class MainForm : Form
    {
        //���������� ������ id ������������ ������� ����������� ���� �������
        private int idHosp;
        private string �������� = string.Empty;
        private decimal ���� = 0.0m;
        private decimal ����������� = 0.0m;

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

        /// <summary>
        /// ��������� ��������� ��� ����
        /// </summary>
        private bool flag���������� = false;

        /// <summary>
        /// ���� ��������� ���������� ������� ��� ���.
        /// </summary>
        public bool FlagConnectServer { get; set; }

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
            //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            //openFileDialog1.Filter = "|*.r";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                //SerializableObject objToSerialize = null;
                FileStream fstream = File.Open(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //������� �������� ������� ����������� ���������
                List<Unload> unloads = (List<Unload>)binaryFormatter.Deserialize(fstream);

                // ������� �����.
                fstream.Dispose();

                // ��������� �� �������� ��������.
                foreach (var unl in unloads)
                {
                    // ������� ������� � ���������.
                    DataTable numDog = unl.�������;

                    foreach (DataRow row in numDog.Rows)
                    {
                        if (row[1].ToString() == "8/6815")
                        {
                            string iTest = "test";
                        }
                    }

                    var itemUnload = unl;
                }

                IEnumerable<Unload> unloadS = unloads.Where(un => un.������������������� != null).Select(un => un).ToList();

                List<Unload> unload = new List<Unload>();

                unload.AddRange(unloadS);

               // ������ �������� � ���������.
                List<Unload> iUnload = unload;

                string queryFlag = "select [��������������] from [Table��������������������������] " +
                                 "WHERE idConfig = 1";

                int intFlag = Convert.ToInt32(���������.GetTableSQL(queryFlag, "TabConfig").Rows[0]["��������������"]);

                if (intFlag == 1)
                {
                    Form������������������������� formCorrect = new Form�������������������������();
                    formCorrect.ListUnload = unload;
                    formCorrect.Show();

                    return;
                }

                ////�������� ������ ������� �������� ����������� � �������
                List<ErrorReestr> list = new List<ErrorReestr>();

                //�������� ������ ������� �������� ������ ������� ������ �������� 
                List<ReestrControl> listControlReestr = new List<ReestrControl>();

                //������� � ������������ ��������
                Dictionary<string, DisplayReestr> listReest = new Dictionary<string, DisplayReestr>();

                //������� ���������� � �� �� �������
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    //�������� �� � ������ ����������
                    SqlTransaction transact = con.BeginTransaction();

                    //������� ����� ��������� �����
                    ������������������� = 0.0m;

                    //������� ������ � ��������
                    list.Clear();

                    listReest.Clear();

                    listControlReestr.Clear();

                    #region ������ ����������
                    //������� ���������� ������� � �������� � ���� ������

                    foreach (Unload un in unload)
                    {
                        ErrorReestr errorReestr = new ErrorReestr();

                        string ��� = un.��������.Rows[0][1].ToString();
                        if (��� == "�������")
                        {
                            string s = "test";
                        }

                        //�������� ��������� ������� ��� �������� ������ ������� �� ��������� ������ ������� ��������� ��������
                        ReestrControl rControl = new ReestrControl();

                        //������� ��� ��������� �������� ������� � ������ ������ � ������
                        DataRow row�������� = un.��������.Rows[0];

                        string ������� = row��������["�������"].ToString().Trim();

                        string ��� = row��������["���"].ToString().Trim();
                        string �������� = row��������["��������"].ToString().Trim();

                        //������� ���� ��������
                        string query������������ = "";

                        //������� � ������ ��� �������� ���������
                        errorReestr.��� = ������� + " " + ��� + " " + ��������;

                        //������� ��� ���������
                        rControl.��� = ������� + " " + ��� + " " + ��������;

                        //������� ���� � ����� �������� �� �������� �����
                        DataRow rowControlReestr������� = un.�������.Rows[0];

                        //������� ����� ������������ � ����� ��������
                        string ������������� = rowControlReestr�������["�������������"].ToString();

                        //������� �� ������� ������ �� �������� � ������� �� � ���������� ������� � � ������� �� ���������

                        //������� ���� ��������
                        string ������������ = Convert.ToDateTime(rowControlReestr�������["������������"]).ToShortDateString();


                        //������� ���� � ����� �������� � ������
                        rControl.����������������� = ������������� + " " + ������������;

                        //������� ���� � ����� ���� ��������� �����
                        DataRow rowControlReestr��� = un.�������������������.Rows[0];

                        //������� ����� ���� 
                        string ��������� = rowControlReestr���["���������"].ToString();

                        //������� ���� ���� ��������� �����
                        string �������� = Convert.ToDateTime(rowControlReestr���["��������������"]).ToShortDateString();

                        //�������� ��������� ������ DisplayReestr
                        DisplayReestr dispR = new DisplayReestr();
                        dispR.��������� = ��������;

                        //������� ����� ���� ����������� �����
                        rControl.����� = ���������.Trim();

                        //������� � ������ ����� � ���� ���� ��������� ����� 
                        rControl.����������������������� = ��������� + " " + ��������;

                        //������� ����� � ����� ��������� � ����� �� ������
                        DataRow row����������� = un.��������.Rows[0];

                        //����� ���������
                        string ����� = row�����������["��������������"].ToString();

                        //������� ����� ������������ � ����� ��������
                        string �������������� = row�����������["��������������"].ToString();

                        //������� ���� ��������
                        string ������������� = Convert.ToDateTime(row�����������["�������������������"]).ToShortDateString();

                        rControl.�������������� = ����� + " " + �������������� + " " + �������������;

                        //������� �������� �������� ���������
                        ����������������� = un.�����������������;

                        //�������� ������ ������� �������� ����������� � �������
                        List<ErrorsReestrUnload> listError = new List<ErrorsReestrUnload>();

                        //������ ����� ������������ ����������� ���� �������
                        DataRow rowHosp = un.������������.Rows[0];

                        //������� id ������������
                        string queryIdHosp = "select id_������������ from dbo.������������ where ��� = '" + rowHosp["���"].ToString() + "' ";

                        SqlCommand com = new SqlCommand(queryIdHosp, con);
                        com.Transaction = transact;
                        SqlDataReader read = com.ExecuteReader();

                        while (read.Read())
                        {
                            idHosp = Convert.ToInt32(read["id_������������"]);
                        }

                        read.Close();

                        /*�������� ��� � �������� �������� 
                         * ��������� ������ ���� ��������, ��� ���, ���������� ������
                         * ��� ����� �������, ��� �����, 
                         * 
                         * */

                        if (�������������.Trim() == "���/56")
                        {
                            string i2Test = "Test";
                        }

                        //�������� id ��������
                        string sid_���Serv = "select top 1 id_������� from ������� where ������������� = '" + �������������.Trim() + "' and ������������ = 'True' ";
                        DataTable tab_idServ = ���������.GetTableSQL(sid_���Serv, "�������", con, transact);

                        //�������� ���������� �� ����� ������� � ��� �� �������
                        if (tab_idServ.Rows.Count != 0)
                        {
                   
                            string s�������� = " SELECT     dbo.��������.�������, dbo.��������.���, dbo.��������.��������, dbo.��������.������������, dbo.��������.�����, dbo.��������.���������, " +
                                                " dbo.��������.������, dbo.��������.�������������, dbo.��������.�������������, dbo.��������.�������������, " +
                                                "dbo.��������.������������������, dbo.��������.���������������, dbo.��������.��������������, dbo.��������.��������������,  " +
                                                "dbo.��������.�������������������, dbo.��������.����������������, dbo.��������.id_�����������������,  " +
                                                "dbo.�����������������.����������������� " +
                                                "FROM         dbo.�������� INNER JOIN " +
                                                "dbo.����������������� ON dbo.��������.id_����������������� = dbo.�����������������.id_����������������� " +
                                                "WHERE     (dbo.��������.id_�������� IN " +
                                                "(SELECT     id_�������� " +
                                                "FROM          dbo.������� " +
                                                "WHERE ( LOWER(LTRIM(RTRIM(�������������))) = '" + �������������.Trim().ToLower() + "') AND (������������ = 'True'))) ";


                            DataTable tab_�������� = ���������.GetTableSQL(s��������, "��������", con, transact);

                            foreach (DataRow rowFIO in tab_��������.Rows)
                            {
                                if (������� != rowFIO["�������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                if (��� != rowFIO["���"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                if (�������� != rowFIO["��������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //���� ���������
                                string ������� = Convert.ToDateTime(row��������["������������"]).ToShortDateString();
                                if (������� != Convert.ToDateTime(rowFIO["������������"]).ToShortDateString())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //�����
                                string ����� = row��������["�����"].ToString().Trim();
                                if (����� != rowFIO["�����"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //����� ����
                                string ��������� = row��������["���������"].ToString().Replace("'", string.Empty).Trim();
                                if (��������� != rowFIO["���������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //������
                                string ������ = row��������["������"].ToString().Trim();
                                if (������ != rowFIO["������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //����� ��������
                                string numApartment = row��������["�������������"].ToString().Trim();
                                if (numApartment != rowFIO["�������������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //����� ��������
                                string ������������� = row��������["�������������"].ToString().Trim();
                                if (������������� != rowFIO["�������������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //����� ��������
                                string serPassport = row��������["�������������"].ToString().Trim();
                                if (serPassport != rowFIO["�������������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //������������������
                                //string ������������������ =  row��������["������������������"].ToString().Trim();
                                string ������������������ = Convert.ToDateTime(row��������["������������������"]).ToShortDateString();
                                //if (������������������ != rowFIO["������������������"].ToString().Trim())
                                if (������������������ != Convert.ToDateTime(rowFIO["������������������"]).ToShortDateString())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //���������������
                                string ��������������� = row��������["���������������"].ToString().Trim();
                                if (��������������� != rowFIO["���������������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //����� ���������
                                string �������������� = row��������["��������������"].ToString().Trim();
                                if (�������������� != rowFIO["��������������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //����� ���������
                                string num��������� = row��������["��������������"].ToString().Trim();
                                if (num��������� != rowFIO["��������������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //���� ������ ���������
                                //string ������������������� = row��������["�������������������"].ToString().Trim();
                                string ������������������� = Convert.ToDateTime(row��������["�������������������"]).ToShortDateString();//.ToString().Trim();
                                if (������������������� != Convert.ToDateTime(rowFIO["�������������������"]).ToShortDateString())// rowFIO["�������������������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }

                                //����������������
                                string ���������������� = row��������["����������������"].ToString().Trim();
                                if (���������������� != rowFIO["����������������"].ToString().Trim())
                                {
                                    //������
                                    dispR.FlagError = false;

                                    //������ ������������ ������
                                    dispR.ErrorPerson = true;
                                }


                                //���� ������� �������� �����������.
                                if (�������������.Trim() == "���/56")
                                {
                                    //un.����������������� = "������ �������� ���������";
                                    string sTest = "";
                                }

                                if (un.�����������������.ToLower().Replace(" ", string.Empty).Trim() != rowFIO["�����������������"].ToString().Trim().ToLower().Replace(" ", string.Empty))
                                {
                                    // ������ ��� ���������� �� �������� ���������.
                                    dispR.ErrorPrefCategory = true;
                                }

                                // ������� �������� ��������� �� �� � �������� ��������� �� �����.
                                PreferentCategory pc = new PreferentCategory();
                                pc.PActegoryFile = un.�����������������.Trim();
                                pc.PCategoryServer = rowFIO["�����������������"].ToString().Trim();

                                dispR.����������������� = pc;
                            }

                            //������� ���������� ��� �������� ����� ����� � ��� �� �������
                            ����������� = 0.0m;

                            //���������� ��� �������� ����� �� �������� ���������� � ��� �� �������
                            decimal summaServer = 0.0m;

                            //������� ������
                            int iCount = 0;

                            //������� ������ ����� �� �������� �������� �� ����� ��������
                            DataTable tab������� = un.����������������;
                            foreach (DataRow rowDog in tab�������.Rows)
                            {
                                //������� ��������� 
                                �������� = string.Empty;
                                ���� = 0.0m;
                                //����������� = 0.0m;

                                //���������� ������ ����� ������ �� �������;
                                string �������������� = string.Empty;

                                //�������� ��������� ������� ��� �������� ��������� ���������� ��� ����������� ���������
                                ErrorsReestrUnload error = new ErrorsReestrUnload();

                                string linkText = "'" + rowDog["������������������"].ToString() + "'";

                                //������� id �������� ��������
                                int id_������� = Convert.ToInt32(rowDog["id_�������"]);

                                //�������� �� ������������ ����� �� �������� (���� ����� ���)


                                //������� ������ ������� ��
                                DataTable dt������ = un.����������������;


                                //������� �������� ������ � ����� �� ������� � ���� ������� ��������� � ��� �� ������� ��� ������� �������� (���������� �������� �� ���������� �������� ���������)
                                string queryViewServices = "select top 1  ������������������,����,��������������,����� from dbo.���������������� " +
                                                           "where ������������������ = " + linkText + " and id_������� in( " +
                                                           "SELECT [id_�������] " +
                                                           "FROM [�������] " +
                                                           "where [�������������] = '" + �������������.Trim() + "' and ������������ = 'True' ) ";



                                SqlCommand comViewServ = new SqlCommand(queryViewServices, con);
                                comViewServ.Transaction = transact;
                                SqlDataReader readViewServ = comViewServ.ExecuteReader();

                                //���� ������ �� ����������� �� � ����� ����� ������ � ��� �� ������� ������� �� �� ����� ��������
                                if (readViewServ.HasRows == false)
                                {
                                    //������� ���������� ������������
                                    error.Error������������������ = rowDog["������������������"].ToString().Trim();

                                    //������� �� ���������� ����
                                    error.Error���� = Convert.ToDecimal(rowDog["����"]);

                                    //������� �� ���������� �����
                                    error.Error����� = Convert.ToDecimal(rowDog["�����"]);

                                    //������� ��������� �������� ������� ������ � ������ ������
                                    listError.Add(error);
                                }

                                //������� �������� ������ � ��������� ������� ��������� � ��� �� �������
                                while (readViewServ.Read())
                                {
                                    //������� ����������
                                    �������� = string.Empty;
                                    ���� = 0.0m;
                                    �������������� = string.Empty;
                                    //����������� = 0.0m;

                                    �������� = readViewServ["������������������"].ToString().Trim();
                                    ���� = Convert.ToDecimal(readViewServ["����"]);
                                    �������������� = readViewServ["��������������"].ToString().Trim();
                                    //����������� = Convert.ToDecimal(readViewServ["�����"]);

                                    //������� ����� ��� �������� ����� ������� ��������� � ��� �� �������
                                    summaServer = Math.Round(Math.Round(summaServer, 2) + Math.Round(�����������, 2), 2);

                                    if (�������������.Trim() == "����/54")
                                    {
                                        string sTest = "test";
                                    }

                                    //������ ������� ��� ����� � ���� � ��� ����� � ����� �������
                                    if (rowDog["������������������"].ToString().Trim() == ��������.Trim() && rowDog["��������������"].ToString().Trim() == ��������������.Trim())
                                    {
                                        //������ ���
                                        errorFlag�������� = false;

                                        //������� ��� �����
                                        //�������� = string.Empty;

                                        //������� ���������� ������������
                                        error.������������������ = ��������.Trim();

                                        //������� ���������� ������������
                                        error.Error������������������ = rowDog["������������������"].ToString().Trim();

                                        DateService ds = new DateService();
                                        ds.������������ = ��������.Trim();

                                        ds.���� = Convert.ToDecimal(����).ToString("c");

                                    }
                                    else
                                    {
                                        //������
                                        errorFlag�������� = true;

                                        //������� ���������� ������������
                                        error.������������������ = ��������.Trim();

                                        //������� ������
                                        error.Error������������������ = rowDog["������������������"].ToString().Trim();

                                    }

                                    //������ ������� ��������� ������
                                    if (Convert.ToDecimal(rowDog["����"]) == ���� && rowDog["��������������"].ToString().Trim() == ��������������.Trim())
                                    {
                                        //������ ���
                                        errorFlag���� = false;

                                        //error.���� = ����;
                                        //error.Error���� = Convert.ToDecimal(rowDog["����"]);
                                    }
                                    else
                                    {
                                        //������
                                        errorFlag���� = true;

                                        //������� �������
                                        error.���� = ����;
                                        error.Error���� = Convert.ToDecimal(rowDog["����"]);
                                    }

                                    //������ �������� ��������� �� ��������� ����� ��������� ����� �� ������� ���� ������
                                    int ���������� = Convert.ToInt32(rowDog["����������"]);

                                    //���������� ����������� ����� ��������� �����
                                    decimal ����� = Math.Round((Math.Round(����, 2) * ����������), 2);

                                    //���������� �������� ����� ����� ��� ����������� ���������
                                    ������������������� = Math.Round((������������������� + �����), 2);

                                    //������� ���������� ����� ��� �� ���� ������
                                    error������������������� = 0.0m;

                                    //���������� ����� � ����� �������� ��� ����������� �������
                                    error������������������� = Math.Round((error������������������� + Convert.ToDecimal(rowDog["�����"])), 2);

                                    //������ ����� �� ������
                                    //if (Convert.ToDecimal(rowDog["�����"]) == �����)
                                    if (Convert.ToDecimal(rowDog["�����"]) == ����� && Convert.ToDecimal(rowDog["����"]) == ���� && rowDog["��������������"].ToString().Trim() == ��������������.Trim())
                                    {
                                        //������ ���
                                        errorFlag��������������� = false;
                                    }
                                    else
                                    {
                                        //������
                                        errorFlag��������������� = true;

                                        //������� �������
                                        error.����� = �����;
                                        error.Error����� = Convert.ToDecimal(rowDog["�����"]);
                                    }

                                    //������� ��������� ������� � ��� ���������
                                    if (errorFlag�������� == false && errorFlag���� == false && errorFlag��������������� == false)
                                    {
                                        //������ � ������ ���� ����� �� ���������
                                    }
                                    else
                                    {
                                        //��������� ������ � �� �������� ���� ������� � ������ 
                                        error������ = true;

                                        //������� ��������� �������� ������� ������ � ������ ������
                                        listError.Add(error);
                                    }

                                }

                                //������� ����� ����� ��� �������� �������� ������� ��������� � ��� �� �������
                                dispR.������������������ = summaServer;

                                //������� DataReader
                                readViewServ.Close();

                                //�������� ������� �� 1
                                iCount++;

                                //������ ����� ��������� ������ ������� �������� listError � rControl
                            }

                            //��������� ���� ����������� ��� ���� ������ � �������� ��� ���
                            if (listError.Count == 0)
                            {
                                //������ ���
                                dispR.FlagError = true;
                            }
                            else
                            {
                                //������� ������ ������
                                dispR.������������ = listError;

                                //���� ��������� ��� ���������� ����������� � ���������� �������
                                dispR.FlagError = false;
                            }

                            //������� ��������� ����� �� ����� � �� ������� � ��
                            if (������������������� == error�������������������)
                            {
                                //������� ��������� �����
                                rControl.�������������� = �������������������.ToString();
                            }
                            else
                            {
                                errorReestr.������������������������ = �������������������;
                                errorReestr.Error������������������������ = error�������������������;
                            }

                            //������� � ������ ������ ���������� ������ � ������� ���������� �����������
                            errorReestr.ErrorList������ = listError;

                            if (error������ == true)
                            {
                                //������� � ������ � �������� �������� ��������� �� ����� �������������
                                list.Add(errorReestr);
                            }

                            listControlReestr.Add(rControl);

                            dispR.�������� = rControl;

                            //������� ����� ����
                            dispR.Sum = Math.Round(�������������������, 2);

                            //��������� ������ ���������� ����� �� �������
                            int countService = 0;

                            //���������� ������ ���������� ����� � ����� ��������
                            int countServiceFile = 0;


                            //�������� ���� �� ����������� � ����������� ����� �� ������� � � ����� ��������

                            //������� ���������� ����� � ��� �� ������� ��� �������� ��������
                            string queryCountServices = "select * from dbo.���������������� " +
                                                        "where id_������� in (select top 1 id_������� from dbo.������� where ������������� = '" + �������������.Trim() + "' and ������������ = 'True' )";//  and ��������������� = 'True')";

                            //������� �������� ������ ������� � ��� �� ������� 
                            DataTable tab = ���������.GetTableSQL(queryCountServices, "��������������������������", con, transact);

                            //���� ���������� ����� �� ������� � � ����� �� ����� 0
                            //if (tab.Rows.Count != 0 && un.����������������.Rows.Count != 0)
                            //{

                            //���� ����������� ����� � �� ������� � � ����� �������� ����������
                            if (tab.Rows.Count == un.����������������.Rows.Count)
                            {
                                List<ErrorsReestrUnload> listR = new List<ErrorsReestrUnload>();

                                //������ ������ ���� ���������� �� �������
                                List<ErrorsReestrUnload> listServcS = new List<ErrorsReestrUnload>();


                                //�������� ����� ������ �� ������� � �������� ����������� �� �������
                                foreach (DataRow rowS in tab.Rows)
                                {
                                    //�������� ��������� ��� �������� ������ � �������
                                    ErrorsReestrUnload itemS = new ErrorsReestrUnload();

                                    itemS.������������������ = rowS["������������������"].ToString().Trim();
                                    itemS.���� = Math.Round(Convert.ToDecimal(rowS["����"]), 2);
                                    itemS.����� = Math.Round(Convert.ToDecimal(rowS["�����"]), 2);
                                    itemS.FlagWrite = false;
                                    itemS.���������� = Convert.ToInt32(rowS["����������"]);
                                    listServcS.Add(itemS);
                                }

                                //������ ������ ���� ���������� � �����
                                List<ErrorsReestrUnload> listServcF = new List<ErrorsReestrUnload>();

                                //�������� ����� ������ �� ������� � �������� ����������� �� �������
                                foreach (DataRow rowF in un.����������������.Rows)
                                {
                                    //�������� ��������� ��� �������� ������ � �������
                                    ErrorsReestrUnload itemF = new ErrorsReestrUnload();

                                    itemF.Error������������������ = rowF["������������������"].ToString().Trim();
                                    itemF.Error���� = Math.Round(Convert.ToDecimal(rowF["����"]), 2);
                                    itemF.Error����� = Math.Round(Convert.ToDecimal(rowF["�����"]), 2);
                                    itemF.FlagWrite = false;
                                    itemF.���������������� = Convert.ToInt32(rowF["����������"]);
                                    listServcF.Add(itemF);
                                }

                                List<ErrorsReestrUnload> test1 = listServcS;
                                List<ErrorsReestrUnload> test2 = listServcF;

                                //�������� �� ���������
                                foreach (ErrorsReestrUnload itemF in listServcF)
                                {
                                    string iTest = itemF.Error������������������;

                                    if (iTest == "��������� �������" && �������������.Trim() == "�������/216")
                                    {
                                        string aTest = "Test";
                                    }

                                    foreach (ErrorsReestrUnload itemS in listServcS)
                                    {
                                        //==========���� �������==========
                                        string iiTest = itemS.������������������;


                                        if (iiTest == "��������� �������" && �������������.Trim() == "�������/216")
                                        {
                                            string aTest = "Test";
                                        }
                                        //=======����� �����==============

                                        if (itemF.Error������������������.Trim() == itemS.������������������.Trim() && itemF.Error���� == itemS.���� && itemF.Error����� == itemS.����� && itemF.FlagWrite != true && itemS.FlagWrite != true)
                                        {
                                            //���� ������������ ������ ��������� � ����� ����� � �� ������� � � ����� �� ������ ���� � true
                                            itemF.FlagWrite = true;
                                            itemS.FlagWrite = true;
                                        }
                                    }
                                }


                                //�������� ��������� � ������� �������� ���������� ������� � ������� ���� FlagWrite == false
                                List<ErrorsReestrUnload> listErrors = new List<ErrorsReestrUnload>();
                                foreach (ErrorsReestrUnload itemF in listServcF)
                                {
                                    if (itemF.FlagWrite == false)
                                    {
                                        listErrors.Add(itemF);
                                    }
                                }



                                //�������� ����������� ������ ��� �������
                                List<ErrorsReestrUnload> listServer = new List<ErrorsReestrUnload>();
                                foreach (ErrorsReestrUnload itemS in listServcS)
                                {
                                    if (itemS.FlagWrite == false)
                                    {
                                        listServer.Add(itemS);
                                    }
                                }

                                //������ ������� ��� ���������
                                if (listErrors.Count == listServer.Count)
                                {
                                    foreach (ErrorsReestrUnload er in listErrors)
                                    {
                                        if (er.FlagWrite == false)
                                        {
                                            ErrorsReestrUnload eru = new ErrorsReestrUnload();
                                            eru.Error������������������ = er.������������������;

                                            eru.Error���� = er.����;
                                            eru.Error����� = er.�����;
                                            eru.���������������� = er.����������������;

                                            listR.Add(er);
                                        }
                                    }

                                    int iCountR = 0;
                                    foreach (ErrorsReestrUnload lo in listServer)
                                    {
                                        if (lo.FlagWrite == false)
                                        {
                                            listR[iCountR].������������������ = lo.������������������;
                                            listR[iCountR].���� = lo.����;
                                            listR[iCountR].����� = lo.�����;
                                            listR[iCountR].���������� = lo.����������;
                                        }

                                        iCountR++;
                                    }
                                }

                                //�������� ���� ������
                                if (listR.Count == 0)
                                {
                                    //������ ���
                                    dispR.FlagError = true;
                                }
                                else
                                {
                                    //������
                                    //������ ���
                                    dispR.FlagError = false;
                                }

                                dispR.������������ = listR;
                                ////===============================================================
                                // }

                                decimal summAct = 0.0m;

                                //������� ����� ���� ����������� ����� �������������� ������ ������� ��������� � ����� �������� ������� ����������� �����
                                foreach (DataRow row in un.����������������.Rows)
                                {
                                    summAct = Math.Round(Math.Round(summAct, 2) + Math.Round(Convert.ToDecimal(row["�����"]), 2), 2);
                                }

                                dispR.������������������������� = summAct;

                                //���������� ����� ����� ���������� � ��� �� ������� ��� �������� ��������
                                string queryViewServices = "select top 1 Sum(�����) as '�����' from dbo.���������������� " +
                                                           "where id_������� in( " +
                                                           "SELECT top 1 [id_�������] " +
                                                           "FROM [�������] " +
                                                           "where [�������������] = '" + �������������.Trim() + "' and  ������������ = 'True' ) ";

                                DataTable tabSumServer = ���������.GetTableSQL(queryViewServices, "����������������������", con, transact);

                                if (tabSumServer.Rows[0]["�����"] != DBNull.Value)
                                    dispR.������������������ = Math.Round(Convert.ToDecimal(tabSumServer.Rows[0]["�����"]));


                                #region ������ ����������
                                ///*
                                // * ��� ��� ������ ������� � ��� �� ������� ��� ��� �������� � ������ ����������� listError
                                // */
                                ////�������� � ���������� ��� ��� ��� �� ��������� (������ ��� ����� � ��� �� ������)

                                //////������ ������ �� ������������ ����� � ���� ������� ������������ �������� ��������
                                //string queryDictionary = "select id_�������������,������������������,����,��������������,����� from dbo.���������������� " +
                                //                         "where id_������� in( " +
                                //                         "SELECT [id_�������] " +
                                //                         "FROM [�������] " +
                                //                         "where [�������������] = '" + �������������.Trim() + "') ";

                                //List<ErrorsReestrUnload> listTest2 = listError;



                                ////������� ������� ���������� ������  ��� �������� �������� ���������� � ��� �� �������
                                //DataTable tabServ = ���������.GetTableSQL(queryDictionary, "�������������", con, transact);

                                //DataTable tabFile = un.����������������;

                                //////�������� ���������� ���������� ������ ���������� � ��� �� �������
                                //Dictionary<string, DateService> dicServer = new Dictionary<string, DateService>();

                                //foreach (DataRow row in tabServ.Rows)
                                //{
                                //    DateService ds = new DateService();
                                //    ds.������������ = row["������������������"].ToString().Trim();

                                //    ds.���� = Convert.ToDecimal(row["����"]).ToString("c");
                                //    ds.����� = Convert.ToDecimal(row["�����"]).ToString("c");

                                //    dicServer.Add(row["id_�������������"].ToString().Trim(), ds);
                                //}

                                //if (�������������.Trim() == "8/324")
                                //{
                                //    string sTest = "test";
                                //}

                                ////������ �� ���������� ������ ������� ���� � �� ������� � � ����� ��������
                                //foreach (DataRow rowS in tabServ.Rows)
                                //{
                                //    foreach (DataRow rowF in tabFile.Rows)
                                //    {
                                //        if (rowS["������������������"].ToString().Trim() == rowF["������������������"].ToString().Trim())
                                //        {
                                //            string s_id = rowS["id_�������������"].ToString().Trim();

                                //            dicServer.Remove(s_id);
                                //            //break;

                                //            goto Found;

                                //        }
                                //    }

                                //Found:
                                //    continue;
                                //}

                                //if (�������������.Trim() == "8/324")
                                //{
                                //    string sTest = "test";
                                //}

                                //Dictionary<string, DateService> dTest = dicServer;

                                //int iCount2 = 0;
                                //foreach (DateService ds in dicServer.Values)
                                //{
                                //    listError[iCount2].������������������ = ds.������������;


                                //    //string s���� = ds.����.Replace(',', '.');
                                //    //string ����� = s����.Replace('�',' ');
                                //    string ����� = ds.����.Replace('�', ' ');
                                //    int ����i = �����.Length;
                                //    string ����z = �����.Remove(����i - 1, 1);
                                //    //string ���� = ����z.Replace(',', '.').Trim();

                                //    decimal ����d = decimal.Parse("50,00");

                                //    if (����d != 0.0m)
                                //    {
                                //        listError[iCount2].���� = ����d;// Math.Round(Convert.ToDecimal(����.Trim()), 2);
                                //    }

                                //    string ������ = ds.�����.Replace('�', ' ');
                                //    int �����i = ������.Length;
                                //    string �����z = �����.Remove(�����i - 1, 1);
                                //    //string ����� = �����z.Replace(',', '.');

                                //    decimal �����d = decimal.Parse(�����z);

                                //    if (�����d != 0.0m)
                                //    {
                                //        listError[iCount2].����� = �����d;// Math.Round(Convert.ToDecimal(�����.Trim()), 2);
                                //    }


                                //    iCount2++;
                                //}
                                #endregion

                            }

                            //��������
                            List<ErrorsReestrUnload> listTest = listError;

                            if (tab.Rows.Count > un.����������������.Rows.Count)
                            {
                                //===================================
                                List<ErrorsReestrUnload> listR = new List<ErrorsReestrUnload>();

                                //������ ������ ���� ���������� �� �������
                                List<ErrorsReestrUnload> listServcS = new List<ErrorsReestrUnload>();


                                //�������� ����� ������ �� ������� � �������� ����������� �� �������
                                foreach (DataRow rowS in tab.Rows)
                                {
                                    //�������� ��������� ��� �������� ������ � �������
                                    ErrorsReestrUnload itemS = new ErrorsReestrUnload();

                                    itemS.������������������ = rowS["������������������"].ToString().Trim();
                                    itemS.���� = Math.Round(Convert.ToDecimal(rowS["����"]), 2);
                                    itemS.����� = Math.Round(Convert.ToDecimal(rowS["�����"]), 2);
                                    itemS.FlagWrite = false;
                                    itemS.���������� = Convert.ToInt32(rowS["����������"]);
                                    listServcS.Add(itemS);
                                }

                                //������ ������ ���� ���������� � �����
                                List<ErrorsReestrUnload> listServcF = new List<ErrorsReestrUnload>();

                                //�������� ����� ������ �� ������� � �������� ����������� �� �������
                                foreach (DataRow rowF in un.����������������.Rows)
                                {
                                    //�������� ��������� ��� �������� ������ � �������
                                    ErrorsReestrUnload itemF = new ErrorsReestrUnload();

                                    itemF.Error������������������ = rowF["������������������"].ToString().Trim();
                                    itemF.Error���� = Math.Round(Convert.ToDecimal(rowF["����"]), 2);
                                    itemF.Error����� = Math.Round(Convert.ToDecimal(rowF["�����"]), 2);
                                    itemF.FlagWrite = false;
                                    itemF.���������� = Convert.ToInt32(rowF["����������"]);
                                    listServcF.Add(itemF);
                                }

                                //�������� �� ���������
                                foreach (ErrorsReestrUnload itemF in listServcF)
                                {
                                    string iTest = itemF.Error������������������;
                                    foreach (ErrorsReestrUnload itemS in listServcS)
                                    {
                                        //==========���� �������==========
                                        string iiTest = itemS.������������������;


                                        if (iiTest == "������ ������� ������������")
                                        {
                                            string aTest = "Test";
                                        }
                                        //=======����� �����==============

                                        if (itemF.Error������������������.Trim() == itemS.������������������.Trim() && itemF.Error���� == itemS.���� && itemF.Error����� == itemS.����� && itemF.FlagWrite != true && itemS.FlagWrite != true)
                                        {
                                            //���� ������������ ������ ��������� � ����� ����� � �� ������� � � ����� �� ������ ���� � true
                                            itemF.FlagWrite = true;
                                            itemS.FlagWrite = true;
                                        }
                                    }
                                }

                                //�������� ��������� � ������� �������� ���������� ������� � ������� ���� FlagWrite == false
                                List<ErrorsReestrUnload> listErrors = new List<ErrorsReestrUnload>();

                                foreach (ErrorsReestrUnload itemF in listServcF)
                                {
                                    if (itemF.FlagWrite == false)
                                    {
                                        listErrors.Add(itemF);
                                    }
                                }


                                //�������� ����������� ������ ��� �������
                                List<ErrorsReestrUnload> listServer = new List<ErrorsReestrUnload>();
                                foreach (ErrorsReestrUnload itemS in listServcS)
                                {
                                    if (itemS.FlagWrite == false)
                                    {
                                        listServer.Add(itemS);
                                    }
                                }

                                //������ ������� ��� ���������
                                if (listErrors.Count >= listServer.Count)
                                {
                                    foreach (ErrorsReestrUnload er in listErrors)
                                    {
                                        if (er.FlagWrite == false)
                                        {
                                            ErrorsReestrUnload eru = new ErrorsReestrUnload();
                                            eru.Error������������������ = er.������������������;

                                            eru.Error���� = er.����;
                                            eru.Error����� = er.�����;
                                            eru.���������������� = er.����������������;

                                            listR.Add(er);
                                        }
                                    }

                                    int iCountR = 0;
                                    foreach (ErrorsReestrUnload lo in listServer)
                                    {
                                        if (lo.FlagWrite == false)
                                        {
                                            listR[iCountR].������������������ = lo.������������������;
                                            listR[iCountR].���� = lo.����;
                                            listR[iCountR].����� = lo.�����;
                                            listR[iCountR].���������� = lo.����������;
                                        }

                                        iCountR++;
                                    }
                                }

                                if (listErrors.Count <= listServer.Count)
                                {
                                    foreach (ErrorsReestrUnload er in listServer)
                                    {
                                        if (er.FlagWrite == false)
                                        {
                                            ErrorsReestrUnload eru = new ErrorsReestrUnload();
                                            eru.������������������ = er.������������������;

                                            eru.���� = er.����;
                                            eru.����� = er.�����;

                                            listR.Add(er);
                                        }
                                    }

                                    int iCountR = 0;
                                    foreach (ErrorsReestrUnload lo in listErrors)
                                    {
                                        if (lo.FlagWrite == false)
                                        {
                                            listR[iCountR].Error������������������ = lo.Error������������������;
                                            listR[iCountR].Error���� = lo.Error����;
                                            listR[iCountR].Error����� = lo.Error�����;
                                        }

                                        iCountR++;
                                    }
                                }

                                //�������� ���� ������
                                if (listR.Count == 0)
                                {
                                    //������ ���
                                    dispR.FlagError = true;
                                }
                                else
                                {
                                    //������
                                    dispR.FlagError = false;
                                }

                                dispR.������������ = listR;

                                //������� ����� ���� ����������� �����
                                decimal summAct = 0.0m;

                                //������� ����� ���� ����������� ����� �������������� ������ ������� ��������� � ����� �������� ������� ����������� �����
                                foreach (DataRow row in un.����������������.Rows)
                                {
                                    summAct = Math.Round(Math.Round(summAct, 2) + Math.Round(Convert.ToDecimal(row["�����"]), 2), 2);
                                }

                                dispR.������������������������� = summAct;

                                string queryViewServices = "select top 1 Sum(�����) as '�����' from dbo.���������������� " +
                                                           "where id_������� in( " +
                                                           "SELECT top 1 [id_�������] " +
                                                           "FROM [�������] " +
                                                           "where [�������������] = '" + �������������.Trim() + "' and  ������������ = 'True') ";

                                DataTable tabSumServer = ���������.GetTableSQL(queryViewServices, "����������������������", con, transact);

                                if (tabSumServer.Rows[0]["�����"] != DBNull.Value)
                                    dispR.������������������ = Math.Round(Convert.ToDecimal(tabSumServer.Rows[0]["�����"]), 2);

                            }


                            //� ����� �������� ����� ������ ��� �������� � ��� �� �������
                            if (tab.Rows.Count < un.����������������.Rows.Count)
                            {

                                List<ErrorsReestrUnload> listR = new List<ErrorsReestrUnload>();

                                //������ ������ ���� ���������� �� �������
                                List<ErrorsReestrUnload> listServcS = new List<ErrorsReestrUnload>();


                                //�������� ����� ������ �� ������� � �������� ����������� �� �������
                                foreach (DataRow rowS in tab.Rows)
                                {
                                    //�������� ��������� ��� �������� ������ � �������
                                    ErrorsReestrUnload itemS = new ErrorsReestrUnload();

                                    itemS.������������������ = rowS["������������������"].ToString().Trim();
                                    itemS.���� = Math.Round(Convert.ToDecimal(rowS["����"]), 2);
                                    itemS.����� = Math.Round(Convert.ToDecimal(rowS["�����"]), 2);
                                    itemS.FlagWrite = false;
                                    itemS.���������������� = Convert.ToInt32(rowS["����������"]);
                                    listServcS.Add(itemS);
                                }

                                //������ ������ ���� ���������� � �����
                                List<ErrorsReestrUnload> listServcF = new List<ErrorsReestrUnload>();

                                //�������� ����� ������ �� ������� � �������� ����������� �� �������
                                foreach (DataRow rowF in un.����������������.Rows)
                                {
                                    //�������� ��������� ��� �������� ������ � �������
                                    ErrorsReestrUnload itemF = new ErrorsReestrUnload();

                                    itemF.Error������������������ = rowF["������������������"].ToString().Trim();
                                    itemF.Error���� = Math.Round(Convert.ToDecimal(rowF["����"]), 2);
                                    itemF.Error����� = Math.Round(Convert.ToDecimal(rowF["�����"]), 2);
                                    itemF.FlagWrite = false;
                                    itemF.���������������� = Convert.ToInt32(rowF["����������"]);
                                    listServcF.Add(itemF);
                                }

                                //�������� �� ���������
                                foreach (ErrorsReestrUnload itemF in listServcF)
                                {
                                    string iTest = itemF.Error������������������;
                                    foreach (ErrorsReestrUnload itemS in listServcS)
                                    {
                                        //==========���� �������==========
                                        string iiTest = itemS.������������������;


                                        //if (iiTest == "������ ������� ������������")
                                        //{
                                        //    string aTest = "Test";
                                        //}
                                        //=======����� �����==============

                                        if (itemF.Error������������������.Trim() == itemS.������������������.Trim() && itemF.Error���� == itemS.���� && itemF.Error����� == itemS.����� && itemF.FlagWrite != true && itemS.FlagWrite != true)
                                        {
                                            //���� ������������ ������ ��������� � ����� ����� � �� ������� � � ����� �� ������ ���� � true
                                            itemF.FlagWrite = true;
                                            itemS.FlagWrite = true;
                                        }
                                    }
                                }

                                //�������� ��������� � ������� �������� ���������� ������� � ������� ���� FlagWrite == false
                                List<ErrorsReestrUnload> listErrors = new List<ErrorsReestrUnload>();
                                foreach (ErrorsReestrUnload itemF in listServcF)
                                {
                                    if (itemF.FlagWrite == false)
                                    {
                                        listErrors.Add(itemF);
                                    }
                                }


                                //�������� ����������� ������ ��� �������
                                List<ErrorsReestrUnload> listServer = new List<ErrorsReestrUnload>();
                                foreach (ErrorsReestrUnload itemS in listServcS)
                                {
                                    if (itemS.FlagWrite == false)
                                    {
                                        listServer.Add(itemS);
                                    }
                                }

                                //������ ������� ��� ���������
                                if (listErrors.Count >= listServer.Count)
                                {
                                    foreach (ErrorsReestrUnload er in listErrors)
                                    {
                                        if (er.FlagWrite == false)
                                        {
                                            ErrorsReestrUnload eru = new ErrorsReestrUnload();
                                            eru.Error������������������ = er.������������������;

                                            eru.Error���� = er.����;
                                            eru.Error����� = er.�����;
                                            eru.���������������� = er.����������������;

                                            listR.Add(er);
                                        }
                                    }

                                    int iCountR = 0;
                                    foreach (ErrorsReestrUnload lo in listServer)
                                    {
                                        if (lo.FlagWrite == false)
                                        {
                                            listR[iCountR].������������������ = lo.������������������;
                                            listR[iCountR].���� = lo.����;
                                            listR[iCountR].����� = lo.�����;
                                            listR[iCountR].���������� = lo.����������;
                                        }

                                        iCountR++;
                                    }
                                }

                                if (listErrors.Count <= listServer.Count)
                                {
                                    foreach (ErrorsReestrUnload er in listServer)
                                    {
                                        if (er.FlagWrite == false)
                                        {
                                            ErrorsReestrUnload eru = new ErrorsReestrUnload();
                                            eru.������������������ = er.������������������;

                                            eru.���� = er.����;
                                            eru.����� = er.�����;

                                            listR.Add(er);
                                        }
                                    }

                                    int iCountR = 0;
                                    foreach (ErrorsReestrUnload lo in listErrors)
                                    {
                                        if (lo.FlagWrite == false)
                                        {
                                            listR[iCountR].Error������������������ = lo.Error������������������;
                                            listR[iCountR].Error���� = lo.Error����;
                                            listR[iCountR].Error����� = lo.Error�����;
                                        }

                                        iCountR++;
                                    }
                                }

                                //�������� ���� ������
                                if (listR.Count == 0)
                                {
                                    //������ ���
                                    dispR.FlagError = true;
                                }
                                else
                                {
                                    //������
                                    dispR.FlagError = false;
                                }

                                dispR.������������ = listR;
                                ////===============================================================
                            }

                            //}
                            bool flag������������� = false;

                            //������ ���� �� � �������� �������� ��� ����������
                            DataTable tab������� = un.�������������;
                            if (tab�������.Rows.Count != 0)
                            {
                                //���� ���� �� ������ flag � true
                                flag������������� = true;
                                dispR.FlagAddContract = flag�������������;
                            }


                            // �������� ����������� �� ������� �������.
                            string queryValidDoc = "select ��������� from ������������������� " +
                                                   "where id_������� = ( " +
                                                    "select id_������� from ������� " +
                                                    "where LTRIM(RTRIM(LOWER(�������������))) = '" + un.�������.Rows[0]["�������������"].ToString().Trim().ToLower() + "' and ������������ = 'True') ";

                            DataTable tabValidAct = ���������.GetTableSQL(queryValidDoc, "ValidAct");

                            if (tabValidAct.Rows.Count > 0)
                            {
                                // errorReestr.������������������� = tabValidAct.Rows[0]["���������"].ToString().Trim();

                                // ��� �������� ������, ��� ��� �������.
                                dispR.FlagError����������������� = true;

                                // ������ ����� ����� ����������� ����.
                                dispR.�������������������� = tabValidAct.Rows[0]["���������"].ToString().Trim();
                            }

                            // ���� � ��� ������ �� ��������� ���������, ����� ����������� ������.
                            if (dispR.ErrorPrefCategory == true)
                            {
                                dispR.FlagError = false;
                            }

                            var test = dispR;

                            listReest.Add(���������, dispR);

                            //������� ����� ���� ����������� �����
                            decimal summAct1 = 0.0m;

                            //������� ����� ���� ����������� ����� �������������� ������ ������� ��������� � ����� �������� ������� ����������� �����
                            foreach (DataRow row in un.����������������.Rows)
                            {
                                summAct1 = Math.Round(Math.Round(summAct1, 2) + Math.Round(Convert.ToDecimal(row["�����"]), 2), 2);
                            }

                            dispR.������������������������� = summAct1;

                            string queryViewServices1 = "select top 1 Sum(�����) as '�����' from dbo.���������������� " +
                                                           "where id_������� in( " +
                                                           "SELECT top 1 [id_�������] " +
                                                           "FROM [�������] " +
                                                           "where [�������������] = '" + �������������.Trim() + "' and  ������������ = 'True') ";

                            DataTable tabSumServer1 = ���������.GetTableSQL(queryViewServices1, "����������������������", con, transact);

                            if (tabSumServer1.Rows[0]["�����"] != DBNull.Value)
                                dispR.������������������ = Math.Round(Convert.ToDecimal(tabSumServer1.Rows[0]["�����"]), 2);

                            //}
                            //else
                            //{
                            //    MessageBox.Show("������ �������� � " + �������������.Trim() + " �� ������� �� ����������");
                            //}
                        }


                        #endregion
                    }

                    //Dictionary<string, ValidateContract> validReestr 
                    Dictionary<string, DisplayReestr> iTestDisp = listReest;

                    FormDisplayReestr fd = new FormDisplayReestr();
                    fd.Unloads = unload;
                    fd.��������� = listReest;
                    fd.Show();

                    fstream.Close();

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
            //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;


            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            //openFileDialog1.Filter = "|*.r";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                try
                {

                    //SerializableObject objToSerialize = null;
                    FileStream fstream = File.Open(fileName, FileMode.Open);
                    BinaryFormatter binaryFormatter = new BinaryFormatter();

                    //������� �� ����� ������� � ����������
                    unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);

                     //������� �������� �����
                    fstream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " �������� �� ������� ������ ������� �� �������� �������� �������� ���������.");

                    return;
                }

                //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
                Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

                //������� ���������� ����
                openFileDialog1.Dispose();



            }
            else
            {
                //���� ������������ ����� ������ ������ �� ������� �� ������
                return;
            }


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


            //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            // ��� ��� ������ ���������.
            //Dictionary<string, Unload> unload2 = unload.Where(w => w.Key != "�������/1554").ToDictionary(w => w.Key,w=>w.Value);


            //�������� ���������� �� ���� ������ � ����
            Validate���� ���� = new Validate����(unload, listValContr, iConfig);

            // ������ ���������� �� �� � �������� ��� ���.
            ����.FlagConnectServer = this.FlagConnectServer;

            if (flag���������� == true)
            {
                ����.Flag�������� = true;
            }

            // �������� ������ �������� ��������� � ������ ������ ����.
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

        private void �������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormValidContract fvc = new FormValidContract();
            fvc.MdiParent = this;
            fvc.WindowState = FormWindowState.Maximized;

            fvc.FlagValid = true;
            fvc.Show();
        }

        private void ������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormValidContract fvc = new FormValidContract();
            fvc.MdiParent = this;

            fvc.WindowState = FormWindowState.Maximized;

            fvc.FlagValid = false;
            fvc.Show();
        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.flag���������� = true;

            // ������� ���������� ��� ������ �������� � �� ����� �������� ������������, �.�. ������� � �� ������� ������� ������������ � ������ �������� 2013 �.


            //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.r";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                //SerializableObject objToSerialize = null;
                FileStream fstream = File.Open(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();


                //������� �������� ������� ����������� ���������
                List<Unload> unloads = (List<Unload>)binaryFormatter.Deserialize(fstream);

                IEnumerable<DataTable> iTable = unloads.Select(a => a.�������);

                foreach (DataTable numDog in iTable)
                {
                    foreach (DataRow row in numDog.Rows)
                    {
                        if (row[1].ToString() == "5/1087")
                        {
                            string iTest = "test";
                        }
                    }
                }

                IEnumerable<Unload> unloadS = unloads.Where(un => un.������������������� != null).Select(un => un);

                List<Unload> unload = new List<Unload>();
                foreach (Unload und in unloadS)
                {
                    unload.Add(und);
                }

                List<Unload> iUnload = unload;

                // ������� ���� � ������� ������� 
                FormReestrDate frdForm = new FormReestrDate();
                frdForm.ShowDialog();

                if (frdForm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    string ������������ = frdForm.������������;
                    DateTime ����������� = frdForm.�����������;

                    string ���������������� = frdForm.����������������;
                    DateTime ��������������� = frdForm.���������������;

                    ClassLibrary1.DataClasses1DataContext dc = new ClassLibrary1.DataClasses1DataContext();
                    using (System.Transactions.TransactionScope myScope = new System.Transactions.TransactionScope())
                    {
                        try
                        {

                            foreach (Unload un in iUnload)
                            {
                                // ������� � �� ������ �� ���������.
                                DataTable dt�������������������� = un.�������������������;

                                DataTable dtPerson = un.��������;



                                int id_�������� = 0;
                                int id_������� = 0;

                                int id_�� = 0;

                                // ��������.
                                foreach (DataRow itPerson in dtPerson.Rows)
                                {
                                    ClassLibrary1.�������� person = new ClassLibrary1.��������();

                                    // ������� ���������.
                                    person.������� = itPerson[1].ToString().Trim();
                                    person.��� = itPerson[2].ToString().Trim();
                                    person.�������� = itPerson[3].ToString().Trim();
                                    person.������������ = Convert.ToDateTime(itPerson[4]);
                                    person.����� = itPerson[5].ToString().Trim();
                                    person.��������� = itPerson[6].ToString().Trim();
                                    person.������ = itPerson[7].ToString().Trim();
                                    person.������������� = itPerson[8].ToString().Trim();
                                    person.������������� = itPerson[9].ToString().Trim();
                                    person.������������� = itPerson[10].ToString().Trim();
                                    person.������������������ = Convert.ToDateTime(itPerson[11]);
                                    person.��������������� = itPerson[12].ToString().Trim();

                                    // id_�������� ���������.
                                    id_�� = dc.�����������������.Where(l => l.�����������������1 == un.�����������������.Trim()).Select(l => l.id_�����������������).First();
                                    person.id_����������������� = id_��;

                                    // id ��������.
                                    int id_doc = dc.������������.Where(doc => doc.������������������������� == un.������������.Rows[0][1].ToString().Trim()).Select(doc => doc.id_��������).First();
                                    person.id_�������� = id_doc;

                                    // ����� ���������.
                                    person.�������������� = itPerson[15].ToString().Trim();
                                    person.�������������� = itPerson[16].ToString().Trim();

                                    // ����� ������ ���������.
                                    person.������������������� = Convert.ToDateTime(itPerson[17]);
                                    // ��� ����� ��������.
                                    person.���������������� = itPerson[18].ToString().Trim();

                                    // ������� id ����������� ������� � ��� ��� 1.
                                    person.id_������� = 1;

                                    // ������� id ������.
                                    person.id_����� = -1;
                                    person.id_�������� = -1;

                                    // ������� � ��.
                                    dc.��������.InsertOnSubmit(person);
                                    dc.SubmitChanges();

                                    // ������ id ���������.
                                    id_�������� = person.id_��������;

                                }


                                // �������.
                                foreach (DataRow rCont in un.�������.Rows)
                                {
                                    // ������� �������.
                                    ClassLibrary1.������� contr = new ClassLibrary1.�������();
                                    contr.������������� = rCont[1].ToString().Trim();
                                    contr.������������ = Convert.ToDateTime(rCont[2]);

                                    // ������ ���� ���������� ���� ����������� �����.
                                    DateTime dateAct = Convert.ToDateTime(un.�������������������.Rows[0][4]);

                                    contr.������������������������ = dateAct;

                                    // ������ ����� ���� ����������� �����.
                                    DataTable tabSum = un.����������������;

                                    decimal sum = 0.0m;
                                    foreach (DataRow rSum in tabSum.Rows)
                                    {
                                        sum = Math.Round(sum + Convert.ToDecimal(rSum[6]), 4);
                                    }


                                    contr.������������������������� = sum;

                                    // ������ id �������� ���������.
                                    contr.id_����������������� = id_��;

                                    // id ������������ (id 1 ������������ = 92).
                                    contr.id_������������ = 92;

                                    contr.id_������� = 1;
                                    contr.������������������� = true;
                                    contr.��������������� = true;

                                    contr.id_�������� = id_��������;
                                    contr.����������������� = rCont[12].ToString().Trim();

                                    contr.���������� = "������� ������ 2013 �.";

                                    contr.������������������ = DateTime.Now.Date;

                                    contr.������������ = true;
                                    contr.������������ = ������������;
                                    contr.����������� = �����������;
                                    contr.����������������� = ����������������;
                                    contr.��������������� = ���������������;

                                    contr.logWrite = "������� ���� 2013";

                                    // ������� � �� � ������ id_��������.
                                    dc.�������.InsertOnSubmit(contr);
                                    dc.SubmitChanges();

                                    // ������ id �������.
                                    id_������� = contr.id_�������;


                                }

                                // ������� ��� ����������� �����.
                                foreach (DataRow rAct in un.�������������������.Rows)
                                {
                                    ClassLibrary1.������������������� act = new ClassLibrary1.�������������������();
                                    act.��������� = rAct[1].ToString().Trim();
                                    act.id_������� = id_�������;
                                    act.�������������� = "True";
                                    act.�������������� = Convert.ToDateTime(rAct[4]);
                                    act.�������������� = "True";
                                    act.������������������ = "";
                                    act.���� = 0.0m;
                                    act.���������� = 0;
                                    act.����� = 0.0m;
                                    act.����������������� = "";
                                    act.������������ = ������������;
                                    act.����������� = �����������;
                                    act.���������������� = ����������������;
                                    act.��������������� = ���������������;
                                    act.���������� = "";
                                    act.logWrite = "������ 2013 ��� �������";
                                    act.logDate = DateTime.Now.Date.ToShortDateString();

                                    dc.�������������������.InsertOnSubmit(act);
                                    dc.SubmitChanges();




                                }

                                // ������� ������ �� ��������.
                                foreach (DataRow rUsl in un.����������������.Rows)
                                {
                                    ClassLibrary1.���������������� usl = new ClassLibrary1.����������������();
                                    usl.������������������ = rUsl[1].ToString().Trim();
                                    usl.���� = Convert.ToDecimal(rUsl[2]);
                                    usl.���������� = Convert.ToInt32(rUsl[3]);
                                    usl.id_������� = id_�������;
                                    usl.�������������� = rUsl[5].ToString().Trim();
                                    usl.����� = Convert.ToDecimal(rUsl[6]);
                                    usl.������� = Convert.ToInt32(rUsl[7]);

                                    // ������� � ��.
                                    dc.����������������.InsertOnSubmit(usl);
                                    dc.SubmitChanges();
                                }

                            }
                        }
                        catch
                        {
                            MessageBox.Show("��� ������ �������� ������.");
                        }

                        myScope.Complete();

                        MessageBox.Show("������ ������� ��������");
                    }

                }

            }
        }

        private void ��������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;


            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.r";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
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

                //��� �����


                //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
                Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;


                //������� ���������� ����
                openFileDialog1.Dispose();

            }
            else
            {
                //���� ������������ ����� ������ ������ �� ������� �� ������
                return;
            }


            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                SqlTransaction transact = con.BeginTransaction();

                //������ ��� �������� ������� ���������
                Dictionary<string, string> list = new Dictionary<string, string>();

                //�������� ���������� �������
                foreach (Unload ul in unload.Values)
                {
                    DataTable tab��� = ul.�������;
                    DataRow row = tab���.Rows[0];

                    list.Add(row["�������������"].ToString().Trim(), row["�������������"].ToString().Trim());
                }

                Dictionary<string, string> iList2 = list;

                //������� ������ � ������� �� �������
                foreach (Unload ul in unload.Values)
                {
                    DataTable tab��� = ul.�������;
                    DataRow row = tab���.Rows[0];

                    string query = "select ������������� from ������� where ������������� = '" + row["�������������"].ToString().Trim() + "' ";
                    DataTable tabServ = ���������.GetTableSQL(query, "�������", con, transact);

                    //���� ������ ����� �� ������� ������ ������� �� ����������
                    if (row["�������������"].ToString().Trim() == tabServ.Rows[0]["�������������"].ToString().Trim())
                    {
                        list.Remove(row["�������������"].ToString().Trim());
                    }
                }

                Dictionary<string, string> iList = list;

                List<string> listNum = new List<string>();
                foreach (string num in list.Keys)
                {
                    listNum.Add(num);
                }
                //������� ������ � �������� ��������� ������� �� ���������� � ����
                FormControlContract fcc = new FormControlContract();
                fcc.List = listNum;
                fcc.MdiParent = this;
                fcc.Show();


            }
        }

        private static bool EndsWithSaurus(string sName, DateService ds)
        {
            bool flag = false;
            List<DateService> list = new List<DateService>();
            DateService dss = new DateService();
            dss.������������ = ds.������������.Trim();

            dss.���� = ds.�����;//.ToString("c");
            dss.����� = ds.����;//.ToString("c");

            list.Add(dss);

            if (dss.������������ == sName)
            {
                flag = true;
            }

            return flag;
        }

        private void ��������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormValidContract fvc = new FormValidContract();
            fvc.MdiParent = this;
            fvc.WindowState = FormWindowState.Maximized;

            fvc.FlagValid = false;

            //�������� �� ��������� ��������
            fvc.FlagCheck = true;
            fvc.Show();
        }

        private void �����������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHeadDepartment fhd = new FormHeadDepartment();
            fhd.MdiParent = this;
            fhd.Show();
        }

        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSelectHeadDepartment fSelect = new FormSelectHeadDepartment();
            fSelect.MdiParent = this;
            fSelect.Show();
        }

        private void ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;


            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.r";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                //SerializableObject objToSerialize = null;
                FileStream fstream = File.Open(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //������� �� ����� ������� � ����������
                unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);
            }
            else
                return;

            foreach (Unload un in unload.Values)
            {
                //������� �������� �������� ��������� ������� ����������� ������� ��������
                string i����������������� = un.�����������������;

                //������� ����� ��������
                string numContract = un.�������.Rows[0]["�������������"].ToString().Trim();

                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();


                    //������������ ���������� id �������� ���������
                    string queryUpdateConvert = "declare @id_Lk int " +
                                                "select @id_Lk = id_����������������� from dbo.����������������� " +
                                                "where ����������������� = '" + i����������������� + "' " +
                                                "declare @id_�������� int " +
                                                "select @id_�������� = id_�������� from dbo.�������� " +
                                                "where id_�������� in ( " +
                                                "select id_�������� from dbo.������� " +
                                                "where ������������� = '" + numContract + "') " +
                                                "update dbo.������� " +
                                                "set id_����������������� = @id_Lk " +
                                                "where id_������� in ( " +
                                                "select id_������� from dbo.������� " +
                                                "where ������������� = '" + numContract + "') " +
                                                "update dbo.�������� " +
                                                "set id_����������������� = @id_Lk " +
                                                "where id_�������� = @id_�������� ";

                    //ExecuteQuery.Execute(queryUpdateConvert);
                }




            }
        }

        private void ��������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReestrDisplay fdr = new FormReestrDisplay();
            fdr.MdiParent = this;
            fdr.WindowState = FormWindowState.Maximized;
            fdr.Show();
        }

        private void �������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDepartment fd = new FormDepartment();
            fd.MdiParent = this;
            fd.Show();
        }

        private void ���������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfig����� fc = new FormConfig�����();
            fc.MdiParent = this;
            fc.Show();
        }

        private void �������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;


            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.r";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                //SerializableObject objToSerialize = null;
                FileStream fstream = File.Open(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //������� �� ����� ������� � ����������
                unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);
            }

            //string query�������� = "select id_��������,�������,���,��������,������������,id_����������������� from �������� " +
            //                        "where id_�������� in ( " +
            //                        "SELECT  id_�������� FROM ������� where id_����������������� is null ) ";


            //DataTable tab�������� = ���������.GetTableSQL(query��������, "��������");

            foreach (Unload un in unload.Values)
            {
                //������� �������� �������� ��������� ������� ����������� ������� ��������
                string i����������������� = un.�����������������;

                //������� ����� ��������
                DataRow rDog = un.�������.Rows[0];
                string ������������� = rDog["�������������"].ToString().Trim();

                //������� ������ � ����������
                DataRow rowLgot = un.��������.Rows[0];
                string ������� = rowLgot["�������"].ToString().Trim();
                string ��� = rowLgot["���"].ToString().Trim();

                string �������� = rowLgot["��������"].ToString().Trim();
                string ������������ = �����.����(Convert.ToDateTime(rowLgot["������������"]).ToShortDateString().Trim());

                string _������������ = �����.����(������������);


                string query�������� = "select id_��������,�������,���,��������,������������,id_����������������� from �������� " +
                                        "where id_�������� in ( " +
                                        "SELECT  id_�������� FROM ������� where id_����������������� is null and " +
                                        "������� = '" + ������� + "' and ��� = '" + ��� + "' and �������� = '" + �������� + "' and ������������ = '" + ������������ + "') ";
                try
                {
                    DataTable tab����� = ���������.GetTableSQL(query��������, "��������");
                    if (tab�����.Rows.Count != 0)
                    {
                        DataTable tab�����2 = ���������.GetTableSQL(query��������, "��������");

                        string update = "update �������� " +
                                   "set id_����������������� = (select id_����������������� from ����������������� " +
                                   "where ����������������� = '" + i����������������� + "') " +
                                   "where id_�������� = " + Convert.ToInt32(tab�����.Rows[0]["id_��������"]) + " " +
                                   "update ������� " +
                                   "set id_����������������� = (select id_����������������� from ����������������� " +
                                   "where ����������������� = '" + i����������������� + "') " +
                                   "where id_�������� = " + Convert.ToInt32(tab�����.Rows[0]["id_��������"]) + " ";

                        ExecuteQuery.Execute(update);
                    }
                }
                catch
                {
                    MessageBox.Show("�� ���������� ������ � ����� ��������");
                }


            }

            MessageBox.Show("��������������� ���������");


            //foreach (Unload un in unload.Values)
            //{
            //    //������� �������� �������� ��������� ������� ����������� ������� ��������
            //    string i����������������� = un.�����������������;

            //    //������� ����� ��������
            //    DataRow rDog = un.�������.Rows[0];
            //    string ������������� = rDog["�������������"].ToString().Trim();

            //    //������� ������ � ����������
            //    DataRow rowLgot = un.��������.Rows[0];
            //    string ������� = rowLgot["�������"].ToString().Trim();
            //    string ��� = rowLgot["���"].ToString().Trim();

            //    string �������� = rowLgot["��������"].ToString().Trim();
            //    string ������������ = �����.����(Convert.ToDateTime(rowLgot["������������"]).ToShortDateString().Trim());

            //    //�����, ����� ������� � ���� ������
            //    //string ������������� = string.Empty;

            //    //������ ����� �������� � ������� 00 00 
            //    string serPass = string.Empty;

            //    //string ���������� = rowLgot["�������������"].ToString().Trim();
            //    string ������������� = rowLgot["�������������"].ToString().Trim();

            //    //��������� ������ ����� �������� � ������� 00 00
            //    StringBuilder build = new StringBuilder();

            //    //������� ������
            //    int iiCount = 0;
            //    foreach (char ch in �������������)
            //    {
            //        if (iiCount == 2)
            //        {
            //            string sNum = " " + ch.ToString().Trim();
            //            build.Append(sNum);
            //        }
            //        else
            //        {
            //            build.Append(ch);
            //        }


            //        iiCount++;
            //    }

            //    //�������� � ���������� ����� � ����� �������� � ������� 00 00
            //    serPass = build.ToString().Trim();

            //    string ������������� = rowLgot["�������������"].ToString().Trim();
            //    string ������������������ = �����.����(Convert.ToDateTime(rowLgot["������������������"]).ToShortDateString().Trim());

            //    //�����, ����� � ���� ������ ��������� ������� ����� �� ������
            //    string �������������� = rowLgot["��������������"].ToString().Trim();
            //    string �������������� = rowLgot["��������������"].ToString().Trim();
            //    string ������������������� = �����.����(Convert.ToDateTime(rowLgot["�������������������"]).ToShortDateString().Trim());

            //    string queryUpdate = "declare @id_����������������� int " +
            //                         "select @id_����������������� = id_����������������� from ����������������� " +
            //                         "where ����������������� = '" + i����������������� + "' " +
            //                         "declare @id_�������� int " +
            //                         "select @id_�������� = id_�������� from �������� " +
            //                         "where ������� = '" + ������� + "' and ��� = '" + ��� + "' and �������� = '" + �������� + "' and ������������ = '" + ������������ + "' and ������������� = '" + ������������� + "' and ������������� = '" + ������������� + "'  " +
            //                         "and ������������������ = '" + ������������������ + "' and �������������� = '" + �������������� + "' and �������������� = '" + �������������� + "' and ������������������� = '" + ������������������� + "' " +
            //                         "update dbo.������� " +
            //                         "set id_����������������� = @id_����������������� " +
            //                         "where id_������� in( " +
            //                         "select id_������� from ������� " +
            //                         "where id_�������� in ( " +
            //                         "select id_�������� from dbo.�������� " +
            //                         "where id_��������  = @id_��������)) " +
            //                         "update dbo.�������� " +
            //                         "set id_����������������� = @id_����������������� " +
            //                         "where id_��������  = @id_�������� ";

            //    ExecuteQuery.Execute(queryUpdate);
            //}


        }

        private void ������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDate fDate = new FormDate();
            fDate.ShowDialog();

            if (fDate.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                FormReestrDisplay fdr = new FormReestrDisplay();
                fdr.MdiParent = this;
                fdr.Date = fDate.Date;
                fdr.FlagLetter = true;
                fdr.WindowState = FormWindowState.Maximized;
                fdr.Show();
            }
        }

        private void ��������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //���������� ������ �� ����������� ���������� �� ����������. �.�. ����� ���������� ������� ����� ����� ������ ��������
            string queryBi = "SELECT     �������, ���, ��������,������������, �������������, id_�������� " +
                             "FROM         (SELECT     dbo.��������.�������, dbo.��������.���, dbo.��������.��������,dbo.��������.������������, dbo.�������.�������������, dbo.�������.id_�������� " +
                             "FROM          dbo.������� INNER JOIN " +
                             "dbo.�������� ON dbo.�������.id_�������� = dbo.��������.id_��������) AS derivedtbl_1 " +
                             "where id_�������� in (SELECT      id_�������� " +
                             "FROM         (SELECT     �������������, COUNT(�������������) AS Expr1, id_��������, COUNT(id_��������) AS Expr2 " +
                             "FROM          dbo.������� " +
                             "WHERE      (id_�������� IN " +
                             "(SELECT     id_�������� " +
                             "FROM          dbo.������� AS �������_1 " +
                             "GROUP BY id_�������� " +
                             "HAVING      (COUNT(�������������) > 1))) " +
                             "GROUP BY �������������, id_��������) AS derivedtbl_1 " +
                             "where id_�������� in ( " +
                             "SELECT      id_��������  " +
                             "FROM         (SELECT     �������������, COUNT(�������������) AS Expr1, id_��������, COUNT(id_��������) AS Expr2 " +
                             "FROM          dbo.������� " +
                             "WHERE      (id_�������� IN " +
                             "(SELECT     id_�������� " +
                             "FROM          dbo.������� AS �������_1 " +
                             "GROUP BY id_�������� " +
                             "HAVING      (COUNT(�������������) > 1))) " +
                             "GROUP BY �������������, id_��������) AS derivedtbl_1 " +
                             "group by id_�������� " +
                             "having count(id_��������) > 1)) " +
                             "group by �������, ���, ��������,������������, �������������, id_�������� " +
                             "order by ������� asc ";

            DataTable tableDubli = ���������.GetTableSQL(queryBi, "��������������");

            //�������� ��������� ������� �������� ������ ��� ������
            List<�������������> list = new List<�������������>();

            //������� ����� ������
            ������������� ����� = new �������������();
            �����.����� = "� �.�.";

            �����.��� = "��� ���������";
            �����.������������ = "���� �������� ���������";

            �����.������������� = "����� ��������";
            list.Add(�����);

            int iCount = 1;

            //�������� ����� �������
            foreach (DataRow row in tableDubli.Rows)
            {
                string f = row["�������"].ToString().Trim();
                string i = row["���"].ToString().Trim();
                string o = row["��������"].ToString().Trim();

                ������������� item = new �������������();
                item.����� = iCount.ToString().Trim();

                item.��� = f + " " + i + " " + o;
                item.������������ = Convert.ToDateTime(row["������������"]).ToShortDateString();

                item.������������� = row["�������������"].ToString().Trim();
                list.Add(item);

                iCount++;
            }


            string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ ������������� ���������.doc";


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
            Range wrdRng = doc.Bookmarks.get_Item(ref bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 4, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 6;
            table.Columns[1].Width = 40;
            table.Columns[2].Width = 150;
            table.Columns[3].Width = 150;
            table.Columns[3].Width = 150;
            table.Borders.Enable = 1; // ����� - �������� �����
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 10;
            //������� �����
            //int i = 1;

            //�������� �������
            int k = 1;
            //������� ������ � �������
            foreach (������������� item in list)
            {

                table.Cell(k, 1).Range.Text = item.�����;
                table.Cell(k, 2).Range.Text = item.���;

                table.Cell(k, 3).Range.Text = item.������������;
                table.Cell(k, 4).Range.Text = item.�������������;


                //doc.Words.Count.ToString();
                Object beforeRow1 = Type.Missing;
                table.Rows.Add(ref beforeRow1);

                k++;
            }

            table.Rows[k].Delete();

            //��������� ��������
            app.Visible = true;
        }

        private void �����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //������� � ���� ����� ���� ����������� ����� ������� ������� �����
            string queryTab = "select id_��� from �������������������";
            DataTable tab��� = ���������.GetTableSQL(queryTab, "�������������������");

            //�������
            int iCount = 0;

            StringBuilder builder = new StringBuilder();

            foreach (DataRow row in tab���.Rows)
            {

                string queryUpdate = " declare @summ_" + iCount + " money " +
                       "select @summ_" + iCount + " = SUM(�����) from ���������������� " +
                       "where id_������� = ( " +
                       "SELECT id_������� FROM ������������������� " +
                       "where id_��� = " + Convert.ToInt32(row["id_���"]) + " ) " +
                       "update ������� " +
                       "set ������������������������� = @summ_" + iCount + " " +
                       "where id_������� = (SELECT id_������� FROM ������������������� " +
                       "where id_��� = " + Convert.ToInt32(row["id_���"]) + " ) ";

                builder.Append(queryUpdate);

                iCount++;
            }

            string iTest = builder.ToString().Trim();

            ExecuteQuery.Execute(iTest);
        }

        private void ��������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //�������� �� ������� � ������� ��������
            //string query�������� = "select id_��������,�������,���,��������,������������,id_����������������� from ��������";
            string query�������� = "select id_��������,�������,���,��������,������������,�������������,�������������,id_����������������� from �������� " +
                                    "where id_�������� in ( " +
                                    "SELECT  id_�������� FROM ������� where id_����������������� is null ) ";


            DataTable tab�������� = ���������.GetTableSQL(query��������, "��������");
            foreach (DataRow rowL in tab��������.Rows)
            {
                //���������� ������ �������� ���������
                string �������� = string.Empty;

                //�������� �� ���� ����� �����������
                PullConnectBD pull = new PullConnectBD();
                Dictionary<string, string> pullConnect = pull.GetPull(this.FlagConnectServer);

                foreach (KeyValuePair<string, string> dStringConnect in pullConnect)
                {

                    string sConnection = string.Empty;
                    sConnection = dStringConnect.Value.ToString().Trim();

                    //�������� �������� � ������ ���������� ��� ����������� ������ (������ �����������)
                    using (SqlConnection con = new SqlConnection(sConnection))
                    {
                        //string queryL = "select A_NAME from PPR_CAT " +
                        //                "where A_ID in ( " +
                        //                "select A_CATEGORY from SPR_NPD_MSP_CAT " +
                        //                "where A_ID in ( " +
                        //                "select A_SERV from ESRN_SERV_SERV  " +
                        //                "where A_PERSONOUID = ( " +
                        //                "select OUID from WM_PERSONAL_CARD  " +
                        //                "where SURNAME = (select OUID from SPR_FIO_SURNAME  " +
                        //                "where A_NAME = '"+ rowL["�������"].ToString().Trim() +"') and A_NAME = (select OUID from SPR_FIO_NAME " +
                        //                "where A_NAME = '"+ rowL["���"].ToString().Trim() +"') and A_SECONDNAME = (select OUID from SPR_FIO_SECONDNAME " +
                        //                //"where A_NAME = '" + rowL["��������"].ToString().Trim() + "') and CONVERT(char(10), WM_PERSONAL_CARD.BIRTHDATE, 104) = '" + Convert.ToDateTime(rowL["������������"]).ToShortDateString().Trim() + "' )))and A_NAME in ('������� �����','����������������� ����','�������� ����','�������  ������� ������','������� ����� ����������� �������')  ";
                        //                "where A_NAME = '" + rowL["��������"].ToString().Trim() + "') )))and A_NAME in ('������� �����','����������������� ����','�������� ����','�������  ������� ������','������� ����� ����������� �������')  ";

                        ////��������� ������ ����� �������� � ������� 00 00
                        //StringBuilder build = new StringBuilder();

                        ////������� ������
                        //int iiCount = 0;
                        //foreach (char ch in rowL["�������������"].ToString().Trim())
                        //{
                        //    if (iiCount == 2)
                        //    {
                        //        string sNum = " " + ch.ToString().Trim();
                        //        build.Append(sNum);
                        //    }
                        //    else
                        //    {
                        //        build.Append(ch);
                        //    }


                        //    iiCount++;
                        //}

                        ////�������� � ���������� ����� � ����� �������� � ������� 00 00
                        //string serPass = build.ToString().Trim();
                        //

                        string serPass = rowL["�������������"].ToString().Trim();
                        serPass = serPass + " ";

                        string queryL = "select PPR_CAT.A_NAME from PPR_CAT " +
                                        "where PPR_CAT.A_ID  " +
                                        "in ( select A_CATEGORY from SPR_NPD_MSP_CAT where A_ID " +
                                        "in ( select A_SERV from ESRN_SERV_SERV  " +
                                         "where A_PERSONOUID = (select OUID from WM_PERSONAL_CARD  " +
                                         "where SURNAME = (select OUID from SPR_FIO_SURNAME  where A_NAME = '" + rowL["�������"].ToString().Trim() + "') " +
                                         "and A_NAME = (select OUID from SPR_FIO_NAME where A_NAME = '" + rowL["���"].ToString().Trim() + "') " +
                                         "and A_SECONDNAME = (select OUID from SPR_FIO_SECONDNAME where A_NAME = '" + rowL["��������"].ToString().Trim() + "') " +
                                         "and OUID = (select PERSONOUID from WM_ACTDOCUMENTS " +
                                         "where DOCUMENTSERIES = '" + serPass + "' and DOCUMENTSNUMBER = '" + rowL["�������������"].ToString().Trim() + "') " +
                                         "))and A_NAME in ('������� �����','����������������� ����','�������� ����','�������  ������� ������','������� ����� ����������� �������'))  ";

                        try
                        {
                            DataTable tabLK = ���������.GetTableSQL(queryL, "PPR_CAT", con);
                            if (tabLK.Rows.Count != 0)
                            {
                                �������� = tabLK.Rows[0]["A_NAME"].ToString();

                                string update = "update �������� " +
                                                "set id_����������������� = (select id_����������������� from ����������������� " +
                                                "where ����������������� = '" + �������� + "') " +
                                                "where id_�������� = " + Convert.ToInt32(rowL["id_��������"]) + " " +
                                                "update ������� " +
                                                "set id_����������������� = (select id_����������������� from ����������������� " +
                                                "where ����������������� = '" + �������� + "') " +
                                                "where id_�������� = " + Convert.ToInt32(rowL["id_��������"]) + " ";

                                ExecuteQuery.Execute(update);

                                break;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("������ " + dStringConnect.Key + "�� ��������");
                        }

                    }
                }


                //string update = "update �������� " +
                //                "set id_����������������� = (select id_����������������� from ����������������� " +
                //                "where ����������������� = '" + �������� + "') " +
                //                "where id_�������� = "+ Convert.ToInt32(rowL["id_��������"]) +" " +
                //                "update ������� " +
                //                "set id_����������������� = (select id_����������������� from ����������������� " +
                //                "where ����������������� = '" + �������� + "') " +
                //                "where id_�������� = " + Convert.ToInt32(rowL["id_��������"]) + " ";

                //ExecuteQuery.Execute(update);
            }

            MessageBox.Show("������� ����������");

        }

        private void �����������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void �������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //��������� ������� ��������
            CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCInfo;


            //������� ���������� �����������
            string queryCountHosp = "select id_������������,������������������������,���,COUNT(���) from ������������ " +
                                    "where id_������������ in (select id_������������ from �������) " +
                                    "group by ���,id_������������,������������������������ " +
                                    "order by id_������������ asc";



            DataTable tabNameH = ���������.GetTableSQL(queryCountHosp, "�������������������");

            //// ��������� �����������.
            //List<HospitalCount> listHosp = new List<HospitalCount>();

            //// ����������� � ��������� ������� tabNameH.
            // foreach (DataRow row in tabNameH.Rows)
            // {
            //     HospitalCount hosp = new HospitalCount();
            //     hosp.Id������������ = Convert.ToInt32(row["id_������������"]);
            //     hosp.������������������������ = row["������������������������"].ToString().Trim();
            //     hosp.��� = row["���"].ToString().Trim();
            //     //hosp.Count��� = Convert.ToInt32(row["COUNT(���)"]);

            //     listHosp.Add(hosp);
            // }

            // IEnumerable<IGrouping<string,HospitalCount>> query = listHosp.GroupBy(l => l.���);//.Select(l => l);

            // List<string> lll = new List<string>();

            // int iiCount = 0;
            // foreach (IGrouping<string,HospitalCount> HospitalCount in query)
            // {
            //     foreach (HospitalCount kk in HospitalCount)
            //     {
            //         string sTest = kk.���;
            //         lll.Add(sTest);
            //     }
            // }

            // List<string> asd = lll;

            // int iTest = iiCount;

            //������ ��� �������� ������ ��� ������
            List<ReportMSO> list = new List<ReportMSO>();

            //���������� ����� �������
            ReportMSO ����� = new ReportMSO();
            �����.Num = "� �/�";
            �����.NameHospital = "������������ ���������� ���������������";
            �����.CountContractHospital = "���������� ����������� ��������� � ������������ ��������������� (����������� ������)";

            �����.SumContractHospital = "�����, �� ������� ��������� �������� (����������� ������ ���. ���)";
            �����.SumPaidContract = "����� ����������� �� ��������� (����������� ������) (���. ���.)";
            �����.CountRoseRecords = "����������� �������� �� ���� (����������� ������)";

            list.Add(�����);

            //������� ����������� ������
            int iCount = 1;

            //��1���� ���������� ����������
            int iCountContract = 0;

            //������� ������ �����
            decimal iCountSum = 0m;

            //�������� �� �������������
            foreach (DataRow row in tabNameH.Rows)// -- ������ ����������.
            //foreach (HospitalCount item in listHosp)
            {


                //��� ��� � ������� ������������ ���� ������ Dentist ��� ������������ ������� ��������� � �. �������
                //��������� � ����� (������ 8 �������) �� �� ������ ������� 
                if (iCount != 8)
                {
                    ReportMSO reportItem = new ReportMSO();


                    //������� ���������� �����
                    reportItem.Num = iCount.ToString();

                    string nameHosp = row["������������������������"].ToString().Trim();
                    //string nameHosp = item.������������������������


                    //������� ������������ ����������
                    NameHosp hosp = new NameHosp(nameHosp);
                    reportItem.NameHospital = hosp.GetNameHosp();

                    //���������� ���������� ����������� ���������
                    //string queryCountContract = "select Max(id_�������),id_�������� from View_�������� " +
                    //                            "where id_������������ in (" + Convert.ToInt32(row["id_������������"]) + ") " +
                    //                            "group by id_�������� ";

                    string queryCountContract = "select id_�������,id_�������� from View_�������� " +
                                                "where id_������������ in (" + Convert.ToInt32(row["id_������������"]) + ") ";
                    //"group by id_�������� ";

                    //���������� ���������� ����������� ��������� ��� ������� ������������
                    DataTable tabCount = ���������.GetTableSQL(queryCountContract, "CountContr");
                    int counContr = tabCount.Rows.Count + hosp.GetCount();

                    //reportItem.CountContractHospital = tabCount.Rows.Count.ToString();
                    reportItem.CountContractHospital = counContr.ToString();

                    //���������� ����� ���������� ����������� ���������
                    iCountContract = iCountContract + tabCount.Rows.Count + hosp.GetCount();

                    //���������� ����� �� ������� ��������� ��������
                    //string queryCount = "select SUM(�����) as '�����' from dbo.���������������� " +
                    //                    "where id_������� in (select Max(id_�������) from View_�������� where id_������������ in (" + Convert.ToInt32(row["id_������������"]) + ")  " +
                    //                    "group by id_��������) ";

                    string queryCount = "select SUM(�����) as '�����' from dbo.���������������� " +
                                        "where id_������� in (select id_������� from View_�������� where id_������������ in (" + Convert.ToInt32(row["id_������������"]) + ") ) ";
                    //"group by id_��������) ";

                    DataTable tabSum = ���������.GetTableSQL(queryCount, "count");

                    if (tabSum.Rows[0]["�����"] != DBNull.Value)
                    {
                        //������ ����� ��� ������� � �� ����� 29.08.2013 �
                        decimal sum = Math.Round(Convert.ToDecimal(tabSum.Rows[0]["�����"]), 2);
                        //reportItem.SumContractHospital = (sum / 1000).ToString("c");

                        //�������� � ����� ��������� ����������� 30.08.2013 � � ����� � ��� �� ����� ��������� ����������� �� 29.08.2013 �. ������������ 29.08.2013 �
                        decimal sumContr = Math.Round(Math.Round(sum, 2) + Math.Round(hosp.GetSum(), 2), 2);
                        reportItem.SumContractHospital = Math.Round((sumContr / 1000), 2).ToString();

                        //iCountSum = Math.Round((Math.Round(iCountSum, 2) + Math.Round((sum), 2)), 2);
                        iCountSum = Math.Round((Math.Round(iCountSum, 2) + Math.Round((sumContr), 2)), 2);
                    }
                    else
                    {
                        //���� ������� ����� 29.08.2013 �. ��� �� ������� ����� ��������� �� 29.08.2013 �. ������������
                        decimal sumContr = Math.Round(hosp.GetSum(), 2);
                        reportItem.SumContractHospital = (sumContr / 1000).ToString();

                        iCountSum = Math.Round((Math.Round(iCountSum, 2) + Math.Round((sumContr), 2)), 2);
                    }

                    // ������� ���.
                    reportItem.��� = row["���"].ToString().Trim();

                    list.Add(reportItem);
                }
                else
                {
                    ReportMSO reportItemSar = new ReportMSO();
                    reportItemSar.NameHospital = "����� �������";
                    reportItemSar.CountContractHospital = (iCountContract).ToString();
                    reportItemSar.SumContractHospital = Math.Round(iCountSum / 1000, 2).ToString();
                    list.Add(reportItemSar);

                    // Test
                    //if (row["������������������������"].ToString().Trim() == "����������� ���")
                    //{
                    //    string iTest = "Test";
                    //}


                    ReportMSO reportItem = new ReportMSO();

                    //������� ���������� �����
                    reportItem.Num = iCount.ToString();

                    string nameHosp = row["������������������������"].ToString().Trim();


                    //������� ������������ ����������
                    NameHosp hosp = new NameHosp(nameHosp);
                    reportItem.NameHospital = hosp.GetNameHosp();

                    //���������� ���������� ����������� ���������
                    //string queryCountContract = "select Max(id_�������),id_�������� from View_�������� " +
                    //                            "where id_������������ in (" + Convert.ToInt32(row["id_������������"]) + ") " +
                    //                            "group by id_�������� ";

                    string queryCountContract = "select Count(id_�������),id_�������� from View_�������� " +
                                               "where id_������������ in (" + Convert.ToInt32(row["id_������������"]) + ") " +
                                               "group by id_�������� ";

                    //���������� ���������� ����������� ��������� ��� ������� ������������
                    DataTable tabCount = ���������.GetTableSQL(queryCountContract, "CountContr");
                    int counContr = tabCount.Rows.Count + hosp.GetCount();

                    //������� ���������� ����������� ���������
                    reportItem.CountContractHospital = counContr.ToString();

                    //���������� ����� ���������� ����������� ���������
                    //iCountContract = iCountContract + tabCount.Rows.Count;

                    //���������� ����� ���������� ����������� ���������
                    iCountContract = iCountContract + tabCount.Rows.Count + hosp.GetCount();

                    //���������� ����� �� ������� ��������� ��������
                    string queryCount = "select SUM(�����) as '�����' from dbo.���������������� " +
                                        "where id_������� in (select Max(id_�������) from View_�������� where id_������������ in (" + Convert.ToInt32(row["id_������������"]) + ")  " +
                                        "group by id_��������) ";

                    DataTable tabSum = ���������.GetTableSQL(queryCount, "count");

                    if (tabSum.Rows[0]["�����"] != DBNull.Value)
                    {
                        decimal sum = Math.Round(Convert.ToDecimal(tabSum.Rows[0]["�����"]), 2);
                        //reportItem.SumContractHospital = (sum / 1000).ToString("c");

                        decimal sumContr = Math.Round(Math.Round(sum, 2) + Math.Round(hosp.GetSum(), 2), 2);
                        reportItem.SumContractHospital = (sumContr / 1000).ToString();

                        iCountSum = Math.Round((Math.Round(iCountSum, 2) + Math.Round((sumContr), 2)), 2);
                    }
                    else
                    {
                        decimal sumContr = Math.Round(hosp.GetSum(), 2);
                        reportItem.SumContractHospital = (sumContr / 1000).ToString();

                        iCountSum = Math.Round((Math.Round(iCountSum, 2) + Math.Round((sumContr), 2)), 2);
                    }

                    // ������� ���.
                    reportItem.��� = row["���"].ToString().Trim();

                    list.Add(reportItem);
                }

                iCount++;

                //list.Add(reportItem);
            }


            //���������� �����
            ReportMSO itenItog = new ReportMSO();
            itenItog.NameHospital = "�����";
            itenItog.CountContractHospital = iCountContract.ToString();
            itenItog.SumContractHospital = Math.Round(iCountSum / 1000, 2).ToString();

            list.Add(itenItog);

            //// ������� � ����� ��������� ������ � �������.
            List<ReportMSO> newList = new List<ReportMSO>();

            foreach (ReportMSO rmso in list)
            {
                int iCountRow = 0;
                if (rmso.��� != null)
                {
                    // ����� ����� ���������� � ����� ��������� ������� � ������� ������� ���.
                    iCountRow = newList.Where(n => n.��� == rmso.���).Select(n => n).Count();
                }
                if (iCountRow == 1)
                {
                    // ����� ���������� ������������.
                    var query = newList.Where(n => n.��� == rmso.���).Select(n => n);
                    foreach (ReportMSO myRmso in query)
                    {
                        // ������ ���������� ����������� ���������.
                        int newCountContractHospital = Convert.ToInt32(myRmso.CountContractHospital);
                        int CountContractHospital = Convert.ToInt32(rmso.CountContractHospital);

                        int sumCountContractHospital = newCountContractHospital + CountContractHospital;
                        myRmso.CountContractHospital = sumCountContractHospital.ToString().Trim();


                        myRmso.CountRoseRecords = rmso.CountRoseRecords;

                        myRmso.NameHospital = rmso.NameHospital;
                        myRmso.Num = rmso.Num;

                        // ������ ����� �� ������� ��������� ��������.
                        decimal newSumContractHospital = Convert.ToDecimal(myRmso.SumContractHospital);
                        decimal ContractHospital = Convert.ToDecimal(rmso.SumContractHospital);

                        decimal sumContractHospital = Math.Round(Math.Round(newSumContractHospital, 2) + Math.Round(ContractHospital, 2), 2);
                        myRmso.SumContractHospital = sumContractHospital.ToString().Trim();

                        myRmso.SumPaidContract = rmso.SumPaidContract;
                    }
                }
                //if (rmso.Flag == false)
                else
                {
                    ReportMSO newItem = new ReportMSO();
                    newItem.CountContractHospital = rmso.CountContractHospital;
                    newItem.CountRoseRecords = rmso.CountRoseRecords;

                    newItem.NameHospital = rmso.NameHospital;
                    newItem.Num = rmso.Num;

                    newItem.SumContractHospital = rmso.SumContractHospital;
                    newItem.SumPaidContract = rmso.SumPaidContract;

                    newItem.��� = rmso.���;

                    // ��������� ���� � true, ��� ������ ��� ������ ������ ��� �������������.
                    rmso.Flag = true;

                    newList.Add(newItem);
                }

            }


            //������� ������ �� ���� 
            string filName = System.Windows.Forms.Application.StartupPath + @"\������\���-�� �� ������ ��������� � �����������.doc";


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
            Range wrdRng = doc.Bookmarks.get_Item(ref bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 6;
            table.Columns[1].Width = 40;
            table.Columns[2].Width = 120;
            table.Columns[3].Width = 80;
            table.Columns[4].Width = 80;
            table.Columns[5].Width = 80;
            table.Columns[6].Width = 80;
            table.Borders.Enable = 1; // ����� - �������� �����
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 10;
            //������� �����
            //int i = 1;

            string date = DateTime.Now.ToShortDateString();

            object wdrepl = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt = "date";
            object newtxt = (object)date;
            //object frwd = true;
            object frwd = false;
            doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            ref missing, ref missing);

            //�������� �������
            int k = 1;
            //������� ������ � �������
            //foreach (ReportMSO item in list)
            foreach (ReportMSO item in newList)
            {
                //table.Cell(i, 1).Range.Text = i.ToString();//item.���������������;
                table.Cell(k, 1).Range.Text = item.Num;

                table.Cell(k, 2).Range.Text = item.NameHospital;
                table.Cell(k, 3).Range.Text = item.CountContractHospital;
                table.Cell(k, 4).Range.Text = item.SumContractHospital;
                table.Cell(k, 5).Range.Text = item.SumPaidContract;
                table.Cell(k, 6).Range.Text = item.CountRoseRecords;


                //doc.Words.Count.ToString();
                Object beforeRow1 = Type.Missing;
                table.Rows.Add(ref beforeRow1);

                k++;
            }

            table.Rows[k].Delete();


            //�������� ������� ��������� �������� � �������
            DataTable tab���������;

            //������� �������� ���������� ������ �������������
            string query��������� = "select ���������,�������,�������� from ���������������������� " +
                                    "where id_��������� = (select id_��������� from dbo.����������������) ";

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                SqlTransaction transact = con.BeginTransaction();

                tab��������� = ���������.GetTableSQL(query���������, "���������", con, transact);

            }

            //try
            //{

            //������� ��������� ���������� ������
            string dolzhnost = tab���������.Rows[0]["���������"].ToString().Trim();

            //������� ��� ���������� ������
            string familija = tab���������.Rows[0]["�������"].ToString().Trim();

            //������� ��������
            string inicialy = tab���������.Rows[0]["��������"].ToString().Trim();

            object wdrepl2 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt2 = "familija";
            object newtxt2 = (object)familija;
            //object frwd = true;
            object frwd2 = false;
            doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            ref missing, ref missing);

            object wdrepl3 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt3 = "dolzhnost";
            object newtxt3 = (object)dolzhnost;
            //object frwd = true;
            object frwd3 = false;
            doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            ref missing, ref missing);

            object wdrepl4 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt4 = "inicialy";
            object newtxt4 = (object)inicialy;
            //object frwd = true;
            object frwd4 = false;
            doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
            ref missing, ref missing);


            //��������� ��������
            app.Visible = true;

        }

        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ������������2014�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ��� ��� ���������� ������ �������� ����� ������ �� 2014 �. � ����������� ������, �� �� � ����� �������� ���� ����� � ���.

            // ������ ������ 2014 �.
            //string beginDate = "01.01.2014";

            //string endDate = DateTime.Now.ToShortDateString();

            // ��� �����.
            string beginDate = "01.01.2014";

            string endDate = "31.12.2014";


            StatisticHospital statReport = new StatisticHospital(beginDate, endDate);
            statReport.ReportWord();
        }

        private void ��������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string scom = "select * from ������� where ��������������� = 'True' and ������������ = 'True'";
            DataTable tab������� = ���������.GetTableSQL(scom, "�������");

            //������ ���������� �� ��� �������� � �������� ���������
            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();

                foreach (DataRow row in tab�������.Rows)
                {
                    // ������� �� ����� ��������.
                    string numContr = row["�������������"].ToString().Trim();

                    // ������� id ��������.
                    int id������� = Convert.ToInt32(row["id_�������"]);

                    // ������� ����� �������.
                    string ������������ = row["������������"].ToString().Trim();

                    // ������ ���� �� ����� ������� � ������� � ������.

                    string like = numContr + "%";

                    string queryComp = "select * from ������������������� where ��������� like '" + like + "' ";
                    DataTable tabCompare = ���������.GetTableSQL(queryComp, "���������������������������");

                    if (tabCompare.Rows.Count != 0)
                    {
                        string queryUpdate = "update ������������������� " +
                                              "set id_������� = " + id������� + ", " +
                                              "������������ = '" + ������������ + "' " +
                                              "where ��������� like '" + like + "' ";


                        SqlCommand comUpdate = new SqlCommand(queryUpdate, con);
                        comUpdate.ExecuteNonQuery();

                    }

                }

            }
        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            �hangeAkt frmChange = new �hangeAkt();
            frmChange.MdiParent = this;
            frmChange.Show();
        }

        private void ����������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //������� �� ����� ������� � ����������
            //Dictionary<string, Unload> unload;
            //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            //�������� ������ ��� ����������� ����������� ��������
            List<ContractDisplayWord> list = new List<ContractDisplayWord>();

            // ������� � ��������� list ����� �������.
            ContractDisplayWord ����� = new ContractDisplayWord();
            �����.��������������� = "� �/�";
            �����.������������� = "� ��������";
            �����.��� = "���";
            �����.SumService = "����� ��������";

            list.Add(�����);


            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.r";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                //SerializableObject objToSerialize = null;
                FileStream fstream = File.Open(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //������� �� ����� ������� � ����������
                unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);

                // ��������� ������ � ��������� Word.

                //���������� ������ ����� �������� ��������� � ������� ������� (������ ���������)
                decimal sumCount = 0.0m;

                // ���������� ��� �������� ����������� ������ � �������.
                int numPP = 1;

                foreach (string keyVK in unload.Keys)
                {
                    ContractDisplayWord contract = new ContractDisplayWord();
                    contract.������������� = keyVK.Trim();

                    // ���������� �����.
                    contract.��������������� = numPP.ToString();

                    //������� ��� ������� ���� ������ ���������
                    Unload iv = unload[keyVK.Trim()];

                    //������ �� ���� ������� ������ ��������� ��� ���������
                    DataRow rowL = iv.��������.Rows[0];

                    // 
                    string ������� = rowL["�������"].ToString();
                    string ��� = rowL["���"].ToString();
                    string �������� = rowL["��������"].ToString();

                    //�������� ������ ��������� ��� ���������
                    contract.��� = ������� + " " + ��� + " " + ��������;


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

                    numPP++;
                }

                // ���������� �������� Word.
                //������� ������ �� ���� 
                string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ �������� ���������.doc";

                //������ ����� Word.Application
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                //��������� ��������
                Microsoft.Office.Interop.Word.Document doc = null;

                object fileName2 = filName;
                object falseValue = false;
                object trueValue = true;
                object missing = Type.Missing;

                doc = app.Documents.Open(ref fileName2, ref missing, ref trueValue,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing);

                //�������������� �������������� �����
                //doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;


                object bookNaziv = "�������";
                Range wrdRng = doc.Bookmarks.get_Item(ref bookNaziv).Range;

                object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 4, ref behavior, ref autobehavior);
                table.Range.ParagraphFormat.SpaceAfter = 6;
                table.Columns[1].Width = 30;
                table.Columns[2].Width = 80;
                table.Columns[3].Width = 180;
                table.Columns[4].Width = 180;
                //table.Columns[5].Width = 80;
                //table.Columns[6].Width = 80;
                table.Borders.Enable = 1; // ����� - �������� �����
                table.Range.Font.Name = "Times New Roman";
                table.Range.Font.Size = 10;

                //string date = DateTime.Now.ToShortDateString();

                //object wdrepl = WdReplace.wdReplaceAll;
                ////object searchtxt = "GreetingLine";
                //object searchtxt = "date";
                //object newtxt = (object)date;
                ////object frwd = true;
                //object frwd = false;
                //doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                //ref missing, ref missing);

                //�������� �������
                int k = 1;
                //������� ������ � �������
                //foreach (ReportMSO item in list)
                foreach (ContractDisplayWord item in list)
                {

                    //table.Cell(i, 1).Range.Text = i.ToString();//item.���������������;
                    table.Cell(k, 1).Range.Text = item.���������������;

                    table.Cell(k, 2).Range.Text = item.�������������;
                    table.Cell(k, 3).Range.Text = item.���;
                    table.Cell(k, 4).Range.Text = item.SumService;



                    //doc.Words.Count.ToString();
                    Object beforeRow1 = Type.Missing;
                    table.Rows.Add(ref beforeRow1);

                    k++;
                }

                table.Rows[k].Delete();

                //��������� ��������
                app.Visible = true;

                //������� �������� �����
                fstream.Close();

                //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
                Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

                //������� ���������� ����
                openFileDialog1.Dispose();

            }
            else
            {
                //���� ������������ ����� ������ ������ �� ������� �� ������
                return;
            }
        }

        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormWriteBD form = new FormWriteBD();
            form.MdiParent = this;
            form.Show();
        }

        private void ��������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form����������������� form = new Form�����������������();
            form.ShowDialog();

            if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string ������ = form.������������.Trim();
                int ��� = form.���;
                List<PersonRecipient> persons;
                try
                {
                    persons = GetPersons���������������������.GetPersons(������, ���);
                }
                catch (ExceptionYearNumber ex)
                {
                    ex.ErrorText = "�� ������ ����� ������� ��� ���";
                    MessageBox.Show(ex.ErrorText, "������", MessageBoxButtons.OK);

                    return;
                }

                // ������� ���������� ���������� �� ������.
                WordLetter word = new WordLetter(persons);
                word.PrintDoc();

            }
        }

        private void ��������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ��������� ��.
            string sProvid = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.mdb";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                string connect = "Provider=Microsoft.JET.OLEDB.4.0;data source=" + fileName;

                try
                {
                    DataSetClass ds = new DataSetClass(connect);
                    DataSetHospital dsh = ds.GetDataHospital();

                    FormEditDateHospital form = new FormEditDateHospital(dsh, connect);
                    form.Show();
                }
                catch (ExceptionUser exc)
                {
                    MessageBox.Show(exc.ErrorText, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }



            //StreamReader s = File.OpenText(System.Windows.Forms.Application.StartupPath + "\\path.txt");
            //Data.connectionString = s.ReadLine();
            //s.Close();
            //Data.connectionString = Data.connectionString + Application.StartupPath + "\\db.mdb";

            //OleDbConnection con = new OleDbConnection(
        }

        private void ������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInv form = new FormInv();
            form.Show();
        }

        private void ����������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfig form = new FormConfig();
            form.Show();
        }

        private void �������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FlagConnectServer == false)
            {
                FlagConnectServer = true;

                foreach (ToolStripMenuItem tsmi in this.menuStrip1.Items)
                {
                    if (tsmi.Text == "���������")
                    {
                        foreach (ToolStripMenuItem item in tsmi.DropDownItems)
                        {
                            if (item.Text == "��������� �������� ��������")
                            {
                                item.Text = "�������� �������� ��������";
                            }
                        }
                    }
                }
            }
            else
            {
                FlagConnectServer = false;

                foreach (ToolStripMenuItem tsmi in this.menuStrip1.Items)
                {
                    if (tsmi.Text == "���������")
                    {
                        foreach (ToolStripMenuItem item in tsmi.DropDownItems)
                        {
                            if (item.Text == "�������� �������� ��������")
                            {
                                item.Text = "��������� �������� ��������";
                            }
                        }
                    }
                }
            }
        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillPerson fillPerson = new FillPerson();
            List<Person> listPerson = fillPerson.GetPersonzs();

            var rTestCOunt = listPerson.Count;

            if (listPerson.Count > 0)
            {
                PullConnectBD pull = new PullConnectBD();
                Dictionary<string, string> pullConnect = pull.GetPull(FlagConnectServer);

                foreach (KeyValuePair<string, string> dStringConnect in pullConnect)
                {

                    string sConnection = string.Empty;
                    sConnection = dStringConnect.Value.ToString().Trim();

                    var test = listPerson;

                    EsrnValid esrn = new EsrnValid(sConnection, listPerson);
                    bool flagErrorConnect = esrn.FlagError;

                    if (flagErrorConnect == false)
                    {

                        esrn.Validate();
                    }

                }

                // ������� ��������� ����������.
                var listWrite = listPerson.Where(w => w.FlagValid == true);

                var iCount = listWrite.Count();

                WriteSity wr = new WriteSity(listWrite);
                wr.WriteDB();

                MessageBox.Show("������ ������� �����������");
            }
        }

        private void �����������������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReportPrint formPrint = new FormReportPrint();
            formPrint.Show();
        }

        private void ������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // 
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            openFileDialog1.Filter = "|*.r";
            //openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                FileInfo fi = new FileInfo(fileName);

                // ������� ���� � �����.
                string DirectoryPath = fi.DirectoryName;

                try
                {

                    // ������� �������� ����� � ��������� ��� ����� �� ��.
                    DirectoryInfo dir = new DirectoryInfo(DirectoryPath);

                    // ������� ����� �� ����������.
                    FileInfo[] files = dir.GetFiles();

                    var asd = files.Count();

                    foreach (FileInfo file in files)
                    {
                        var sTest = "";

                        var iLegth = file.Name.Split('+').Length;

                        if (file.Name.Split('+').Length == 2)
                        {

                            var rr = file.Name;

                            // ������� �������� �����.
                            FileStream fstream = file.Open(FileMode.Open);

                            BinaryFormatter binaryFormatter = new BinaryFormatter();

                            // ������� ���������� �����.
                            unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);

                            List<Fire.FireHelp> listFire = new List<Fire.FireHelp>();

                            foreach (var itm in unload)
                            {
                                Fire.FireHelp item = new Fire.FireHelp();

                                item.NumberContract = itm.Value.�������.Rows[0]["�������������"].ToString().Trim();
                                item.����������������� = itm.Value.�����������������.Trim();

                                listFire.Add(item);
                            }

                            Fire.IFireExecute fireLK = new Fire.HelpFire�����������������();
                            string query = fireLK.Query(listFire);

                            try
                            {
                                ExecuteQuery.Execute(query);
                            }
                            catch (Exception ex)
                            {
                                string queryTest = query;


                                string iTest = "";
                            }

                            //������� �������� �����
                            fstream.Close();
                        }


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " �������� �� ������� ������ ������� �� �������� �������� �������� ���������.");

                    return;
                }

                //��������� ��� ����� ��������� ������� ���������� ��� ����������� ���������� ���� � ��
                Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

                //������� ���������� ����
                openFileDialog1.Dispose();

                MessageBox.Show("������");

            }
            else
            {
                //���� ������������ ����� ������ ������ �� ������� �� ������
                return;
            }

        }

        private void �������������VipNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltrPersonForVipNet fp = new FiltrPersonForVipNet();
            fp.Show();
        }

        /// <summary>
        /// �������� ������ ���������� ���������.
        /// </summary>
        /// <param name="unitDate"></param>
        /// <param name="displayContracts"></param>
        /// <param name="idProjectRegistr"></param>
        private string InsertNumbersContracts(List<DisplayContract> displayContracts, int idProjectRegistr)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (DisplayContract itm in displayContracts)
            {
                string query = @"INSERT INTO [dbo].[ListNumbersProgectsReestr]
                               ([IdProjectRegistrFiles]
                               ,[NumberContract]
                               ,[SummaContract]
                               ,[FIO])
                         VALUES
                               (" + idProjectRegistr + " " +
                               ",'" + itm.NumberContract + "' " +
                               "," + itm.SummContract + " " +
                               ",'" + itm.FioPerson + "') ";

                stringBuilder.Append(query);

                //ListNumbersProgectsReestr item = new ListNumbersProgectsReestr();

                //// Id ������ ����� �������� ���������.
                //item.IdProjectRegistrFiles = idProjectRegistr;

                //// ����� ���������.
                //item.NumberContract = itm.NumberContract;

                //// ����� ���������.
                //item.SummaContract = itm.SummContract;

                //// ��� ���������.
                //item.FIO = itm.FioPerson;

                //// �������� � ��.
                //unitDate.ListNumbersProgectsReestr.Insert(item);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// �������� ������ ���������� ���������.
        /// </summary>
        /// <param name="unitDate"></param>
        /// <param name="displayContracts"></param>
        /// <param name="idProjectRegistr"></param>
        private void InsertNumbersContracts(UnitDate unitDate, List<DisplayContract> displayContracts, int idProjectRegistr)
        {
            foreach (DisplayContract itm in displayContracts)
            {
                ListNumbersProgectsReestr item = new ListNumbersProgectsReestr();

                // Id ������ ����� �������� ���������.
                item.IdProjectRegistrFiles = idProjectRegistr;

                // ����� ���������.
                item.NumberContract = itm.NumberContract;

                // ����� ���������.
                item.SummaContract = itm.SummContract;

                // ��� ���������.
                item.FIO = itm.FioPerson;

                // �������� � ��.
                unitDate.ListNumbersProgectsReestr.Insert(item);
            }
        }



        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ���������� ��� �������� ����� �����.
            string fileName = string.Empty;

            // ������� ���������� ����������.
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            //openFileDialog1.Filter = "|*.r";
            // openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;

            // ���� � ����� ��� ���������.
            openFileDialog1.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            // ������ ��� �������� ������ �� ������� �������� ��������.
            List<ItemLibrary> packegeDateContract = new List<ItemLibrary>();

            // ���������� ���� ��� �������� ������ �������� ���������.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // �������� ��� ������.
                fileName = openFileDialog1.FileName;

                try
                {

                    using (FileStream fstream = File.Open(fileName, FileMode.Open))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();

                        // ������� �� ����� ������� � ����������.
                        unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);
                    }
                }
                catch
                {
                    MessageBox.Show("���� �� �������� �������� ������� ���������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ������ �������� ��������� ������������ � ������.
                List<DisplayContract> listContracts = new List<DisplayContract>();

                // ����������� ������ �������� ���������.
                Registr registrContracts = new Registr(unload);

                // ��������� ���������� ������� � ���������� ���� � ��������� �������� ��� � ��.
                IPrintContract printContract = new PrintContract(registrContracts);
                bool flagWriteDB = printContract.PrintContractDraft();

            //    if (flagWriteDB == true)
            //    {
            //        // �������� ������ �������� ���������.
            //        var listUnload = unload.Values.ToList<Unload>();

            //        // ������� ������ �������� ���������.
            //        foreach (var unload in listUnload)
            //        {
            //            using (var dc = new DContext(ConnectDB.ConnectionString()))
            //            {
            //                #region ���� �� ��������.
            //                //// ��������� ��������� ������ �� �������.
            //                //ItemLibrary itemLibrary = new ItemLibrary();

            //                //// ����� � ������� � ��������.
            //                //PackageClass packege = new PackageClass();

            //                //var outer = MyTask.Task.Factory.StartNew(() =>
            //                //{

            //                //    // � ��������� ������ �������� ���������� �����.
            //                //    var outetThoun = MyTask.Task.Factory.StartNew(() =>
            //                //    {
            //                //        // ��������� ���������� ����� �� ������� �������� ���������.
            //                //        DataTable tabSity = unload.��������������;

            //                //        // ���������� ����� � ������ �������� ��������.
            //                //        IReadRegistr<���������������> read��������������� = new Read���������������(dc, tabSity);

            //                //        // ������� ���������� ����� � ud ����� ��.
            //                //        ��������������� thoun = read���������������.Get();

            //                //        // ������� �������� ����������� ������.
            //                //        packege.�������������� = thoun;

            //                //        return thoun;

            //                //        // �������������� ���������� ������� ������ � ����������.
            //                //    }, MyTask.TaskCreationOptions.AttachedToParent);

            //                //    // ������� ���������� �����.
            //                //    ��������������� �������������� = outetThoun.Result;

            //                //    // � ��������� ������ �������� �������� ���������.
            //                //    var outerLK = MyTask.Task.Factory.StartNew(() =>
            //                //    {
            //                //        // ������� �������� ���������.
            //                //        string ����������������� = unload.�����������������.Trim();

            //                //        IReadRegistr<������������������> readRegistrLK = new Read�����������������(dc, �����������������);

            //                //        ������������������ lk = readRegistrLK.Get();

            //                //        // ������� ��������� ���������.
            //                //        packege.������������������ = lk;

            //                //        return lk;

            //                //        // �������������� ���������� ������� ������ � ����������.
            //                //    }, MyTask.TaskCreationOptions.AttachedToParent);

            //                //    // ������� �������� ���������.
            //                //    ������������������ ������������������ = outerLK.Result;

            //                //    // ������ ������ �� ���������.
            //                //    var outerPersonDate = MyTask.Task.Factory.StartNew(() =>
            //                //    {
            //                //        // ���������� �� ����� �������� ��� ������ �� ���������.
            //                //        DataRow rw_�������� = unload.��������.Rows[0];

            //                //        // ������� ������ �� ��������� �� ������� �������� ��������.
            //                //        IReadRegistr<���������> readPerson = new Read��������(dc, rw_��������);

            //                //        ��������� person = readPerson.Get();

            //                //        // �������� ��������� id ����������� ������.
            //                //        if (�������������� != null)
            //                //        {
            //                //            // �������� ��������� id ����������� ������.
            //                //            person.id_�������� = ��������������.id_��������;
            //                //        }
            //                //        else
            //                //        {
            //                //            // ���� �� ���� �� ����� �� ���������.
            //                //            //�������� exception.
            //                //        }

            //                //        // �������� �������� ���������.
            //                //        if (������������������ != null)
            //                //        {
            //                //            person.id_����������������� = ������������������.id_�����������������;
            //                //        }
            //                //        else
            //                //        {
            //                //            // ������� Exception.
            //                //        }

            //                //        // ������� ���������.
            //                //        packege.�������� = person;

            //                //        return person;

            //                //        // �������������� ��������� ������ � �������.
            //                //    }, MyTask.TaskCreationOptions.AttachedToParent);

            //                //    // ������� ������ �� ���������.
            //                //    ��������� �������� = outerPersonDate.Result;

            //                //    // ������� ������ �� ��������.
            //                //    var outerDoc = MyTask.Task.Factory.StartNew(() =>
            //                //    {
            //                //        // ��������� ��� ��������� ���������.
            //                //        IReadRegistr<������������> readTypeDoc = new Read������������(dc, unload.������������);
            //                //        ������������ typeDoc = readTypeDoc.Get();

            //                //        // ������� ��� ���������.
            //                //        packege.������������ = typeDoc;

            //                //        // �������� ��� ���������.
            //                //        if (typeDoc != null)
            //                //            ��������.id_�������� = typeDoc.id_��������;

            //                //    }, MyTask.TaskCreationOptions.AttachedToParent);

            //                //    // ������ �� ������������.
            //                //    var outtHospitl = MyTask.Task.Factory.StartNew(() =>
            //                //    {
            //                //        // ������� ������ �� ������������.
            //                //        DataRow rowHosp = unload.������������.Rows[0];

            //                //        string inn = rowHosp["���"].ToString();

            //                //        IReadRegistr<�������������> readHosp = new ReadRegistrHospital(dc, inn, rowHosp);
            //                //        ������������� hosp = readHosp.Get();

            //                //        // ������� ������ �� ������������.
            //                //        packege.hosp = hosp;

            //                //        // �������������� ���������� � ������� �����.
            //                //    }, MyTask.TaskCreationOptions.AttachedToParent);

            //                //    var outerContract = MyTask.Task.Factory.StartNew(() =>
            //                //    {
            //                //        // ������� �������.
            //                //        DataRow rowC = unload.�������.Rows[0];

            //                //        // ��������� ������ �� ��������.
            //                //        IReadRegistr<��������> readContract = new Read�������(dc, rowC);
            //                //        �������� �������� = readContract.Get();

            //                //        return ��������;

            //                //    }, MyTask.TaskCreationOptions.AttachedToParent);

            //                //    //var outerContract = MyTask.Task.Factory.StartNew(() =>
            //                //    //{
            //                //    //    // ������ �� ��������.
            //                //    //    DataTable tabServices = unload.����������������;

            //                //    //    Read���������������� read������������� = new Read����������������(tabServices);
            //                //    //    List<�����������������> listUSlug = read�������������.Get();

            //                //    //    // ������ �� ��������.
            //                //    //    packege.listUSlug = listUSlug;
            //                //    //})

            //                //});

            //                //// �������� ��������� �������� ������.
            //                //outer.Wait();

            //                #endregion

            //                ItemLibrary itemLibrary = new ItemLibrary();

            //                // ����� � ������� � ��������.
            //                PackageClass packege = new PackageClass();

            //                // ��������� ���������� ����� �� ������� �������� ���������.
            //                DataTable tabSity = unload.��������������;

            //                // ���������� ����� � ������ �������� ��������.
            //                IReadRegistr<���������������> read��������������� = new Read���������������(dc, tabSity);

            //                // ������� ���������� ����� � ud ����� ��.
            //                ��������������� �������������� = read���������������.Get();

            //                // ������� �������� ����������� ������.
            //                packege.�������������� = ��������������;

            //                // ������� �������� ���������.
            //                string ����������������� = unload.�����������������.Trim();

            //                IReadRegistr<������������������> readRegistrLK = new Read�����������������(dc, �����������������);

            //                ������������������ ������������������ = readRegistrLK.Get();

            //                // ������� �������� ���������.
            //                packege.������������������ = ������������������;

            //                // ���������� �� ����� �������� ��� ������ �� ���������.
            //                DataRow rw_�������� = unload.��������.Rows[0];

            //                // ������� ������ �� ��������� �� ������� �������� ��������.
            //                IReadRegistr<���������> readPerson = new Read��������(dc, rw_��������);

            //                ��������� �������� = readPerson.Get();

            //                // �������� ��������� id ����������� ������.
            //                if (�������������� != null)
            //                {
            //                    // �������� ��������� id ����������� ������.
            //                    ��������.id_�������� = ��������������.id_��������;
            //                }
            //                else
            //                {
            //                    // ���� �� ���� �� ����� �� ���������.
            //                    //�������� exception.
                                
            //                }

            //                // �������� �������� ���������.
            //                if (������������������ != null)
            //                {
            //                    ��������.id_����������������� = ������������������.id_�����������������;
            //                }
            //                else
            //                {
            //                    // ������� Exception.
            //                }

            //                // ������� ���������.
            //                packege.�������� = ��������;

            //                // ��������� ��� ��������� ���������.
            //                IReadRegistr<������������> readTypeDoc = new Read������������(dc, unload.������������);
            //                ������������ typeDoc = readTypeDoc.Get();

            //                // ������� ��� ���������.
            //                packege.������������ = typeDoc;

            //                // �������� ��� ���������.
            //                if (typeDoc != null)
            //                    ��������.id_�������� = typeDoc.id_��������;

            //                // ������� ������ �� ������������.
            //                DataRow rowHosp = unload.������������.Rows[0];

            //                string inn = rowHosp["���"].ToString();

            //                IReadRegistr<�������������> readHosp = new ReadRegistrHospital(dc, inn, rowHosp);
            //                ������������� hosp = readHosp.Get();

            //                // ������� ������ �� ������������.
            //                packege.hosp = hosp;

            //                // ������� �������.
            //                DataRow rowC = unload.�������.Rows[0];

            //                // ��������� ������ �� ��������.
            //                IReadRegistr<��������> readContract = new Read�������(dc, rowC);
            //                �������� �������� = readContract.Get();

            //                // ������� ������ �� ��������.
            //                packege.�������� = ��������;

            //                // ������ �� ��������.
            //                DataTable tabServices = unload.����������������;

            //                Read���������������� read������������� = new Read����������������(tabServices);
            //                List<�����������������> listUSlug = read�������������.Get();

            //                // ������ �� ��������.
            //                packege.listUSlug = listUSlug;

            //                itemLibrary.NumContract = ��������.�������������;
            //                itemLibrary.Packecge = packege;

            //                packegeDateContract.Add(itemLibrary);
            //            }

            //        }

            //    }
            }

            
            //// ������� ������ ��������� �� ����� ������� ��� ��������� ��� ����������� ��������� ���������.
            //ValidateContractPerson vclPrint = new ValidateContractPerson(packegeDateContract);
            //List<PrintContractsValidate> listDoc = vclPrint.GetContract();

            //// ���� ���������� ���������� �� ����� ������� > 0.
            //if (listDoc != null && listDoc.Count > 0)
            //{
            //    try
            //    {
            //        // ����������� ���� ������� .
            //        string nameFile = System.IO.Path.GetFileName(fileName);

            //        string directoryName = System.IO.Path.GetDirectoryName(fileName);

            //        FileInfo file = new FileInfo(fileName);
            //        file.Rename(nameFile);
            //    }
            //    catch
            //    {
            //        MessageBox.Show("���� ������� �������� ��������� �� ������������","������",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    }

            //    // ������� ������ ���������� �������� �� ������ � Word.
            //    WordReport wordPrint = new WordReport(listDoc);

            //    DocPrint docPrint = new DocPrint(wordPrint);
            //    docPrint.Execute();
            //}

            //// ���� ������ �������� ������ 0 ����� �������� �������� � ����.
            //if (listDoc != null && listDoc.Count > 0)
            //{

            //    DialogResult result = MessageBox.Show("������ �������� �������� ���������", "��������", MessageBoxButtons.OKCancel);

            //    if (result == DialogResult.OK)
            //    {
            //        // ������������ �������� �������� �� �� ����.
            //        EsrnPersonValidate esrnPersonValidate = new EsrnPersonValidate(packegeDateContract);
            //        esrnPersonValidate.Validate();

            //        // �������� ��������� �� ����.
            //        var itemResult = packegeDateContract;

            //        // ����������� � ����� ��.
            //        using (DContext dc = new DContext(ConnectDB.ConnectionString()))
            //        {
            //            // ����������� ������ ������������.
            //            IServices<ControlDantist.DataBaseContext.���������> servicesHospital = new ServicesMedicalHospital(dc);

            //            //packegeDateContract.Where(w=>w.)

            //            // ������ ���������.
            //            ReestrContract reestrContract = new ReestrContract(packegeDateContract);

            //            // ������� ������ ����� ��������� �� �������� ����� � ��.
            //            ControlDantist.MedicalServices.ValidateMedicalServices validateMedServis = new MedicalServices.ValidateMedicalServices(reestrContract, servicesHospital);

            //            // �������� ��������� ����� � ������� � ������� ����������� �� ����� �������.
            //            validateMedServis.ValidateServices();

            //        }

            //        // ��������� ��������� ��������.
            //        FormValidOutEsrn formValid = new FormValidOutEsrn(packegeDateContract);

            //        // ������� ���� � ������������ ��������.
            //        formValid.Show();

            //    }
            //}

        }



        private void ��������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLimitMoney formLimitMoney = new FormLimitMoney();
            formLimitMoney.MdiParent = this;
            formLimitMoney.Show();
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ������� ��� ����� ���� ���������� �������� ���������.
            ReceptionContractsForm receptionContractsForm = new ReceptionContractsForm();

            receptionContractsForm.SaveProjectFIle = false;

            DialogResult dialogResult = receptionContractsForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                //    DateTime dtOut;

                //    if (DateTime.TryParse(receptionContractsForm.DateLetter.ToShortDateString(), out dtOut) == true)
                //    {
                //        IVisableRegistrs registrProjectContract = new RegistrProjectContract(receptionContractsForm.NumberLetter, receptionContractsForm.DateLetter, receptionContractsForm.IdHospital, this.FlagConnectServer);

                //        registrProjectContract.Create();
                //    }

                return;

            }
        }

        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormStatistic formStatistic = new FormStatistic();
            formStatistic.Show();

            return;
        }

        private void ��������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ����� ����������������� ������.
            FindLettert findLettert = new FindLettert();
            //findLettert.MdiParent = this;
            //findLettert.WindowState = FormWindowState.Maximized;
            findLettert.Show();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ���������� ��� �������� ����� �����.
            string fileName = string.Empty;

            // ������� ���������� ����������.
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            //openFileDialog1.Filter = "|*.r";
            // openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;

            // ���� � ����� ��� ���������.
            openFileDialog1.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            // ������ ��� �������� ������ �� ������� �������� ��������.
            List<ItemLibrary> packegeDateContract = new List<ItemLibrary>();

            // ���������� ���� ��� �������� ������ �������� ���������.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // �������� ��� ������.
                fileName = openFileDialog1.FileName;

                try
                {

                    using (FileStream fstream = File.Open(fileName, FileMode.Open))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();

                        // ������� �� ����� ������� � ����������.
                        unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);
                    }
                }
                catch
                {
                    MessageBox.Show("���� �� �������� �������� ������� ���������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ������ �������� ��������� ������������ � ������.
                List<DisplayContract> listContracts = new List<DisplayContract>();

                // ����������� ������ �������� ���������.
                Registr registrContracts = new Registr(unload);

                // �������� ������ �������� ���������.
                var listUnload = unload.Values.ToList<Unload>();

                // ������� ������ �������� ���������.
                foreach (var unload in listUnload)
                {
                    // ����������� � ��.
                    using (var dc = new DContext(ConnectDB.ConnectionString()))
                    {
                        ItemLibrary itemLibrary = new ItemLibrary();

                        // ����� � ������� � ��������.
                        PackageClass packege = new PackageClass();

                        // ��������� ���������� ����� �� ������� �������� ���������.
                        DataTable tabSity = unload.��������������;

                        // ���������� ����� � ������ �������� ��������.
                        IReadRegistr<���������������> read��������������� = new Read���������������(dc, tabSity);

                        // ������� ���������� ����� � ud ����� ��.
                        ��������������� �������������� = read���������������.Get();

                        // ������� �������� ����������� ������.
                        packege.�������������� = ��������������;

                        // ������� �������� ���������.
                        string ����������������� = unload.�����������������.Trim();

                        IReadRegistr<������������������> readRegistrLK = new Read�����������������(dc, �����������������);

                        ������������������ ������������������ = readRegistrLK.Get();

                        // ������� �������� ���������.
                        packege.������������������ = ������������������;

                        // ���������� �� ����� �������� ��� ������ �� ���������.
                        DataRow rw_�������� = unload.��������.Rows[0];

                        // ������� ������ �� ��������� �� ������� �������� ��������.
                        IReadRegistr<���������> readPerson = new Read��������(dc, rw_��������);

                        ��������� �������� = readPerson.Get();

                        // �������� ��������� id ����������� ������.
                        if (�������������� != null)
                        {
                            // �������� ��������� id ����������� ������.
                            ��������.id_�������� = ��������������.id_��������;
                        }
                        else
                        {
                            // ���� �� ���� �� ����� �� ���������.
                            //�������� exception.

                        }

                        // �������� �������� ���������.
                        if (������������������ != null)
                        {
                            ��������.id_����������������� = ������������������.id_�����������������;
                        }
                        else
                        {
                            // ������� Exception.
                        }

                        // ������� ���������.
                        packege.�������� = ��������;

                        // ��������� ��� ��������� ���������.
                        IReadRegistr<������������> readTypeDoc = new Read������������(dc, unload.������������);
                        ������������ typeDoc = readTypeDoc.Get();

                        // ������� ��� ���������.
                        packege.������������ = typeDoc;

                        // �������� ��� ���������.
                        if (typeDoc != null)
                            ��������.id_�������� = typeDoc.id_��������;

                        // ������� ������ �� ������������.
                        DataRow rowHosp = unload.������������.Rows[0];

                        string inn = rowHosp["���"].ToString();

                        IReadRegistr<�������������> readHosp = new ReadRegistrHospital(dc, inn, rowHosp);
                        ������������� hosp = readHosp.Get();

                        // ������� ������ �� ������������.
                        packege.hosp = hosp;

                        // ������� �������.
                        DataRow rowC = unload.�������.Rows[0];

                        // ��������� ������ �� ��������.
                        IReadRegistr<��������> readContract = new Read�������(dc, rowC);
                        �������� �������� = readContract.Get();

                        // ������� ������ �� ��������.
                        packege.�������� = ��������;

                        // ������ �� ��������.
                        DataTable tabServices = unload.����������������;

                        Read���������������� read������������� = new Read����������������(tabServices);
                        List<�����������������> listUSlug = read�������������.Get();

                        // ������ �� ��������.
                        packege.listUSlug = listUSlug;

                        itemLibrary.NumContract = ��������.�������������;
                        itemLibrary.Packecge = packege;

                        packegeDateContract.Add(itemLibrary);
                    }
                }

                // ������� ������ ��������� �� ����� ������� ��� ��������� ��� ����������� ��������� ���������.
                ValidateContractPerson vclPrint = new ValidateContractPerson(packegeDateContract);
                List<PrintContractsValidate> listDoc = vclPrint.GetContract();

                // ���� ���������� ���������� �� ����� ������� > 0.
                if (listDoc != null && listDoc.Count > 0)
                {
                    try
                    {
                        // ����������� ���� ������� .
                        string nameFile = System.IO.Path.GetFileName(fileName);

                        string directoryName = System.IO.Path.GetDirectoryName(fileName);

                        FileInfo file = new FileInfo(fileName);
                        file.Rename(nameFile);
                    }
                    catch
                    {
                        MessageBox.Show("���� ������� �������� ��������� �� ������������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //// ������� ������ ���������� �������� �� ������ � Word.
                    //WordReport wordPrint = new WordReport(listDoc);

                    //DocPrint docPrint = new DocPrint(wordPrint);
                    //docPrint.Execute();
                }

                // ���� ������ �������� ������ 0 ����� �������� �������� � ����.
                if (listDoc != null && listDoc.Count > 0)
                {

                    DialogResult result = MessageBox.Show("������ �������� �������� ���������", "��������", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.OK)
                    {
                        // ������������ �������� �������� �� �� ����.
                        EsrnPersonValidate esrnPersonValidate = new EsrnPersonValidate(packegeDateContract);
                        esrnPersonValidate.Validate();

                        // �������� ��������� �� ����.
                        var itemResult = packegeDateContract;

                        // ����������� � ����� ��.
                        using (DContext dc = new DContext(ConnectDB.ConnectionString()))
                        {
                            // ����������� ������ ������������.
                            IServices<ControlDantist.DataBaseContext.���������> servicesHospital = new ServicesMedicalHospital(dc);

                            //packegeDateContract.Where(w=>w.)

                            // ������ ���������.
                            ReestrContract reestrContract = new ReestrContract(packegeDateContract);

                            // ������� ������ ����� ��������� �� �������� ����� � ��.
                            ControlDantist.MedicalServices.ValidateMedicalServices validateMedServis = new MedicalServices.ValidateMedicalServices(reestrContract, servicesHospital);

                            // �������� ��������� ����� � ������� � ������� ����������� �� ����� �������.
                            validateMedServis.ValidateServices();

                        }

                        // ��������� ��������� ��������.
                        FormValidOutEsrn formValid = new FormValidOutEsrn(packegeDateContract);

                        // ������� ���� � ������������ ��������.
                        formValid.Show();

                    }
                }


            }

        }

        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlDantist.Reports.PrintReportLimitLBO pr = new ControlDantist.Reports.PrintReportLimitLBO();
            pr.Print();
        }

        private void ������2019���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            //���������� ������������
            int iConfig;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������� ���� �������";
            //openFileDialog1.Filter = "|*.r";
            // openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;

            // ���� � ����� ��� ���������.
            openFileDialog1.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

            }
        }

        private void ������2019���ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            //// ������ ���������� ���� � ����������.
            //string dirName = null;

            ////���������� ������������
            //int iConfig;

            ////// ������ ��� ������� � �����������.
            ////FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            ////DialogResult result = folderBrowserDialog1.ShowDialog();

            //// ����������� ��� ������� � ��.
            //UnitDate unitDate = new UnitDate();

            //Repository.DataClasses1DataContext dc = unitDate.DateContext;

            //var table = dc.���������������.Select(w => new ���Hosp { IdHosp = Convert.ToInt32(w.F1), ��� = (double)w.F3 }).ToList();

            ////if (result == DialogResult.OK)
            ////{
            //// ������� ���� � �����.
            ////dirName = folderBrowserDialog1.SelectedPath;

            ////dirName = @"\\10.159.102.128\d$\��� ������\������������������ 2019\��������\8\�������\"; // ����������";

            ////MessageBox.Show("���������� ������ - " + Directory.GetDirectories(dirName).Count());

            //dirName = @"F:\!\1�����������";

            //    // ��������� �� ���� ��������� ���������.
            //foreach (var dir in Directory.GetDirectories(dirName))
            //    {
            //        // �������� ���������� �� ������ ����������.
            //        if (Directory.Exists(dir))
            //        {
            //            string[] files = Directory.GetFiles(dir);

            //            // �������� �� ������ � ��������� ����������.
            //            foreach (var fileName in files)
            //            {
            //                // ������� ��� ����� � ��� ���� ��� ������ ���� � ����� ����� � ���.
            //                FileInfo fileInf = new FileInfo(fileName);

            //                string fileFull = fileInf.FullName;

            //                using (FileStream fstream = File.Open(fileName, FileMode.Open))
            //                {
            //                    BinaryFormatter binaryFormatter = new BinaryFormatter();

            //                //try
            //                //{
            //                // �������� ������������� �� ���� �������.
            //                //if ((Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream) is Dictionary<string, Unload>)
            //                //{
            //                // ������� �� ����� ������� � ����������.
            //                unload = (Dictionary<string, DantistLibrary.Unload>)binaryFormatter.Deserialize(fstream);
            //                //List<Unload> unload = (List<Unload>)binaryFormatter.Deserialize(fstream);
            //                //}
            //                //else
            //                //{
            //                //    continue;
            //                //}
            //                //}
            //                //catch
            //                //{
            //                //    using (TextWriter write = File.AppendText(@"F:\!\����\2020 ���\������\fileLog.txt"))
            //                //    {
            //                //        write.WriteLine(fileFull);
            //                //    }

            //                //    continue;
            //                //}

            //                // ����������� ������ �������� ���������.
            //                Registr registrContracts = new Registr(unload);

            //                    // ��������� ������ �������� ����������.
            //                    var option = new System.Transactions.TransactionOptions();
            //                    option.IsolationLevel = System.Transactions.IsolationLevel.Serializable;

            //                    // ������� ��������� � ����� � ��.
            //                    // ����� ������ � ������� � ������ ����������.
            //                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, option))
            //                    {
            //                        var listUnload = unload.Values.ToList<Unload>();

            //                        foreach (Unload unload2 in listUnload)
            //                        {
            //                            // // ������������ ����������� ������.
            //                            ISity sity = new NameSity();

            //                            DataTable tabSity = unload.��������������;

            //                            if (tabSity.Rows.Count > 0 && tabSity.Rows[0]["������������"] != DBNull.Value)
            //                            {
            //                                // ������� ������������ ����������� ������ � ������� ��������� ��������.
            //                                sity.NameTown = tabSity.Rows[0]["������������"].ToString().Trim();
            //                            }
            //                            else
            //                            {
            //                                sity.NameTown = "";
            //                            }

            //                            // ������� �������� ���������.
            //                            Repository.����������������� ����������������� = unitDate.�����������������Repository.Get�����������������(unload.�����������������.Trim());

            //                            // ���������� �� ����� �������� ��� ������ �� ���������.
            //                            DataRow rw_�������� = unload.��������.Rows[0];

            //                            ��������Add personFull = new ��������Add();

            //                            personFull.������� = rw_��������["�������"].ToString().Trim();
            //                            personFull.��� = rw_��������["���"].ToString().Trim();
            //                            personFull.�������� = rw_��������["��������"].ToString().Trim();
            //                            //personFull.DateBirtch = " Convert(datetime,'" + �����.����(Convert.ToDateTime(rw_��������["������������"]).ToShortDateString().Trim()) + "',112)  ";
            //                            personFull.������������ = Convert.ToDateTime(rw_��������["������������"]);
            //                            personFull.����� = rw_��������["�����"].ToString().Trim();
            //                            personFull.��������� = rw_��������["���������"].ToString().Trim();
            //                            personFull.������ = rw_��������["������"].ToString().Trim();
            //                            personFull.������������� = rw_��������["�������������"].ToString().Trim();
            //                            personFull.������������� = rw_��������["�������������"].ToString().Trim();
            //                            personFull.������������� = rw_��������["�������������"].ToString().Trim();
            //                            personFull.������������������ = Convert.ToDateTime(rw_��������["������������������"]);
            //                            personFull.��������������� = rw_��������["���������������"].ToString().Trim();
            //                            personFull.id_����������������� = �����������������.id_�����������������;
            //                            personFull.id_�������� = (int)unload.������������.Rows[0][0];//                      ",@id��������_" + iCount + " " +
            //                            personFull.�������������� = rw_��������["��������������"].ToString().Trim();
            //                            personFull.�������������� = rw_��������["��������������"].ToString().Trim();
            //                            personFull.������������������� = Convert.ToDateTime(rw_��������["�������������������"]);
            //                            personFull.���������������� = rw_��������["����������������"].ToString().Trim();
            //                            personFull.id_������� = 1;//id ������� � ��� �� ��������� 
            //                            personFull.id_����� = Convert.ToInt16(rw_��������["id_�����"]);

            //                            if ((personFull.�������.Trim().ToLower() == "������".ToLower().Trim()) && (personFull.���.Trim().ToLower() == "������".ToLower().Trim()) && (personFull.��������.Trim().ToLower() == "����������".ToLower().Trim()))
            //                            {
            //                                DateTime dt = new DateTime(1942, 8, 2);

            //                                personFull.������������������ = dt;
            //                            }

            //                            // ������� id ����������� ������.
            //                            var findSity = unitDate.���������������Repository.Filtr���������������(sity.NameTown);

            //                            if (findSity != null)
            //                            {
            //                                personFull.id_�������� = findSity.id_��������;
            //                            }
            //                            else
            //                            {
            //                                �������������� �������������� = new ��������������();
            //                                ��������������.������������ = sity.NameTown;

            //                                // ������� �� ����� ���������� �����.
            //                                unitDate.���������������Repository.Insert(��������������);

            //                                personFull.id_�������� = ��������������.id_��������;
            //                            }

            //                            ��������Add personAdd = unitDate.��������AddRepository.SelectPerson(personFull);

            //                            // �������� ���� �� �������� � ��� � ����� �������� � ��.
            //                            if (personAdd != null)
            //                            {
            //                                // ������� ������ �� ���������.
            //                                unitDate.��������AddRepository.Update(personFull);

            //                                // �������� id ���������.
            //                                personFull.id_�������� = personAdd.id_��������;
            //                            }
            //                            else
            //                            {
            //                                // ������� ������ ���������.
            //                                unitDate.��������AddRepository.Insert(personFull);
            //                            }

            //                            // ������� �������.
            //                            DataRow rowC = unload.�������.Rows[0];

            //                            �������Add contract = new �������Add();

            //                            // ������� ������ �� ������������.
            //                            DataRow rowHosp = unload.������������.Rows[0];

            //                            // �������� ������ �� ������������.
            //                            //int idHospital = Convert.ToInt32(unitDate.���������������Repository.SelectAll().ToList().Where(w=>w.F3.Value.ToString().Trim() == rowHosp["���"].ToString()).FirstOrDefault().F3);
            //                            int idHospital = table.Where(w => w.��� == Convert.ToDouble(rowHosp["���"])).FirstOrDefault().IdHosp;

            //                            contract.������������� = rowC["�������������"].ToString();
            //                            contract.������������ = Convert.ToDateTime("01.01.1900");
            //                            contract.������������������������ = Convert.ToDateTime("01.01.1900");
            //                            contract.������������������������� = 0.0m;
            //                            contract.id_����������������� = �����������������.id_�����������������;
            //                            contract.id_�������� = personFull.id_��������;
            //                            contract.id_������� = 1;
            //                            contract.id_������������ = idHospital;
            //                            contract.������������ = null;
            //                            contract.������������������ = DateTime.Now.Date;
            //                            contract.������������ = null;
            //                            contract.����������� = null;
            //                            contract.��������������� = null;
            //                            contract.������������ = null;
            //                            contract.����������������� = null;
            //                            contract.���������� = null;
            //                            contract.������������������������� = 0.0m;
            //                            contract.�������������� = false;
            //                            contract.���������������������� = false;
            //                            contract.����������������� = rowC["�������������"].ToString();
            //                            contract.��������������� = false;
            //                            contract.������������������� = false;
            //                            contract.������������ = false;
            //                            contract.������� = null;
            //                            contract.���������� = null;
            //                            contract.idFileRegistProgect = 0;
            //                            contract.flag���������� = false;
            //                            contract.flag���������������� = false;

            //                            // ������� ��� ��� �������.
            //                            contract.logWrite = MyAplicationIdentity.GetUses();

            //                            // ������� ������ �� ��������.
            //                            unitDate.�������AddRepository.Insert(contract);

            //                            // ������ �� ��������.
            //                            DataTable tabServices = unload.����������������;

            //                            // ���������� ��� �������� ������ ������� �� ���������� ����� � ��������.
            //                            StringBuilder servicesInsert = new StringBuilder();

            //                            List<IServicesContract> listServicesContract = new List<IServicesContract>();

            //                            // ���������� ������ �� ���������� �����.
            //                            foreach (DataRow row in tabServices.Rows)
            //                            {
            //                                ����������������Add services = new ����������������Add();
            //                                services.������������������ = row["������������������"].ToString();
            //                                services.���� = Convert.ToDecimal(row["����"]);
            //                                services.���������� = Convert.ToInt32(row["����������"]);
            //                                services.id_������� = contract.id_�������;
            //                                services.�������������� = row["��������������"].ToString();
            //                                services.����� = Convert.ToDecimal(row["�����"]);
            //                                services.������� = Convert.ToInt16(row["�������"]);

            //                                unitDate.����������������AddRepository.Insert(services);
            //                            }


            //                        }



            //                        //// ������� � �� ������� ��������� �� �������� - ��������� ��������.
            //                        //WriteBD writBD = new WriteBD(listUnload);

            //                        //    writBD.UnitDate = unitDate;

            //                        //    IStringQuery stringQuery = new StringQueryAdd();

            //                        //    writBD.queryWrite = stringQuery;

            //                        //    // ������� � ��.
            //                        //string query = writBD.Write();

            //                        //dc.ExecuteCommand(query);

            //                        // �������� ����������
            //                        scope.Complete();

            //                        //MessageBox.Show("���� �������� ��������� � �� �������");
            //                    }
            //                    //}


            //                }
            //            }
            //        }
            //    }

                

            ////}


            //MessageBox.Show("������� � �� ��������");

            //// ������ �������� ��������� ������������ � ������.
            ////List<DisplayContract> listContracts = new List<DisplayContract>();


        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ���������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void �����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindPersonESRN findPersonESRN = new FindPersonESRN();
            findPersonESRN.MdiParent = this;
            findPersonESRN.Show();
        }

        private void ����������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ���������2019ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

            // ������ ���������� ���� � ����������.
            string dirName = null;

            //���������� ������������
            int iConfig;

            //// ������ ��� ������� � �����������.
            //FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            //DialogResult result = folderBrowserDialog1.ShowDialog();

            // ����������� ��� ������� � ��.
            UnitDate unitDate = new UnitDate();

            Repository.DataClasses1DataContext dc = unitDate.DateContext;

            var table = dc.���������������.Select(w => new ���Hosp { IdHosp = Convert.ToInt32(w.F1), ��� = (double)w.F3 }).ToList();

            ////if (result == DialogResult.OK)
            ////{
            //// ������� ���� � �����.
            ////dirName = folderBrowserDialog1.SelectedPath;

            ////dirName = @"\\10.159.102.128\d$\��� ������\������������������ 2019\��������\8\�������\"; // ����������";

            ////MessageBox.Show("���������� ������ - " + Directory.GetDirectories(dirName).Count());

            //dirName = @"F:\!\1�����������\���������� 1 ����";
            dirName = @"F:\!\1�����������\����� ��������� �� 2019";

            //// ��������� �� ���� ��������� ���������.
            //foreach (var dir in Directory.GetDirectories(dirName))
            //{
            //    // �������� ���������� �� ������ ����������.
            //    if (Directory.Exists(dir))
            //    {

            string[] files = Directory.GetFiles(dirName);

                   // �������� �� ������ � ��������� ����������.
                    foreach (var fileName in files)
                    {
                        // ������� ��� ����� � ��� ���� ��� ������ ���� � ����� ����� � ���.
                        FileInfo fileInf = new FileInfo(fileName);

                        string fileFull = fileInf.FullName;

                        using (FileStream fstream = File.Open(fileName, FileMode.Open))
                        {
                            BinaryFormatter binaryFormatter = new BinaryFormatter();

                            //try
                            //{
                            // �������� ������������� �� ���� �������.
                            //if ((Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream) is Dictionary<string, Unload>)
                            //{
                            // ������� �� ����� ������� � ����������.
                            //unload = (Dictionary<string, Unload>)binaryFormatter.Deserialize(fstream);
                            //List<Unload> unload = (List<Unload>)binaryFormatter.Deserialize(fstream);
                            List<Unload> listUnload = (List<Unload>)binaryFormatter.Deserialize(fstream);
                            //}
                            //else
                            //{
                            //    continue;
                            //}
                            //}
                            //catch
                            //{
                            //    using (TextWriter write = File.AppendText(@"F:\!\����\2020 ���\������\fileLog.txt"))
                            //    {
                            //        write.WriteLine(fileFull);
                            //    }

                            //    continue;
                            //}

                            // ����������� ������ �������� ���������.
                            // Registr registrContracts = new Registr(unload);

                            // ��������� ������ �������� ����������.
                            var option = new System.Transactions.TransactionOptions();
                            option.IsolationLevel = System.Transactions.IsolationLevel.Serializable;


                            // ������� ��������� � ����� � ��.
                            // ����� ������ � ������� � ������ ����������.
                            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, option))
                            {
                        //var listUnload = unload.Values.ToList<Unload>();

                        foreach (Unload unload2 in listUnload)
                        {

                            //// ������� �������.
                            //DataRow rowC2 = unload2.�������.Rows[0];

                            //if (rowC2["�������������"].ToString().Trim() == "8/6669".Trim())
                            //{
                            //    var test = "";

                            //    string fileNameStop = fileName;

                            //    MessageBox.Show("����� �������� - " + rowC2["�������������"].ToString() + " ���� - " + fileNameStop);
                            //}

                            //continue;


                            // // ������������ ����������� ������.
                            ISity sity = new NameSity();

                                    DataTable tabSity = unload2.��������������;

                                    if (tabSity.Rows.Count > 0 && tabSity.Rows[0]["������������"] != DBNull.Value)
                                    {
                                        // ������� ������������ ����������� ������ � ������� ��������� ��������.
                                        sity.NameTown = tabSity.Rows[0]["������������"].ToString().Trim();
                                    }
                                    else
                                    {
                                        sity.NameTown = "";
                                    }

                                    // ������� �������� ���������.
                                    Repository.����������������� ����������������� = unitDate.�����������������Repository.Get�����������������(unload2.�����������������.Trim());

                                    // ���������� �� ����� �������� ��� ������ �� ���������.
                                    DataRow rw_�������� = unload2.��������.Rows[0];

                                    ��������Add personFull = new ��������Add();

                                    personFull.������� = rw_��������["�������"].ToString().Trim();
                                    personFull.��� = rw_��������["���"].ToString().Trim();
                                    personFull.�������� = rw_��������["��������"].ToString().Trim();
                                    //personFull.DateBirtch = " Convert(datetime,'" + �����.����(Convert.ToDateTime(rw_��������["������������"]).ToShortDateString().Trim()) + "',112)  ";
                                    personFull.������������ = Convert.ToDateTime(rw_��������["������������"]);
                                    personFull.����� = rw_��������["�����"].ToString().Trim();
                                    personFull.��������� = rw_��������["���������"].ToString().Trim();
                                    personFull.������ = rw_��������["������"].ToString().Trim();
                                    personFull.������������� = rw_��������["�������������"].ToString().Trim();
                                    personFull.������������� = rw_��������["�������������"].ToString().Trim();
                                    personFull.������������� = rw_��������["�������������"].ToString().Trim();
                                    personFull.������������������ = Convert.ToDateTime(rw_��������["������������������"]);
                                    personFull.��������������� = rw_��������["���������������"].ToString().Trim();
                                    personFull.id_����������������� = �����������������.id_�����������������;
                                    personFull.id_�������� = (int)unload2.������������.Rows[0][0];//                      ",@id��������_" + iCount + " " +
                                    personFull.�������������� = rw_��������["��������������"].ToString().Trim();
                                    personFull.�������������� = rw_��������["��������������"].ToString().Trim();
                                    personFull.������������������� = Convert.ToDateTime(rw_��������["�������������������"]);
                                    personFull.���������������� = rw_��������["����������������"].ToString().Trim();
                                    personFull.id_������� = 1;//id ������� � ��� �� ��������� 
                                    personFull.id_����� = Convert.ToInt16(rw_��������["id_�����"]);

                                    if ((personFull.�������.Trim().ToLower() == "������".ToLower().Trim()) && (personFull.���.Trim().ToLower() == "������".ToLower().Trim()) && (personFull.��������.Trim().ToLower() == "����������".ToLower().Trim()))
                                    {
                                        DateTime dt = new DateTime(1942, 8, 2);

                                        personFull.������������������ = dt;
                                    }

                                    // ������� id ����������� ������.
                                    var findSity = unitDate.���������������Repository.Filtr���������������(sity.NameTown);

                                    if (findSity != null)
                                    {
                                        personFull.id_�������� = findSity.id_��������;
                                    }
                                    else
                                    {
                                        Repository.�������������� �������������� = new Repository.��������������();
                                        ��������������.������������ = sity.NameTown;

                                        // ������� �� ����� ���������� �����.
                                        unitDate.���������������Repository.Insert(��������������);

                                        personFull.id_�������� = ��������������.id_��������;
                                    }

                                    ��������Add personAdd = unitDate.��������AddRepository.SelectPerson(personFull);

                                    // �������� ���� �� �������� � ��� � ����� �������� � ��.
                                    if (personAdd != null)
                                    {
                                        // ������� ������ �� ���������.
                                        unitDate.��������AddRepository.Update(personFull);

                                        // �������� id ���������.
                                        personFull.id_�������� = personAdd.id_��������;
                                    }
                                    else
                                    {
                                        // ������� ������ ���������.
                                        unitDate.��������AddRepository.Insert(personFull);
                                    }

                                    // ������� �������.
                                    DataRow rowC = unload2.�������.Rows[0];

                                    �������Add contract = new �������Add();

                                    // ������� ������ �� ������������.
                                    DataRow rowHosp = unload2.������������.Rows[0];

                                    // �������� ������ �� ������������.
                                    //int idHospital = Convert.ToInt32(unitDate.���������������Repository.SelectAll().ToList().Where(w=>w.F3.Value.ToString().Trim() == rowHosp["���"].ToString()).FirstOrDefault().F3);
                                    int idHospital = table.Where(w => w.��� == Convert.ToDouble(rowHosp["���"])).FirstOrDefault().IdHosp;

                                    contract.������������� = rowC["�������������"].ToString();
                                    contract.������������ = Convert.ToDateTime("01.01.1900");
                                    contract.������������������������ = Convert.ToDateTime("01.01.1900");
                                    contract.������������������������� = 0.0m;
                                    contract.id_����������������� = �����������������.id_�����������������;
                                    contract.id_�������� = personFull.id_��������;
                                    contract.id_������� = 1;
                                    contract.id_������������ = idHospital;
                                    contract.������������ = null;
                                    contract.������������������ = DateTime.Now.Date;
                                    contract.������������ = null;
                                    contract.����������� = null;
                                    contract.��������������� = null;
                                    contract.������������ = null;
                                    contract.����������������� = null;
                                    contract.���������� = null;
                                    contract.������������������������� = 0.0m;
                                    contract.�������������� = false;
                                    contract.���������������������� = false;
                                    contract.����������������� = rowC["�������������"].ToString();
                                    contract.��������������� = false;
                                    contract.������������������� = false;
                                    contract.������������ = false;
                                    contract.������� = null;
                                    contract.���������� = null;
                                    contract.idFileRegistProgect = 0;
                                    contract.flag���������� = false;
                                    contract.flag���������������� = false;

                                    // ������� ��� ��� �������.
                                    contract.logWrite = MyAplicationIdentity.GetUses();

                                    // ������� ������ �� ��������.
                                    unitDate.�������AddRepository.Insert(contract);

                                    // ������ �� ��������.
                                    DataTable tabServices = unload2.����������������;

                                    // ���������� ��� �������� ������ ������� �� ���������� ����� � ��������.
                                    StringBuilder servicesInsert = new StringBuilder();

                                    List<IServicesContract> listServicesContract = new List<IServicesContract>();

                                    // ���������� ������ �� ���������� �����.
                                    foreach (DataRow row in tabServices.Rows)
                                    {
                                        ����������������Add services = new ����������������Add();
                                        services.������������������ = row["������������������"].ToString();
                                        services.���� = Convert.ToDecimal(row["����"]);
                                        services.���������� = Convert.ToInt32(row["����������"]);
                                        services.id_������� = contract.id_�������;
                                        services.�������������� = row["��������������"].ToString();
                                        services.����� = Convert.ToDecimal(row["�����"]);
                                        services.������� = Convert.ToInt16(row["�������"]);

                                        unitDate.����������������AddRepository.Insert(services);
                                    }


                                }



                                //// ������� � �� ������� ��������� �� �������� - ��������� ��������.
                                //WriteBD writBD = new WriteBD(listUnload);

                                //    writBD.UnitDate = unitDate;

                                //    IStringQuery stringQuery = new StringQueryAdd();

                                //    writBD.queryWrite = stringQuery;

                                //    // ������� � ��.
                                //string query = writBD.Write();

                                //dc.ExecuteCommand(query);

                                // �������� ����������
                                scope.Complete();

                                

                            }
                        }

                //  
               // MessageBox.Show("���� �������� ��������� � �� �������");
            }
            //    }
            //}

            MessageBox.Show("������� � �� ��������");


        }

        private void �������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PullConnectBD pull = new PullConnectBD();
            //Dictionary<string, string> pullConnect = pull.GetPull(FlagConnectServer);

            // ������ ����������� � �� ��� ����.
            ConfigLibrary.Config config = new ConfigLibrary.Config();

            //���� ��������������.
            //�������� ������� �� �������� ����������� � ��� ����.
            Dictionary<string, string> pullConnect = config.Select();

            // �������.
            int iCount = 1;

            foreach (KeyValuePair<string, string> dStringConnect in pullConnect)
            {
                // ������

                //���������.GetTableSQL()

                //========

                string regionName = dStringConnect.Key;

                // ������ ��� �������� SQL �������.
                StringBuilder queryInsert = new StringBuilder();

                // ������ ��� �������� SQL ������� � ��.
                string sConnection = string.Empty;
                sConnection = dStringConnect.Value.ToString().Trim();

                // ������ ������� � �� ����, ��� ������ ���������� � ����.
                IFindPerson findPersonQuery = new FindPersonsWow();
                string query = findPersonQuery.Query();

                DataTable tabPersons = ���������.GetTableSQL(query, "PersonWoW2" + iCount.ToString(), sConnection);
                
                // �������� ������� �������, ����� � ������� � ���������� ����� � �������.
                if(tabPersons != null && tabPersons.Rows != null && tabPersons.Rows.Count > 0)
                {
                    foreach (DataRow row in tabPersons.Rows)
                    {
                        string famili = string.Empty;
                        string name = string.Empty;
                        string surName = string.Empty;
                        string dr = string.Empty;
                        string address = string.Empty;
                        string category = string.Empty;

                        if (row["�"] != null)
                            famili = row["�"].ToString();

                        if (row["�"] != null)
                            name = row["�"].ToString();

                        if (row["�"] != null)
                            surName = row["�"].ToString();

                        if (row["��"] != null)
                        {
                            dr = Convert.ToDateTime(row["��"]).ToShortDateString();
                        }
                        else
                        {
                            DateTime dt = new DateTime(1900, 1, 1);
                            dr = dt.ToShortDateString();
                        }

                        if (row["�����"] != null)
                            address = row["�����"].ToString().Replace(","," ");

                        if (row["���������"] != null)
                            category = row["���������"].ToString().Replace(",", " ");

                        // ��������� ��������� � ������ �������.
                        WritePersonWow writePersonWow = new WritePersonWow(famili, name, surName,�����.����(dr), address, category, regionName);
                        queryInsert.Append(writePersonWow.InsertPerson());

                    }
                }

                //  �������� ������ ������� ��� Insert � ������ ����������.
                string strQueryInsert = queryInsert.ToString();

                if (strQueryInsert != "")
                {
                    // �������� ������ � ��.
                    ExecuteQuery.Execute(strQueryInsert, ConnectDB.ConnectionString());
                }
                iCount++;

            }

            MessageBox.Show("������������ ���������");
        }

        private void �����������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

