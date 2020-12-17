using System;
using System.Collections.Generic;
using System.Text;
using ControlDantist.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

using DantistLibrary;

namespace ControlDantist.Classes
{
    class WriteBD
    {
        //������ ����������� ������
        private List<Unload> list;

        private string ����������������� = string.Empty;
        private string �������� = string.Empty;
        private string ������������ = string.Empty;
        private string �������������� = string.Empty;
        private string ������������ = string.Empty;
        private string ��������������� = string.Empty;
        private string ������� = string.Empty;

        //���������, ��� ������� � ����� ������� ��� ������� � ��
        private bool flagDog = false;

        public WriteBD(List<Unload> unload)
        {
            list = unload;
        }

        /// <summary>
        /// ���������� ������ � ���� ������
        /// </summary>
        public void Write()
        {
            //��������� ������� ��������
            CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCInfo;

            //��� ��� �� ����� ������ ������� � �� ���������� �������� ��� ������� 
            //�������� ��� �� �������, � ������ �� ������������ ��� � �� ����������
            //���������� ����� � ������ �����������(������ ���� ����� ����� ���� � ������� TSQL)
            int iTest = list.Count;

                foreach (Unload unload in list)
                {
                    //������� 
                    flagDog = false;
                    
                    //������ ���� �� ����� ������� � ��
                    using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                    {
                        con.Open();

                       //������� ����� �������� � ������� �������
                       DataTable tab������� = unload.�������;
                       string numDog = tab�������.Rows[0]["�������������"].ToString().Trim();
                       //string queryDog = "select COUNT(*) as '����������' from ������� where ������������� = '" + numDog + "' ";


                        DataTable tab�������� = unload.��������;

                        string ������� = tab��������.Rows[0]["�������"].ToString().Trim();
                        string ��� = tab��������.Rows[0]["���"].ToString().Trim();
                        string �������� = tab��������.Rows[0]["��������"].ToString().Trim();
                        string ������������ = tab��������.Rows[0]["������������"].ToString().Trim();

                        string queryDog = "select COUNT(*) as '����������' from ������� where ������������� = '" + numDog + "' " +
                                          "and id_�������� in (select id_�������� from �������� " +
                                          "where ������� = '" + ������� + "' and ��� = '" + ��� + "' and �������� = '" + �������� + "' and ������������ = '" + ������������ + "') ";
                       
                       
                        SqlTransaction transact = con.BeginTransaction();
                        DataTable t = ���������.GetTableSQL(queryDog, "�������", con, transact);

                        int i = Convert.ToInt32(t.Rows[0]["����������"]);

                        if (i != 0)
                        {
                            //���� � �� ��� ���������� ������� �������
                            flagDog = true;

                            //������� �������������, ��� ������ ������� ��� ����������
                            //DataTable tab�������� = unload.��������;
                            //string ������� = tab��������.Rows[0]["�������"].ToString().Trim();
                            //string ��� = tab��������.Rows[0]["���"].ToString().Trim();
                            //string �������� = tab��������.Rows[0]["��������"].ToString().Trim();

                            System.Windows.Forms.MessageBox.Show("������� � " + numDog + " �� " + ������� + " " + ��� + " " + �������� + " � ���� ������ ��� �������");
                        }
                    }

                    if(flagDog == false)
                    {

                    //�������� �� � ����� ����������
                    StringBuilder builder�������� = new StringBuilder();

                    using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                    {
                        con.Open();
                        SqlTransaction transact = con.BeginTransaction();

                        //�������� �� � ����� ����������
                        StringBuilder builder = new StringBuilder();

                        //������� ��������� ����� � �������� ��� id
                        if (unload.��������������.Rows.Count != 0)
                        {
                            DataRow row�������� = unload.��������������.Rows[0];
                            �������������� = row��������["������������"].ToString();
                        }

                        //�������� ���� �� ����� ��������� ����� � ���� ������ �� �������
                        string control�������� = "select count(������������) from �������������� where ������������ like '%" + �������������� + "%'";

                        //SqlCommand comContr�������� = new SqlCommand(control��������, con);
                        //comContr��������.Transaction = transact;

                        //int iCountRow�������� = (int)comContr��������.ExecuteScalar();

                        int iCountRow�������� = ValidateRows.Row(control��������, con, transact);
                        if (iCountRow�������� == 0)
                        {
                            string insert�������� = "INSERT INTO ��������������(������������)VALUES('" + �������������� + "') ";
                            builder.Append(insert��������);
                        }

                        //������� ��������� ������
                        if (unload.������������������ != null)
                        {
                            if (unload.������������������.Rows.Count != 0)
                            {
                                DataRow row����� = unload.������������������.Rows[0];
                                ������������ = row�����["������������"].ToString();
                            }
                        }

                        //�������� ���� �� � �� ����� �����
                        string control����� = "select count(������������) from ������������������ where ������������ like '%" + ������������ + "%'";

                        int iCount����� = ValidateRows.Row(control�����, con, transact);

                        if (iCount����� == 0)
                        {
                            string insert����� = "INSERT INTO ������������������ (������������)VALUES('" + ������������ + "')";
                            builder.Append(insert�����);
                        }

                        //������� �������� ���������
                        ����������������� = unload.�����������������;

                        //�������� ���� �� ����� �������� ��������� � ��
                        string control�� = "select count(�����������������) from ����������������� where ����������������� like '%" + ����������������� + "%'";
                        int iCount�� = ValidateRows.Row(control��, con, transact);

                        if (iCount�� == 0)
                        {
                            string insert�� = "INSERT INTO ����������������� (�����������������)VALUES('" + ����������������� + "')";
                            builder.Append(insert��);
                        }

                        //������� ��� ��������� ����� ���������� ��������
                        DataRow row��� = unload.������������.Rows[0];

                        //������ ���� �� ����� �������� � ��
                        �������� = row���["�������������������������"].ToString();

                        string control��� = "select count(�������������������������) from ������������ where ������������������������� like '%" + �������� + "%'";

                        //�������� ���� �� ����� ���4����� � ��
                        int iCount��� = ValidateRows.Row(control���, con, transact);

                        if (iCount��� == 0)
                        {
                            string insert��� = "INSERT INTO ������������(�������������������������)VALUES('" + �������� + "')";
                            builder.Append(insert���);
                        }

                        //������� ������ ������������
                        DataRow rowHosp = unload.������������.Rows[0];
                        //������������ = rowHosp["������������������������"].ToString().Trim();

                        //������� ��� ������������ 
                        ��������������� = rowHosp["���"].ToString().Trim();

                        //������� ��� ���� �����
                        string �������� = unload.�������;
                        

                        //�������� ���� �� ����� ���� ���� � ��
                        string controlBigEsculap = "select count(���_��������) from dbo.����������� where ���_�������� like '%"+ �������� +"%' ";
                        int iCountEsculap = ValidateRows.Row(controlBigEsculap, con, transact);

                        if(iCountEsculap == 0)
                        {
                            string query�������� = "INSERT INTO ����������� " +
                                           "([���_��������] " +
                                           ",[���_������������]) " +
                                           "VALUES " +
                                           "('"+ �������� +"' " +
                                           ",'" + ��������������� + "' ) ";

                            //������� � builder ���� �����
                            builder.Append(query��������);
                        }

                        //������� ��� ���� ����
                        if (builder.ToString() != "")
                        {
                            //�������� ������ �� ������� � ��
                            ExecuteQuery.Execute(builder.ToString());
                        }
                    }
                
                    //����� ������ � �� �� ������������
                    using(SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                    {
                        con.Open();

                        //�������� ���� �� ��������� ������������ � ��
                        string controlHosp = "select COUNT(������������������������) from dbo.������������ where ������������������������ like '%" + ������������ + "%'";
                        int iCountHosp = ValidateRows.Row(controlHosp, con);

                        //���� ��� ������ ������������ �� ������� � � ��
                        if(iCountHosp == 0)
                        {
                            DataRow rowHosp = unload.������������.Rows[0];
                            ������������ = rowHosp["������������������������"].ToString().Trim();

                            string insertHosp = "declare @idBigEsc int " +
                                               "select @idBigEsc = id_�������� from dbo.����������� where ���_�������� like '%" + unload.������� + "%' " +
                                               "INSERT INTO [������������] " +
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
                                               "('"+ ������������ +"' " +
                                               ",'"+ rowHosp["���������������"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["����������������"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["����������������"].ToString().Trim() +"' " +
                                               ",@idBigEsc " +
                                               ","+ Convert.ToInt32(rowHosp["id_�������"]) +" " +
                                                ",'"+ rowHosp["������������������������"].ToString().Trim() +"' " +
                                                ",'"+ rowHosp["���"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["���"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["���"].ToString().Trim() +"' " +
                                              ",'"+ rowHosp["�����������������"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["�������������"].ToString().Trim() +"' " +
                                              ",'"+ rowHosp["�����������"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["�������������"].ToString().Trim() +"' " +
                                               ",'"+ Convert.ToDateTime(rowHosp["�����������������������"]).ToShortDateString() +"' " +
                                               ",'"+ rowHosp["����"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["�����������������������������"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["���������������������"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["�������������"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["����"].ToString().Trim() +"' " +
                                               ",'"+ rowHosp["�����"].ToString().Trim() +"' " +
                                               ","+ Convert.ToInt32(rowHosp["Flag"]) +" " +
                                               "," + Convert.ToInt32(rowHosp["����������������������"]) + " )";

                            //�������� ������ �� ������� � ��
                            ExecuteQuery.Execute(insertHosp);
                        }
                    }

                    using (SqlConnection con�������� = new SqlConnection(ConnectDB.ConnectionString()))
                    {
                        con��������.Open();
                        //SqlTransaction transact = con��������.BeginTransaction();

                        //������� ������
                        int iCount = 0;

                        //������� ������ �� �������� ���������
                        DataTable tab�������� = unload.��������;


                        if(tab��������.Rows.Count != 0)
                        {
                            foreach (DataRow rw�������� in tab��������.Rows)
                            {

                                string query = "if " +
                                           "(select count(�������) from dbo.�������� " +
                                           "where ������� = '" + rw��������["�������"].ToString().Trim() + "' " +
                                           "and ��� = '" + rw��������["���"].ToString().Trim() + "' " +
                                           "and �������� = '" + rw��������["��������"].ToString().Trim() + "' " +
                                           "and ������������ = '" + Convert.ToDateTime(rw��������["������������"]).ToShortDateString().Trim() + "' ) = 0 " +
                                           "begin " +
                                           "declare @id��_" + iCount + "  int " +
                                           "select @id��_" + iCount + " = id_����������������� from ����������������� where ����������������� like '%" + ����������������� + "%' " +
                                           "declare @id��������_" + iCount + " int " +
                                           "select @id��������_" + iCount + " = id_�������� from ������������ where ������������������������� like '%" + �������� + "%' " +
                                           "declare @id_�����_" + iCount + " int " +
                                           "select @id_�����_" + iCount + " = id_����� from ������������������ where ������������ like '%" + ������������ + "%' " +
                                           "declare @id_��������_" + iCount + " int " +
                                           "select @id_��������_" + iCount + " = id_�������� from �������������� where ������������ like '%" + �������������� + "%' " +
                                           "INSERT INTO �������� " +
                                           "([�������] " +
                                           ",[���] " +
                                           ",[��������] " +
                                           ",[������������] " +
                                           ",[�����] " +
                                           ",[���������] " +
                                           ",[������] " +
                                           ",[�������������] " +
                                           ",[�������������] " +
                                           ",[�������������] " +
                                           ",[������������������] " +
                                           ",[���������������] " +
                                           ",[id_�����������������] " +
                                           ",[id_��������] " +
                                           ",[��������������] " +
                                           ",[��������������] " +
                                           ",[�������������������] " +
                                           ",[����������������] " +
                                           ",[id_�������] " +
                                           ",[id_�����] " +
                                           ",[id_��������]) " +
                                           "VALUES " +
                                           "('" + rw��������["�������"].ToString().Trim() + "' " +
                                           ",'" + rw��������["���"].ToString().Trim() + "' " +
                                           ",'" + rw��������["��������"].ToString().Trim() + "' " +
                                           ",'" + Convert.ToDateTime(rw��������["������������"]).ToShortDateString().Trim() + "' " +
                                           ",'" + rw��������["�����"].ToString().Trim() + "' " +
                                           ",'" + rw��������["���������"].ToString().Trim() + "' " +
                                           ",'" + rw��������["������"].ToString().Trim() + "' " +
                                           ",'" + rw��������["�������������"].ToString().Trim() + "' " +
                                           ",'" + rw��������["�������������"].ToString().Trim() + "' " +
                                           ",'" + rw��������["�������������"].ToString().Trim() + "' " +
                                           ",'" + Convert.ToDateTime(rw��������["������������������"]).ToShortDateString().Trim() + "' " +
                                           ",'" + rw��������["���������������"].ToString().Trim() + "' " +
                                           ",@id��_" + iCount + " " +
                                           ",@id��������_" + iCount + " " +
                                           ",'" + rw��������["��������������"].ToString().Trim() + "' " +
                                           ",'" + rw��������["��������������"].ToString().Trim() + "' " +
                                           ",'" + Convert.ToDateTime(rw��������["�������������������"]).ToShortDateString().Trim() + "' " +
                                           ",'" + rw��������["����������������"].ToString().Trim() + "' " +
                                           ",1 " + //id ������� � ��� �� ��������� 1
                                           ",@id_�����_" + iCount + " " +
                                           ",@id_��������_" + iCount + " ) " +
                                           "end ";

                                //string query = "declare @id��_"+ iCount +"  int " +
                                //           "select @id��_" + iCount + " = id_����������������� from ����������������� where ����������������� like '%" + ����������������� + "%' " +
                                //           "declare @id��������_"+ iCount +" int " +
                                //           "select @id��������_" + iCount + " = id_�������� from ������������ where ������������������������� like '%" + �������� + "%' " +
                                //           "declare @id_�����_"+ iCount +" int " +
                                //           "select @id_�����_" + iCount + " = id_����� from ������������������ where ������������ like '%" + ������������ + "%' " +
                                //           "declare @id_��������_"+ iCount +" int " +
                                //           "select @id_��������_" + iCount + " = id_�������� from �������������� where ������������ like '%" + �������������� + "%' " +
                                //           "INSERT INTO �������� " +
                                //           "([�������] " +
                                //           ",[���] " +
                                //           ",[��������] " +
                                //           ",[������������] " +
                                //           ",[�����] " +
                                //           ",[���������] " +
                                //           ",[������] " +
                                //           ",[�������������] " +
                                //           ",[�������������] " +
                                //           ",[�������������] " +
                                //           ",[������������������] " +
                                //           ",[���������������] " +
                                //           ",[id_�����������������] " +
                                //           ",[id_��������] " +
                                //           ",[��������������] " +
                                //           ",[��������������] " +
                                //           ",[�������������������] " +
                                //           ",[����������������] " +
                                //           ",[id_�������] " +
                                //           ",[id_�����] " +
                                //           ",[id_��������]) " +
                                //           "VALUES " +
                                //           "('" + rw��������["�������"].ToString().Trim() + "' " +
                                //           ",'" + rw��������["���"].ToString().Trim() + "' " +
                                //           ",'" + rw��������["��������"].ToString().Trim() + "' " +
                                //           ",'" + Convert.ToDateTime(rw��������["������������"]).ToShortDateString().Trim() + "' " +
                                //           ",'" + rw��������["�����"].ToString().Trim() + "' " +
                                //           ",'" + rw��������["���������"].ToString().Trim() + "' " +
                                //           ",'" + rw��������["������"].ToString().Trim() + "' " +
                                //           ",'" + rw��������["�������������"].ToString().Trim() + "' " +
                                //           ",'" + rw��������["�������������"].ToString().Trim() + "' " +
                                //           ",'" + rw��������["�������������"].ToString().Trim() + "' " +
                                //           ",'" + Convert.ToDateTime(rw��������["������������������"]).ToShortDateString().Trim() + "' " +
                                //           ",'" + rw��������["���������������"].ToString().Trim() + "' " +
                                //           ",@id��_" + iCount + " " +
                                //           ",@id��������_" + iCount + " " +
                                //           ",'" + rw��������["��������������"].ToString().Trim() + "' " +
                                //           ",'" + rw��������["��������������"].ToString().Trim() + "' " +
                                //           ",'" + Convert.ToDateTime(rw��������["�������������������"]).ToShortDateString().Trim() + "' " +
                                //           ",'" + rw��������["����������������"].ToString().Trim() + "' " +
                                //           ",1 " + //id ������� � ��� �� ��������� 1
                                //           ",@id_�����_" + iCount + " " +
                                //           ",@id_��������_" + iCount + " ) ";


                                builder��������.Append(query);

                                iCount++;
                            }

                            //�������� ������ �� ������� � ��
                            ExecuteQuery.Execute(builder��������.ToString());
                        }
                    }

                    //�������� �� ������ �������
                        using (SqlConnection con������� = new SqlConnection(ConnectDB.ConnectionString()))
                        {
                            con�������.Open();
                            DataTable tab������� = unload.�������;

                            //������� ��������� ���������
                            DataRow r = unload.��������.Rows[0];

                            //������� ����� � ����� �������� ��� ������������� ���������
                            string ����� = r["�������������"].ToString().Trim();
                            string ����� = r["�������������"].ToString().Trim();

                            //�������� �� � ������ ����������
                            StringBuilder build = new StringBuilder();

                            if(tab�������.Rows.Count != 0)
                            {
                                int iCount = 0;

                                    foreach(DataRow row in tab�������.Rows)
                                    {
                                        string query������� = "declare @id��_" + iCount + " int " +
                                                             "select @id��_" + iCount + " = id_����������������� from ����������������� where ����������������� like '%" + ����������������� + "%' " +
                                                             "declare @IdHosp_" + iCount + " int " +
                                                             "select @IdHosp_" + iCount + " = id_������������ from dbo.������������ where ��� like '%" + ��������������� + "%' " +
                                                             "declare @id��������_" + iCount + " int " +
                                                             "select @id��������_" + iCount + " = id_�������� from dbo.�������� where ������������� like '%" + ����� + "%' and ������������� like '%" + ����� + "%' " +
                                                               "INSERT INTO [�������] " +
                                                               "([�������������] " +
                                                               ",[������������] " +
                                                               ",[������������������������] " +
                                                               ",[�������������������������] " +
                                                               ",[id_�����������������] " +
                                                               ",[id_������������] " +
                                                               ",[����������] " +
                                                               ",[id_�������] " +
                                                               ",[�������������������] " +
                                                               ",[���������������] " +
                                                               " ,[id_��������] " +
                                                               ",[�����������������]) " +
                                                               "VALUES " +
                                                               "('" + row["�������������"].ToString() + "' " +
                                                               //",'" + Convert.ToDateTime(row["������������"]).ToShortDateString() + "' " +
                                                               //",'" + Convert.ToDateTime(row["������������������������"]).ToShortDateString() + "' " +
                                                               //"," + Convert.ToDecimal(row["�������������������������"]) + " " +
                                                               ", '' " +
                                                               ", '' " +
                                                               ", '' " +
                                                               ",@id��_" + iCount + " " +
                                                               ",@IdHosp_" + iCount + " " +
                                                               ",'" + row["����������"].ToString() + "' " +
                                                               ",1" + //��������� 1 ��� ��� id �������� �� ���� ����� 1
                                                               ",'" + Convert.ToBoolean(row["�������������������"]) + "' " +
                                                               ",'" + Convert.ToBoolean(row["���������������"]) + "' " +
                                                               ",@id��������_" + iCount + " " +
                                                               ",'" + row["�������������"].ToString() + "' ) ";

                                        //������ ������ � ������������ ������
                                          build.Append(query�������);

                                        iCount++;
                                    }
                                    //�������� ������ �� ������� � ��
                                    ExecuteQuery.Execute(build.ToString());

                                }
                        }

                    //������� ������ �� ��������
                        using (SqlConnection con������ = new SqlConnection(ConnectDB.ConnectionString()))
                        {
                            con������.Open();

                            //������ ��� �������� ������� ������������ � ������ ����������
                            StringBuilder builder = new StringBuilder();
                        
                            //�������
                            int iCount = 0;

                            //������� ������� �������
                            DataRow row������� = unload.�������.Rows[0];

                            //������� ����� �������� ��������
                            string ������������� = row�������["�������������"].ToString();

                            //������� ������ �� ��������
                            DataTable tab������ = unload.����������������;

                            if (tab������.Rows.Count != 0)
                            {
                                foreach (DataRow row in tab������.Rows)
                                {
                                    string query = "declare @id�������_" + iCount + " int " +
                                                   "select @id�������_" + iCount + " = id_������� from dbo.������� where ������������� like '%" + ������������� + "%' " +
                                                   "INSERT INTO ���������������� " +
                                                   "([������������������] " +
                                                   ",[����] " +
                                                   ",[����������] " +
                                                   ",[id_�������] " +
                                                   ",[��������������] " +
                                                   ",[�����] " +
                                                   ",[�������]) " +
                                                   "VALUES " +
                                                    "('" + row["������������������"].ToString() + "' " +
                                                    "," + Convert.ToDecimal(row["����"]) + " " +
                                                    "," + Convert.ToInt32(row["����������"]) + " " +
                                                    ",@id�������_" + iCount + " " +
                                                    ",'" + row["��������������"].ToString() + "' " +
                                                    "," + Convert.ToDecimal(row["�����"]) + " " +
                                                    "," + Convert.ToInt32(row["����������"]) + " ) ";

                                    builder.Append(query);

                                    iCount++;
                                }

                                //�������� ������ �� ���������� ����� �� �������� � ������ ����������

                                ExecuteQuery.Execute(builder.ToString());
                            }
                        }

                    //������� � �� �� ������ ������ �� ��� ���������� ��� ������� ��������
                        using (SqlConnection con������������� = new SqlConnection(ConnectDB.ConnectionString()))
                        {
                            con�������������.Open();

                             //�������
                            int iCount = 0;

                            //������� ������� �������
                            DataRow row������� = unload.�������.Rows[0];

                            //������� ����� �������� ��������
                            string ������������� = row�������["�������������"].ToString();

                            DataTable tab������������� = unload.�������������;

                            //������� ������������� � ������ ����������
                            StringBuilder builder = new StringBuilder();

                            if (tab�������������.Rows.Count != 0)
                            {
                                foreach (DataRow row in tab�������������.Rows)
                                {
                                    string query = "declare @id�������_" + iCount + " int " +
                                                   "select @id�������_" + iCount + " = id_������� from dbo.������� where ������������� like '%" + ������������� + "%' " +
                                                   "INSERT INTO ������������� " +
                                                   "([id_�������] " +
                                                   ",[������������������] " +
                                                   ",[����]) " +
                                                   "VALUES " +
                                                   "(@id�������_" + iCount + " " +
                                                   ",'" + row["������������������"].ToString() + "' " +
                                                   ",'" + Convert.ToDateTime(row["����"]).ToShortDateString() + "' ) ";

                                    builder.Append(query);
                                }

                                //�������� ������ � ������ ����������
                                ExecuteQuery.Execute(builder.ToString());
                            }
                        }

                    //������� ��� ����������� ����� � ���� ������ �� ������
                        using (SqlConnection con����������� = new SqlConnection(ConnectDB.ConnectionString()))
                        {
                            con�����������.Open();

                             //�������
                            int iCount = 0;

                            //������� ������� �������
                            DataRow row������� = unload.�������.Rows[0];

                            //������� ����� �������� ��������
                            string ������������� = row�������["�������������"].ToString();

                            //��� ������ �������� �������� ��������� � �� �� ����� ���������� ������ �� ����� ����������� �����

                            //������� ��� ����������� ����� � ������ ����������
                            //StringBuilder builder = new StringBuilder();
                            //DataTable tab��������� = unload.�������������������;

                            ////�������� ����� � ������� ��� �������
                            //if (tab���������.Rows.Count != 0)
                            //{
                            //    foreach (DataRow row in tab���������.Rows)
                            //    {
                            //        string query = "declare @id�������_" + iCount + " int " +
                            //                       "select @id�������_" + iCount + " = id_������� from dbo.������� where ������������� like '%" + ������������� + "%' " +
                            //                       "INSERT INTO ������������������� " +
                            //                       "([���������] " +
                            //                       ",[id_�������] " +
                            //                       ",[��������������] " +
                            //                       ",[��������������] " +
                            //                       ",[��������������] " +
                            //                       ",[������������������] " +
                            //                       ",[����] " +
                            //                       ",[����������] " +
                            //                       ",[�����] " +
                            //                       ",[�����������������]) " +
                            //                 "VALUES " +
                            //                       "('" + row["���������"].ToString() + "' " +
                            //                       ",@id�������_" + iCount + " " +
                            //                       ",'" + row["��������������"].ToString() + "' " +
                            //                       ",'" + Convert.ToDateTime(row["��������������"]).ToShortDateString() + "' " +
                            //                       ",'" + row["��������������"].ToString() + "' " +
                            //                       ",'" + row["������������������"].ToString() + "' " +
                            //                       "," + Convert.ToDecimal(row["����"]) + " " +
                            //                       "," + Convert.ToInt32(row["����������"]) + " " +
                            //                       "," + Convert.ToDecimal(row["�����"]) + " " +
                            //                       ",'" + row["�����������������"].ToString() + "') ";

                            //        builder.Append(query);
                            //    }

                            //    //�������� ������ � ������ ����������
                            //    ExecuteQuery.Execute(builder.ToString());
                            //}

                        }

                    
                      }
                     }//����� �������� ������� � ����������� ������
                    
                    }//������� ��� ������ �������� � �� ���

                
           // }
        }
    }
//}
