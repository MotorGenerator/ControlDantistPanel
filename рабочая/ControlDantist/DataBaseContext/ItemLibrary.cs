﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public class ItemLibrary
    {
        /// <summary>
        /// Номер договора.
        /// </summary>
        public string NumContract { get; set; }

        /// <summary>
        /// Флаг указывающий что договор прошёл проверку.
        /// </summary>
        public bool FlagValidateEsrn { get; set; }

        /// <summary>
        /// Флаг проверки медицинских услуг.
        /// </summary>
        public bool FlagValidateMedicalServices { get; set; }

        /// <summary>
        /// Флаг указывающий что договор прошёл проверку.
        /// </summary>
        public bool FlagValidContract { get; set; }

        /// <summary>
        /// Пакет данных к договору.
        /// </summary>
        public PackageClass Packecge { get; set; }

        /// <summary>
        /// Адрес льготника для отображения в форме отображения результата проверки.
        /// </summary>
        public string AddressPerson { get; set; }

        /// <summary>
        /// Дата рождения льготника.
        /// </summary>
        public string DateBirdthPerson { get; set; }

        /// <summary>
        /// Дата выдачи паспорта.
        /// </summary>
        public string DateDoc { get; set; }

        /// <summary>
        /// Дата выдачи паспорта.
        /// </summary>
        public string DatePassword { get; set; }

    }
}
