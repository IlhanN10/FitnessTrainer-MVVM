using System.Collections.Generic;
using Model;
namespace Interfaces
{
    /// <summary>
    /// AUFGABE 6: Abstrakter Vertrag für einen Datenspeicher.
    /// Das ViewModel verwendet nur diese Schnittstelle, nicht die Datenbanktechnik.
    /// </summary>
    public interface IDataAccess
    {
        bool SpeichereTrainingsEinheit(Trainingseinheit trainingseinheit);
        List<Trainingseinheit> HoleTrainingsEinheiten();
    }
}
