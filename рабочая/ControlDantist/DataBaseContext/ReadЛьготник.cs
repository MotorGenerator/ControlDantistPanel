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




            //personFull.Фамилия = rw_Льготник["Фамилия"].ToString().Trim();
            //personFull.Имя = rw_Льготник["Имя"].ToString().Trim();
            //personFull.Отчество = rw_Льготник["Отчество"].ToString().Trim();
            ////personFull.DateBirtch = " Convert(datetime,'" + Время.Дата(Convert.ToDateTime(rw_Льготник["ДатаРождения"]).ToShortDateString().Trim()) + "',112)  ";
            //personFull.ДатаРождения = Convert.ToDateTime(rw_Льготник["ДатаРождения"]);
            //personFull.улица = rw_Льготник["улица"].ToString().Trim();
            //personFull.НомерДома = rw_Льготник["НомерДома"].ToString().Trim();
            //personFull.корпус = rw_Льготник["корпус"].ToString().Trim();
            //personFull.НомерКвартиры = rw_Льготник["НомерКвартиры"].ToString().Trim();
            //personFull.СерияПаспорта = rw_Льготник["СерияПаспорта"].ToString().Trim();
            //personFull.НомерПаспорта = rw_Льготник["НомерПаспорта"].ToString().Trim();
            //personFull.ДатаВыдачиПаспорта = Convert.ToDateTime(rw_Льготник["ДатаВыдачиПаспорта"]);
            //personFull.КемВыданПаспорт = rw_Льготник["КемВыданПаспорт"].ToString().Trim();
            //personFull.id_льготнойКатегории = льготнаяКатегория.id_льготнойКатегории;
            //personFull.id_документ = (int)unload.ТипДокумента.Rows[0][0];//                      ",@idДокумент_" + iCount + " " +
            //personFull.СерияДокумента = rw_Льготник["СерияДокумента"].ToString().Trim();
            //personFull.НомерДокумента = rw_Льготник["НомерДокумента"].ToString().Trim();
            //personFull.ДатаВыдачиДокумента = Convert.ToDateTime(rw_Льготник["ДатаВыдачиДокумента"]);
            //personFull.КемВыданДокумент = rw_Льготник["КемВыданДокумент"].ToString().Trim();
            //personFull.id_область = 1;//id области у нас по умолчанию 
            //personFull.id_район = Convert.ToInt16(rw_Льготник["id_район"]);

            //// Запишем id населенного пункта.
            //var findSity = unitDate.НаселенныйПунктRepository.FiltrНаселенныйПункт(sity.NameTown);

            //if (findSity != null)
            //{
            //    personFull.id_насПункт = findSity.id_насПункт;
            //}
            //else
            //{
            //    НаселённыйПункт населённыйПункт = new НаселённыйПункт();
            //    населённыйПункт.Наименование = sity.NameTown;

            //    // Запишем по новой населенный пункт.
            //    unitDate.НаселенныйПунктRepository.Insert(населённыйПункт);

            //    personFull.id_насПункт = населённыйПункт.id_насПункт;
            //}

            //// Запишем льготника в таблицу.
            //unitDate.ЛьготникRepository.Insert(personFull);

            return personFull;
        }
    }
}
