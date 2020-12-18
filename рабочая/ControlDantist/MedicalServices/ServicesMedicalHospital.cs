using ControlDantist.DataBaseContext;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;

namespace ControlDantist.MedicalServices
{
    public class ServicesMedicalHospital : IServices<КлассификаторУслуг>
    {
        private DContext dc;

        private int idHospital { get; set; }

        public ServicesMedicalHospital(DContext dc)
        {
            this.dc = dc;
        }

        /// <summary>
        /// Возвращает список услуг поликлинники.
        /// </summary>
        /// <returns></returns>
        public List<КлассификаторУслуг> ServicesMedical()
        {
            List<КлассификаторУслуг> listServices = new List<КлассификаторУслуг>();

            var list = dc.КлассификаторУслугs?.Where(w => w.id_поликлинника == this.idHospital)?.AsQueryable()?? null;

            if (list != null)
                listServices.AddRange(list);

            return listServices;
        }

        /// <summary>
        /// Установим id госпиталя.
        /// </summary>
        /// <param name="id"></param>
        public void SetIdentificator(int id)
        {
            this.idHospital = id;
        }
    }
}
