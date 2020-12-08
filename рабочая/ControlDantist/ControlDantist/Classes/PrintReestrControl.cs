using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Runtime.Serialization.Formatters.Binary;

using ControlDantist.Classes;


namespace ControlDantist.Classes
{
    /// <summary>
    /// ������� �� ����� �������� word ���������� ������ ���������
    /// </summary>
    class PrintReestrControl
    {
        //��������� ������ ���������� �������
        private List<ReestrControl> list;
        
        //���������� ������ ��� ������������ �����
        private string fName = string.Empty;

        //������ ����� ����� �������
        private string ���������� = string.Empty;

        //������ �������� �������� ���������
        private string ������������ = string.Empty;

        public PrintReestrControl(List<ReestrControl> listReestrControl,string fileName, string ����������, string �����������������)
        {
            list = listReestrControl;
            fName = fileName;
            ���������� = ����������;
            ������������ = �����������������;
        }

        public void Print()
        {
            //string fName = "������ " + this.dtStart.Value.ToShortDateString().Replace('.', '_') + " " + this.dtEnd.Value.ToShortDateString().Replace('.', '_');
            //string fName = fileName;

            //try
            //{
            //    //��������� ������ � ����� ���������
            //    //FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\������\������.doc");
            //    ////fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\���������\" + fName + ".doc", true);
            //    ////fn.CopyTo(@"\���������\" + fName + ".doc", true);
            //    ////fn.CopyTo(@"\���������\" + ������ + ".doc", true);
            //    //fn.Delete();
            //}
            //catch
            //{
            //    MessageBox.Show("�������� � ���� ������ ��� ����������");
            //}

            //string filName = System.Windows.Forms.Application.StartupPath + @"\���������\" + fName + ".doc";
            string filName = System.Windows.Forms.Application.StartupPath + @"\������\������.doc";
            //string filName = fName;

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

            //�������� �������
            object bookNaziv = "�������";
            Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 6;
            table.Columns[1].Width = 40;
            table.Columns[2].Width = 140;
            table.Columns[3].Width = 80;
            table.Columns[4].Width = 80;
            table.Columns[5].Width = 80;
            table.Columns[5].Width = 80;
            table.Borders.Enable = 1; // ����� - �������� �����
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 10;
            //������� �����
            int i = 1;

            List<ReestrControl> listItem = new List<ReestrControl>();

            ReestrControl ����� = new ReestrControl();
            �����.����� = "� �.�.";
            �����.��� = "�.�.�.";
            �����.����������������� = "� � ���� �������� �� �������������� �����";
            �����.����������������������� = "� � ���� ���� �������� �����";
            �����.�������������� = "����� � ���� ��������� � ����� �� ������";
            �����.�������������� = "��������� ������ ���.";

            listItem.Add(�����);

            foreach (ReestrControl item in list)
            {
                ReestrControl r = new ReestrControl();
                r.����� = i.ToString();
                r.���  = item.���;
                r.����������������� = item.�����������������;
                r.�����������������������  = item.�����������������������;
                r.�������������� = item.��������������;
                r.�������������� = item.��������������;

                listItem.Add(r);
                i++;
            }

            //������� ������ �����
            ReestrControl ����� = new ReestrControl();
            �����.����� = "";
            �����.��� = "����� �� �������:";
            �����.����������������� = "";
            �����.����������������������� = "";
            �����.�������������� = "";
            �����.�������������� = ����������;

            //������� � ��������� ������ � �������� ������
            listItem.Add(�����);

            int k = 1;
            //������� ������ � �������
            foreach (ReestrControl item in listItem)
            {
                //table.Cell(i, 1).Range.Text = i.ToString();//item.���������������;
                table.Cell(k, 1).Range.Text = item.�����;

                table.Cell(k, 2).Range.Text = item.���;

                table.Cell(k, 3).Range.Text = item.�����������������;
                table.Cell(k, 4).Range.Text = item.�����������������������;
                table.Cell(k, 5).Range.Text = item.��������������;
                table.Cell(k, 6).Range.Text = item.��������������;

                //doc.Words.Count.ToString();
                Object beforeRow1 = Type.Missing;
                table.Rows.Add(ref beforeRow1);

                k++;
            }

            table.Rows[k].Delete();

            ////����� ��������
            object wdrepl = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt = "���������";
            object newtxt = (object)������������;
            //object frwd = true;
            object frwd = false;
            doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            ref missing, ref missing);

            app.Visible = true;



        }
                
    }
}
