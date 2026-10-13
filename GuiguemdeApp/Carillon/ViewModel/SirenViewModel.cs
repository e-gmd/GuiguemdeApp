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
    public ICommand CancelAlarmCommand { get; set; }
    public ICommand ToggleAlarmCommand { get; }

    public SirenViewModel() {
        _alarmService = new AlarmService();
        _alarmScheduler = new AlarmScheduler();
        AddAlarmCommand = new Command(AddAlarm);
        CancelAlarmCommand = new Command<Alarm>(CancelAlarm);
        ToggleAlarmCommand = new Command<Alarm>(ToggleAlarm);
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

    private void CancelAlarm(Alarm alarm)
    {
        _alarmScheduler.Cancel(alarm);
    }

    private void ActiveAlarm(Alarm alarm)
    {
        _alarmScheduler.Schedule(alarm);
    }
    
    private void ToggleAlarm(Alarm alarm)
    {
        if (alarm == null)
            return;
        
        if(alarm.Actif){
            ActiveAlarm(alarm);}
        else
            CancelAlarm(alarm);
    }
    
    
}