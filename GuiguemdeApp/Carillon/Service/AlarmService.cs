using System.Collections.ObjectModel;
using GuiguemdeApp.Carillon.Model;


namespace GuiguemdeApp.Carillon.Service;

internal class AlarmService
{
    public ObservableCollection<Alarm> Alarms { get; }

    public AlarmService()
    {
        Alarms = new ObservableCollection<Alarm>();
        
        DateTime date1 = new DateTime(2010, 1, 1, 8, 0, 15);
        DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
        DateTime date3 = new DateTime(2026, 10, 17, 13, 00, 00);
        Alarms.Add(new Alarm(date1, "alarm1"));
        Alarms.Add(new Alarm(date2, "alarm2"));
        Alarms.Add(new Alarm(date3, "RÉVOLUTION"));
        

    }

    public void AddAlarm(Alarm alarm)
    {
        Alarms.Add(alarm);
    }
}
