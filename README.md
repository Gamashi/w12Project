# W12 - Workout & Progression Tracker 🏋️‍♂️

A high-performance mobile application built natively with **.NET 9** and **.NET MAUI**, designed to track hypertrophy training performance, monitor progressive overload, and automate **1RM (One-Rep Max)** calculations.

W12 replaces manual tracking with an intuitive, offline-first digital experience, allowing athletes to log exercise sets, review historic PRs, and visualize strength progression over time.

---

## 📱 Key Features

- **Dynamic Performance Dashboard:** Instant overview of the latest completed exercise, session metrics, and real-time personal records (PR).
- **Automated 1RM Estimation:** Real-time calculation of theoretical maximum single-rep capacity using the **Brzycki Formula**, highlighting daily milestones and all-time records.
- **Progress Tracking & Data Visualization:** Interactive strength evolution curves powered by **LiveCharts2**, plotting historic 1RM progression per exercise.
- **Historic Workout Log with Live Search:** Searchable list of past sessions with real-time filtering by exercise name or execution date (`dd/MM/yyyy`).
- **Exercise & Category Organization:** Flexible categorization of exercises and muscle groups.
- **100% Offline-First Architecture:** Local storage with zero reliance on cloud connectivity for instant logging during gym sessions.

---

## 🚀 Tech Stack & Libraries

- **Framework:** [.NET MAUI (.NET 9)](https://dotnet.microsoft.com/en-us/apps/maui) - Cross-platform native mobile development.
- **Architecture:** MVVM (Model-View-ViewModel) with Source Generators.
- **MVVM Toolkit:** [`CommunityToolkit.Mvvm`](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/) (`[ObservableProperty]`, `[RelayCommand]`).
- **Data Persistence:** [`sqlite-net-pcl`](https://github.com/praeclarum/sqlite-net) - Embedded relational database with asynchronous queries and indexed relationships.
- **Charts & Visualizations:** [`LiveChartsCore.SkiaSharpView.Maui`](https://livecharts.dev/) - Hardware-accelerated vector charting via SkiaSharp.
- **UI Enhancements:** [`CommunityToolkit.Maui`](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/).

---

## 📐 Architecture & Project Structure

The project strictly follows the MVVM pattern with separation of concerns and dependency injection:

```text
w12/
├── Helpers/         # Domain calculations (e.g., OneRepMaxCalculator via Brzycki)
├── Models/          # SQLite relational entities (ExecutionExercise, BaseExercise)
├── Services/        # Local SQLite database provider and query execution
├── ViewModels/      # Decoupled UI logic leveraging CommunityToolkit.Mvvm
├── Views/           # ContentPages, custom controls, and ContentViews
└── Resources/       # Theme brushes, application icons, and vector assets


🚧 Status & Roadmap
[x] MVVM architecture and Shell routing

[x] Asynchronous SQLite database integration

[x] Automated 1RM calculation logic (Brzycki formula)

[x] Exercise and muscle group categorization

[x] Workout history with instant search/filtering

[x] Interactive 1RM progression charts (LiveCharts2)

[ ] Export workout data to CSV / JSON

[ ] Rest timer integration for active workout sessions


🛠️ How to Run Locally
Prerequisites
Visual Studio 2022 (v17.12 or newer) with the .NET Multi-platform App UI development workload installed.

.NET 9 SDK.

Android Emulator / Physical device with Developer Mode enabled.

Steps
1.Clone the repository:
	Bash
	git clone [https://github.com/Gamashi/w12Project](https://github.com/Gamashi/w12Project)
	cd W12

2.Restore NuGet dependencies:
	Bash
	dotnet restore

3.Build and launch on your target platform:
	Bash
	dotnet build -t:Run -f net9.0-android

📄 License
This project is licensed under the MIT License.