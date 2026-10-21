using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using GuiguemdeApp.Carillon.Model;


namespace GuiguemdeApp.Carillon.Service;

public class AlarmService
{
    public ObservableCollection<Alarm> Alarms { get; }
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options;

    public AlarmService()
    {
        Alarms = new ObservableCollection<Alarm>();
        _filePath = Path.Combine(FileSystem.AppDataDirectory, "alarms.json");                       // c'est comme ça qu'on fait les chemin qui plantent pas les émulateur
        Process.Start("open", FileSystem.AppDataDirectory);
        _options = new(JsonSerializerDefaults.Web) { WriteIndented = true };
        _= LoadAsync();
        
        /*TimeSpan time2 = new TimeSpan(18, 17, 0);
        TimeSpan time3 = new TimeSpan(12, 17,0);
        DateTime date1 = DateTime.Now;
        
        DateTime date2 = DateTime.Today.Add(time2);
        DateTime date3 = DateTime.Today.Add(time3);

        Alarms.Add(new Alarm(date1, "maintenant"){IsActive = false});
        Alarms.Add(new Alarm(date2, "Bus pour aller danser le jerk"){IsActive = false});
        Alarms.Add(new Alarm(date3, "Teuf maison de retraite"){IsActive = false});
        */

    }

    public async Task AddAlarm(Alarm alarm)
    {
        Alarms.Add(alarm);
        await SaveAsync();
    }

    public async Task DeleteAlarm(Alarm alarm)
    {
        Alarms.Remove(alarm);
        await SaveAsync();
    }
    
    public async Task LoadAsync()
    {
        if (!File.Exists(_filePath))
            return;

        string json = await File.ReadAllTextAsync(_filePath);

        var loadedAlarms = JsonSerializer.Deserialize<List<Alarm>>(json, _options);
        if (loadedAlarms == null)
            return;
        
        Alarms.Clear();

        foreach (Alarm alarm in loadedAlarms)
            Alarms.Add(alarm);
    }
    
    private async Task SaveAsync()
    {
        string json = JsonSerializer.Serialize(Alarms,  _options);

        await File.WriteAllTextAsync(_filePath, json);
    }
}
