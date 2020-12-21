using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.MedicalServices
{
    /// <summary>
    /// Сарвнивает медицинские услуги в реестре с услугами из БД.
    /// </summary>
    public class ValidateMedicalServices
    {
        private ReestrContract reestrContract;

        private IServices<ТВидУслуг> servisHospital;

        public ValidateMedicalServices(ReestrContract reestrContract, IServices<ТВидУслуг> servisHospital)
        {
            this.reestrContract = reestrContract;
            this.servisHospital = servisHospital;
        }

        /// <summary>
        /// Получает id поликлинники.
        /// </summary>
        /// <param name="servisHospital"></param>
        //public void GetHospital(IServices<КлассификаторУслуг> servisHospital)
        //{
        //    this.servisHospital = servisHospital;
        //    //int idHospital = this.reestrContract.IdHospital();

        //    //servisHospital.ServicesMedical.
        //}

        public void ValidateServices()
        {
            // Получаем id поликлинники.
            int idHospital = this.reestrContract.IdHospital();
            
            // Если id поликлинники найден корректно.
            if(idHospital != 0)
            {
                // Установим id для поликлинники.
                servisHospital.SetIdentificator(idHospital);

                // Получим услуги поликлинники.
                List<ТВидУслуг> listKU = servisHospital.ServicesMedical();

                if (listKU.Count > 0)
                {
                    foreach (var item in this.reestrContract.SetRegistServices())
                    {
                        // Получим количество услуг в договоре.
                        int countServices = item.Packecge.listUSlug.Count();

                        // Приведем списки к одному типу.
                        var listServicesHosp = listKU.Select(x => new УслугиПоДоговору { НаименованиеУслуги = x.ВидУслуги, цена = x.Цена }).ToList();

                        var result = from x in item.Packecge.listUSlug
                                     join y in listServicesHosp
                                     on new { X1 = x.НаименованиеУслуги, X2 = x.цена } equals new { X1 = y.НаименованиеУслуги, X2 = y.цена }
                                     select new
                                     {
                                         x.НаименованиеУслуги,
                                         x.Сумма
                                     };

                        // Если услуги в договоре совпали с услугами на сервере.
                        if(result.Count() == countServices)
                        {
                            // Считаем что договор прошёл проверку по медицинским услугам.
                            item.FlagValidateMedicalServices = true;
                        }
                    }
                }

            }

        }
    }
}
