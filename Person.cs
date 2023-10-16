
namespace Laba3
{
    public class Person : IDateAndCopy

    {
        protected string name;
        protected string lastName;
        protected DateTime dateOfBirth;
        public DateTime Date { get; set; } = DateTime.Today;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public int YearOfBirth
        {
            get { return dateOfBirth.Year; }
            set { dateOfBirth = new DateTime(value, dateOfBirth.Month, dateOfBirth.Day); }
        }

        public Person(string name, string lastName, DateTime dateOfBirth)
        {
            this.name = name;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
        }

        public Person()
        {
            name = "John";
            lastName = "Doe";
            dateOfBirth = new DateTime(2000, 1, 1);
        }

        public override string ToString()
        {
            return $"{Name} {LastName} {DateOfBirth.ToShortDateString()}";
        }

        public virtual string ToShortString()
        {
            return $"{Name} {LastName}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Person other = (Person)obj;
            return this.Name == other.Name && this.lastName == other.lastName && this.dateOfBirth == other.dateOfBirth;
        }

        public static bool operator ==(Person p1, Person p2)
        {
            if (ReferenceEquals(p1, p2))
            {
                return true;
            }

            if (p1 is null || p2 is null)
            {
                return false;
            }

            return p1.Equals(p2);
        }

        public static bool operator !=(Person p1, Person p2)
        {
            return !(p1 == p2);
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Name.GetHashCode();
            hash = hash * 31 + LastName.GetHashCode();
            hash = hash * 31 + DateOfBirth.GetHashCode();
            return hash;
        }

        public virtual object DeepCopy()
        {
            Person copy = new Person();
            copy.Name = this.Name;
            copy.LastName = this.LastName;
            copy.DateOfBirth = this.DateOfBirth;
            return copy;
        }

    }
}
