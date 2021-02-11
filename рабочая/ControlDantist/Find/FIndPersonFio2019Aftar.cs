﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Find
{
    public class FIndPersonFio2019Aftar : IFindPerson
    {
        private string FirstName = string.Empty;
        private string Name = string.Empty;

        public FIndPersonFio2019Aftar(string firstName, string name)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public virtual string Query()
        {
            //string query = @"declare @FirstName nvarchar(150)
            //                    declare @Name nvarchar(150)
            //                    declare @Surname nvarchar(150)
            //                    declare @DateBirth date
            //                    declare @NumContract nchar(100)
            //           set @FirstName = '" + this.FirstName + "' " +
            //                    " set @Name = '"+ this.Name +"' " +
            //                    @" select Договор.id_договор,НомерДоговора,Льготник.Фамилия,Льготник.Имя,Льготник.Отчество,ЛьготнаяКатегория.ЛьготнаяКатегория,
            //           SUM(УслугиПоДоговору.Сумма) as Сумма,Договор.ДатаЗаписиДоговора,Договор.НомерРеестра,Договор.НомерСчётФактрура
            //           ,АктВыполненныхРабот.ДатаПодписания,Договор.logWrite,НомерАкта,Договор.flag2019AddWrite, 2020 as 'Год',flagАнулирован from dbo.Договор
            //                  inner join Льготник
            //                    ON Договор.id_льготник = Льготник.id_льготник
            //                    inner join ЛьготнаяКатегория
            //                    ON Договор.id_льготнаяКатегория = ЛьготнаяКатегория.id_льготнойКатегории
            //                    inner join УслугиПоДоговору
            //                    ON УслугиПоДоговору.id_договор = Договор.id_договор
            //                    left outer join АктВыполненныхРабот
            //                    ON АктВыполненныхРабот.id_договор = Договор.id_договор
            //                     where[Фамилия] = @FirstName and Имя = @Name
            //                    and Договор.ФлагПроверки = 'True'
            //                    and YEAR(Договор.ДатаЗаписиДоговора) > 2019
            //                    --and(ДатаАктаВыполненныхРабот is not null) and(YEAR(ДатаАктаВыполненныхРабот) <> 1900)
            //                    group by Договор.id_договор,НомерДоговора,Льготник.Фамилия,Льготник.Имя,Льготник.Отчество,ЛьготнаяКатегория.ЛьготнаяКатегория,
            //           Договор.ДатаЗаписиДоговора,Договор.НомерРеестра,Договор.НомерСчётФактрура ,АктВыполненныхРабот.НомерАкта
            //           ,АктВыполненныхРабот.ДатаПодписания,Договор.logWrite,Договор.flag2019AddWrite,flagАнулирован";

            //return query;

            string query = @"declare @FirstName nvarchar(150)
                                declare @Name nvarchar(150)
                                declare @Surname nvarchar(150)
                                declare @DateBirth date
                                declare @NumContract nchar(100) " +
                               " set @FirstName = '" + this.FirstName + "' " +
                               " set @Name = '" + this.Name + "' " +
                               @"select ДоговорАрхив.id_договор,НомерДоговора,ЛьготникАрхив.Фамилия,ЛьготникАрхив.Имя,ЛьготникАрхив.Отчество,ЛьготнаяКатегория.ЛьготнаяКатегория,
			                    SUM(УслугиПоДоговоруАрхив.Сумма) as Сумма,ДоговорАрхив.ДатаЗаписиДоговора,ДоговорАрхив.НомерРеестра,ДоговорАрхив.НомерСчётФактрура
			                    ,АктВыполненныхРабот.ДатаПодписания,ДоговорАрхив.logWrite,НомерАкта,ДоговорАрхив.flag2019AddWrite, 2017 as 'Год',flagАнулирован from dbo.ДоговорАрхив
                              inner join ЛьготникАрхив
                                ON ДоговорАрхив.id_льготник = ЛьготникАрхив.id_льготник
                                inner join ЛьготнаяКатегория
                                ON ДоговорАрхив.id_льготнаяКатегория = ЛьготнаяКатегория.id_льготнойКатегории
                                inner join УслугиПоДоговоруАрхив
                                ON УслугиПоДоговоруАрхив.id_договор = ДоговорАрхив.id_договор
                                left outer join АктВыполненныхРабот
                                ON АктВыполненныхРабот.id_договор = ДоговорАрхив.id_договор
                                 where[Фамилия] = @FirstName and Имя = @Name
                                and ДоговорАрхив.ФлагПроверки = 'True'
                                and YEAR(ДоговорАрхив.ДатаЗаписиДоговора) > 2019
                                --and(ДатаАктаВыполненныхРабот is not null) and(YEAR(ДатаАктаВыполненныхРабот) <> 1900)
                                group by ДоговорАрхив.id_договор,НомерДоговора,ЛьготникАрхив.Фамилия,ЛьготникАрхив.Имя,ЛьготникАрхив.Отчество,ЛьготнаяКатегория.ЛьготнаяКатегория,
			                    ДоговорАрхив.ДатаЗаписиДоговора,ДоговорАрхив.НомерРеестра,ДоговорАрхив.НомерСчётФактрура ,АктВыполненныхРабот.НомерАкта
			                    ,АктВыполненныхРабот.ДатаПодписания,ДоговорАрхив.logWrite,ДоговорАрхив.flag2019AddWrite,flagАнулирован ";

            return query;
        }
    }
}
