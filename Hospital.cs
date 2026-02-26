using WpfApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace WpfApp1
{
    [Serializable]
    public class Hospital
    {
        [XmlArray("peoples")]
        [XmlArrayItem("Person", typeof(Person))]
        [XmlArrayItem("Doctor", typeof(Doctor))]
        [XmlArrayItem("MedCard", typeof(MedCard))]
        public List<object> Peoples { get; set; } = new List<object>();

        public Hospital() { }

        public void Add(object item)
        {
            Peoples.Add(item);
        }

        public void Remove(Person person)
        {
            if (Peoples.Contains(person))
            {
                Peoples.Remove(person);
            }
            else
            {
                throw new ArgumentException("Пациент не найден в списке.");
            }
        }

        public void Remove(Doctor doctor)
        {
            if (Peoples.Contains(doctor))
            {
                Peoples.Remove(doctor);
            }
            else
            {
                throw new ArgumentException("Врач не найден в списке.");
            }
        }

        public void Remove(MedCard medCard)
        {
            if (Peoples.Contains(medCard))
            {
                Peoples.Remove(medCard);
            }
            else
            {
                throw new ArgumentException("Медицинская карта не найдена в списке.");
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Peoples.Count)
            {
                Peoples.RemoveAt(index);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Индекс выходит за пределы списка.");
            }
        }

        public void Update(Person updatedItem)
        {
            // Ищем объект в списке по уникальному идентификатору
            var existingItem = Peoples.OfType<Person>().FirstOrDefault(p => p.Id == updatedItem.Id);

            if (existingItem != null)
            {
                // Находим индекс существующего объекта
                int index = Peoples.IndexOf(existingItem);

                // Обновляем объект в списке
                Peoples[index] = updatedItem;
            }
            else
            {
                throw new ArgumentException("Пациент не найден в списке.");
            }
        }

        public void Update(Doctor updatedItem)
        {
            var existingItem = Peoples.OfType<Doctor>().FirstOrDefault(d => d.Id == updatedItem.Id);
            if (existingItem != null)
            {
                int index = Peoples.IndexOf(existingItem);
                Peoples[index] = updatedItem;
            }
            else
            {
                throw new ArgumentException("Доктор не найден в списке.");
            }
        }

        public void Update(MedCard updatedItem)
        {
            var existingItem = Peoples.OfType<MedCard>().FirstOrDefault(m => m.Id == updatedItem.Id);
            if (existingItem != null)
            {
                int index = Peoples.IndexOf(existingItem);
                Peoples[index] = updatedItem;
            }
            else
            {
                throw new ArgumentException("Медицинская карта не найдена в списке.");
            }
        }

        public List<Doctor> GetDoctors()
        {
            return Peoples.OfType<Doctor>().ToList();
        }

        public List<Person> GetPatients()
        {
            return Peoples.OfType<Person>().ToList();
        }

        public List<MedCard> GetMedicalCards()
        {
            return Peoples.OfType<MedCard>().ToList();
        }
    }
}