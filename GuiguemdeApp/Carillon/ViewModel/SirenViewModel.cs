using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using GuiguemdeApp.Carillon.Model;
using GuiguemdeApp.Carillon.Service;

namespace GuiguemdeApp.Carillon.ViewModel;

internal class SirenViewModel : BaseViewModel
{
    public AlarmService _alarmService { get; set; }
    private readonly AlarmScheduler _alarmScheduler;
    public ObservableCollection<Alarm> Alarms => _alarmService.Alarms; //pour l'affichage dans xaml
    public ICommand AddAlarmCommand { get; set; }
    public ICommand ToggleAlarmCommand { get; }
    public ICommand DeleteAlarmCommand { get; }

    public SirenViewModel() {
        _alarmService = new AlarmService();
        _alarmScheduler = new AlarmScheduler();
        AddAlarmCommand = new Command(AddAlarm);
        ToggleAlarmCommand = new Command<Alarm>(ToggleAlarm);
        DeleteAlarmCommand = new Command(DeleteAlarm);
    }

    private Alarm selectedAlarm;// attribut viewmodel (mais de type alarm)

    public Alarm SelectedAlarm // fonction de viewmodel
    {
        get { return selectedAlarm; }
        set { selectedAlarm = value; OnPropertyChanged(); }
    }
    
    private string name = "Alarme";
    public string Name
    {
        get => name;
        set { name = value; OnPropertyChanged(); }
    }
    
    
    private TimeSpan time = DateTime.Now.TimeOfDay;

    public TimeSpan Time
    {
        get => time;
        set { time = value; OnPropertyChanged(); }
    }
    private void AddAlarm()
    {
        Alarm alarm = new Alarm(Time, Name);

        _alarmService.AddAlarm(alarm);
        _alarmScheduler.Schedule(alarm);
    }
    
    private void ToggleAlarm(Alarm alarm)
    {
        if (alarm == null)
            return;
        
        if(alarm.IsActive)
            _alarmScheduler.Schedule(alarm);
        else
            _alarmScheduler.Cancel(alarm);
    }

    private void DeleteAlarm()
    {
        if (SelectedAlarm == null)
            return;
        SelectedAlarm.IsActive = false;                 //parce que Toggle s'affole sinon
        _alarmScheduler.Cancel(SelectedAlarm);
        _alarmService.DeleteAlarm(SelectedAlarm);
        

        SelectedAlarm = null;
        Debug.WriteLine("Alarm deleted");
    }
}