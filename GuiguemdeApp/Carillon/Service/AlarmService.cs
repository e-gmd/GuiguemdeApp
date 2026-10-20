using System.Collections.ObjectModel;
using GuiguemdeApp.Carillon.Model;


namespace GuiguemdeApp.Carillon.Service;

public class AlarmService
{
    public ObservableCollection<Alarm> Alarms { get; }

    public AlarmService()
    {
        Alarms = new ObservableCollection<Alarm>();
        TimeSpan time2 = new TimeSpan(18, 17, 0);
        TimeSpan time3 = new TimeSpan(12, 17,0);
        DateTime date1 = DateTime.Now;
        
        DateTime date2 = DateTime.Today.Add(time2);
        DateTime date3 = DateTime.Today.Add(time3);

        Alarms.Add(new Alarm(date1, "maintenant"){IsActive = false});
        Alarms.Add(new Alarm(date2, "Bus pour aller danser le jerk"){IsActive = false});
        Alarms.Add(new Alarm(date3, "Teuf maison de retraite"){IsActive = false});
        

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
