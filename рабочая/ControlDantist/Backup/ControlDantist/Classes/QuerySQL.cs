using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ������ � ����
    /// </summary>
    class QuerySQL
    {

        /// <summary>
        /// ����� �������� �� ���� ����
        /// </summary>
        /// <param name="�������">�������</param>
        /// <param name="���">���</param>
        /// <param name="��������">��������</param>
        /// <returns></returns>
        public static string Query����(string �������, string ���, string ��������)
        {
            string query = "select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as �������,dbo.SPR_FIO_NAME.A_NAME as ���,SPR_FIO_SECONDNAME.A_NAME as ��������,WM_ACTDOCUMENTS.DOCUMENTSTYPE,WM_ACTDOCUMENTS.DOCUMENTSERIES as '����� ���������',WM_ACTDOCUMENTS.DOCUMENTSNUMBER as '����� ���������',WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE as '���� ������',PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE as '�����'  from dbo.WM_PERSONAL_CARD " +
                           "join  SPR_FIO_SURNAME " +
                           "on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID " +
                           "join SPR_FIO_NAME " +
                           "on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME " +
                           "join SPR_FIO_SECONDNAME " +
                           "on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME " +
                           "join dbo.WM_ACTDOCUMENTS " +
                           "on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID " +
                           "join PPR_DOC " +
                           "on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID " +
                           "join WM_ADDRESS " +
                           "on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT " +
                           "where SPR_FIO_SURNAME.A_NAME = '" + �������.Trim() + "' and SPR_FIO_NAME.A_NAME = '" + ���.Trim() + "' and SPR_FIO_SECONDNAME.A_NAME = '" + ��������.Trim() + "' " +
                           //"and CONVERT(char(10), WM_PERSONAL_CARD.BIRTHDATE, 104) = '" + ������������ + "'  " +
                           "and PPR_DOC.A_NAME in ('������������� �������� �����','������������� �������� ����� ����������� �������','������������� � ����� �� ������ (������� - ��.20)','������������� � ����� �� ������ ��� ����������������� ���','������� � ������������','������������� � ����� �� ������ ��� ���, ���������� ������������� �� ������������ ���������','������� � ��������� ������������ �� ������������ ���������','������������� �������� ������� ������') and WM_PERSONAL_CARD.A_PCSTATUS = 1";//WM_PERSONAL_CARD.A_PCSTATUS = 1 - ����������� ������

            return query;
        }


        /// <summary>
        /// ����� �������� �� ���� ����
        /// </summary>
        /// <param name="�������">�������</param>
        /// <param name="���">���</param>
        /// <param name="��������">��������</param>
        /// <param name="������������">���� ��������</param>
        /// <returns></returns>
        public static string Query����(string �������, string ���, string ��������, string ������������)
        {
            string query = "select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as �������,dbo.SPR_FIO_NAME.A_NAME as ���,SPR_FIO_SECONDNAME.A_NAME as ��������,WM_ACTDOCUMENTS.DOCUMENTSTYPE,WM_ACTDOCUMENTS.DOCUMENTSERIES as '����� ���������',WM_ACTDOCUMENTS.DOCUMENTSNUMBER as '����� ���������',WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE as '���� ������',PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE as '�����'  from dbo.WM_PERSONAL_CARD " +
                           "join  SPR_FIO_SURNAME " +
                           "on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID " +
                           "join SPR_FIO_NAME " +
                           "on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME " +
                           "join SPR_FIO_SECONDNAME " +
                           "on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME " +
                           "join dbo.WM_ACTDOCUMENTS " +
                           "on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID " +
                           "join PPR_DOC " +
                           "on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID " +
                           "join WM_ADDRESS " +
                           "on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT " +
                           "where SPR_FIO_SURNAME.A_NAME = '"+ �������.Trim() +"' and SPR_FIO_NAME.A_NAME = '"+ ���.Trim() +"' and SPR_FIO_SECONDNAME.A_NAME = '"+ ��������.Trim() +"' " +
                           "and CONVERT(char(10), WM_PERSONAL_CARD.BIRTHDATE, 104) = '"+ ������������ + "'  " +
                           "and PPR_DOC.A_NAME in ('������������� �������� �����','������������� �������� ����� ����������� �������','������������� � ����� �� ������ (������� - ��.20)','������������� � ����� �� ������ ��� ����������������� ���','������� � ������������','������������� � ����� �� ������ ��� ���, ���������� ������������� �� ������������ ���������','������� � ��������� ������������ �� ������������ ���������','������������� �������� ������� ������') and WM_PERSONAL_CARD.A_PCSTATUS = 1";//WM_PERSONAL_CARD.A_PCSTATUS = 1 - ����������� ������

            return query;
        }



        /// <summary>
        /// ����� �������� �� ���� ����
        /// </summary>
        /// <param name="�������">�������</param>
        /// <param name="���">���</param>
        /// <param name="��������">��������</param>
        /// <param name="������������">���� ��������</param>
        /// <param name="�������������������">���� ������ ��������� ������� ����� �� ������</param>
        /// <returns></returns>
        public static string Query����(string �������, string ���, string ��������, string ������������, string �������������������)
        {
            string query = "select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as �������,dbo.SPR_FIO_NAME.A_NAME as ���,SPR_FIO_SECONDNAME.A_NAME as ��������,WM_ACTDOCUMENTS.DOCUMENTSTYPE,WM_ACTDOCUMENTS.DOCUMENTSERIES as '����� ���������',WM_ACTDOCUMENTS.DOCUMENTSNUMBER as '����� ���������',WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE as '���� ������',PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE as '�����'  from dbo.WM_PERSONAL_CARD " +
                           "join  SPR_FIO_SURNAME " +
                           "on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID " +
                           "join SPR_FIO_NAME " +
                           "on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME " +
                           "join SPR_FIO_SECONDNAME " +
                           "on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME " +
                           "join dbo.WM_ACTDOCUMENTS " +
                           "on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID " +
                           "join PPR_DOC " +
                           "on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID " +
                           "join WM_ADDRESS " +
                           "on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT " +
                           "where SPR_FIO_SURNAME.A_NAME = '" + �������.Trim() + "' and SPR_FIO_NAME.A_NAME = '" + ���.Trim() + "' and SPR_FIO_SECONDNAME.A_NAME = '" + ��������.Trim() + "' " +
                           "and CONVERT(char(10), WM_PERSONAL_CARD.BIRTHDATE, 104) = '" + ������������ + "'  " +
                           "and PPR_DOC.A_NAME in ('������������� �������� �����','������������� �������� ����� ����������� �������','������������� � ����� �� ������ (������� - ��.20)','������������� � ����� �� ������ ��� ����������������� ���','������� � ������������','������������� � ����� �� ������ ��� ���, ���������� ������������� �� ������������ ���������','������� � ��������� ������������ �� ������������ ���������','������������� �������� ������� ������') " +
                           "and CONVERT(char(10), WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE, 104) = '" + �������������������.Trim() + "' and WM_PERSONAL_CARD.A_PCSTATUS = 1";//WM_PERSONAL_CARD.A_PCSTATUS = 1 - ����������� ������ // and WM_ACTDOCUMENTS.DOCUMENTSERIES = '64' and WM_ACTDOCUMENTS.DOCUMENTSNUMBER = '4106938'

            return query;
        }


        /// <summary>
        /// ����� �������� �� ���� ����
        /// </summary>
        /// <param name="�������">�������</param>
        /// <param name="���">���</param>
        /// <param name="��������">��������</param>
        /// <param name="������������">���� ��������</param>
        /// <param name="�������������������">���� ������ ��������� ������� ����� �� ������</param>
       /// <param name="��������������">����� ���������</param>
       /// <param name="��������������">����� ���������</param>
       /// <returns></returns>
        public static string Query����(string �������, string ���, string ��������, string ������������, string �������������������, string ��������������, string ��������������)
        {
            string query = "select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as �������,dbo.SPR_FIO_NAME.A_NAME as ���,SPR_FIO_SECONDNAME.A_NAME as ��������,WM_ACTDOCUMENTS.DOCUMENTSTYPE,WM_ACTDOCUMENTS.DOCUMENTSERIES as '����� ���������',WM_ACTDOCUMENTS.DOCUMENTSNUMBER as '����� ���������',WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE as '���� ������',PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE as '�����'  from dbo.WM_PERSONAL_CARD " +
                           "join  SPR_FIO_SURNAME " +
                           "on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID " +
                           "join SPR_FIO_NAME " +
                           "on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME " +
                           "join SPR_FIO_SECONDNAME " +
                           "on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME " +
                           "join dbo.WM_ACTDOCUMENTS " +
                           "on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID " +
                           "join PPR_DOC " +
                           "on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID " +
                           "join WM_ADDRESS " +
                           "on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT " +
                           "where SPR_FIO_SURNAME.A_NAME = '" + �������.Trim() + "' and SPR_FIO_NAME.A_NAME = '" + ���.Trim() + "' and SPR_FIO_SECONDNAME.A_NAME = '" + ��������.Trim() + "' " +
                           "and CONVERT(char(10), WM_PERSONAL_CARD.BIRTHDATE, 104) = '" + ������������ + "'  " +
                           "and PPR_DOC.A_NAME in ('������������� �������� �����','������������� �������� ����� ����������� �������','������������� � ����� �� ������ (������� - ��.20)','������������� � ����� �� ������ ��� ����������������� ���','������� � ������������','������������� � ����� �� ������ ��� ���, ���������� ������������� �� ������������ ���������','������� � ��������� ������������ �� ������������ ���������','������������� �������� ������� ������') " +
                           "and CONVERT(char(10), WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE, 104) = '" + �������������������.Trim() + "' and WM_ACTDOCUMENTS.DOCUMENTSERIES = '" + �������������� + "' and WM_ACTDOCUMENTS.DOCUMENTSNUMBER = '" + �������������� + "' and WM_PERSONAL_CARD.A_PCSTATUS = 1";//WM_PERSONAL_CARD.A_PCSTATUS = 1 - ����������� ������

            return query;
        }


        /// <summary>
        /// ����� �������� �� ���� ����
        /// </summary>
        /// <param name="�������">�������</param>
        /// <param name="���">���</param>
        /// <param name="��������">��������</param>
        /// <param name="������������">���� ��������</param>
        /// <param name="�������������������">���� ������ ��������� ������� ����� �� ������</param>
        /// <param name="��������������">����� ���������</param>
        /// <param name="��������������">����� ���������</param>
        /// <returns></returns>
        public static string Query����(string �������, string ���, string ��������, string ������������, string �������������������, string ��������������, string ��������������, string ������������������, string �������������, string �������������)
        {
            string query = "select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as �������,dbo.SPR_FIO_NAME.A_NAME as ���,SPR_FIO_SECONDNAME.A_NAME as ��������,WM_ACTDOCUMENTS.DOCUMENTSTYPE,WM_ACTDOCUMENTS.DOCUMENTSERIES as '����� ���������',WM_ACTDOCUMENTS.DOCUMENTSNUMBER as '����� ���������',WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE as '���� ������',PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE as '�����'  from dbo.WM_PERSONAL_CARD " +
                           "join  SPR_FIO_SURNAME " +
                           "on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID " +
                           "join SPR_FIO_NAME " +
                           "on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME " +
                           "join SPR_FIO_SECONDNAME " +
                           "on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME " +
                           "join dbo.WM_ACTDOCUMENTS " +
                           "on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID " +
                           "join PPR_DOC " +
                           "on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID " +
                           "join WM_ADDRESS " +
                           "on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT " +
                           "where SPR_FIO_SURNAME.A_NAME = '" + �������.Trim() + "' and SPR_FIO_NAME.A_NAME = '" + ���.Trim() + "' and SPR_FIO_SECONDNAME.A_NAME = '" + ��������.Trim() + "' " +
                           "and CONVERT(char(10), WM_PERSONAL_CARD.BIRTHDATE, 104) = '" + ������������ + "'  " +
                           "and PPR_DOC.A_NAME in ('������������� �������� �����','������������� �������� ����� ����������� �������','������������� � ����� �� ������ (������� - ��.20)','������������� � ����� �� ������ ��� ����������������� ���','������� � ������������','������������� � ����� �� ������ ��� ���, ���������� ������������� �� ������������ ���������','������� � ��������� ������������ �� ������������ ���������','������������� �������� ������� ������','������� ���������� ������') " +
                           "and (WM_ACTDOCUMENTS.DOCUMENTSERIES = '" + �������������� + "' and WM_ACTDOCUMENTS.DOCUMENTSNUMBER = '" + �������������� + "') " + // -- ��������
                           // "and (WM_ACTDOCUMENTS.DOCUMENTSERIES = '" + ������������� + "' and WM_ACTDOCUMENTS.DOCUMENTSNUMBER = '" + ������������� + "') " + //-- �������
                           "and WM_PERSONAL_CARD.A_PCSTATUS = 1 ";

            return query;
        }

        /// <summary>
        /// ����� �������� �� ���� ���� �� �������� ���������� ��
        /// </summary>
        /// <param name="�������">�������</param>
        /// <param name="���">���</param>
        /// <param name="��������">��������</param>
        /// <param name="������������">���� ��������</param>
        /// <param name="�������������������">���� ������ ���������</param>
        /// <param name="��������������">����� ���������</param>
        /// <param name="��������������">����� ���������</param>
        /// <param name="������������������">���� ������ ��������</param>
        /// <param name="�������������">����� ��������</param>
        /// <param name="�������������">����� ��������</param>
        /// <returns></returns>
        public static string QueryPassport����(string �������, string ���, string ��������, string ������������, string �������������������, string ��������������, string ��������������, string ������������������, string �������������, string �������������)
        {
            string query = "select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as �������,dbo.SPR_FIO_NAME.A_NAME as ���,SPR_FIO_SECONDNAME.A_NAME as ��������,WM_ACTDOCUMENTS.DOCUMENTSTYPE,WM_ACTDOCUMENTS.DOCUMENTSERIES as '����� ���������',WM_ACTDOCUMENTS.DOCUMENTSNUMBER as '����� ���������',WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE as '���� ������',PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE as '�����'  from dbo.WM_PERSONAL_CARD " +
                           "join  SPR_FIO_SURNAME " +
                           "on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID " +
                           "join SPR_FIO_NAME " +
                           "on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME " +
                           "join SPR_FIO_SECONDNAME " +
                           "on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME " +
                           "join dbo.WM_ACTDOCUMENTS " +
                           "on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID " +
                           "join PPR_DOC " +
                           "on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID " +
                           "join WM_ADDRESS " +
                           "on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT " +
                           "where SPR_FIO_SURNAME.A_NAME = '" + �������.Trim() + "' and SPR_FIO_NAME.A_NAME = '" + ���.Trim() + "' and SPR_FIO_SECONDNAME.A_NAME = '" + ��������.Trim() + "' " +
                           //"and CONVERT(char(10), WM_PERSONAL_CARD.BIRTHDATE, 104) = '" + ������������ + "'  " +
                           "and PPR_DOC.A_NAME in ('������������� �������� �����','������������� �������� ����� ����������� �������','������������� � ����� �� ������ (������� - ��.20)','������������� � ����� �� ������ ��� ����������������� ���','������� � ������������','������������� � ����� �� ������ ��� ���, ���������� ������������� �� ������������ ���������','������� � ��������� ������������ �� ������������ ���������','������������� �������� ������� ������','������� ���������� ������') " +
                           //"and (WM_ACTDOCUMENTS.DOCUMENTSERIES = '" + �������������� + "' and WM_ACTDOCUMENTS.DOCUMENTSNUMBER = '" + �������������� + "') " + // -- ��������
                           "and (WM_ACTDOCUMENTS.DOCUMENTSERIES = '" + ������������� + "' and WM_ACTDOCUMENTS.DOCUMENTSNUMBER = '" + ������������� + "') " + //-- �������
                           "and WM_PERSONAL_CARD.A_PCSTATUS = 1 ";

            return query;
        }


    }
}
