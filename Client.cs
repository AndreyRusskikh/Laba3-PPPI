namespace Laba3
{
    public class Client : Person
    {
        public string typemist { get; set; } // Вид несправності
        public DateTime dateSto { get; set; } // Дата доставлення авто до СТО
        public Client(Person per, string typeMist, DateTime dateSto)
          : base(per.Name, per.LastName, per.DateOfBirth)
        {
            typemist = typeMist;
            this.dateSto = dateSto;
        }
        public Client()
        {
            typemist = "No petrol";
            dateSto = DateTime.Today;
        }
        public override string ToString()
        {
            return $"Вид несправності: {typemist} \n Дата доставлення авто до СТО: {dateSto}";
        }
        public override object DeepCopy()
        {
            return new Client(new Person(Name, LastName, DateOfBirth), typemist, dateSto);
        }

    }
    public enum Rank
    {
        First,
        Second,
        High
    }

}
