using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControlDantist.DataBaseContext;
using System.Collections.Generic;
using ControlDantist.ValidateRegistrProject;
using ControlDantist.ExceptionClassess;
using ControlDantist.Classes;
using System;


namespace UnitTestControlDantistPanel
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Тестируем получение льготной категории если льготная категория не установлена.
        /// </summary>
        [TestMethod]
        public void TestValidatePrefCategoryList2GetPreferentCategoryNull()
        {
            // arrange.
            List<ItemLibrary> list = new List<ItemLibrary>();

            //ТЛьготнаяКатегория тЛьготнаяКатегория = new ТЛьготнаяКатегория();
            //тЛьготнаяКатегория.ЛьготнаяКатегория = "";


            ItemLibrary itemLibrary = new ItemLibrary();

            //PackageClass package = new PackageClass();
            //package.тЛьготнаяКатегория = тЛьготнаяКатегория;

            PackageClass package = new PackageClass();

            itemLibrary.Packecge = package;

            list.Add(itemLibrary);

            // act.
            ValidatePrefCategoryList2 validatePrefCategoryList2 = new ValidatePrefCategoryList2(list);
            var asd = validatePrefCategoryList2.GetPreferentCategory();

            // result
            Assert.AreEqual(asd, "Категория_не_установлена");
        }

        [TestMethod]
        public void TestValidatePrefCategoryList2GetPreferentCategoryВетеран_Труда()
        {
            // arrange.
            List<ItemLibrary> list = new List<ItemLibrary>();

            ТЛьготнаяКатегория тЛьготнаяКатегория = new ТЛьготнаяКатегория();
            тЛьготнаяКатегория.ЛьготнаяКатегория = "Ветеран труда";

            PackageClass package = new PackageClass();
            package.тЛьготнаяКатегория = тЛьготнаяКатегория;

            ItemLibrary itemLibrary = new ItemLibrary();

            itemLibrary.Packecge = package;

            list.Add(itemLibrary);

            // act.
            ValidatePrefCategoryList2 validatePrefCategoryList2 = new ValidatePrefCategoryList2(list);
            var asd = validatePrefCategoryList2.GetPreferentCategory();

            // result
            Assert.AreEqual(asd, "Ветеран труда");

        }

        [TestMethod]
        public void TestValidErrorPerson()
        {
            // arrange.
            List<ItemLibrary> list = new List<ItemLibrary>();


            ItemLibrary itemLibrary = new ItemLibrary();

            ТЛЬготник person = new ТЛЬготник();

            person.Фамилия = "Иванов";
            person.Имя = "Иван";

            string s = person.Отчество.Do(w => w, "");

            DateTime dr = new DateTime(1957, 1, 10);
            person.ДатаРождения = dr;

            DateTime dateDoc = new DateTime(2000, 1, 10);
            person.ДатаВыдачиДокумента = dateDoc;

            DateTime datePassword = new DateTime(2002, 3, 15);
            person.ДатаВыдачиПаспорта = dateDoc;

            // act.
            ValidErrorPerson ver = new ValidErrorPerson(person);


            Assert.IsTrue(ver.Validate());
         
        }

    }
}
