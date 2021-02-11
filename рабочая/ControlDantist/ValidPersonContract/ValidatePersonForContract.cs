﻿using System;
using ControlDantist.Classes;

namespace ControlDantist.ValidPersonContract
{
    public class ValidatePersonForContract : IValidatePersonContract
    {
        private string famili = string.Empty;
        private string name = string.Empty;
        private string secondName = string.Empty;
        private DateTime dateBirth;
        //private string query = string.Empty;

        public ValidatePersonForContract(string famili, string name, string secondName, DateTime dateBirth)//, string query)
        {
            this.famili = famili ?? throw new ArgumentNullException(nameof(famili));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));
            this.dateBirth = dateBirth;
            //this.query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string Execute()
        {
            return @"declare @FistName nvarchar(100)
                            declare @Name nvarchar(100)
                            declare @Surname nvarchar(100)
                            declare @DR date
                            set @FistName = '" + this.famili + "' " +
                            " set @Name = '"+ name +"' " +
                            " set @Surname = '"+ this.secondName +"' " +
                            "set @DR = '"+ Время.Дата(this.dateBirth.ToShortDateString()) + "' " +
                            @" select НомерДоговора, Договор.ДатаАктаВыполненныхРабот from Льготник
                            inner join Договор
                            on Льготник.id_льготник = Договор.id_льготник
                            --inner join АктВыполненныхРабот
                            --on АктВыполненныхРабот.id_договор = Договор.id_договор
                             where Договор.ФлагПроверки = 1 and LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name))) 
                            and((LOWER(LTRIM(RTRIM(Отчество))) = LOWER(LTRIM(RTRIM(@Surname)))) or Отчество is null) and ДатаРождения = @DR
                            union
                            select НомерДоговора,ДоговорAdd.ДатаДоговора from ЛьготникAdd
                            inner join ДоговорAdd
                            on ЛьготникAdd.id_льготник = ДоговорAdd.id_льготник
                            --inner join АктВыполненныхРабот
                            --on АктВыполненныхРабот.id_договор = ДоговорAdd.id_договор
                             where ДоговорAdd.ФлагПроверки = 1 and LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) 
                            and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name))) 
                            and((LOWER(LTRIM(RTRIM(Отчество))) = LOWER(LTRIM(RTRIM(@Surname)))) or Отчество is null) and ДатаРождения = @DR";

        }
    }
}
