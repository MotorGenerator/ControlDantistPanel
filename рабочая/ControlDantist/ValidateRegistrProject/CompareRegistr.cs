using System;
using System.Collections.Generic;
using ControlDantist.DataBaseContext;
using System.Data;


namespace ControlDantist.ValidateRegistrProject
{
    /// <summary>
    /// Сравнение данных из файла реестра проектов договоров с результатом проверки в ЭСРН.
    /// </summary>
    public class CompareRegistr
    {
        // Переменная для хранения реестра проектов договоров.
        private List<ItemLibrary> packegeDateContract;

        public CompareRegistr(List<ItemLibrary> packegeDateContract)
        {
            this.packegeDateContract = packegeDateContract;
        }

        /// <summary>
        /// Сравнение резултата проверки с данными из файла.
        /// </summary>
        /// <param name="dataTable"></param>
        public void Compare(DataTable dataTableFio, DataTable dataTableDoc)
        {
            if (dataTableFio?.Rows?.Count > 0)
            {
                foreach (DataRow row in dataTableFio.Rows)
                {
                    foreach (var itm in packegeDateContract)
                    {
                        if (Convert.ToInt32(row["id_договор"]) == itm.Packecge.тДоговор.id_договор)
                        {
                            foreach(DataRow rowDoc in dataTableDoc.Rows)
                            {
                                if(Convert.ToInt32(rowDoc["id_договор"]) == itm.Packecge.тДоговор.id_договор)
                                {
                                    // Пометим только договора которые не прошли проверку.
                                    if (itm.FlagValidateEsrn == false)
                                    {
                                        // Пометим договор как прошедший проверку.
                                        itm.FlagValidateEsrn = true;

                                         // Запишем адрес льготника найденного по ЭСРН.
                                        if (row["Адрес"] != DBNull.Value)
                                        {
                                            itm.AddressPerson = row["Адрес"].ToString().Trim();
                                        }
                                    }
                                }
                            }

                            //// Пометим только договора которые не прошли проверку.
                            //if (itm.FlagValidateEsrn == false)
                            //{
                            //    // Пометим договор как прошедший проверку.
                            //    itm.FlagValidateEsrn = true;

                            //    //if (row["Имя"] != DBNull.Value)
                            //    //{
                            //    //    val.Имя = row["Имя"].ToString();
                            //    //}

                            //    //if (row["Отчество"] != DBNull.Value)
                            //    //{
                            //    //    val.Отчество = row["Отчество"].ToString();
                            //    //}

                            //    //if (row["Фамилия"] != DBNull.Value)
                            //    //{
                            //    //    val.Фамилия = row["Фамилия"].ToString();
                            //    //}

                            //    ////if (row["A_NAME"] != DBNull.Value)
                            //    ////{
                            //    ////    val.НазваниеДокумента = row["A_NAME"].ToString();
                            //    ////}

                            //    ////if (row["Серия документа"] != DBNull.Value)
                            //    ////{
                            //    ////    val.СерияДокумента = row["Серия документа"].ToString().Trim();
                            //    ////}

                            //    ////if (row["Номер документа"] != DBNull.Value)
                            //    ////{
                            //    ////    val.НомерДокумента = row["Номер документа"].ToString().Trim();
                            //    ////}

                            //    ////if (row["дата выдачи"] != DBNull.Value)
                            //    ////{
                            //    ////    val.ДатаВыдачиДокумента = Convert.ToDateTime(row["дата выдачи"]).ToShortDateString();
                            //    ////}

                            //    ////val.ДатаРождения = датаРождения;

                            //    // Запишем адрес льготника найденного по ЭСРН.
                            //    if (row["Адрес"] != DBNull.Value)
                            //    {
                            //        itm.AddressPerson = row["Адрес"].ToString().Trim();
                            //    }

                            //    //// Снилс льготника прочитанный из ЭСРН.
                            //    //if (row["A_SNILS"] != DBNull.Value)
                            //    //{
                            //    //    vContract.SnilsPerson = row["A_SNILS"].ToString().Trim();
                            //    //}

                            //    //// ID района области где проживает льготник.
                            //    //if (row["A_REGREGIONCODE"] != DBNull.Value)
                            //    //{
                            //    //    vContract.IdRegionEsrn = row["A_REGREGIONCODE"].ToString().Trim();
                            //    //}

                            //}

                        }
                    }
                }
            }
        }
    }
}
