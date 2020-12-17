using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControlDantist.DataBaseContext;
using System.Collections.Generic;
using ControlDantist.ValidateRegistrProject;


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
        public void TestТЛьготник()
        {
            // arrange.
            ТЛЬготник person = new ТЛЬготник();

            // ac
            var _фамилия = person.Фамилия?:



            var _фамилияТест = _фамилия;

            // result.
            Assert.IsNull(person.Фамилия);
        }

    }
}
