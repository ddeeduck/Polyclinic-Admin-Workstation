using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace WpfApp1
{
    [Serializable]
    public class Doctor : Person
    {
        private string specialization; //специализация
        private int experience; //опыт работы
        private int office; //кабинет

        public string Specialization
        {
            get { return specialization; }
            set
            {
                if (value != string.Empty) specialization = value;
                else throw new Exception("Некорректное значение специализации доктора.");
            }
        }
        public int Experience
        {
            get { return experience; }
            set
            {
                if (value >= 0) experience = value;
                else experience = 0;
            }
        }
        public int Office
        {
            get { return office; }
            set
            {
                if (value > 0) office = value;
                else throw new Exception("Некорректное значение номера кабинета.");
            }
        }

        public Doctor(string name, string surname, string patronymic, DateTime dateBirthday,
            string gender, string address, string number, string specialization, int experience, int office)
            : base(name, surname, patronymic, dateBirthday, gender, address, number)
        {
            this.Specialization = specialization;
            this.Experience = experience;
            this.Office = office;
        }

        public Doctor() { }

        public override bool Equals(object obj)
        {
            if (obj is Doctor other)
            {
                return base.Equals(other) &&
                       Specialization == other.Specialization &&
                       Experience == other.Experience &&
                       Office == other.Office;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Specialization, Experience, Office);
        }
    }
}
