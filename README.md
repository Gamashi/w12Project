# W12 - Workout & Progression Tracker 🏋️‍♂️ (Work in Progress)

## About the Project
W12 is a mobile application built natively with **.NET MAUI** designed to track hypertrophy training performance and load progression. Created to replace manual tracking methods with a robust digital solution, the app allows users to structure exercise routines, monitor progressive overload, and manage daily nutritional targets (such as hitting a 117g daily protein goal). 

This project currently serves as a practical laboratory for applying advanced software engineering concepts in a real-world mobile development scenario.

## 🚀 Tech Stack & Architecture
* **Framework:** .NET MAUI (C#)
* **Architecture:** MVVM (Model-View-ViewModel)
* **Libraries:** `CommunityToolkit.Mvvm`
* **Local Storage:** SQLite (Offline-first data persistence)
* **Core Concepts:** Dependency Injection (via MAUI Shell), Reactive Messaging, and Asynchronous Database Queries.

## 🚧 Current Status: Active Development
This application is currently in the development phase. 

**Recently Implemented:**
* Initial project structure and MAUI Shell routing configuration.
* Base ViewModels setup using `CommunityToolkit.Mvvm`.
* UI wireframing for the main dashboard.

**Upcoming Features:**
* Implementation of the SQLite local database for offline workout logging.
* Reactive messaging integration for seamless UI updates.
* Performance optimization for asynchronous queries on workout history.

## 🛠️ How to Run Locally
1. Clone this repository: `git clone https://github.com/your-username/W12.git`
2. Open the `.sln` file in Visual Studio 2022 (ensure the .NET MAUI workload is installed).
3. Restore the NuGet packages.
4. Select your target device (Android Emulator, iOS Simulator, or Windows Machine) and hit Run.
