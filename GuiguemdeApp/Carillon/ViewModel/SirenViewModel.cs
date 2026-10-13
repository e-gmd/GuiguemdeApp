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

    public SirenViewModel() {
        _alarmService = new AlarmService();
        _alarmScheduler = new AlarmScheduler();
        AddAlarmCommand = new Command(AddAlarm);
        CancelAlarmCommand = new Command<Alarm>(CancelAlarm);
        
    }

    private Alarm selectedAlarm;// attribut viewmodel (mais de type alarm)

    public Alarm SelectedAlarm // fonction de viewmodel
    {
        get { return selectedAlarm; }
        set { selectedAlarm = value; OnPropertyChanged(); }
    }
    
    private string name;
    public string Name
    {
        get => name;
        set { name = value; OnPropertyChanged(); }
    }

    private string date;
    public string Date
    {
        get => date;
        set { date = value; OnPropertyChanged(); }
    }
    private void AddAlarm()
    {
        if (!DateTime.TryParse(Date, out DateTime alarmDate))
            return;
        
        Alarm alarm = new Alarm(alarmDate, Name);

        _alarmService.AddAlarm(alarm);
        _alarmScheduler.Schedule(alarm);
    }

    private void CancelAlarm(Alarm alarm)
    {
        _alarmScheduler.Cancel(alarm);
    }
}