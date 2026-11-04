using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommandHelper;
using DataAccess;
using Interfaces;
using Model;
namespace Fitnesstrainer.ViewModel
{
    /// <summary>
    /// Das ViewModel verbindet die View über Data Binding mit Model und DataAccess.
    /// INotifyPropertyChanged sorgt dafür, dass WPF nach Property-Änderungen
    /// den angezeigten Text aktualisiert, ohne direkt auf Controls zuzugreifen.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        // AUFGABE 3: Alle Formularfelder stammen aus einem Model-Objekt.
        private Trainingseinheit _trainingsEinheit = new();
        // AUFGABE 6: Abhängigkeit vom Interface statt von einer DBAccess-Variable.
        private readonly IDataAccess _dataAccess;
        private string _speicherStatus = string.Empty;
        private string _infoText = string.Empty;
        private Trainingseinheit _ausgewaehlteTrainingseinheit;
        // Commands werden an WPF-Buttons gebunden.
        private readonly ICommand _loeschenCommand, _speichernCommand, _infoCommand, _ladenCommand;
        public event PropertyChangedEventHandler PropertyChanged;
        private void MeldeAenderung(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public string NameTrainingseinheit
        {
            get => _trainingsEinheit.Name;
            set
            {
                if (_trainingsEinheit.Name == value) return;
                _trainingsEinheit.Name = value;
                MeldeAenderung(nameof(NameTrainingseinheit));
            }
        }
        public int DauerTrainingseinheit
        {
            get => _trainingsEinheit.Dauer;
            set
            {
                if (_trainingsEinheit.Dauer == value) return;
                _trainingsEinheit.Dauer = value;
                MeldeAenderung(nameof(DauerTrainingseinheit));
            }
        }
        public string SpeicherStatus
        {
            get => _speicherStatus;
            set { _speicherStatus = value; MeldeAenderung(nameof(SpeicherStatus)); }
        }
        public string InfoText
        {
            get => _infoText;
            set { _infoText = value; MeldeAenderung(nameof(InfoText)); }
        }
        // AUFGABE 5: ObservableCollection benachrichtigt die View über Änderungen.
        public ObservableCollection<Trainingseinheit> TrainingsEinheitenListe { get; } = new();
        public Trainingseinheit AusgewaehlteTrainingseinheit
        {
            get => _ausgewaehlteTrainingseinheit;
            set { _ausgewaehlteTrainingseinheit = value; MeldeAenderung(nameof(AusgewaehlteTrainingseinheit)); }
        }
        public ICommand LoeschenCommand => _loeschenCommand;
        public ICommand SpeichernCommand => _speichernCommand;
        public ICommand InfoCommand => _infoCommand;
        public ICommand LadenCommand => _ladenCommand;
        // Parameterloser Konstruktor ist für die XAML-Instanziierung notwendig.
        public MainViewModel() : this(DBAccess.GetInstance()) { }
        // Übergabe des Interfaces erlaubt später z.B. Test-Datenquellen.
        public MainViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            _loeschenCommand = new RelayCommand(_ => LoescheTrainingseinheit());
            _speichernCommand = new RelayCommand(_ => SpeichereTrainingseinheit());
            _infoCommand = new RelayCommand(_ => GibInfoAus());
            _ladenCommand = new RelayCommand(_ => LadeTrainingseinheiten());
            LadeTrainingseinheiten();
        }
        private void LoescheTrainingseinheit()
        {
            // AUFGABE 1: Properties benutzen, damit PropertyChanged ausgelöst wird.
            NameTrainingseinheit = string.Empty;
            DauerTrainingseinheit = 0;
            SpeicherStatus = string.Empty;
            InfoText = string.Empty;
        }
        private void SpeichereTrainingseinheit()
        {
            // AUFGABE 2: Eingaben prüfen; ein leerer Name oder Dauer 0 ist ungültig.
            if (string.IsNullOrWhiteSpace(NameTrainingseinheit))
            {
                SpeicherStatus = "Bitte einen Namen eingeben.";
                return;
            }
            if (DauerTrainingseinheit <= 0)
            {
                SpeicherStatus = "Bitte eine Dauer größer als 0 Minuten eingeben.";
                return;
            }
            // Das gespeicherte Objekt wird vom Eingabeobjekt getrennt.
            var neu = new Trainingseinheit
            {
                Name = NameTrainingseinheit.Trim(),
                Dauer = DauerTrainingseinheit
            };
            // Zugriff erfolgt ausschließlich über IDataAccess.
            bool ok = _dataAccess.SpeichereTrainingsEinheit(neu);
            SpeicherStatus = ok ? "Trainingseinheit erfolgreich gespeichert (Fake-DB)." : "Speichern fehlgeschlagen.";
            if (ok)
            {
                LadeTrainingseinheiten();
                _trainingsEinheit = new Trainingseinheit();
                MeldeAenderung(nameof(NameTrainingseinheit));
                MeldeAenderung(nameof(DauerTrainingseinheit));
            }
        }
        private void GibInfoAus()
        {
            // AUFGABE 4: Bei Auswahl wird das gespeicherte Training angezeigt.
            var te = AusgewaehlteTrainingseinheit ?? _trainingsEinheit;
            InfoText = $"Name: {te.Name}\nErstellt: {te.ErstellDatum:dd.MM.yyyy HH:mm:ss}\nDauer: {te.Dauer} Minuten";
        }
        private void LadeTrainingseinheiten()
        {
            // AUFGABE 5: Geladene Elemente werden in die gebundene Collection kopiert.
            TrainingsEinheitenListe.Clear();
            foreach (var te in _dataAccess.HoleTrainingsEinheiten())
                TrainingsEinheitenListe.Add(te);
            AusgewaehlteTrainingseinheit = null;
        }
    }
}
