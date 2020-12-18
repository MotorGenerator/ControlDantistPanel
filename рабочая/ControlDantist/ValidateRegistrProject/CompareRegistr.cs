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
                            if(itm.FlagValidateEsrn == false)
                            // Пометим договор как прошедший проверку.
                            itm.FlagValidateEsrn = true;
                        }
                    }
                }
            }
        }
    }
}
