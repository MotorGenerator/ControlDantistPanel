using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.WriteDB
{
    /// <summary>
    /// Проверяет есть ли у льготника еще договорпрошедший проверку но не закрытый актом выполненнных работ
    /// </summary>
    public class ValidContractForPerson : IValidBD<ТДоговор>
    {
        ТЛЬготник тЛЬготник;

        public ValidContractForPerson(ТЛЬготник тЛЬготник)
        {
            this.тЛЬготник = тЛЬготник ?? throw new ArgumentNullException(nameof(тЛЬготник));
        }

        public ТДоговор Get()
        {
            throw new NotImplementedException();
        }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
