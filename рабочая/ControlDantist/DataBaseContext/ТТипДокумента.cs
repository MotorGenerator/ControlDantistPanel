using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    /// <summary>
    /// Сущьность описывающая тип документа.
    /// </summary>
    public class ТТипДокумента
    {
        [Key]
        public int id_документ { get; set; }

        [MaxLength(255)]
        public string НаименованиеТипаДокумента { get; set; }
    }
}
