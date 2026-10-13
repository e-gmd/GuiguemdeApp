using System.Collections.ObjectModel;
using GuiguemdeApp.Carillon.Model;


namespace GuiguemdeApp.Carillon.Service;

internal class AlarmService
{
    public ObservableCollection<Alarm> Alarms { get; }

    public AlarmService()
    {
        Alarms = new ObservableCollection<Alarm>();
        
        TimeSpan time1 = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute+1, 0);
        TimeSpan time2 = new TimeSpan(18, 17, 0);
        TimeSpan time3 = new TimeSpan(12, 17,0);

        Alarms.Add(new Alarm(time1, "maintenant"){IsActive = false});
        Alarms.Add(new Alarm(time2, "Bus pour aller danser le jerk"){IsActive = false});
        Alarms.Add(new Alarm(time3, "Teuf maison de retraite"){IsActive = false});
        

    }

    public void AddAlarm(Alarm alarm)
    {
        Alarms.Add(alarm);
    }

    public void DeleteAlarm(Alarm alarm)
    {
        Alarms.Remove(alarm);
    }
}
