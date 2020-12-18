using ControlDantist.DataBaseContext;
using System.Collections.Generic;

namespace ControlDantist.MedicalServices
{
    /// <summary>
    /// 
    /// </summary>
    public class ReestrContract
    {
        private List<ItemLibrary> packegeDateContract;
        //private ServicesMedialContract servisMedicalContr;

        private IServices<ТУслугиПоДоговору> servisMedicalContr;


        public ReestrContract(List<ItemLibrary> packegeDateContract)
        {
            this.packegeDateContract = packegeDateContract;

            servisMedicalContr = new ServicesMedialContract(packegeDateContract);
        }

        /// <summary>
        /// Возвращает id поликлинники.
        /// </summary>
        /// <returns></returns>
        public int IdHospital()
        {
            int idHospital = 0;

            if (this.packegeDateContract != null && this.packegeDateContract[0].Packecge != null && this.packegeDateContract[0].Packecge.hosp != null)
            {
                idHospital = this.packegeDateContract[0].Packecge.hosp.id_поликлинника;
            }

            return idHospital;
        }

        /// <summary>
        /// Возвращает список услуг по договору.
        /// </summary>
        /// <param name="idContract"></param>
        /// <returns></returns>
        public List<ТУслугиПоДоговору> GetServicesContract(int idContract)
        {
            // Установим id контракта.
            servisMedicalContr.SetIdentificator(idContract);

            return servisMedicalContr.ServicesMedical();
        }
    }
}
