using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace ControlDantist.DataBaseContext
{
    public class DContext : DbContext
    {
        public DContext(string connectionString) : base(connectionString)
        {
            // Запретим генерировать новую БД, подключемся к существующей.
            Database.SetInitializer<DContext>(null);
        }

        /// <summary>
        /// Таблица населенный пункт.
        /// </summary>
        public DbSet<ТНаселённыйПункт> ТабНаселенныйПункт { get; set; }

        /// <summary>
        /// Таблица льготная категория.
        /// </summary>
        public DbSet<ТЛьготнаяКатегория> ТабЛьготнаяКатегория { get; set; }
    }
}
