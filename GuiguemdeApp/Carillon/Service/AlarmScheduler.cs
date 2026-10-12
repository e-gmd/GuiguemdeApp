using System.Diagnostics;
using GuiguemdeApp.Carillon.Model;

namespace GuiguemdeApp.Carillon.Service;

internal class AlarmScheduler
{
    public void Schedule(Alarm alarm)
    {
        _ = WaitAndTriggerAsync(alarm);  //_= on previent qu'on s'en fiche du resultat
    }

    private async Task WaitAndTriggerAsync(Alarm alarm)
    {
        TimeSpan delay = alarm.Date - DateTime.Now;

        if (delay <= TimeSpan.Zero)
        {
            Debug.WriteLine($"L'alarme {alarm.Name} est dans le passé.");
            return;
        }

        Debug.WriteLine(
            $"Alarme {alarm.Name} programmée dans {delay}."
        );

        await Task.Delay(delay);

        Debug.WriteLine(
            $"DÉCLENCHEMENT : {alarm.Name} à {DateTime.Now}."
        );
    }
}