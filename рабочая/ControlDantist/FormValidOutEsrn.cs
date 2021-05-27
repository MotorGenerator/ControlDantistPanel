﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using ControlDantist.ClassValidRegions;
using ControlDantist.DisplayRegistr;
using ControlDantist.DisplayDatePerson;
using ControlDantist.Letter;
using ControlDantist.Repository;
using ControlDantist.Classes;
using ControlDantist.ValidPersonContract;
using ControlDantist.LetterClassess;
using DantistLibrary;
using ControlDantist.DataBaseContext;
using ControlDantist.MedicalServices;
using ControlDantist.WriteDB;



namespace ControlDantist
{
    public partial class FormValidOutEsrn : Form
    {
        private Dictionary<string, PersonValidEsrn> dictionary;

        private string инн = string.Empty;

        // Реестр с проетками договоров.
        private IEnumerable<ItemLibrary> listProjectContrats;

        /// <summary>
        /// Делегат установки флагов проверки 
        /// </summary>
        /// <param name="itemLibrary"></param>
        private delegate void SetValidToReestr(ItemLibrary itemLibrary);

        /// <summary>
        /// Id файла с ррестром файлов проектов договоров.
        /// </summary>
        public int IdFileRegistr { get; set; }

        public FormValidOutEsrn(Dictionary<string, PersonValidEsrn> dictionary, string инн)
        {
            InitializeComponent();

            if (dictionary != null)
            {
                this.dictionary = dictionary;

                this.инн = инн;
            }
            else
            {
                throw new Exception("Отсутствуют данные в реестре проектов догворов");
            }
        }

        public FormValidOutEsrn(IEnumerable<ItemLibrary> listProjectContrats)
        {
            this.listProjectContrats = listProjectContrats;

            InitializeComponent();
        }

        private void FormValidOutEsrn_Load(object sender, EventArgs e)
        {
            //DisplayRegistrProject displayRegistrProject = new DisplayRegistrProject(dictionary);

            DisplayRegistrProject displayRegistrProject = new DisplayRegistrProject(this.listProjectContrats);

            // 
            this.dataGridView1.DataSource = displayRegistrProject.GetRegistr();

            // Отформатируем DataGrid.
            ViewDataGrid();

            // Выведим строку итого: количество договоров и общая сумма договоров.
            var countContracts = displayRegistrProject.GetRegistr().Count;
            if (countContracts > 0)
            {
                string итого = "кол-во договоров - " + countContracts + " на сумму " + displayRegistrProject.GetSumContracts().ToString("c");

                this.lblCount.Text = "";

                this.lblCount.Text = итого;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewDataGrid()
        {
            this.dataGridView1.Columns["IdContract"].Visible = false;
            this.dataGridView1.Columns["НомерДоговора"].Width = 60;
            this.dataGridView1.Columns["FlagValidEsrn"].HeaderText = "Проверка ЭСРН";
            this.dataGridView1.Columns["FlagValidServices"].HeaderText = "Проверка услуг";
            this.dataGridView1.Columns["FlagSaveContract"].HeaderText = "Сохранить в БД";
            this.dataGridView1.Columns["SumContract"].HeaderText = "Сумма договора";
            this.dataGridView1.Columns["Адрес"].HeaderText = "Адрес";
            this.dataGridView1.Columns["Адрес"].Visible = false;
            this.dataGridView1.Columns["СерияНомерУдостоверения"].HeaderText = "Серия номер удостоверения";
            this.dataGridView1.Columns["СерияНомерУдостоверения"].Visible = false;

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            this.txtЛьготник.Text = "";
            this.txtАдрес.Text = "";
            this.txtДокумент.Text = "";

            // Отображать данные по льготнику будем только у прошедших проверку.
            DataGridViewRow row = this.dataGridView1.CurrentRow;

            // ПОлучим id договора.
            int idContract = Convert.ToInt32(row.Cells["IdContract"].Value);

            // Номер договора.
            string numContract = row.Cells["НомерДоговора"].Value.ToString();

            // Если договор прошел проверку.
            if (Convert.ToBoolean(row.Cells["FlagValidEsrn"].Value) == true)
            {
                // Отобразим данные из договора.
                DisplayResultValidate displayResultValidate = new DisplayResultValidate(numContract);

                DatePersonForDisplay dp = displayResultValidate.GetFioPerson(this.listProjectContrats.ToList());

                this.txtЛьготник.Text = dp.Фио + " " + dp.ДатаРождения + " г. р.";
                this.txtАдрес.Text = dp.Адрес;
                this.txtДокумент.Text = dp.Удостоверение;
            }

            // Если договор не прошел проверку.
            if (Convert.ToBoolean(row.Cells["FlagValidServices"].Value) == false)
            {
                // Реестр проектов договоров.
                ReestrContract rc = new ReestrContract(this.listProjectContrats.ToList());

                //  Подключение к БД.
                using (DContext dc = new DContext(ConnectDB.ConnectionString()))
                {
                    // Доступ к медицинским услугам поликлинники.
                    ServicesMedicalHospital medServices = new ServicesMedicalHospital(dc);

                    // Установим id поликлинники чьи медицинские услуги необходимо получить.
                    medServices.SetIdentificator(rc.IdHospital());

                    // Получим услуги поликлинники.
                    List<ТВидУслуг> listKU = medServices.ServicesMedical();

                    // Если список с улугами существует.
                    if (listKU.Count() > 0)
                    {
                        // Услуги по текущему договору.
                        List<ТУслугиПоДоговору> listServicesContract = rc.GetServicesContract(idContract);

                        //// Сконвертим listKU к типу List<ТУслугиПоДоговору>.
                        //List<ТУслугиПоДоговору> listHospital = listKU;

                        if (listServicesContract.Count() > 0)
                        {
                            // Сравним услуги 
                            var listServicesCont = listServicesContract.Select(w => new ТВидУслуг { ВидУслуги = w.НаименованиеУслуги, Цена = (decimal)w.цена }).AsQueryable();

                            // Список для хранения расхождения услуг.
                            List<ТУслугиПоДоговору> displayList = new List<ТУслугиПоДоговору>();

                            // Так как я не разобрался с LINQ LEFT JOIN.
                            foreach (var itm in listServicesContract)
                            {
                                var result = listKU.Where(w => w.ВидУслуги.Trim().ToLower().Replace(" ", "") == itm.НаименованиеУслуги.Trim().ToLower().Replace(" ", "") &&
                                w.Цена == itm.цена).FirstOrDefault();

                                if (result == null)
                                {
                                    displayList.Add(itm);
                                }

                            }

                            ////Сджойним усулуги по договру и услуги в поликлиннике.
                            //var result = from x in listServicesCont
                            //             join y in listKU
                            //             on new ТВидУслуг { ВидУслуги = x.ВидУслуги.Trim().ToLower(), Цена = x.Цена } equals new ТВидУслуг { ВидУслуги = y.ВидУслуги.Trim().ToLower(), Цена = y.Цена }
                            //             into gj
                            //             from subpet in gj.DefaultIfEmpty()
                            //             select new ТВидУслуг
                            //             {
                            //                 ВидУслуги = x.ВидУслуги,
                            //                 Цена = x.Цена
                            //             };


                            // Сравним количество услуг в договре и резултат проверки.
                            if (listServicesContract != null)
                            {
                                this.dataError.DataSource = displayList.Select(w => new { НаименованиеУслуги = w.НаименованиеУслуги, Цена = w.цена }).ToList();

                                this.txtЛьготник.Text = "";
                                this.txtАдрес.Text = "";
                                this.txtДокумент.Text = "";
                            }


                        }
                    }
                }


                //// Выведим разницу в услугах у льготников которые не прошли проверку по услугам.
                //ShowDifference showDifference = new ShowDifference(idContract);

                //// 
                //var list = showDifference.Display();

                //if (list != null && list.Count > 0)
                //{
                //    this.dataError.DataSource = list;

                //    this.txtЛьготник.Text = "";
                //    this.txtАдрес.Text = "";
                //    this.txtДокумент.Text = "";
                //}
                //else
                //{
                //    var listServ = showDifference.DisplayErrorServer();

                //    this.dataCorrect.DataSource = listServ;

                //    this.txtЛьготник.Text = "";
                //    this.txtАдрес.Text = "";
                //    this.txtДокумент.Text = "";
                //}

                //// Покажем услуги которых нет на сервере.

            }

        }

        private void btnDist_Click(object sender, EventArgs e)
        {
            // Обьявим переменную типа делегат.
            SetValidToReestr svDel;

            // Прсвим выполняемый метод.
            svDel = SetValidateContract;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                string numContract = row.Cells["НомерДоговора"].Value.ToString();

                if (Convert.ToBoolean(row.Cells["FlagValidEsrn"].Value) == true && (Convert.ToBoolean(row.Cells["FlagValidServices"].Value) == true))
                {
                    row.Cells["FlagSaveContract"].Value = true;

                    // Получим текущий контракт.
                    var currContract = listProjectContrats.Where(w => w.NumContract.Trim() == numContract.Trim()).FirstOrDefault();

                    // Выполним метод.
                    svDel.Invoke(currContract);

                }
                else
                {
                    //MessageBox.Show("Льготник - "+ row.Cells["ФиоЛьготник"].Value.ToString()  + " Договор № - " + numContract.Trim() + "  не может быть записан");
                    MessageBox.Show("Льготник " + row.Cells["ФиоЛьготник"].Value.ToString().Trim() + "  не может быть записан");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Соберем всех прошедших проверку.
            //List<PersonValidEsrn> dictionaryTrue = dictionary.Values.Where(w => w.flagValidEsrn == true && w.flagValidMedicalServices == true).ToList();


            var personalsTrue = this.listProjectContrats.Where(w => w.FlagValidateEsrn == true && w.FlagValidateMedicalServices == true).ToList();

            // Преобразуем данные из формата ItemLibrary в фомат PersonValidEsrn.
            List<PersonValidEsrn> dictionaryTrue = ConvertDate(personalsTrue);//, out this.инн);

            // Выведим письмо на печать.
            ILetter tryLetter = new ПодтвержденныеПисьма(dictionaryTrue, инн);

            tryLetter.Print();
        }

        private List<PersonValidEsrn> ConvertDate(IEnumerable<ItemLibrary> items)//, out string инн)
        {
            // Список для хранения данных по льготникам прошедшим проверку.
            List<PersonValidEsrn> dictionaryTrue = new List<PersonValidEsrn>();

            foreach (var contract in items)
            {
                // Пункт для письма.
                PersonValidEsrn person = new PersonValidEsrn();

                // Получаем инн поликлинники.
                this.инн = contract.Packecge?.hosp?.ИНН ?? "";

                // Получим данные по льготнику.
                ТЛЬготник льготник = contract.Packecge.льготник;

                person.фамилия = льготник.Фамилия.Trim();

                person.имя = льготник.Имя.Trim();

                person.отчество = льготник.Отчество.Trim();

                // Договор.
                person.номерДоговора = contract.NumContract.Trim();

                dictionaryTrue.Add(person);
            }

            return dictionaryTrue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Соберем всех прошедших роверку.
            // Соберем всех непрошедших проверку.
            //List<PersonValidEsrn> dictionaryTrue = dictionary.Values.Where(w => w.flagValidEsrn == false || w.flagValidMedicalServices == false).ToList();

            var personalsTrue = this.listProjectContrats.Where(w => w.FlagValidateEsrn == false || w.FlagValidateMedicalServices == false).ToList();

            // Преобразуем данные из формата ItemLibrary в фомат PersonValidEsrn.
            List<PersonValidEsrn> dictionaryTrue = ConvertDate(personalsTrue);//, out this.инн);

            ILetter tryLetter = new НеПодтвержденные(dictionaryTrue, инн);

            tryLetter.Print();
        }


        /// <summary>
        /// Функция перевода реестра в состяние проверки.
        /// </summary>
        //private void SetValidateContract(bool flagValidEsrn, bool flagValidMedicalServices, ItemLibrary item)
        private void SetValidateContract(ItemLibrary item)
        {
            if (item.FlagValidateEsrn == true && item.FlagValidateMedicalServices == true)
            {
                item.FlagValidContract = true;
            }
            else
            {
                item.FlagValidContract = false;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //int idContract = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["IdContract"].Value);

            // Проверим значение флага проверки льготника по ЭСРН.
            bool flagValidEsrn = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["FlagValidEsrn"].Value);

            // Номер договора.
            string numContract = this.dataGridView1.CurrentRow.Cells["НомерДоговора"].Value.ToString();

            // Получим текущий контракт.
            var currContract = listProjectContrats.Where(w => w.NumContract.Trim() == numContract.Trim()).FirstOrDefault();

            // Проверяем значение флага проверки медицинских услуг.
            bool flagValidServices = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["FlagValidServices"].Value);

            // Если Установлен флаг проверки в True.
            if (flagValidEsrn == true)
            {
                // Установим что льготнико прошел проверку.
                if (currContract != null)
                {
                    currContract.FlagValidateEsrn = true;
                    //currContract..FlagValidPersonFioEsrn = true;
                    //currContract.FlagValidPersonPassword = true;
                }
            }
            else
            {
                // Если флаг установлен в False.
                if (currContract != null)
                {
                    // То установим, что льгоник не прошел проверку.
                    currContract.FlagValidateEsrn = false;
                    //currContract.FlagValidPersonFioEsrn = false;
                    //currContract.FlagValidPersonPassword = false;
                }
            }

            // Если флаг установлен в положение прошедшее проверку.
            if (flagValidServices == true)
            {
                if (currContract != null)
                {
                    // Кстановим прошёл проверку по медицинским услугам.
                    currContract.FlagValidateMedicalServices = true;
                }
            }
            else
            {
                if (currContract != null)
                {
                    // Кстановим не прошёл проверку по медицинским услугам.
                    currContract.FlagValidateMedicalServices = false;
                }
            }

            // Установим флаг прошедшего проверку.
            // Обьявим переменную типа делегат.
            SetValidToReestr svDel;

            // Прсвим выполняемый метод.
            svDel = SetValidateContract;

            // Выполним метод.
            svDel.Invoke(currContract);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получим список договоров из файла реестра для документа для отображения повторных договоров.
            ValidateContractPerson vclPrint = new ValidateContractPerson(this.listProjectContrats.ToList());
            List<PrintContractsValidate> listDoc = vclPrint.GetContract();

            // Если количество догооворов из файла реестра > 0.
            if (listDoc != null && listDoc.Count > 0)
            {
                // Выведим список совподений договров на бумагу в Word.
                WordReport wordPrint = new WordReport(listDoc);

                // Выведим список проектов договоров на печать.
                DocPrint docPrint = new DocPrint(wordPrint);
                docPrint.Execute();
            }

            // Вывидем диалоговое окно с предложением о записи проектов договоров.
            //Предложим запись в БД 
            FormDalogWriteBD formDialog = new FormDalogWriteBD();
            formDialog.ShowDialog();

            // Если ответили нет, значит запись проектов договоров отменяется.
            if (formDialog.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            // Выполним в единой транзакции запись проектов договоров.
            using (DContext dc = new DContext(ConnectDB.ConnectionString()))
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    //try
                    //{
                    // Проходимся по списку договоров.
                    foreach (var itm in this.listProjectContrats)
                    {
                        // Номер договора.
                        ТДоговор тДоговор = itm.Packecge.тДоговор;

                        // Проверим есть ли у текущего договора акты выполненных работ.
                        IValidBD<ТАктВыполненныхРабот> act = new FindActForContract(dc, тДоговор.НомерДоговора.Trim());

                        // Если flagExesAct = true то писать в БД нельзя.
                        bool flagExesAct = act.Validate();

                        // Если акт есть flagExesAct = true значит выводим сообщение о том акт существует и договор записать нельзя.
                        if (flagExesAct == true)
                        {

                            // Выведим сообщение что такой льготник уже существует.
                            FormModalPerson fmp = new FormModalPerson();
                            fmp.NumContract = act.Get()?.НомерАкта ?? "Акт существует но номер не известен";
                            fmp.ShowDialog();

                            // Перейдес к следующей итерации.
                            continue;
                        }

                        // Укажем прошел договор проверку или нет.
                        тДоговор.ФлагПроверки = itm.FlagValidContract;

                        // Льготник.
                        ТЛЬготник тЛЬготник = itm.Packecge.льготник;

                        // Установим дату рождения льготника.
                        //тЛЬготник.ДатаРождения = Convert.ToDateTime(itm.DateBirdthPerson).Date;

                        //// Дата выдачи паспорта.
                        //тЛЬготник.ДатаВыдачиПаспорта = Convert.ToDateTime(itm.DatePassword).Date;

                        //// Дата выдачи документа.
                        //тЛЬготник.ДатаВыдачиДокумента = Convert.ToDateTime(itm.DateDoc).Date;

                        // Проверим есть ли договор в реестре.
                        IValidBD<ТДоговор> validBDcontract = new ProjectContract(dc, тДоговор);

                        // Результат проверки можно писать в БД или нельзя.
                        bool flagWriteContract = validBDcontract.Validate();

                        // Проверим есмть ли у льготника договор который прошел проверку но нет акта выполненных работ.


                        // ПРоверим записан ли данный льготник из реестра проектов договров в БД.
                        IValidBD<ТЛЬготник> validBPerson = new PersonWriteDB(dc, тЛЬготник);

                        // Результат есть льготник в БД или его нет.
                        bool flagWritePersonDB = validBPerson.Validate();

                        // Если льготника в БД нет то флаг flagWritePersonDB указывает на разрешение записи в БД.
                        if (flagWritePersonDB == true)
                        {
                            // Запишем договор в БД.
                            dc.ТабЛьгоготник.Add(тЛЬготник);

                            // Сохраним изменения в базе данных.
                            dc.SaveChanges();
                        }

                        // Проверим есть ли данного льготника в БД договор который прощёл проверку,
                        // Но у него нет акта выполненных работ.


                        // Если договора в БД нет.
                        if (flagWriteContract == true)
                        {
                            ТЛЬготник person = validBPerson.Get();

                            // Запишем id договора.
                            тДоговор.id_льготник = validBPerson.Get()?.id_льготник ?? 0;

                            // Установим временные параметры договора от 1 января 1900 года.
                            тДоговор.ДатаДоговора = new DateTime(1900, 1, 1);
                            тДоговор.ДатаАктаВыполненныхРабот = new DateTime(1900, 1, 1);
                            тДоговор.ДатаРеестра = new DateTime(1900, 1, 1);
                            тДоговор.ДатаСчётФактура = new DateTime(1900, 1, 1);
                            тДоговор.датаВозврата = new DateTime(1900, 1, 1);
                            тДоговор.ДатаЗаписиДоговора = DateTime.Now;
                            тДоговор.ДатаПроверки = DateTime.Now;

                            // id поликлинники.
                            тДоговор.id_поликлинника = itm.Packecge.hosp?.id_поликлинника ?? 0;


                            dc.ТДоговор.Add(тДоговор);

                            dc.SaveChanges();

                            // Запишем услуги по договору.
                            foreach (var uslug in itm.Packecge.listUSlug)
                            {
                                uslug.id_договор = тДоговор.id_договор;

                                dc.ТУслугиПоДоговору.Add(uslug);

                                dc.SaveChanges();
                            }

                            //}
                            //else
                            //{
                            //    MessageBox.Show("У льготника есть договор " + validBDcontract.Get()?.НомерДоговора?.Trim() ?? "Сбой проверки договора " + "  прошедший проверку но не имеющий акта","Внимание",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                            //    continue;
                            //}

                        }

                        ////Предложим запись в БД 
                        //FormDalogWriteBD formDialog = new FormDalogWriteBD();
                        //formDialog.ShowDialog();

                        //if (formDialog.DialogResult == System.Windows.Forms.DialogResult.OK)
                        //{
                        // Завершшим транзакцию.
                        scope.Complete();

                        MessageBox.Show("Данные записаны");
                        //}


                        //}
                        //catch (Exception ex)
                        //{
                        //    // Откатим транзакцию.
                        //    scope.Dispose();

                        //    MessageBox.Show("Ошибка в записи - " + ex.Message);
                        //}
                    }
                }

                //// Данные для отчета о наличии договоров у льготников.
                //List<PrintContractsValidate> listDoc = new List<PrintContractsValidate>();

                //List<ValidItemsContract> listContracts = new List<ValidItemsContract>();

                ////Узнаем содержатся ли ещё договора у текущего льготника
                //using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                //{
                //    con.Open();

                //    SqlTransaction transact = con.BeginTransaction();

                //    // Пройдемся по всем договорам и проверим есть ли оплаченные.
                //    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                //    {
                //        // Получим номер догвора.
                //        string numContract = row.Cells["НомерДоговора"].Value.ToString();

                //        //  Для ускорения вставляем старый код.
                //        // узнаем есть ли на сервере такой номер договора который прошёл проверку
                //        string queryNumDogServ = "select count(НомерДоговора) from Договор where LOWER(RTRIM(LTRIM(НомерДоговора))) = LOWER(RTRIM(LTRIM('" + numContract + "'))) and  ФлагПроверки = 'True' ";

                //        //string queryNumDogServ = "select count(НомерДоговора) from Договор where LOWER(RTRIM(LTRIM(НомерДоговора))) = LOWER(RTRIM(LTRIM('" + numContract + "'))) ";

                //        // Таблица с результатами проверки.
                //        DataTable tab = ТаблицаБД.GetTableSQL(queryNumDogServ, "ПроверкаДоговора", con, transact);

                //        // Получим ID контракта.
                //        int idContract = Convert.ToInt32(row.Cells["IdContract"].Value);

                //        //Тест коментарий снять 18.08.2020 года.
                //        if (Convert.ToInt32(tab.Rows[0][0]) != 0)
                //        {
                //            // Выведим сообщение что такой льготник уже существует.
                //            FormModalPerson fmp = new FormModalPerson();
                //            fmp.NumContract = numContract;
                //            fmp.ShowDialog();

                //            if (fmp.DialogResult == System.Windows.Forms.DialogResult.OK)
                //            {
                //                // Получим текущий договор.
                //                var contract = dictionary.Values.Where(w => w.IdContract == idContract).FirstOrDefault();

                //                // Пометим договор как не прошщедший проверку.
                //                contract.flagValidEsrn = false;

                //                //если такой договор уже в БД существует то сразу переходим к другой итерации
                //                continue;
                //            }
                //        }


                //        var person = dictionary.Values.Where(w => w.IdContract == idContract).FirstOrDefault();

                //        if(Convert.ToBoolean(row.Cells["FlagSaveContract"].Value) == false)
                //        {
                //            // Пометим как не прошедший проверку.
                //            person.flagValidEsrn = false;

                //            // Пометим что договор отправлен на доработку.
                //        }


                //        // Проверим есть ли у данного льготника ещё заключенные договора.
                //        ValidContractForPerson validContract = new ValidContractForPerson(person.фамилия, person.имя, person.отчество.Do(x => x, ""), Convert.ToDateTime(person.датаРождения));
                //        //validContract.listContracts = listContracts;
                //        validContract.SetSqlConnection(con);
                //        validContract.SetSqlTransaction(transact);
                //        validContract.SetNumContract(numContract);
                //        PrintContractsValidate договор = validContract.GetContract();

                //        listDoc.Add(договор);

                //    }

                //}

                //if (listDoc.Count > 0)
                //{
                //    WordReport wordPrint = new WordReport(listDoc);

                //    DocPrint docPrint = new DocPrint(wordPrint);
                //    docPrint.Execute();
                //}

                //var предохранитьель = "";

                ////Предложим запись в БД 
                //FormDalogWriteBD formDialog = new FormDalogWriteBD();
                //formDialog.ShowDialog();

                //if (formDialog.DialogResult == System.Windows.Forms.DialogResult.OK)
                //{

                //    // Установим уровни изоляции транзакций.
                //    var option = new System.Transactions.TransactionOptions();
                //    option.IsolationLevel = System.Transactions.IsolationLevel.Serializable;

                //    // Добавим льготника и адрес в БД.
                //    // Внесём данные в таблицу в едино транзакции.
                //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, option))
                //    {
                //        try
                //        {
                //            UnitDate unitDate = new UnitDate();

                //            foreach (var itm in dictionary.Values)
                //            {

                //                var contract = unitDate.ДоговорRepository.SelectContract(itm.IdContract);

                //                // Льготник прошёл проверку.
                //                if (itm.flagValidEsrn == true)
                //                {
                //                    // Файл прошёл проверку.
                //                    contract.ФлагПроверки = true;

                //                    // Флаг ожидания проверку и True так как файл проверен.
                //                    contract.flagОжиданиеПроверки = true;

                //                    // Дата записи договора при проверке.
                //                    contract.ДатаЗаписиДоговора = (DateTime)DateTime.Now.Date;

                //                    // Дата проверки.
                //                    contract.ДатаПроверки = (DateTime)DateTime.Now.Date;

                //                    // кто записал проверку.
                //                    contract.logWrite = MyAplicationIdentity.GetUses();

                //                    // Отправим флаг на доработку.
                //                    contract.ФлагВозвратНаДоработку = false;
                //                }
                //                else
                //                {
                //                    // Установим флаг ожидания провреки в true, так как файл уже проверен.
                //                    contract.flagОжиданиеПроверки = true;

                //                    // Отправим флаг на доработку.
                //                    contract.ФлагВозвратНаДоработку = true;

                //                    // Установим флаг проверки как не прошедший проверку.
                //                    contract.ФлагПроверки = false;

                //                    // Дата записи договора при проверке.
                //                    contract.ДатаЗаписиДоговора = (DateTime)DateTime.Now.Date;

                //                    // Дата проверки.
                //                    contract.ДатаПроверки = (DateTime)DateTime.Now.Date;

                //                    // кто записал проверку.
                //                    contract.logWrite = MyAplicationIdentity.GetUses();
                //                }

                //                // Сохраним изменения в БД.
                //                unitDate.ДоговорRepository.Update(contract);

                //                ProjectRegistrFiles projectRegistrFiles = unitDate.ProjectRegistrFilesRepository.Select(this.IdFileRegistr).OrderByDescending(w=>w.IdProjectRegistr).FirstOrDefault();

                //                if (projectRegistrFiles != null)
                //                {
                //                    // Реестр прошёл проверку.
                //                    projectRegistrFiles.flagValidateRegistr = true;

                //                    // Сохраним изменения в БД.
                //                    unitDate.ProjectRegistrFilesRepository.Update(projectRegistrFiles);
                //                }

                //            }

                //            // Завершим транзакцию.
                //            scope.Complete();

                //            MessageBox.Show("Договора сохранены");
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message);
                //        }
                //    }
                //}

                // Закрыл окно.
                this.Close();
            }
        }

            /// <summary>
            /// Событие УКАЗЫВАЕТ положение флага проверки для реестра.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
            {

                if (dataGridView1.CurrentCell.ColumnIndex.Equals(7) && e.RowIndex != -1)
                {
                    // Проверим какой флаг установлен.
                    bool flagValidEsrn = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["FlagValidEsrn"].Value);

                    // Флаг наличия проверки медицинских услуг.
                    bool flagValidServices = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["FlagValidServices"].Value);

                    if (flagValidEsrn == false || flagValidServices == false)
                    {
                        MessageBox.Show("Договор не прошел проверку");

                        return;
                    }

                }

            }

            public void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                DataGridViewRow rows = this.dataGridView1.Rows[e.RowIndex];

                //получим id договора.
                //this.ПроектыДоговоров;
                int idContract = Convert.ToInt32(rows.Cells["IdContract"].Value.Do(x => x, 0));


                FormInfoЛьготник formInfoЛьготник = new FormInfoЛьготник(idContract);

                // Передадим в форму список проектов договоров.
                formInfoЛьготник.Contracts = this.listProjectContrats.ToList();
                formInfoЛьготник.Show();



            }
        }
}
