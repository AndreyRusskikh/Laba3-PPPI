using Newtonsoft.Json;
using System.Security;
using System.Text;
using System.Text.Json;


namespace Laba3
{
    public interface IDateAndCopy
    {
        object DeepCopy();
        DateTime Date { get; set; }
    }

    public class Licenses
    {

        public string OrganisationName { get; set; }

        public string Cvalification { get; set; }

        public DateTime DataSert { get; set; }

        public Licenses(string name, string cvalif, DateTime datasert)
        {
            OrganisationName = name;
            Cvalification = cvalif;
            DataSert = datasert;
        }

        public Licenses()
        {
            OrganisationName = "Default Task";
            Cvalification = "Expert";
            DataSert = new DateTime(2000, 1, 1);
        }

        public override string ToString()
        {
            return $"{OrganisationName} {Cvalification} {DataSert}";
        }
        public object DeepCopy()
        {
            Licenses copy = new Licenses();
            copy.OrganisationName = this.OrganisationName;
            copy.Cvalification = this.Cvalification;
            copy.DataSert = this.DataSert;
            return copy;
        }

    }


    class Program
    {
        static void Main()

        {

            Console.OutputEncoding = Encoding.UTF8;
            Person person5 = new("Eliot", "Spenser", new DateTime(1985, 4, 12));
            StationWorker worker = new StationWorker(person5, "Ingener", Rank.First, 2);
            Licenses[] licenses = new Licenses[]
          {
                new Licenses("Licens1", "Ingener", new DateTime(1990, 3, 15)),
                new Licenses("Licens2", "Tehnic", new DateTime(1995, 11, 12)),
                new Licenses("Licens3", "Master", new DateTime(1997, 5, 20)),
                new Licenses("Licens4", "Margarita", new DateTime(1999, 8, 18))
          };
            List<Client> clients = new List<Client>()
            {
                new Client(new Person(), "Break motor", new DateTime(2015, 5, 21)) {},
                new Client(person5, "Break motor", new DateTime(2019, 1, 27))

            };

            Console.WriteLine("=================== Пункт 1 - Объект и копия ====================");

            worker.AddLicense(licenses);
           // worker.AddCliens(clients);
            Console.WriteLine("Оригинальный объект:");
            Console.WriteLine(worker.ToString());
            Console.WriteLine("Копия:");
            StationWorker workerCopy = worker.DeepCopy();
            Console.WriteLine(workerCopy.ToString());

            Console.WriteLine("=================== Пункты 2,3 - Выгрузка из файла ====================");

            try
            {
                Console.WriteLine("Введите имя файла:");
                string fileName = Console.ReadLine();

                if (!File.Exists(fileName))
                {
                    using (File.Create(fileName))
                    {
                        Console.WriteLine("Файл был создан.");
                    }
                }
                else
                {
                    worker.Load(fileName);
                    Console.WriteLine("После инициализации из файла");
                    Console.WriteLine(worker.ToString());
                }
                Console.WriteLine("=================== Пункт 4 - Ввод из консоли и сохранение ====================");

                worker.AddFromConsole();
                worker.Save(fileName);
                Console.WriteLine(worker.ToString());

                Console.WriteLine("=================== Пункт 5 - Загрузка из сохранения, изменение, сохранение ====================");

                StationWorker.LoadStat(fileName, worker);
                Console.WriteLine("Объект после загрузки:");
                Console.WriteLine(worker.ToString());
                worker.AddFromConsole();
                StationWorker.SaveStat(fileName, worker);
                Console.WriteLine("Объект после добавления:");
                Console.WriteLine(worker.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Нет доступа к файлу: " + ex.Message);
            }
            catch (SecurityException ex)
            {
                Console.WriteLine("Произошла ошибка безопасности: " + ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Ошибка формата данных: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Файл не найден: " + ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена: " + ex.Message);
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine("Слишком длинный путь: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

        }
    }
}