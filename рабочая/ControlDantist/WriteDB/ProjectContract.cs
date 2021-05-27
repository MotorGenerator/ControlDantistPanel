using ControlDantist.DataBaseContext;
using System.Linq;
using System;

namespace ControlDantist.WriteDB
{
    public class ProjectContract : IValidBD<ТДоговор>
    {
        private ТДоговор договор;

        private bool flafValide = false;

        private DContext dc;

        //private delegate bool ValidateContract(ТДоговор тДоговор);

        public ProjectContract(DContext dc,ТДоговор contract)
        {
            if (contract != null)
            {
                договор = contract;
            }
            else
            {
                throw new NullReferenceException("Отсутствует договор в рееестре");
            }

            this.dc = dc;
        }

        public bool Validate()
        {
            // Делегат поиск договора по номеру при условии что он или прошёл проверк у или анулирован.
            bool flagWriteDB = false;

            // Поиск договора.
            var contract = this.dc.ТДоговор.Where(w => w.НомерДоговора.Trim() == this.договор.НомерДоговора.Trim() && (w.ФлагПроверки == true || w.ФлагАнулирован == true)).OrderByDescending(w => w.id_договор).FirstOrDefault();//.//.//.FirstOrDefault();
            //var contract = this.dc.ТДоговор.Where(w => w.НомерДоговора.Trim() == this.договор.НомерДоговора.Trim() && (w.ФлагПроверки == true)).OrderByDescending(w => w.id_договор).FirstOrDefault();//.//.//.FirstOrDefault();

            // Ели договор найден значит писать в БД нельзя.
            if (contract != null)
            {
                  flagWriteDB = false;

                  this.договор = contract;
            }
            else
            {
                // Разрешим запись договора в БД.
                flagWriteDB = true;
            }

            return flagWriteDB;
        }

        ///// <summary>
        ///// Проврека договора.
        ///// </summary>
        ///// <param name="predicate">Предикат</param>
        ///// <returns>false - в БД писать можно, true - писать в БД нельзя</returns>
        //public bool Validate(Func<ТДоговор, bool> predicate)
        //{
        //    if (predicate(договор) == true)
        //    {
        //        flafValide = false;
        //    }
        //    else
        //    {
        //        flafValide = true;
        //    }

        //    return flafValide;
        //}

        public ТДоговор Get()
        {
            ТДоговор tContract;

            if(flafValide == true)
            {
                tContract = договор;
            }
            else
            {
                tContract = null;
            }

            return tContract;
        }
    }
}
