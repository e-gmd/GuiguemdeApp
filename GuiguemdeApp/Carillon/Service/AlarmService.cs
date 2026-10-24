using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using GuiguemdeApp.Carillon.Model;


namespace GuiguemdeApp.Carillon.Service;

/// <summary>
/// Manage all the Alarms of the App
/// Deals with the loading, saving and Alarm changes
/// </summary>
public class AlarmService
{
    /// <summary>
    /// Saved Alarm's list
    /// </summary>
    public ObservableCollection<Alarm> Alarms { get; }
    /// <summary>
    /// File where the alarms are saved
    /// </summary>
    private readonly string _filePath;
    /// <summary>
    /// To have a clean and pretty back-up file
    /// </summary>
    private readonly JsonSerializerOptions _options;
    /// <summary>
    /// Initialize everything above
    /// Load the Alarms from the back-up file 
    /// </summary>
    public AlarmService()
    {
        Alarms = new ObservableCollection<Alarm>();
        _filePath = Path.Combine(FileSystem.AppDataDirectory, "alarms.json");                       // c'est comme ça qu'on fait les chemin qui plantent pas les émulateur
        //Process.Start("open", FileSystem.AppDataDirectory);                                       // vers fichier bakup
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
    
    /// <summary>
    /// Add an alarm to the List and save it in the backup file
    /// </summary>
    /// <param name="alarm"> The Alarm to be added </param>
    public async Task AddAlarm(Alarm alarm)
    {
        Alarms.Add(alarm);
        await SaveAsync();
    }
    
    /// <summary>
    /// Delete an alarm from the List and from the backup file
    /// </summary>
    /// <param name="alarm">The Alarm to be deleted</param>
    public async Task DeleteAlarm(Alarm alarm)
    {
        Alarms.Remove(alarm);
        await SaveAsync();
    }
    
    /// <summary>
    /// Load every alarm from the backup file to the Alarm list.
    /// Of course if the file don't exist or is empty is does nothing
    /// <remarks>THE ALARMS ARE NOT SCHEDULE</remarks>
    /// </summary>
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
    
    /// <summary>
    /// Save al the alarms in the backup file.
    /// </summary>
    private async Task SaveAsync()
    {
        string json = JsonSerializer.Serialize(Alarms,  _options);
        await File.WriteAllTextAsync(_filePath, json);
    }
}
