using System;

namespace WpfApp1
{
    [Serializable]
    public class MedCard
    {
        public Guid Id { get; set; } // Уникальный идентификатор
        public Person Person { get; set; }
        private DateTime startDate; //дата начала лечения
        private DateTime endDate; //дата окончания лечения
        private string anamnesis; //текст с анамнезом

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (value < DateTime.Now) startDate = value;
                else throw new Exception("Некорректное значение даты начала лечения.");
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (value <= DateTime.Now) endDate = value;
                else throw new Exception("Некорректное значение даты окончания болезни.");
            }
        }

        public string Anamnesis
        {
            get { return anamnesis; }
            set { if (value != String.Empty) anamnesis = value; }
        }

        public MedCard(Person person, DateTime startDate, DateTime endDate, string anamnesis)
        {
            Id = Guid.NewGuid(); // Генерируем уникальный идентификатор
            this.Person = person ?? throw new ArgumentNullException(nameof(person), "Без пациента не может быть медицинской карты");
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Anamnesis = anamnesis;
        }

        public MedCard() { Id = Guid.NewGuid(); } // Генерируем уникальный идентификатор по умолчанию

        public override bool Equals(object obj)
        {
            if (obj is MedCard other)
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