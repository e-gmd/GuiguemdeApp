using System.Diagnostics;
using GuiguemdeApp.Carillon.Model;
using Plugin.Maui.Audio;

namespace GuiguemdeApp.Carillon.Service;

public class AlarmScheduler
{   
    /// <summary>
    /// Dictionary of the schedules alarms indexed by Id
    /// </summary>
    private readonly Dictionary<int, CancellationTokenSource> _scheduledAlarms = new();
    /// <summary>
    /// Audio handler (to be honest i don't really understand how it works
    /// </summary>
    private readonly IAudioManager _audioManager;
    private IAudioPlayer? _player;
    
    /// <summary>
    /// Set the audio manager
    /// </summary>
    public AlarmScheduler()
    {
        _audioManager = AudioManager.Current;
    }
    
    /// <summary>
    /// Launch the task, but we didn't want a Task type
    /// </summary>
    /// <param name="alarm"> The alarm we want to Schedule </param>
    public void Schedule(Alarm alarm)
    {
        _ = WaitAndTriggerAsync(alarm);  //_= on previent qu'on s'en fiche du resultat
    }
    
    /// <summary>
    /// The task that schedule the alarm, wait until it's time and ring for 2s
    /// It also remove the alarm from the list and set its IsActive to false
    /// </summary>
    /// <param name="alarm">The alarm we want to Schedule</param>
    private async Task WaitAndTriggerAsync(Alarm alarm)
    {
        TimeSpan delay = alarm.Date - DateTime.Now;

        Debug.WriteLine($"Alarme {alarm.Name} programmée dans {delay}.");
        alarm.IsActive = true;
        
        var cts = new CancellationTokenSource();
        _scheduledAlarms.Add(alarm.Id, cts);
        await Task.Delay(delay, cts.Token);

        Debug.WriteLine($"DÉCLENCHEMENT : {alarm.Name} à {DateTime.Now}.");
        alarm.IsActive = false;
        _scheduledAlarms.Remove(alarm.Id);
        PlaySoundAsync();
        await Task.Delay(2000); //2s
        StopSound();
        
    }

    /// <summary>
    /// Cancels the alarm waiting and remove it from the list
    /// </summary>
    /// <param name="alarm">the alarm to be canceled</param>
    public void Cancel(Alarm alarm)
    {
        if (_scheduledAlarms.TryGetValue(alarm.Id, out var cts))
        {
            cts.Cancel();
            _scheduledAlarms.Remove(alarm.Id);
            Debug.WriteLine($"DÉSACTIVATION : {alarm.Name} à {DateTime.Now}.");
        }
    }
    
    /// <summary>
    /// Play the sound 
    /// </summary>
    public async Task PlaySoundAsync()
    {
        if (_player == null)
        {
            var stream =
                await FileSystem.OpenAppPackageFileAsync("compoudrier.mp3");

            _player = _audioManager.CreatePlayer(stream);
            _player.Loop = true;
        }

        _player.Play();
    }

    /// <summary>
    /// Stop the sound
    /// </summary>
    public void StopSound()
    {
        _player?.Stop();
    }
    
}