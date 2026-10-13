using System.Diagnostics;
using GuiguemdeApp.Carillon.Model;

namespace GuiguemdeApp.Carillon.Service;

internal class AlarmScheduler
{ 
    private readonly Dictionary<int, CancellationTokenSource> _scheduledAlarms = new();
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

        Debug.WriteLine($"Alarme {alarm.Name} programmée dans {delay}.");
        alarm.Actif = true;
        
        var cts = new CancellationTokenSource();
        _scheduledAlarms.Add(alarm.Id, cts);
        await Task.Delay(delay, cts.Token);

        Debug.WriteLine($"DÉCLENCHEMENT : {alarm.Name} à {DateTime.Now}.");
        alarm.Actif = false;
    }

    public void Cancel(Alarm alarm)
    {
        if (_scheduledAlarms.TryGetValue(alarm.Id, out var cts))
        {
            cts.Cancel();
            alarm.Actif = false;
            Debug.WriteLine($"DÉSACTIVATION : {alarm.Name} à {DateTime.Now}.");
        }
    }
    
}