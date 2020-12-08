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
        public Dictionary<string,string> GetPull(bool flagConnect)
        {
            //������� ��������� ��� ��������� ����� �����������
            Dictionary<string, string> pul = new Dictionary<string, string>();

            if (flagConnect == false)
            {
                //������� �������� �����
                string conVolg = ConfigurationSettings.AppSettings["conVolg"].ToString().Trim();
                pul.Add("��������", conVolg);

                //������� ��������� �����
                string conZav = ConfigurationSettings.AppSettings["conZav"].ToString().Trim();
                pul.Add("���������", conZav);

                //������� ��������� �����
                string conKir = ConfigurationSettings.AppSettings["conKir"].ToString().Trim();
                pul.Add("���������", conKir);

                //������� ����������� �����
                string conOkt = ConfigurationSettings.AppSettings["conOkt"].ToString().Trim();
                pul.Add("�����������", conOkt);

                //������� ��������� �����
                string conLen = ConfigurationSettings.AppSettings["conLen"].ToString().Trim();
                pul.Add("���������", conLen);

                //������� ����������� �����
                string conFrun = ConfigurationSettings.AppSettings["conFrun"].ToString().Trim();
                pul.Add("�����������", conFrun);

                ////������� ��������������� �����
                //string conKras = ConfigurationSettings.AppSettings["conKras"].ToString().Trim();
                //pul.Add("���������������", conKras);

                ////������� ����������� �����
                //string conSar = ConfigurationSettings.AppSettings["conSar"].ToString().Trim();
                //pul.Add("�����������", conSar);

                ////������� ����������� �����
                //string conTat = ConfigurationSettings.AppSettings["conTat"].ToString().Trim();
                //pul.Add("�����������", conTat);

                ////������� �������������� �����
                //string conEkat = ConfigurationSettings.AppSettings["conEkat"].ToString().Trim();
                //pul.Add("��������������", conEkat);

                ////������� ����������� �����
                //string conKalin = ConfigurationSettings.AppSettings["conKalin"].ToString().Trim();
                //pul.Add("�����������", conKalin);

                ////������� ����������� �����
                //string conLisGor = ConfigurationSettings.AppSettings["conLisGor"].ToString().Trim();
                //pul.Add("�����������", conLisGor);

                ////������� ���������� �����
                //string conPetrovsk = ConfigurationSettings.AppSettings["conPetrovsk"].ToString().Trim();
                //pul.Add("����������", conPetrovsk);

                ////������� ����� ������ �����
                //string conNewBur = ConfigurationSettings.AppSettings["conNewBur"].ToString().Trim();
                //pul.Add("�����������", conNewBur);

                ////������� ������� �����
                //string conAtcarsk = ConfigurationSettings.AppSettings["conAtcarsk"].ToString().Trim();
                //pul.Add("�������", conAtcarsk);

                ////������� ������� �����
                //string conBasar = ConfigurationSettings.AppSettings["conBasar"].ToString().Trim();
                //pul.Add("��������", conBasar);

                ////������� ������� �����
                //string conBaltai = ConfigurationSettings.AppSettings["conBaltai"].ToString().Trim();
                //pul.Add("������", conBaltai);

                ////����������� ����.
                //string con����������� = ConfigurationSettings.AppSettings["conVoskresensk"].ToString().Trim();
                //pul.Add("�����������", con�����������);

                ////////==========������ ������� ==================================
                //string con����� = ConfigurationSettings.AppSettings["������������-�������"].ToString().Trim();
                //pul.Add("������������-�������", con�����);

                //string con������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�������);

                //string con����������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�����������);


                //string con����������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�����������);


                //string con�������� = ConfigurationSettings.AppSettings["��������"].ToString().Trim();
                //pul.Add("��������", con��������);


                //string con������������ = ConfigurationSettings.AppSettings["������������"].ToString().Trim();
                //pul.Add("������������", con������������);


                //string con����������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�����������);


                //string con��������� = ConfigurationSettings.AppSettings["���������"].ToString().Trim();
                //pul.Add("���������", con���������);

                //string con������������ = ConfigurationSettings.AppSettings["������������"].ToString().Trim();
                //pul.Add("������������", con������������);

                //string con������������� = ConfigurationSettings.AppSettings["�������������"].ToString().Trim();
                //pul.Add("�������������", con�������������);

                //string con������������������ = ConfigurationSettings.AppSettings["������������������"].ToString().Trim();
                //pul.Add("������������������", con������������������);

                //string con����������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�����������);

                //string con������������ = ConfigurationSettings.AppSettings["������������"].ToString().Trim();
                //pul.Add("������������", con������������);

                //string con�������� = ConfigurationSettings.AppSettings["��������"].ToString().Trim();
                //pul.Add("��������", con��������);


                //string con����������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�����������);

                //string con��������� = ConfigurationSettings.AppSettings["���������"].ToString().Trim();
                //pul.Add("���������", con���������);

                //string con����������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�����������);

                //string con���������� = ConfigurationSettings.AppSettings["����������"].ToString().Trim();
                //pul.Add("����������", con����������);

                //string con����������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�����������);

                //string con���������� = ConfigurationSettings.AppSettings["����������"].ToString().Trim();
                //pul.Add("����������", con����������);

                //string con������������ = ConfigurationSettings.AppSettings["������������"].ToString().Trim();
                //pul.Add("������������", con������������);

                //string con��������� = ConfigurationSettings.AppSettings["���������"].ToString().Trim();
                //pul.Add("���������", con���������);

                //string con���������� = ConfigurationSettings.AppSettings["����������"].ToString().Trim();
                //pul.Add("����������", con����������);

                //string con����������� = ConfigurationSettings.AppSettings["�����������"].ToString().Trim();
                //pul.Add("�����������", con�����������);

                //string con���������� = ConfigurationSettings.AppSettings["����������"].ToString().Trim();
                //pul.Add("����������", con����������);

                //string con���������� = ConfigurationSettings.AppSettings["����������"].ToString().Trim();
                //pul.Add("����������", con����������);

            }


            return pul;
        }
    }
}
