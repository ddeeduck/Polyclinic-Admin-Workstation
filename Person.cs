using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    [XmlInclude(typeof(Doctor))]
    [XmlInclude(typeof(MedCard))]
    [Serializable]
    public class Person
    {
        private string name; //имя
        private string surname; //фамилия
        private string patronymic; //отчество
        private DateTime dateBirthday; //дата рождения
        private string gender; //пол
        private string address; //адрес
        private string number; //телефон

        // Уникальный идентификатор
        public Guid Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != String.Empty) name = value;
                else throw new Exception("Некорректное значение имени.");
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value != String.Empty) surname = value;
                else throw new Exception("Некорректное значение фамилии.");
            }
        }
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                if (value != String.Empty) patronymic = value;
                else patronymic = "Отсутствует";
            }
        }

        public DateTime DateBirthday
        {
            get { return dateBirthday; }
            set
            {
                if (value < DateTime.Now) dateBirthday = value;
                else throw new Exception("Некорректное значение даты.");
            }
        }
        public string Gender
        {
            get { return gender; }
            set
            {
                if (value != String.Empty) gender = value;
                else throw new Exception("Некорректное значение пола.");
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                if (value != String.Empty) address = value;
                else throw new Exception("Некорректное значение адреса.");
            }
        }
        public string Number
        {
            get { return number; }
            set
            {
                if (value != String.Empty) number = value;
                else throw new Exception("Некорректное значение номера телефона.");
            }
        }

        public Person(string name, string surname, string patronymic, DateTime dateBirthday, string gender, string address, string number)
        {
            Id = Guid.NewGuid(); // Генерируем уникальный идентификатор
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            DateBirthday = dateBirthday;
            Gender = gender;
            Address = address;
            Number = number;
        }

        public Person() { Id = Guid.NewGuid(); } // Генерируем уникальный идентификатор по умолчанию

        public int GetAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateBirthday.Year;
            if (DateBirthday > today.AddYears(-age)) age--;
            return age;
        }

        public string FullName
        {
            get { return $"{Name} {Surname} {Patronymic}"; }
        }

        public override bool Equals(object obj)
        {
            if (obj is Person other)
            {
                return Id == other.Id; // Сравниваем по уникальному идентификатору
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode(); // Используем идентификатор для хеширования
        }
    }
}