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
        public void Compare(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var itm in packegeDateContract)
                    {
                        if (Convert.ToInt32(row["id_договор"]) == itm.Packecge.тДоговор.id_договор)
                        {
                            // Пометим только договора которые не прошли проверку.
                            if (itm.FlagValidateEsrn == false)
                            {
                                // Пометим договор как прошедший проверку.
                                itm.FlagValidateEsrn = true;

                                //if (dataTable.Rows.Contains("A_REGREGIONCODE") == true)
                                //{
                                    // id район области. Хотя скорее всего здесь не понадобиться.
                                    if (row["A_REGREGIONCODE"] != DBNull.Value)
                                    {
                                        itm.Packecge.льготник.id_район = Convert.ToInt32(row["A_REGREGIONCODE"]);
                                    }
                                //}

                                // Снилс.
                                if (row["A_SNILS"] != DBNull.Value)
                                {
                                    itm.Packecge.льготник.Снилс = row["A_SNILS"].ToString().Trim();
                                }

                                // Дата рождения.
                                if (row["дата выдачи"] != DBNull.Value)
                                {
                                    DateTime dr = Convert.ToDateTime(row["дата выдачи"]);

                                    itm.Packecge.льготник.ДатаРождения = dr.Date;
                                }

                                if (row["Адрес"] != DBNull.Value)
                                {
                                    itm.AddressPerson = row["Адрес"].ToString();
                                }    
                            }

                        }
                    }
                }
            }
        }
    }
}
