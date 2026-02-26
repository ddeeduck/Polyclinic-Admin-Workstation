using WpfApp1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private Hospital hospital;
        private XmlSerializer xmlSerializer;

        public MainWindow()
        {
            InitializeComponent();
            hospital = new Hospital();
            xmlSerializer = new XmlSerializer(typeof(Hospital));
            LoadData();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadData()
        {
            if (File.Exists("hospital.xml"))
            {
                using (StreamReader dataStream = new StreamReader("hospital.xml"))
                {
                    hospital = (Hospital)xmlSerializer.Deserialize(dataStream);
                }
            }
        }

        private void SaveData()
        {
            using (StreamWriter fileStream = new StreamWriter("hospital.xml"))
            {
                xmlSerializer.Serialize(fileStream, hospital);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            PatientAddBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            PatientDeleteBorder.Visibility = Visibility.Collapsed;
            DoctorDeleteBorder.Visibility = Visibility.Collapsed;
            MedicalCardDeleteBorder.Visibility = Visibility.Collapsed;
            PatientSelectForChangeBorder.Visibility = Visibility.Collapsed;
            DoctorSelectForChangeBorder.Visibility = Visibility.Collapsed;
            MedicalCardSelectForChangeBorder.Visibility = Visibility.Collapsed;
            PatientChangeBorder.Visibility = Visibility.Collapsed;
            DoctorChangeBorder.Visibility = Visibility.Collapsed;
            MedicalCardChangeBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Visible;
        }

        private void PAdd_Click(object sender, RoutedEventArgs e)
        {
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            PatientAddBorder.Visibility = Visibility.Visible;
        }

        private void DAdd_Click(object sender, RoutedEventArgs e)
        {
            PatientAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Visible;
        }

        private void MAdd_Click(object sender, RoutedEventArgs e)
        {
            PatientAddBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Visible;
        }

        private void PCheck_Click(object sender, RoutedEventArgs e)
        {
            PatientAddBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Visible;

            var patients = hospital.GetPatients();
            if (patients != null && patients.Any())
            {
                PatientListView.ItemsSource = patients;
            }
            else
            {
                MessageBox.Show("Данные о пациентах отсутствуют.");
            }
        }

        private void DCheck_Click(object sender, RoutedEventArgs e)
        {
            PatientAddBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Visible;

            var doctors = hospital.GetDoctors();
            if (doctors != null && doctors.Any())
            {
                DoctorListView.ItemsSource = doctors;
            }
            else
            {
                MessageBox.Show("Данные о врачах отсутствуют.");
            }
        }

        private void MCheck_Click(object sender, RoutedEventArgs e)
        {
            PatientAddBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Visible;

            var medCards = hospital.GetMedicalCards();
            if (medCards != null && medCards.Any())
            {
                MedicalCardListView.ItemsSource = medCards;
            }
            else
            {
                MessageBox.Show("Данные о медицинских картах отсутствуют.");
            }
        }

        private void PChange_Click(object sender, RoutedEventArgs e)
        {
            MainBorder.Visibility = Visibility.Collapsed;
            PatientSelectForChangeBorder.Visibility = Visibility.Visible;
            PatientChangeListView.ItemsSource = hospital.GetPatients();
        }

        private void DChange_Click(object sender, RoutedEventArgs e)
        {
            MainBorder.Visibility = Visibility.Collapsed;
            DoctorSelectForChangeBorder.Visibility = Visibility.Visible;
            DoctorChangeListView.ItemsSource = hospital.GetDoctors();
        }

        private void MChange_Click(object sender, RoutedEventArgs e)
        {
            MainBorder.Visibility = Visibility.Collapsed;
            MedicalCardSelectForChangeBorder.Visibility = Visibility.Visible;
            MedicalCardChangeListView.ItemsSource = hospital.Peoples.OfType<MedCard>().ToList();
        }

        private void PatientSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = PatientSearchTextBox.Text.ToLower();
            var filteredPatients = hospital.GetPatients()
                .Where(p => p.FullName.ToLower().Contains(searchText))
                .ToList();
            PatientListView.ItemsSource = filteredPatients;
        }

        private void DoctorSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = DoctorSearchTextBox.Text.ToLower();
            var filteredDoctors = hospital.GetDoctors()
                .Where(d => d.FullName.ToLower().Contains(searchText))
                .ToList();
            DoctorListView.ItemsSource = filteredDoctors;
        }

        private void MedicalCardSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = MedicalCardSearchTextBox.Text.ToLower();
            var filteredMedCards = hospital.GetMedicalCards()
                .Where(m => m.Person.FullName.ToLower().Contains(searchText))
                .ToList();
            MedicalCardListView.ItemsSource = filteredMedCards;
        }

        private void PatientSearchDeleteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = PatientSearchDeleteTextBox.Text.ToLower();
            var filteredPatients = hospital.GetPatients()
                .Where(p => p.FullName.ToLower().Contains(searchText))
                .ToList();
            PatientDeleteListView.ItemsSource = filteredPatients;
        }

        private void DoctorSearchDeleteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = DoctorSearchDeleteTextBox.Text.ToLower();
            var filteredDoctors = hospital.GetDoctors()
                .Where(d => d.FullName.ToLower().Contains(searchText))
                .ToList();
            DoctorDeleteListView.ItemsSource = filteredDoctors;
        }

        private void MedicalCardDeleteSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = MedicalCardDeleteSearchTextBox.Text.ToLower();
            var filteredMedCards = hospital.GetMedicalCards()
                .Where(m => m.Person.FullName.ToLower().Contains(searchText))
                .ToList();
            MedicalCardDeleteListView.ItemsSource = filteredMedCards;
        }

        private void SavePatientData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var patient = new Person(
                    FirstNameTextBox.Text,
                    LastNameTextBox.Text,
                    MiddleNameTextBox.Text,
                    BirthDatePicker.SelectedDate.Value,
                    gender,
                    AddressTextBox.Text,
                    PhoneTextBox.Text
                );
                hospital.Add(patient);
                SaveData();
                PatientAddBorder.Visibility = Visibility.Collapsed;
                MainBorder.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveDoctorData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string gender = (DoctorGenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var doctor = new Doctor(
                    DoctorFirstNameTextBox.Text,
                    DoctorLastNameTextBox.Text,
                    DoctorMiddleNameTextBox.Text,
                    DoctorBirthDatePicker.SelectedDate.Value,
                    gender,
                    DoctorAddressTextBox.Text,
                    DoctorPhoneTextBox.Text,
                    DoctorSpecializationTextBox.Text,
                    int.Parse(DoctorExperienceTextBox.Text),
                    int.Parse(DoctorRoomNumberTextBox.Text)
                );
                hospital.Add(doctor);
                SaveData();
                DoctorAddBorder.Visibility = Visibility.Collapsed;
                MainBorder.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveMedicalCardData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string gender = (MedicalCardGenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var person = new Person(
                    MedicalCardFirstNameTextBox.Text,
                    MedicalCardLastNameTextBox.Text,
                    MedicalCardMiddleNameTextBox.Text,
                    MedicalCardBirthDatePicker.SelectedDate.Value,
                    gender,
                    MedicalCardAddressTextBox.Text,
                    MedicalCardPhoneTextBox.Text
                );
                var medCard = new MedCard(
                    person,
                    MedicalCardStartDatePicker.SelectedDate.Value,
                    MedicalCardEndDatePicker.SelectedDate.Value,
                    MedicalCardAnamnesisTextBox.Text
                );
                hospital.Add(medCard);
                SaveData();
                MedicalCardAddBorder.Visibility = Visibility.Collapsed;
                MainBorder.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PatientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientListView.SelectedItem != null)
            {
                var selectedPatient = PatientListView.SelectedItem as Person;
                MessageBox.Show($"Выбран пациент:\n Имя: {selectedPatient.Name}\n Фамилия: {selectedPatient.Surname}\n " +
                    $"Отчество: {selectedPatient.Patronymic}\n Дата рождения: {selectedPatient.DateBirthday}\n" +
                $" Пол: {selectedPatient.Gender}\n Адрес: {selectedPatient.Address}\n Номер телефона: {selectedPatient.Number}");
            }
            else
            {
                MessageBox.Show("Пациент отсутствует.");
            }
        }

        private void DoctorListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorListView.SelectedItem != null)
            {
                var selectedDoctor = DoctorListView.SelectedItem as Doctor;
                MessageBox.Show($"Выбран врач:\n Имя: {selectedDoctor.Name}\n Фамилия: {selectedDoctor.Surname}\n " +
                    $"Отчество: {selectedDoctor.Patronymic}\n Дата рождения: {selectedDoctor.DateBirthday}\n" +
                $" Пол: {selectedDoctor.Gender}\n Адрес: {selectedDoctor.Address}\n Номер телефона: {selectedDoctor.Number}\n" +
                $" Специализация: {selectedDoctor.Specialization}\n Опыт работы: {selectedDoctor.Experience}\n Кабинет: {selectedDoctor.Office}");
            }
            else
            {
                MessageBox.Show("Врач отсутствует.");
            }
        }

        private void MedicalCardListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MedicalCardListView.SelectedItem != null)
            {
                var selectedMedCard = MedicalCardListView.SelectedItem as MedCard;
                if (selectedMedCard != null && selectedMedCard.Person != null)
                {
                    MessageBox.Show($"Выбран пациент:\n Имя: {selectedMedCard.Person.Name}\n Фамилия: {selectedMedCard.Person.Surname}\n " +
                        $"Отчество: {selectedMedCard.Person.Patronymic}\n Дата рождения: {selectedMedCard.Person.DateBirthday}\n" +
                        $" Пол: {selectedMedCard.Person.Gender}\n Адрес: {selectedMedCard.Person.Address}\n Номер телефона: {selectedMedCard.Person.Number}\n" +
                        $" Дата начала лечения: {selectedMedCard.StartDate}\n Дата окончания лечения: {selectedMedCard.EndDate}\n" +
                        $" Анамнез: {selectedMedCard.Anamnesis}");
                }
                else
                {
                    MessageBox.Show("Медицинская карта отсутсвует.");
                }
            }
        }

        private void PDelete_Click(object sender, RoutedEventArgs e)
        {
            PatientAddBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            PatientDeleteBorder.Visibility = Visibility.Visible;
            PatientDeleteListView.ItemsSource = hospital.GetPatients();
        }

        private void DeletePatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientDeleteListView.SelectedItem != null)
            {
                var selectedPatient = PatientDeleteListView.SelectedItem as Person;
                try
                {
                    hospital.Remove(selectedPatient); // Используем новый метод Remove
                    SaveData();
                    PatientSearchDeleteTextBox_TextChanged(null, null); // Обновляем список
                    MessageBox.Show("Пациент успешно удален.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите пациента для удаления.");
            }
        }

        private void DeleteDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorDeleteListView.SelectedItem != null)
            {
                var selectedDoctor = DoctorDeleteListView.SelectedItem as Doctor;
                try
                {
                    hospital.Remove(selectedDoctor); // Используем новый метод Remove
                    SaveData();
                    DoctorSearchDeleteTextBox_TextChanged(null, null); // Обновляем список
                    MessageBox.Show("Врач успешно удален.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите врача для удаления.");
            }
        }

        private void DeleteMedicalCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedicalCardDeleteListView.SelectedItem != null)
            {
                var selectedMedCard = MedicalCardDeleteListView.SelectedItem as MedCard;
                try
                {
                    hospital.Remove(selectedMedCard); // Используем новый метод Remove
                    SaveData();
                    MedicalCardDeleteSearchTextBox_TextChanged(null, null); // Обновляем список
                    MessageBox.Show("Медицинская карта успешно удалена.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите медицинскую карту для удаления.");
            }
        }

        private void DeleteFirstPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (hospital.GetPatients().Count > 0)
            {
                try
                {
                    hospital.RemoveAt(0); // Удаляем первого пациента
                    SaveData();
                    PatientSearchDeleteTextBox_TextChanged(null, null); // Обновляем список
                    MessageBox.Show("Первый пациент успешно удален.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Список пациентов пуст.");
            }
        }

        private void DeleteLastPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (hospital.GetPatients().Count > 0)
            {
                try
                {
                    hospital.RemoveAt(hospital.GetPatients().Count - 1); // Удаляем последнего пациента
                    SaveData();
                    PatientSearchDeleteTextBox_TextChanged(null, null); // Обновляем список
                    MessageBox.Show("Последний пациент успешно удален.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Список пациентов пуст.");
            }
        }

        private void DDelete_Click(object sender, RoutedEventArgs e)
        {
            PatientAddBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            DoctorDeleteBorder.Visibility = Visibility.Visible;
            DoctorDeleteListView.ItemsSource = hospital.GetDoctors();
        }

        private void MDelete_Click(object sender, RoutedEventArgs e)
        {
            PatientAddBorder.Visibility = Visibility.Collapsed;
            DoctorAddBorder.Visibility = Visibility.Collapsed;
            MedicalCardAddBorder.Visibility = Visibility.Collapsed;
            PatientCheckBorder.Visibility = Visibility.Collapsed;
            DoctorCheckBorder.Visibility = Visibility.Collapsed;
            MedicalCardCheckBorder.Visibility = Visibility.Collapsed;
            MainBorder.Visibility = Visibility.Collapsed;
            MedicalCardDeleteBorder.Visibility = Visibility.Visible;
            MedicalCardDeleteListView.ItemsSource = hospital.Peoples.OfType<MedCard>().ToList();
        }

        private void PatientSearchForChangeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = PatientSearchForChangeTextBox.Text.ToLower();
            var filteredPatients = hospital.GetPatients()
                .Where(p => p.FullName.ToLower().Contains(searchText))
                .ToList();
            PatientChangeListView.ItemsSource = filteredPatients;
        }

        private void PatientChangeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientChangeListView.SelectedItem != null)
            {
                var selectedPatient = PatientChangeListView.SelectedItem as Person;
                ChangePatientFirstNameTextBox.Text = selectedPatient.Name;
                ChangePatientLastNameTextBox.Text = selectedPatient.Surname;
                ChangePatientMiddleNameTextBox.Text = selectedPatient.Patronymic;
                ChangePatientBirthDatePicker.SelectedDate = selectedPatient.DateBirthday;
                ChangePatientGenderComboBox.SelectedItem = selectedPatient.Gender == "Мужской" ? ChangePatientGenderComboBox.Items[0] : ChangePatientGenderComboBox.Items[1];
                ChangePatientAddressTextBox.Text = selectedPatient.Address;
                ChangePatientPhoneTextBox.Text = selectedPatient.Number;
            }
        }

        private void SelectPatientForChange_Click(object sender, RoutedEventArgs e)
        {
            if (PatientChangeListView.SelectedItem != null)
            {
                PatientSelectForChangeBorder.Visibility = Visibility.Collapsed;
                PatientChangeBorder.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Выберите пациента для изменения.");
            }
        }

        private void SavePatientChanges_Click(object sender, RoutedEventArgs e)
        {
            if (PatientChangeListView.SelectedItem != null)
            {
                var selectedPatient = PatientChangeListView.SelectedItem as Person;
                var updatedPatient = new Person(
                    ChangePatientFirstNameTextBox.Text,
                    ChangePatientLastNameTextBox.Text,
                    ChangePatientMiddleNameTextBox.Text,
                    ChangePatientBirthDatePicker.SelectedDate.Value,
                    (ChangePatientGenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                    ChangePatientAddressTextBox.Text,
                    ChangePatientPhoneTextBox.Text
                )
                {
                    Id = selectedPatient.Id // Сохраняем уникальный идентификатор
                };

                try
                {
                    hospital.Update(updatedPatient); // Используем новый объект для обновления
                    SaveData();
                    PatientChangeBorder.Visibility = Visibility.Collapsed;
                    MainBorder.Visibility = Visibility.Visible;
                    MessageBox.Show("Данные пациента успешно изменены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите пациента для изменения.");
            }
        }

        private void DoctorSearchForChangeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = DoctorSearchForChangeTextBox.Text.ToLower();
            var filteredDoctors = hospital.GetDoctors()
                .Where(d => d.FullName.ToLower().Contains(searchText))
                .ToList();
            DoctorChangeListView.ItemsSource = filteredDoctors;
        }

        private void DoctorChangeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorChangeListView.SelectedItem != null)
            {
                var selectedDoctor = DoctorChangeListView.SelectedItem as Doctor;
                ChangeDoctorFirstNameTextBox.Text = selectedDoctor.Name;
                ChangeDoctorLastNameTextBox.Text = selectedDoctor.Surname;
                ChangeDoctorMiddleNameTextBox.Text = selectedDoctor.Patronymic;
                ChangeDoctorBirthDatePicker.SelectedDate = selectedDoctor.DateBirthday;
                ChangeDoctorGenderComboBox.SelectedItem = selectedDoctor.Gender == "Мужской" ? ChangeDoctorGenderComboBox.Items[0] : ChangeDoctorGenderComboBox.Items[1];
                ChangeDoctorAddressTextBox.Text = selectedDoctor.Address;
                ChangeDoctorPhoneTextBox.Text = selectedDoctor.Number;
                ChangeDoctorSpecializationTextBox.Text = selectedDoctor.Specialization;
                ChangeDoctorExperienceTextBox.Text = selectedDoctor.Experience.ToString();
                ChangeDoctorRoomNumberTextBox.Text = selectedDoctor.Office.ToString();
            }
        }

        private void SelectDoctorForChange_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorChangeListView.SelectedItem != null)
            {
                DoctorSelectForChangeBorder.Visibility = Visibility.Collapsed;
                DoctorChangeBorder.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Выберите врача для изменения.");
            }
        }

        private void SaveDoctorChanges_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorChangeListView.SelectedItem != null)
            {
                var selectedDoctor = DoctorChangeListView.SelectedItem as Doctor;
                var updatedDoctor = new Doctor(
                    ChangeDoctorFirstNameTextBox.Text,
                    ChangeDoctorLastNameTextBox.Text,
                    ChangeDoctorMiddleNameTextBox.Text,
                    ChangeDoctorBirthDatePicker.SelectedDate.Value,
                    (ChangeDoctorGenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                    ChangeDoctorAddressTextBox.Text,
                    ChangeDoctorPhoneTextBox.Text,
                    ChangeDoctorSpecializationTextBox.Text,
                    int.Parse(ChangeDoctorExperienceTextBox.Text),
                    int.Parse(ChangeDoctorRoomNumberTextBox.Text)
                )
                {
                    Id = selectedDoctor.Id // Сохраняем уникальный идентификатор
                };

                try
                {
                    hospital.Update(updatedDoctor); // Используем новый объект для обновления
                    SaveData();
                    DoctorChangeBorder.Visibility = Visibility.Collapsed;
                    MainBorder.Visibility = Visibility.Visible;
                    MessageBox.Show("Данные врача успешно изменены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите врача для изменения.");
            }
        }

        private void MedicalCardSearchForChangeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = MedicalCardSearchForChangeTextBox.Text.ToLower();
            var filteredMedCards = hospital.Peoples.OfType<MedCard>()
                .Where(m => m.Person.FullName.ToLower().Contains(searchText))
                .ToList();
            MedicalCardChangeListView.ItemsSource = filteredMedCards;
        }

        private void MedicalCardChangeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MedicalCardChangeListView.SelectedItem != null)
            {
                var selectedMedCard = MedicalCardChangeListView.SelectedItem as MedCard;
                ChangeMedicalCardFirstNameTextBox.Text = selectedMedCard.Person.Name;
                ChangeMedicalCardLastNameTextBox.Text = selectedMedCard.Person.Surname;
                ChangeMedicalCardMiddleNameTextBox.Text = selectedMedCard.Person.Patronymic;
                ChangeMedicalCardBirthDatePicker.SelectedDate = selectedMedCard.Person.DateBirthday;
                ChangeMedicalCardGenderComboBox.SelectedItem = selectedMedCard.Person.Gender == "Мужской" ? ChangeMedicalCardGenderComboBox.Items[0] : ChangeMedicalCardGenderComboBox.Items[1];
                ChangeMedicalCardAddressTextBox.Text = selectedMedCard.Person.Address;
                ChangeMedicalCardPhoneTextBox.Text = selectedMedCard.Person.Number;
                ChangeMedicalCardStartDatePicker.SelectedDate = selectedMedCard.StartDate;
                ChangeMedicalCardEndDatePicker.SelectedDate = selectedMedCard.EndDate;
                ChangeMedicalCardAnamnesisTextBox.Text = selectedMedCard.Anamnesis;
            }
        }

        private void SelectMedicalCardForChange_Click(object sender, RoutedEventArgs e)
        {
            if (MedicalCardChangeListView.SelectedItem != null)
            {
                MedicalCardSelectForChangeBorder.Visibility = Visibility.Collapsed;
                MedicalCardChangeBorder.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Выберите медицинскую карту для изменения.");
            }
        }

        private void SaveMedicalCardChanges_Click(object sender, RoutedEventArgs e)
        {
            if (MedicalCardChangeListView.SelectedItem != null)
            {
                var selectedMedCard = MedicalCardChangeListView.SelectedItem as MedCard;
                var updatedPerson = new Person(
                    ChangeMedicalCardFirstNameTextBox.Text,
                    ChangeMedicalCardLastNameTextBox.Text,
                    ChangeMedicalCardMiddleNameTextBox.Text,
                    ChangeMedicalCardBirthDatePicker.SelectedDate.Value,
                    (ChangeMedicalCardGenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                    ChangeMedicalCardAddressTextBox.Text,
                    ChangeMedicalCardPhoneTextBox.Text
                )
                {
                    Id = selectedMedCard.Person.Id // Сохраняем уникальный идентификатор пациента
                };

                var updatedMedCard = new MedCard(
                    updatedPerson,
                    ChangeMedicalCardStartDatePicker.SelectedDate.Value,
                    ChangeMedicalCardEndDatePicker.SelectedDate.Value,
                    ChangeMedicalCardAnamnesisTextBox.Text
                )
                {
                    Id = selectedMedCard.Id // Сохраняем уникальный идентификатор медицинской карты
                };

                try
                {
                    hospital.Update(updatedMedCard); // Используем новый объект для обновления
                    SaveData();
                    MedicalCardChangeBorder.Visibility = Visibility.Collapsed;
                    MainBorder.Visibility = Visibility.Visible;
                    MessageBox.Show("Данные медицинской карты успешно изменены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите медицинскую карту для изменения.");
            }
        }
    }
}