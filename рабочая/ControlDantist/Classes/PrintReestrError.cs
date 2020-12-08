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
    /// ������� �� ����� ������ � �������
    /// </summary>
    class PrintReestrError
    {
        private List<ErrorReestr> list;
        string ���������;

        public PrintReestrError(List<ErrorReestr> listError, string �����������������)
        {
            list = listError;
            ��������� = �����������������;
        }

        public void Print()
        {
            //string filName = System.Windows.Forms.Application.StartupPath + @"\���������\" + fName + ".doc";
            string filName = System.Windows.Forms.Application.StartupPath + @"\������\������ ��������� ��������.doc";
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

            doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

            //�������� �������
            object bookNaziv = "�������";
            Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 9, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 6;
            table.Columns[1].Width = 40;
            table.Columns[2].Width = 140;
            table.Columns[3].Width = 140;
            table.Columns[4].Width = 60;
            table.Columns[5].Width = 60;
            table.Columns[6].Width = 60;
            table.Columns[6].Width = 60;
            table.Columns[6].Width = 60;
            table.Columns[6].Width = 60;
            table.Borders.Enable = 1; // ����� - �������� �����
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 10;
            //������� �����
            int i = 1;

            //������� ��������� ������� ���������� ������ ��� ������� �������
            List<ReestrErrorControl> listItem = new List<ReestrErrorControl>();

            //���������� ����� �������
            ReestrErrorControl ����� = new ReestrErrorControl();
            �����.��������������� = "� �.�.";
            �����.��� = "���";

            �����.������������������ = "������������ ������";
            �����.����Control = "����������� ���� ������";
            �����.����Error = "��������� ���� ������";

            �����.���������������Control = "����������� ��������� ������";
            �����.���������������Error = "��������� ��������� ������";
            �����.�����Control = "����������� ����� ��������� �����";
            �����.����Error = "��������� ��������� ��������� �����";

            listItem.Add(�����);

            //�������� ������� �������
            foreach (ErrorReestr item in list)
            {
                ReestrErrorControl r = new ReestrErrorControl();
                r.��������������� = i.ToString();
                r.��� = item.���;

                //�������
                int iCount = 0;

                foreach (ErrorsReestrUnload err in item.ErrorList������)
                {
                    if (iCount == 0)
                    {
                        r.������������������ = err.������������������;
                        r.����Control = err.����.ToString();
                        r.����Error = err.Error����.ToString();
                        r.���������������Control = err.�����.ToString();
                        r.���������������Error = err.Error�����.ToString();

                        //������� � ���������
                        listItem.Add(r);
                    }
                    else
                    {
                        ReestrErrorControl r2 = new ReestrErrorControl();
                        r2.��������������� = "";
                        r2.��� = "";

                        r2.������������������ = err.������������������;
                        r2.����Control = err.����.ToString();
                        r2.����Error = err.Error����.ToString();
                        r2.���������������Control = err.�����.ToString();
                        r2.���������������Error = err.Error�����.ToString();

                        //������� � ���������
                        listItem.Add(r2);
                    }

                    iCount++;
                }

                ReestrErrorControl r���� = new ReestrErrorControl();
                r����.��������������� = "";
                r����.��� = "";

                r����.������������������ = "";
                r����.����Control = "";
                r����.����Error = "";
                r����.���������������Control = "";
                r����.���������������Error = "";

                r����.�����Control = item.������������������������.ToString();
                r����.����Error = item.Error������������������������.ToString();

                listItem.Add(r����);
                i++;
            }

            int k = 1;
            //������� ������ � �������
            foreach (ReestrErrorControl item in listItem)
            {
                //table.Cell(i, 1).Range.Text = i.ToString();//item.���������������;
                table.Cell(k, 1).Range.Text = item.���������������;

                table.Cell(k, 2).Range.Text = item.���;
                table.Cell(k, 3).Range.Text = item.������������������;

                table.Cell(k, 4).Range.Text = item.����Control;
                table.Cell(k, 5).Range.Text = item.����Error;

                table.Cell(k, 6).Range.Text = item.���������������Control;
                table.Cell(k, 7).Range.Text = item.���������������Error;

                table.Cell(k, 8).Range.Text = item.�����Control;
                table.Cell(k, 9).Range.Text = item.����Error;


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
            object newtxt = (object)���������;
            //object frwd = true;
            object frwd = false;
            doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            ref missing, ref missing);



            //��������� ��������
            app.Visible = true;

        }
    }
}
