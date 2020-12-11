using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public class ItemLibrary
    {
        /// <summary>
        /// Номер кдоговора.
        /// </summary>
        public string NumContract { get; set; }

        /// <summary>
        /// Пакет данных к договору.
        /// </summary>
        public PackageClass Packecge { get; set; }

    }
}
