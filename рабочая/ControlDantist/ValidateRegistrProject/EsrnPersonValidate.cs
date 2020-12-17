using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControlDantist.Classes;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateRegistrProject
{
    public class EsrnPersonValidate
    {
        private bool flagError = false;

        private List<ItemLibrary> list;
        public EsrnPersonValidate(List<ItemLibrary> list)
        {
            if (list != null && list.Count > 0)
            {
                this.list = list;
            }
            else
            {
                throw new System.Exception("Отсутствуют данные для проверки по ЭСРН - EsrnPersonValidate");
            }
        }

        public void Validate()
        {
            // Строки подключения к БД АИС ЭСРН.
            ConfigLibrary.Config config = new ConfigLibrary.Config();

            //Пока закоментирован.
            //Получаем словарь со строками подключения к АИС ЭСРН.
            Dictionary<string, string> pullConnect = config.Select();
            
            // Пройдемся по строкам подключения.
            foreach (KeyValuePair<string, string> dStringConnect in pullConnect)
            {
                // Переменная хранит строку подключения к БД.
                string sConnection = string.Empty;
                sConnection = dStringConnect.Value.ToString().Trim();


                bool isConnected = false;

                //Выполним проверку в единой транзакции для конкретного района (строки подключения)
                using (SqlConnection con = new SqlConnection(sConnection))
                {
                    try
                    {
                        // ПОдключемся к БД.
                        con.Open();
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("Сервер в районе " + dStringConnect.Key + " в настоящий момент не доступен.");
                        continue;
                    }

                    // Переменная для хранения льготной категории.
                    string cateroryPerson = string.Empty;

                    // Проверим льготника по ФИО и номеру документа.
                    IEsrnValidate validatePrefCategoryList2 = new ValidatePrefCategoryList2(this.list);

                    IBuilderQueryValidate queryFindPersonToFioNumDoc = new BuilderQueryValidator(validatePrefCategoryList2);

                    // Переменная дя хранения строки запроса.
                    string queryФИО = string.Empty;

                    queryФИО = queryFindPersonToFioNumDoc.Query();

                    SqlTransaction sqlTransaction = con.BeginTransaction();

                    // Получим льготников найденных в ЭСРН по документам дающим право на получение льгот.
                    DataTable tabФИО = ТаблицаБД.GetTableSQL(queryФИО, "ФИО", con, sqlTransaction);

                    // Генерируем запрос на поиск льготников в ЭСРН по Фио и номеру паспората.

                    //validatePrefCategoryList2.FindPersonsFioDoc()
                }

            }
        }

        
    }
}
