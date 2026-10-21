using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using GuiguemdeApp.Carillon.Model;
using GuiguemdeApp.Carillon.Service;

namespace GuiguemdeApp.Carillon.ViewModel;
/// <summary>
/// Give the command used on the Siren page
/// </summary>
public class SirenViewModel : BaseViewModel
{
    /// <summary>
    /// Alarm service
    /// </summary>
    private readonly AlarmService _alarmService;
    /// <summary>
    /// Alarm scheduler
    /// </summary>
    private readonly AlarmScheduler _alarmScheduler;
    /// <summary>
    /// To be used on the xaml
    /// </summary>
    public ObservableCollection<Alarm> Alarms => _alarmService.Alarms;
    public ICommand AddAlarmCommand { get; set; }
    public ICommand ToggleAlarmCommand { get; }
    public ICommand DeleteAlarmCommand { get; }
    /// <summary>
    /// Set of the commands ONE BY ONE
    /// Schedule all the active alarm
    /// </summary>
    /// <param name="alarmService">Unique alarmService of the app</param>
    /// <param name="alarmScheduler">Unique alarmScheduler of the app</param>
    public SirenViewModel(AlarmService alarmService, AlarmScheduler alarmScheduler) {
        _alarmService = alarmService;
        _alarmScheduler = alarmScheduler;
        AddAlarmCommand = new Command(AddAlarm);
        ToggleAlarmCommand = new Command<Alarm>(ToggleAlarm);
        DeleteAlarmCommand = new Command(DeleteAlarm);
        ScheduleAll();
    }
    
    /// <summary>
    /// Alarm selected on the View
    /// </summary>
    private Alarm selectedAlarm;// attribut viewmodel (mais de type alarm)
    public Alarm SelectedAlarm // fonction de viewmodel
    {
        get { return selectedAlarm; }
        set { selectedAlarm = value; OnPropertyChanged(); }
    }
    
    /// <summary>
    /// Name in the name selector 
    /// </summary>
    private string name = "Alarme";
    public string Name
    {
        get => name;
        set { name = value; OnPropertyChanged(); }
    }
    
    /// <summary>
    /// Time in the Time selector
    /// </summary>
    private TimeSpan time = DateTime.Now.TimeOfDay;

    public TimeSpan Time
    {
        get => time;
        set { time = value; OnPropertyChanged(); }
    }
    /// <summary>
    /// To get a date from a Timestamp
    /// If the time is already passed today, it is set for tomorrow
    /// </summary>
    /// <param name="time">Time of the alarm</param>
    /// <returns>the DateTime of the alarm</returns>
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
    
    /// <summary>
    /// Create an alarm from the selector's infos
    /// Add it to the alarm service and schedule it
    /// </summary>
    private void AddAlarm()
    {
        Alarm alarm = new Alarm(GetAlarmDate(Time), Name);

        _= _alarmService.AddAlarm(alarm);
        _alarmScheduler.Schedule(alarm);
    }

    /// <summary>
    /// Schedule all the active alarms
    /// </summary>
    private void ScheduleAll()
    {
        foreach (Alarm alarm in _alarmService.Alarms)
        {
            if (alarm.IsActive)
                _alarmScheduler.Schedule(alarm);
        }
    }
    /// <summary>
    /// It schedules or unschedules an alarm.
    /// </summary>
    /// <param name="alarm">The alarm that have been activated/deactivated</param>
    private void ToggleAlarm(Alarm alarm)
    {
        if (alarm == null)
            return;
        
        if(alarm.IsActive)
            _alarmScheduler.Schedule(alarm);
        else
            _alarmScheduler.Cancel(alarm);
    }

    /// <summary>
    /// Delete the selected alarm and cancel the scheduler
    /// <remarks> IsActive is set to false because "onPropertyChanged" from the toggle button is trigger</remarks>
    /// </summary>
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