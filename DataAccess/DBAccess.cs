using System;
using System.Collections.Generic;
using Interfaces;
using Model;
namespace DataAccess
{
    /// <summary>
    /// AUFGABEN 2, 5, 6, 7: Simulierte Datenbank.
    /// Implementiert IDataAccess. Die Liste liegt NUR im RAM und wird beim
    /// Beenden der Anwendung gelöscht; sie ist keine echte Datenbank.
    /// </summary>
    public sealed class DBAccess : IDataAccess
    {
        // Singleton-Pattern: Es gibt im Prozess genau eine DBAccess-Instanz.
        private static readonly Lazy<DBAccess> _instance = new(() => new DBAccess());
        private readonly List<Trainingseinheit> _trainingsEinheiten = new();
        // Privat: Andere Klassen können kein 'new DBAccess()' aufrufen.
        private DBAccess() { }
        public static DBAccess GetInstance() => _instance.Value;
        public bool SpeichereTrainingsEinheit(Trainingseinheit trainingseinheit)
        {
            if (trainingseinheit == null) return false;
            // Kopie anlegen, damit eine spätere Änderung am Formular das
            // bereits gespeicherte Training nicht versehentlich verändert.
            _trainingsEinheiten.Add(new Trainingseinheit
            {
                Name = trainingseinheit.Name,
                Dauer = trainingseinheit.Dauer,
                ErstellDatum = trainingseinheit.ErstellDatum
            });
            return true;
        }
        public List<Trainingseinheit> HoleTrainingsEinheiten()
        {
            var ergebnis = new List<Trainingseinheit>();
            foreach (var te in _trainingsEinheiten)
                ergebnis.Add(new Trainingseinheit
                {
                    Name = te.Name,
                    Dauer = te.Dauer,
                    ErstellDatum = te.ErstellDatum
                });
            return ergebnis;
        }
    }
}
