using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using GuiguemdeApp.Carillon.Model;
using GuiguemdeApp.Carillon.Service;

namespace GuiguemdeApp.Carillon.ViewModel;

public class SirenViewModel : BaseViewModel
{
    private readonly AlarmService _alarmService;
    private readonly AlarmScheduler _alarmScheduler;
    public ObservableCollection<Alarm> Alarms => _alarmService.Alarms; //pour l'affichage dans xaml
    public ICommand AddAlarmCommand { get; set; }
    public ICommand ToggleAlarmCommand { get; }
    public ICommand DeleteAlarmCommand { get; }

    public SirenViewModel(AlarmService alarmService, AlarmScheduler alarmScheduler) {
        _alarmService = alarmService;
        _alarmScheduler = alarmScheduler;
        AddAlarmCommand = new Command(AddAlarm);
        ToggleAlarmCommand = new Command<Alarm>(ToggleAlarm);
        DeleteAlarmCommand = new Command(DeleteAlarm);
        ScheduleAll();
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
    private void AddAlarm()
    {
        Alarm alarm = new Alarm(GetAlarmDate(Time), Name);

        _= _alarmService.AddAlarm(alarm);
        _alarmScheduler.Schedule(alarm);
    }

    private void ScheduleAll()
    {
        foreach (Alarm alarm in _alarmService.Alarms)
        {
            if (alarm.IsActive)
                _alarmScheduler.Schedule(alarm);
        }
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
        _= _alarmService.DeleteAlarm(SelectedAlarm);
        

        SelectedAlarm = null;
        Debug.WriteLine("Alarm deleted");
    }
}