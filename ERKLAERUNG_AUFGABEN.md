# Fitnesstrainer – Erklärung der sieben Aufgaben

## Architektur: MVVM
- **View**: `Fitnesstrainer/View/MainView.xaml` definiert ausschließlich Oberfläche und Bindings.
- **ViewModel**: `Fitnesstrainer/ViewModel/MainViewModel.cs` stellt gebundene Properties und Commands bereit.
- **Model**: `Model/Trainingseinheit.cs` speichert Name, Dauer und ErstellDatum.
- **DataAccess**: `DataAccess/DBAccess.cs` hält Trainingseinheiten simuliert im Arbeitsspeicher.
- **Interface**: `Interfaces/IDataAccess.cs` definiert den Vertrag für Datenzugriffe.

## Aufgabe 1 – Clear
Der Button bindet an `LoeschenCommand`. Dessen `RelayCommand` ruft `LoescheTrainingseinheit()` auf. Die Methode setzt `NameTrainingseinheit` und `DauerTrainingseinheit` über die öffentlichen Properties zurück. Dadurch wird `PropertyChanged` ausgelöst; WPF aktualisiert die TextBoxen.

## Aufgabe 2 – Speichern
Der Button bindet an `SpeichernCommand`. `SpeichereTrainingseinheit()` validiert die Eingaben, erstellt ein neues `Trainingseinheit`-Objekt und ruft `_dataAccess.SpeichereTrainingsEinheit()` auf. `SpeicherStatus` enthält die Rückmeldung.

## Aufgabe 3 – Model
Das Model hat `string Name`, `int Dauer` in Minuten und `DateTime ErstellDatum`. Bei `new Trainingseinheit()` wird `DateTime.Now` gesetzt. Model und UI sind getrennt.

## Aufgabe 4 – Info
`InfoCommand` ruft `GibInfoAus()` auf. Die Methode formatiert Name, Datum und Dauer in `InfoText`. Das entsprechende TextBlock in XAML zeigt `InfoText` mittels Binding.

## Aufgabe 5 – Laden
`HoleTrainingsEinheiten()` liefert eine Liste gespeicherter Model-Objekte. Diese werden in `TrainingsEinheitenListe` vom Typ `ObservableCollection<Trainingseinheit>` übertragen. Die ComboBox benutzt `ItemsSource`, `SelectedItem` und `DisplayMemberPath="Name"`.

## Aufgabe 6 – Interface
Das Interface `IDataAccess` definiert `SpeichereTrainingsEinheit` und `HoleTrainingsEinheiten`. `DBAccess : IDataAccess` implementiert beide. `MainViewModel` hält ein Feld des Interface-Typs. So könnte die Datenquelle ausgetauscht werden, ohne das ViewModel neu zu schreiben.

## Aufgabe 7 – Design Patterns
**Singleton:** `DBAccess` besitzt einen privaten Konstruktor, ein statisches `Lazy<DBAccess>` und `GetInstance()`; Aufrufer erhalten dieselbe Instanz. **Command:** `RelayCommand : ICommand` enthält `Execute()`, `CanExecute()` und `CanExecuteChanged`. Ein Button kann Aktionen über Binding auslösen, ohne Methoden direkt im Code-Behind aufzurufen.

## UML – Beziehungen als Text
```
MainView (View)
   -- DataContext --> MainViewModel
MainViewModel
   -- verwendet --> Trainingseinheit (Model)
   -- verwendet --> IDataAccess (Interface)
DBAccess -- implementiert --> IDataAccess
DBAccess -- speichert --> List<Trainingseinheit>
MainViewModel -- erzeugt --> RelayCommand (ICommand)
```

## Funktionstests
1. Nur einen Namen eingeben und speichern: Meldung zur ungültigen Dauer.
2. Name und positive Dauer eingeben: Erfolgsmeldung; Liste zeigt den Eintrag.
3. Eintrag auswählen und Info drücken: Name, Datum und Dauer erscheinen.
4. Clear drücken: Eingabefelder werden geleert.
5. Weitere Einheit speichern: Mehrere Listeneinträge sichtbar.
6. Anwendung schließen und neu starten: Liste ist leer (Fake-Datenbank).
