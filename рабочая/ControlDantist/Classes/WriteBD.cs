using System;
using System.Collections.Generic;
using System.Text;
using ControlDantist.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;

using DantistLibrary;
using ControlDantist.WriteClassDB;
using ControlDantist.Repository;

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
        private DataTable t;

        public IStringQuery queryWrite { get; set; }

        //���������, ��� ������� � ����� ������� ��� ������� � ��
        private bool flagDog = false;

        public UnitDate UnitDate { get; set; }

        public WriteBD(List<Unload> unload)
        {
            list = unload;
        }

        /// <summary>
        /// ID ����� ������� �������� ���������.
        /// </summary>
        public int IdFIleRegistrProject { get; set; }

        public bool FlagAddDate { get; set; }

        /// <summary>
        /// ���������� ������ � ���� ������
        /// </summary>
        public string Write()
        {

            //��������� ������� ��������
            CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCInfo;

            //��� ��� �� ����� ������ ������� � �� ���������� �������� ��� ������� 
            //�������� ��� �� �������, � ������ �� ������������ ��� � �� ����������
            //���������� ����� � ������ �����������(������ ���� ����� ����� ���� � ������� TSQL)
            int iTest = list.Count;

            // ������������ �������� ������ ���������� � �������� ��������� � ���� ������.

            // ������ ��� �������� SQL ������� � �� �� ������ �������� ���������.
            StringBuilder buildQuery = new StringBuilder();

            // ������� ���������.
            int iCountContract = 0;

            //������� ������
            int iCount = 0;

            int iCount2 = 0;

            int iCount33 = 0;

            int iCount4 = 0;


            foreach (Unload unload in list)
            {

                //I�������� personFio = new FindPerson();

                // // �������� ������ �� ��������.
                // DataTable tab����� = unload.��������;

                // // ������ �� ���������.
                // personFio.Famili = tab�����.Rows[0]["�������"].ToString().Trim();
                // personFio.Name = tab�����.Rows[0]["���"].ToString().Trim();
                // personFio.SecondName = tab�����.Rows[0]["��������"].ToString().Trim();
                // personFio.DateBirtch = �����.����(Convert.ToDateTime(tab�����.Rows[0]["������������"]).ToShortDateString());

                // // ������������ ����������� ������.
                 ISity sity = new NameSity();

                 DataTable tabSity = unload.��������������;

                // if(personFio.Famili == "������")
                // {
                //     string iTest3 = "";
                // }


                if (tabSity.Rows.Count == 0)
                {
                    sity.NameTown = "���� �������";
                }
                else
                {
                    // ������� ������������ ����������� ������ � ������� ��������� ��������.
                    sity.NameTown = tabSity.Rows[0]["������������"].ToString().Trim();
                }

                // // ��������� ������ �� ���������.
                // queryWrite.PersonFio = personFio;

                // // ���������� �����.
                // queryWrite.NameSity = sity;

                UnitDate unitDate = new UnitDate();

                // ��������� ���������.
                queryWrite.����������������� = unload.�����������������.Trim();

                // ������� �������� ���������.
                Repository.����������������� ����������������� = unitDate.�����������������Repository.Get�����������������(unload.�����������������.Trim());

                // ��� ���������.
                queryWrite.������������ = unload.������������.Rows[0][1].ToString();

                // ���������� �� ����� �������� ��� ������ �� ���������.
                DataRow rw_�������� = unload.��������.Rows[0];

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
                personFull.id_�������� = 0;//                      ",@id��������_" + iCount + " " +
                personFull.�������������� = rw_��������["��������������"].ToString().Trim();
                personFull.�������������� = rw_��������["��������������"].ToString().Trim();
                personFull.������������������� = Convert.ToDateTime(rw_��������["�������������������"]);
                personFull.���������������� = rw_��������["����������������"].ToString().Trim();
                personFull.id_������� = 1;//id ������� � ��� �� ��������� 
                personFull.id_����� = Convert.ToInt16(rw_��������["id_�����"]);

                 if((personFull.�������.Trim().ToLower() == "������".ToLower().Trim()) && (personFull.���.Trim().ToLower() == "������".ToLower().Trim()) && (personFull.��������.Trim().ToLower() == "����������".ToLower().Trim()))
                 {
                    DateTime dt = new DateTime(1942, 8, 2);

                    personFull.������������������ = dt;
                 }

                // ������� id ����������� ������.
                var findSity = this.UnitDate.���������������Repository.Filtr���������������(sity.NameTown);

                if(findSity != null)
                {
                    personFull.id_�������� = findSity.id_��������;
                }
                else
                {
                    �������������� �������������� = new ��������������();
                    ��������������.������������ = sity.NameTown;

                    // ������� �� ����� ���������� �����.
                    UnitDate.���������������Repository.Insert(��������������);

                    personFull.id_�������� = ��������������.id_��������;
                }

                // �������� ���� �� �������� � ��� � ����� �������� � ��.
                if(unitDate.��������AddRepository.SelectPerson(personFull)!= null)
                {
                    // ������� ������ �� ���������.
                    unitDate.��������AddRepository.Update(personFull);
                }
                else
                {
                    // ������� ������ ���������.
                    unitDate.��������AddRepository.Insert(personFull);
                }
              

                // ������� �������.
                //IContract contract = new Contract();

                DataRow rowC = unload.�������.Rows[0];

                �������Add contract = new �������Add();

                // ������� ������ �� ������������.
                DataRow rowHosp = unload.������������.Rows[0];

                // �������� ������ �� ������������.
                int idHospital = unitDate.���������������Repository.Select���(rowHosp["���"].ToString());

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

                ////contract.Note = "";
                ////contract.FlagContract = false;
                ////contract.FalgAct = false;
                ////contract.numContract = rowC["�������������"].ToString();

                ////// ������� ����.
                ////contract.DateWriteContract = �����.����(DateTime.Now.Date.ToShortDateString());

                //// ���� ������� ����� ��� ��������� ��������.
                // if (unload.FalgWrite == true)
                // {
                //     contract.FlagValidate = true;
                // }
                // else
                // {
                //    contract.FlagValidate = false;
                // }

                // ������� ��� ��� �������.
                 contract.logWrite = MyAplicationIdentity.GetUses();

                //string iTestDate = contract.DateWriteContract;

                // queryWrite.contract = contract;

                // ������� ������ �� ��������.
                unitDate.�������AddRepository.Insert(contract);


                 // ������ �� ��������.
                 DataTable tabServices = unload.����������������;

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


                // ������� ������ � ������������.
                DataRow rowHosp2  = unload.������������.Rows[0];

                IAddHospital ih = new InsertDateHospital();
                ih.������������������������ = rowHosp2["������������������������"].ToString().Trim();
                ih.��������������� = rowHosp2["���������������"].ToString().Trim();
                ih.���������������� = rowHosp2["����������������"].ToString().Trim();
                ih.���������������� = rowHosp2["����������������"].ToString().Trim();
                ih.id_�������� = 1;
                ih.id_������� =1;
                ih.������������������������ = rowHosp2["������������������������"].ToString().Trim();
                ih.��� = rowHosp2["���"].ToString().Trim();
                ih.��� = rowHosp2["���"].ToString().Trim();
                ih.��� = rowHosp2["���"].ToString().Trim();
                ih.����������������� = rowHosp2["�����������������"].ToString().Trim();
                ih.������������� = rowHosp2["�������������"].ToString().Trim();
                ih.����������� = rowHosp2["�����������"].ToString().Trim();
                ih.������������� = rowHosp2["�������������"].ToString().Trim();
                ih.����������������������� = Convert.ToDateTime(rowHosp2["�����������������������"]).ToShortDateString();
                ih.���� = rowHosp2["����"].ToString().Trim();
                ih.����������������������������� = rowHosp2["�����������������������������"].ToString().Trim();
                ih.��������������������� = rowHosp2["���������������������"].ToString().Trim();
                ih.������������� = rowHosp2["�������������"].ToString().Trim();
                ih.���� = rowHosp2["����"].ToString().Trim();
                ih.����� = rowHosp2["�����"].ToString().Trim();
                ih.Flag = Convert.ToBoolean(rowHosp2["Flag"]);
                ih.���������������������� = Convert.ToInt32(rowHosp2["����������������������"]);

                //// ������� ������ � �����.
                //IHospital hospSnils = new SelectHjspital();
                
                //hospSnils.��� = rowHosp2["���"].ToString().Trim();

                //queryWrite.hospInn = hospSnils;

                //// ������� ������ �� ������������.
                //queryWrite.dataHospital = ih;

                //// Id ����� �������� ���������.
                //queryWrite.IdFileRegistrProject = this.IdFIleRegistrProject;

                //// ������� ��������� �� ������ ����� �� ��������.
                ////queryWrite.QueryInsertServicesContract = servicesInsert.ToString();

                //queryWrite.GetServicesContract(listServicesContract);

                ////var testInsert = contract.DateWriteContract;

                //string iTest2 = "";

                // string strQuery = queryWrite.Query(iCount);

                // buildQuery.Append(strQuery);

                //iCount++;

                //string strQueryReceptionCOntract = queryWrite.QueryReception(iCount);

                //buildQuery.Append(strQueryReceptionCOntract);

                iCount++;

            }

            return buildQuery.ToString().Trim();
        }


        // }
    }
}
//}
