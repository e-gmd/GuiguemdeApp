using System.Collections.ObjectModel;
using System.Windows.Input;
using GuiguemdeApp.Carillon.Model;

namespace GuiguemdeApp.Carillon.ViewModel;

internal class SirenViewModel : BaseViewModel
{
    public ObservableCollection<Alarm> Alarms { get; }
    public ICommand AddAlarmCommand { get; }
    

    public SirenViewModel()
    {
        Alarms = new ObservableCollection<Alarm>();
        DateTime date1 = new DateTime(2010, 1, 1, 8, 0, 15);
        DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
        Alarms.Add(new Alarm(date1, "alarm1", 1));
        Alarms.Add(new Alarm(date2, "alarm2", 2));
        
        AddAlarmCommand = new Command(AddAlarm);
    }

    private Alarm selectedAlarm;// attribut viewmodel (mais de type alarm)

    public Alarm SelectedAlarm // fonction de viewmodel
    {
        get { return selectedAlarm; }
        set
        {
            selectedAlarm = value;
            OnPropertyChanged();
        }
    }
    
    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }

    private string date;
    public string Date
    {
        get => date;
        set
        {
            date = value;
            OnPropertyChanged();
        }
    }
    private void AddAlarm()
    {
        if (!DateTime.TryParse(Date, out DateTime alarmDate))
            return;

        Alarms.Add(new Alarm(
            alarmDate,
            Name,
            Alarms.Count + 1));
    }
}