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
    
    private DateTime GetAlarmDate(TimeSpan time)
    {
        DateTime now = DateTime.Now;

        DateTime alarmDate = now.Date.Add(time);

        if (alarmDate <= now)
        {
            alarmDate = alarmDate.AddDays(1);
        }

        return alarmDate;
    }

    private async Task WaitAndTriggerAsync(Alarm alarm)
    {
        DateTime alarmDate = GetAlarmDate(alarm.Time);
        TimeSpan delay = alarmDate - DateTime.Now;

        Debug.WriteLine($"Alarme {alarm.Name} programmée dans {delay}.");
        alarm.Actif = true;
        
        var cts = new CancellationTokenSource();
        _scheduledAlarms.Add(alarm.Id, cts);
        await Task.Delay(delay, cts.Token);

        Debug.WriteLine($"DÉCLENCHEMENT : {alarm.Name} à {DateTime.Now}.");
        alarm.Actif = false;
        _scheduledAlarms.Remove(alarm.Id);
    }

    public void Cancel(Alarm alarm)
    {
        if (_scheduledAlarms.TryGetValue(alarm.Id, out var cts))
        {
            cts.Cancel();
            alarm.Actif = false;
            _scheduledAlarms.Remove(alarm.Id);
            Debug.WriteLine($"DÉSACTIVATION : {alarm.Name} à {DateTime.Now}.");
        }
    }
    
}