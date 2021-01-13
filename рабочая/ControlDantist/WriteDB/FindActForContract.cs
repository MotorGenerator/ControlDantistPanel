using ControlDantist.DataBaseContext;
using System.Linq;

namespace ControlDantist.WriteDB
{
    public class FindActForContract : IValidBD<ТАктВыполненныхРабот>
    {
        private ТАктВыполненныхРабот актВыполненныхРабот;
        private DContext dc;

        // Переменная для хранения номера договора.
        private string numContract = string.Empty;

        //public FindActForContract(DContext dc,ТАктВыполненныхРабот актВыполненныхРабот, string numContract)
        public FindActForContract(DContext dc, string numContract)
        {
            //this.актВыполненныхРабот = актВыполненныхРабот;
            this.dc = dc;
            this.numContract = numContract;
        }

        public ТАктВыполненныхРабот Get()
        {
            return this.актВыполненныхРабот;
        }

        public bool Validate()
        {
            // Флаг указывающий что акт выполненных работ для текущего договора существует.
            bool flagValidate = false;

            var acts = from c in dc.ТДоговор.Where(w => w.НомерДоговора.Trim() == this.numContract.Trim())
                       join a in dc.ТАктВыполненныхРабот on c.id_договор equals a.id_договор
                       select new { idAct = a.id_акт, idContract = a.id_договор, NumAct = a.НомерАкта };

            if(acts.Count() > 0)
            {
                flagValidate = true;

                // Создадим экземпляр актк выполненных работ.
                this.актВыполненныхРабот = new ТАктВыполненныхРабот();

                // Получим последний акт.
                var act = acts.OrderByDescending(w => w.idAct).FirstOrDefault();

                // Присвоим номер акта.
                this.актВыполненныхРабот.НомерАкта = act.NumAct;
            }

            return flagValidate;
        }
    }
}
