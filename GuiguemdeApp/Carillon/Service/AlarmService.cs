using System.Collections.ObjectModel;
using GuiguemdeApp.Carillon.Model;


namespace GuiguemdeApp.Carillon.Service;

internal class AlarmService
{
    public ObservableCollection<Alarm> Alarms { get; }

    public AlarmService()
    {
        Alarms = new ObservableCollection<Alarm>();
        
        TimeSpan time1 = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        TimeSpan time2 = new TimeSpan(18, 17, 00);
        TimeSpan time3 = new TimeSpan(12, 17,00);

        Alarms.Add(new Alarm(time1, "maintenant"));
        Alarms.Add(new Alarm(time2, "Bus pour aller danser le jerk"));
        Alarms.Add(new Alarm(time3, "Teuf maison de retraite"));
        

    }

    public void AddAlarm(Alarm alarm)
    {
        Alarms.Add(alarm);
    }
}
