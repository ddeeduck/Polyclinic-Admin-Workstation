# Polyclinic Administrator Workstation

![WPF](https://img.shields.io/badge/Platform-WPF-blue)
![C#](https://img.shields.io/badge/Language-C%23-green)
![.NET](https://img.shields.io/badge/.NET-6.0-purple)
![License](https://img.shields.io/badge/License-MIT-yellow)

A desktop application for managing patients, doctors, and medical records in a polyclinic. Built with WPF and C#, it provides a simple and modern interface for administrative tasks.

The Russian version is available [here](README.ru.md).

## Description
This application allows a polyclinic administrator to view, add, edit, delete, and search for information about patients, doctors, and their medical cards. Data is stored in an XML file, making the system lightweight and easy to deploy.

## Technologies Used
- **C#** – core programming language
- **WPF (Windows Presentation Foundation)** – for the graphical user interface
- **Material Design In XAML Toolkit** – for modern styling
- **.NET 6.0** – application framework
- **XML Serialization** – for data storage

## Features
- **Patient Management**: Add, edit, delete, and search patients by full name.
- **Doctor Management**: Same operations for doctors, with specialization and office fields.
- **Medical Records**: Create and manage medical cards linked to patients, including treatment dates and anamnesis.
- **Data Persistence**: Automatic loading and saving to an XML file (`hospital.xml`).
- **Modern UI**: Clean interface with Material Design components.

## Screenshots

*Main window*

<img width="600" src="images/main_window.png">

<br>

*Patient list view*

<img width="600" src="images/patient_list.png">

<br>

*Add patient window*

<img width="600" src="images/add_patient.png">

## Example Data Structure

```
xml

<Hospital>
  <Peoples>
    <Person Id="...">
      <Surname>Ivanov</Surname>
      <Name>Ivan</Name>
      <Patronymic>Ivanovich</Patronymic>
      <DateBirthday>1980-05-15</DateBirthday>
      <Gender>Male</Gender>
      <Address>123 Main St</Address>
      <Number>+1234567890</Number>
    </Person>
  </Peoples>
  <Doctors>...</Doctors>
  <MedCards>...</MedCards>
</Hospital>

```

### Installation & Usage
1. Requirements
- Windows OS with .NET 6.0 SDK or runtime
- Visual Studio 2022 (for building from source)

2. Steps
1) Clone the repository:

  `git clone https://github.com/ddeeduck/Polyclinic-Administrator-Workstation.git`

2) Open the solution file (PolyclinicApp.sln) in Visual Studio 2022.

3) Restore NuGet packages (should be automatic).

4) Build the solution (Build > Build Solution).

5) Run the application (F5).

### How to Use
- Upon first launch, an empty hospital.xml file will be created.
- Use the main menu buttons to navigate between patients, doctors, and medical cards.
- Click "View" to see lists, "Add" to create new entries, "Edit" to modify existing ones, and "Delete" to remove records.
- Use the search box to filter lists by full name.

### Limitations
- No user authentication (single‑user mode).
- Data is stored in a local XML file, not suitable for multi‑user concurrent access.
- Validation is basic (checks for empty fields, date correctness).

### Author
Daria – [GitHub](https://github.com/ddeeduck), [Telegram](https://t.me/deeduck), [LinkedIn](www.linkedin.com/in/deeduck), Email: dehterevich.daria@gmail.com

### License
This project is licensed under the MIT License – see the LICENSE file for details.
