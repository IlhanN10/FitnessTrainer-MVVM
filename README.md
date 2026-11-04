# FitnessTrainer-MVVM – Schulprojekt

Vollständig kommentierte Übungslösung zu **C# / WPF / MVVM** mit **.NET 9** für **Visual Studio 2022 (17.12 oder neuer)**.

## Start
1. Repository klonen oder als ZIP herunterladen.
2. **Fitnesstrainer.sln** in Visual Studio 2022 öffnen.
3. Workload „.NET-Desktopentwicklung“ und .NET 9 SDK installieren, sofern noch nicht vorhanden.
4. Fitnesstrainer als Startprojekt auswählen, erstellen und mit F5 starten.

## Aufgaben
1. Clear-Button per ICommand; Properties benachrichtigen die View.
2. Eingaben prüfen und Trainingseinheit in der simulierten Datenbank speichern.
3. Model mit Name, Dauer und ErstellDatum.
4. Info-Ausgabe über Binding.
5. Trainingsliste über ObservableCollection in der ComboBox.
6. IDataAccess als Schnittstelle für Datenzugriff.
7. Singleton (DBAccess) und Command-Pattern (RelayCommand).

**Hinweise:** Die Daten liegen ausschließlich im RAM und verschwinden nach Programmende. CommandHelper wurde hier als kommentierte C#-Klasse statt als schwer nachvollziehbare DLL implementiert. Die Projektmappe ist für Windows/WPF vorgesehen; ein echter Windows-Build wurde hier nicht durchgeführt.
Siehe auch [ERKLAERUNG_AUFGABEN.md](ERKLAERUNG_AUFGABEN.md).
