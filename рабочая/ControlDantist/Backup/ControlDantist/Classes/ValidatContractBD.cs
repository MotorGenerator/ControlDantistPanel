using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DantistLibrary;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ���������� ������ ���������� � �������� � �������� ����������� � ����������� ���� ������
    /// </summary>
    class ValidatContractBD
    {
        private string ����������������� = string.Empty;
        private int idHosp;
        private string �������� = string.Empty;
        private decimal ���� = 0m;
        private bool errorFlag�������� = false;
        private bool errorFlag���� = false;
        private decimal ������������������� = 0m;
        private decimal error������������������� = 0m;
        private bool errorFlag��������������� = false;
        private bool error������ = false;
        private string ������������ = string.Empty;
        private DataRow rowControlReestr���;
        private string ������������� = string.Empty;

        public ValidatContractBD()
        {

        }

        /// <summary>
        /// ���������� ������ � ����������� ���� � � ��������� �������
        /// </summary>
        /// <param name="unloads"></param>
        /// <param name="ValContracts"></param>
        public void Validate(Dictionary<string, Unload> unloads, Dictionary<string, ValidateContract> ValContracts)
        {
            //�������� ���������� ������
            //tab�������� = ���������������;
            //unload = unloads;
            //listValContr = ValContracts;

            //List<bool> 

            //������ ������� ������������ � ��������� ����� ����������� ��������� � ���������� � ������������ � ����� ���������� � ����������� ��
            foreach (Unload un in unloads.Values)
            {
                ////�������� ��������� ������� ��� �������� ��������� ����������
                //ErrorsReestrUnload error = new ErrorsReestrUnload();

                ErrorReestr errorReestr = new ErrorReestr();

                //�������� ��������� ������� ��� �������� ������ ������� �� ��������� ������ ������� ��������� ��������
                ReestrControl rControl = new ReestrControl();

                //������� ��� ��������� �������� ������� � ������ ������ � ������
                DataRow row�������� = un.��������.Rows[0];

                string ������� = row��������["�������"].ToString();
                string ��� = row��������["���"].ToString();
                string �������� = row��������["��������"].ToString();

                //������� � ������ ��� �������� ���������
                errorReestr.��� = ������� + " " + ��� + " " + ��������;

                //������� ��� ���������
                rControl.��� = ������� + " " + ��� + " " + ��������;

                //������� ���� � ����� �������� �� �������� �����
                DataRow rowControlReestr������� = un.�������.Rows[0];

                //������� ����� ������������ � ����� ��������
                ������������� = rowControlReestr�������["�������������"].ToString();

                //������� ���� ��������
                if (rowControlReestr�������["������������"] != DBNull.Value)
                {
                    ������������ = Convert.ToDateTime(rowControlReestr�������["������������"]).ToShortDateString();
                }

                //������� ���� � ����� �������� � ������
                rControl.����������������� = ������������� + " " + ������������;

                //������� ���� � ����� ���� ��������� �����
                if (un.�������������������.Rows.Count != 0)
                {
                    rowControlReestr��� = un.�������������������.Rows[0];
                }

                //������� ����� ���� 
                //string ��������� = rowControlReestr���["���������"].ToString();

                //������� ���� ���� ��������� �����
                //string �������� = Convert.ToDateTime(rowControlReestr���["��������������"]).ToShortDateString();

                //������� � ������ ����� � ���� ���� ��������� ����� 
                //rControl.����������������������� = ��������� + " " + ��������;

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

                //����������� � �������
                
                using(SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {

                    //������� ����������
                    con.Open();

                    //�������� �� � ������ ����������
                    SqlTransaction transact = con.BeginTransaction();

                //������� id ������������
                string queryIdHosp = "select id_������������ from dbo.������������ where ��� = " + rowHosp["���"].ToString() + " ";

                SqlCommand com = new SqlCommand(queryIdHosp, con);
                com.Transaction = transact;
                SqlDataReader read = com.ExecuteReader();

                while (read.Read())
                {
                    idHosp = Convert.ToInt32(read["id_������������"]);
                }

                read.Close();

                //������� ������ ����� �� �������� ��������
                DataTable tab������� = un.����������������;
                foreach (DataRow rowDog in tab�������.Rows)
                {
                    //�������� ��������� ������� ��� �������� ��������� ���������� ��� ����������� ���������
                    ErrorsReestrUnload error = new ErrorsReestrUnload();

                    //string linkText = "'%" + rowDog["������������������"].ToString() + "%'"; � ������������� like
                    //string linkText = "'" + rowDog["������������������"].ToString().Trim() + "'";
                    string linkText = "'" + rowDog["������������������"].ToString() + "'";
                    //�������� �������� ���� ����� � ��������� ������� ������� � ����� �������
                    string queryViewServices = "select ���������,���� from dbo.�������� " +
                                               "where id_������������ = " + idHosp + " and ��������� = " + linkText + " ";

                    SqlCommand comViewServ = new SqlCommand(queryViewServices, con);
                    comViewServ.Transaction = transact;
                    SqlDataReader readViewServ = comViewServ.ExecuteReader();

                    //������� �������� ������ � ��������� ������� ��������� � ��� �� �������
                    while (readViewServ.Read())
                    {
                        //��� ������ �� �������
                        //�������� = readViewServ["���������"].ToString().Trim();

                        �������� = readViewServ["���������"].ToString().Trim();

                        //���� ������ �� �������
                        ���� = Convert.ToDecimal(readViewServ["����"]);
                    }

                    //������� DataReader
                    readViewServ.Close();

                    //������ ������� ��� ����� � ���� � ��� ����� � ����� �������
                    if (rowDog["������������������"].ToString() == ��������.Trim())
                    {
                        error.Error������������������ = rowDog["������������������"].ToString().Trim();


                        //������ ���
                        errorFlag�������� = false;

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
                    if (Convert.ToDecimal(rowDog["����"]) == ����)
                    {
                        //test
                        decimal iTest = Convert.ToDecimal(rowDog["����"]);

                        //������ ���
                        errorFlag���� = false;
                    }
                    else
                    {
                        //������
                        errorFlag���� = true;

                        if (�������� != null)
                        {
                            //������� ������������ ������ 
                            error.������������������ = rowDog["������������������"].ToString().Trim(); //��������.Trim();

                            //������� �������
                            error.���� = ����;
                            error.Error���� = Convert.ToDecimal(rowDog["����"]);
                        }
                    }

                    //������ �������� ��������� �� ��������� ����� ��������� ����� �� ������� ���� ������
                    int ���������� = Convert.ToInt32(rowDog["����������"]);

                    //���������� ����������� ����� ��������� �����
                    decimal ����� = Math.Round((Math.Round(����, 2) * ����������), 2);

                    //���������� �������� ����� ����� ��� ����������� ���������
                    ������������������� = Math.Round((������������������� + �����), 2);

                    //���������� ����� � ����� �������� ��� ����������� �������
                    error������������������� = Math.Round((error������������������� + Convert.ToDecimal(rowDog["�����"])), 2);

                    //������ ����� �� ������
                    if (Convert.ToDecimal(rowDog["�����"]) == �����)
                    {
                        //test
                        decimal iTest = Convert.ToDecimal(rowDog["�����"]);

                        //������ ���
                        errorFlag��������������� = false;
                    }
                    else
                    {
                        //������
                        errorFlag��������������� = true;

                        //������� ������������ ������ 
                        error.������������������ = ��������.Trim();

                        //������� �������
                        error.����� = �����;
                        error.Error����� = Convert.ToDecimal(rowDog["�����"]);
                    }

                    //������� ��������� ������� � ��� ���������
                    if (errorFlag�������� == false && errorFlag���� == false && errorFlag��������������� == false)
                    {
                        if (ValContracts.Count != 0)//�������� ������
                        {
                            try
                            {
                                //������ � ������ ���� ����� �� ���������
                                ValContracts[�������������.Trim()].flagErrorSumm = true;
                            }
                            catch
                            {
                                //���� �������� ���������� ������ ���� � ������ ������ �� ������ ������ ��� ������ ��������� � ���� 
                                ValidateContract vc = new ValidateContract();
                                vc.FlagPerson���� = false;//��������� ��� �������� � ���� �� ������
                                vc.flagErrorSumm = true;//

                                //������� ������ ����������� �������� ����� � ����� �� ������
                                //vc.����������������� = listError;
                                ValContracts.Add(�������������.Trim(), vc);
                            }
                        }
                        else
                        {
                            ValidateContract vc = new ValidateContract();
                            vc.FlagPerson���� = false;//��������� ��� �������� � ���� �� ������
                            vc.flagErrorSumm = true;//

                            //������� ������ ����������� �������� ����� � ����� �� ������
                            //vc.����������������� = listError;
                            ValContracts.Add(�������������.Trim(), vc);
                        }
                    }
                    else
                    {
                        //��������� ������ � �� �������� ���� ������� � ������ 
                        error������ = true;

                        //������� ��������� �������� ������� ������ � ������ ������
                        listError.Add(error);

                        ////��������� ���� ������� �������� � ������ - false
                        //ValContracts[�������������.Trim()].flagErrorSumm = false;
                        //ValContracts[�������������.Trim()].����������������� = listError;
                    }

                    //������� ���������� ��� �������� ���������� 
                    �������� = string.Empty;
                    ���� = 0m;


                }

                if (error������ == true && ValContracts.Count != 0)
                {
                    //��������� ���� ������� �������� � ������ - false
                    ValContracts[�������������.Trim()].flagErrorSumm = false;

                    //������� ������ ����������� �������� ����� � ����� �� ������
                    ValContracts[�������������.Trim()].����������������� = listError;
                }

                    //���� ��������� � ���� ���
                if (error������ == true && ValContracts.Count == 0)
                {
                    ValidateContract vc = new ValidateContract();
                    vc.FlagPerson���� = false;
                    vc.flagErrorSumm = false;

                    //������� ������ ����������� �������� ����� � ����� �� ������
                    vc.����������������� = listError;
                    ValContracts.Add(�������������.Trim(), vc);
                }

                    //if(error������ == false && ValContracts.Count == 0)


                }
            }

        }
    }
}
