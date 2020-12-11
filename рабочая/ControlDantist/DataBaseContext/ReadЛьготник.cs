using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.ReadRegistrProject;
using System.Data;

namespace ControlDantist.DataBaseContext
{
    public class ReadЛьготник : IReadRegistr<ТЛЬготник>
    {
        private DContext dc;
        private DataRow rPers;

        public ReadЛьготник(DContext dc, DataRow rPers)
        {
            this.dc = dc;
            this.rPers = rPers;
        }

        public ТЛЬготник Get()
        {
            ТЛЬготник personFull = new ТЛЬготник();

            personFull.Фамилия = this.rPers.FieldOfDefault<string>("Фамилия");
            personFull.Имя = this.rPers.FieldOfDefault<string>("Имя");
            personFull.Отчество = this.rPers.FieldOfDefault<string>("Отчество");
            personFull.ДатаРождения = this.rPers.FieldOfDefault<DateTime>("ДатаРождения");
            personFull.улица = this.rPers.FieldOfDefault<string>("улица");
            personFull.НомерДома = this.rPers.FieldOfDefault<string>("НомерДома");
            personFull.корпус = this.rPers.FieldOfDefault<string>("корпус");
            personFull.НомерКвартиры = this.rPers.FieldOfDefault<string>("НомерКвартиры");
            personFull.СерияПаспорта = this.rPers.FieldOfDefault<string>("СерияПаспорта");
            personFull.НомерПаспорта = this.rPers.FieldOfDefault<string>("НомерПаспорта");
            personFull.ДатаВыдачиПаспорта = this.rPers.FieldOfDefault<DateTime>("ДатаВыдачиПаспорта");
            personFull.КемВыданПаспорт = this.rPers.FieldOfDefault<string>("КемВыданПаспорт");
            personFull.id_льготнойКатегории = this.rPers.FieldOfDefault<int>("id_льготнойКатегории");
            personFull.id_документ = this.rPers.FieldOfDefault<int>("id_документ");
            personFull.СерияДокумента = this.rPers.FieldOfDefault<string>("СерияДокумента");
            personFull.НомерДокумента = this.rPers.FieldOfDefault<string>("НомерДокумента");
            personFull.ДатаВыдачиДокумента = this.rPers.FieldOfDefault<DateTime>("ДатаВыдачиДокумента");
            personFull.КемВыданДокумент = this.rPers.FieldOfDefault<string>("КемВыданДокумент");
            // Так как мы работаем только с Саратовской областью то id области не меняется.
            personFull.id_область = 1;
            personFull.id_район = this.rPers.FieldOfDefault<int>("id_район");

            //// Запишем льготника в таблицу.
            //unitDate.ЛьготникRepository.Insert(personFull);

            return personFull;
        }
    }
}
