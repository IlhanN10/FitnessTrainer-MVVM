using System;
namespace Model
{
    /// <summary>
    /// AUFGABE 3: Das Model enthält Daten, aber keine Benutzeroberflächenlogik.
    /// Jede Trainingseinheit besitzt einen Namen, eine Dauer und ein Datum.
    /// </summary>
    public class Trainingseinheit
    {
        public string Name { get; set; } = string.Empty;
        // Zeitdauer der Trainingseinheit in Minuten.
        public int Dauer { get; set; }
        // Beim Erstellen eines neuen Objektes wird automatisch die Uhrzeit gesetzt.
        public DateTime ErstellDatum { get; set; } = DateTime.Now;
        public override string ToString() => Name;
    }
}
