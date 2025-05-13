# Project Expense Tracker - User App (MAUI)

## Overview
This repository contains the User App component (built with .NET MAUI) of a comprehensive Project Expense Tracker system designed for modern businesses and project management. This application works in conjunction with a separate Admin App built with Native Android.

This project is being developed as part of the COMP1661 Mobile Application Design and Development module at the University of Greenwich.

## Related Repository
- **Admin App (Native Android)**: [Project Expense Tracker Admin App](https://github.com/Hakeem-404/Project-Expense-Tracker-Android-App)

## Application Purpose
This User App allows team members to:
- View available projects from the cloud service
- Search for projects by various criteria
- Add their own expenses to existing projects
- Set favorite projects for quick access

## Features
- **Project Viewing**
  - Connect to cloud service to retrieve project details
  - View comprehensive project information
  
- **Expense Management**
  - Add expenses to existing projects
  - View expense history and status
  
- **Search Functionality**
  - Search for projects by name and date
  
- **Favorites System**
  - Mark projects as favorites for quick access
  - Manage favorite projects list

## Technical Details
- **Framework**: .NET MAUI
- **Languages**: C#, XAML
- **Platform**: Cross-platform (iOS, Android)
- **Architecture Pattern**: MVVM (Model-View-ViewModel)
- **Cloud Integration**: Firebase

## Installation Instructions
1. Clone this repository:
git clone https://github.com/Hakeem-404/.MAUI-Project-Expense-Tracker-App.git
2. Open the project in Visual Studio
3. Restore NuGet packages
4. Build and run on your preferred platform

## Project Structure
- **Models/**: Data models for projects and expenses
- **Views/**: UI implementation files
- **ViewModels/**: Business logic and data binding
- **Services/**: Cloud connectivity and API services

## Screenshots

<div align="center">

### Project Management

<table>
  <tr>
    <td align="center">
      <img width="300" alt="Project Sourced from Firebase" src="https://github.com/user-attachments/assets/a478723c-7ea3-40a4-a7c7-2d6b40104245" /><br>
      <b>Project Sourced from Firebase</b>
    </td>
    <td align="center">
      <img width="300" alt="Project Details" src="https://github.com/user-attachments/assets/e5c23412-9991-4e9e-81ef-ef3ef2b144f9" /><br>
      <b>Project Details View</b>
    </td>
    <td align="center">
      <img width="300" alt="Marking Project as Favourite" src="https://github.com/user-attachments/assets/11ba9d0f-846d-4788-a1f1-f59ad2032892" /><br>
      <b>Marking Favorites</b>
    </td>
  </tr>
</table>

### Navigation & Features

<table>
  <tr>
    <td align="center">
      <img width="300" alt="App Menu" src="https://github.com/user-attachments/assets/3b67fc57-b8ed-40f0-9f8f-de01bc76c0d2" /><br>
      <b>App Menu</b>
    </td>
    <td align="center">
      <img width="300" alt="Favourite Project List" src="https://github.com/user-attachments/assets/e20818af-cf1f-4ad5-9fbc-e611d32ab897" /><br>
      <b>Favorite Projects</b>
    </td>
  </tr>
</table>

### User Interactions

<table>
  <tr>
    <td align="center">
      <img width="300" alt="Search Functionality" src="https://github.com/user-attachments/assets/4d4b1553-77b0-4cf8-a9c4-fb4909ecab44" /><br>
      <b>Search Projects</b>
    </td>
    <td align="center">
      <img width="300" alt="Add Expense" src="https://github.com/user-attachments/assets/6ee1035e-b329-40f8-afbb-d9b95720ba93" /><br>
      <b>Add Expense</b>
    </td>
  </tr>
</table>

</div>




## License
This project is developed for educational purposes as part of COMP1661 at the University of Greenwich.
