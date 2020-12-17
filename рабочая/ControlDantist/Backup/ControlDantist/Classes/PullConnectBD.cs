using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ��� ����� ����������� � ����
    /// </summary>
    class PullConnectBD
    {
        public Dictionary<string,string> GetPull()
        {
            //������� ��������� ��� ��������� ����� �����������
            Dictionary<string, string> pul = new Dictionary<string, string>();

            //������� �������� �����
            string conVolg = ConfigurationSettings.AppSettings["conVolg"].ToString().Trim();
            pul.Add("��������",conVolg);

            //������� ��������� �����
            string conZav = ConfigurationSettings.AppSettings["conZav"].ToString().Trim();
            pul.Add("���������",conZav);

            //������� ��������� �����
            string conKir = ConfigurationSettings.AppSettings["conKir"].ToString().Trim();
            pul.Add("���������",conKir);

            //������� ����������� �����
            string conOkt = ConfigurationSettings.AppSettings["conOkt"].ToString().Trim();
            pul.Add("�����������",conOkt);

            //������� ��������� �����
            string conLen = ConfigurationSettings.AppSettings["conLen"].ToString().Trim();
            pul.Add("���������",conLen);

            //������� ����������� �����
            string conFrun = ConfigurationSettings.AppSettings["conFrun"].ToString().Trim();
            pul.Add("�����������",conFrun);

            //������� ��������������� �����
            string conKras = ConfigurationSettings.AppSettings["conKras"].ToString().Trim();
            pul.Add("���������������",conKras);

            //������� ����������� �����
            string conSar = ConfigurationSettings.AppSettings["conSar"].ToString().Trim();
            pul.Add("�����������",conSar);

            //������� ����������� �����
            string conTat = ConfigurationSettings.AppSettings["conTat"].ToString().Trim();
            pul.Add("�����������",conTat);

            //������� �������������� �����
            string conEkat = ConfigurationSettings.AppSettings["conEkat"].ToString().Trim();
            pul.Add("��������������",conEkat);

            //������� ����������� �����
            string conKalin = ConfigurationSettings.AppSettings["conKalin"].ToString().Trim();
            pul.Add("�����������",conKalin);

            //������� ����������� �����
            string conLisGor = ConfigurationSettings.AppSettings["conLisGor"].ToString().Trim();
            pul.Add("�����������",conLisGor);

            return pul;
        }
    }
}
