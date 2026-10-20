using System.Diagnostics;
using GuiguemdeApp.Carillon.Model;
using Plugin.Maui.Audio;

namespace GuiguemdeApp.Carillon.Service;

internal class AlarmScheduler
{ 
    private readonly Dictionary<int, CancellationTokenSource> _scheduledAlarms = new();
    private readonly IAudioManager _audioManager;
    private IAudioPlayer? _player;
    
    public AlarmScheduler()
    {
        _audioManager = AudioManager.Current;
    }
    
    public void Schedule(Alarm alarm)
    {
        _ = WaitAndTriggerAsync(alarm);  //_= on previent qu'on s'en fiche du resultat
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

    private async Task WaitAndTriggerAsync(Alarm alarm)
    {
        DateTime alarmDate = GetAlarmDate(alarm.Time);
        TimeSpan delay = alarmDate - DateTime.Now;

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

    public void Cancel(Alarm alarm)
    {
        if (_scheduledAlarms.TryGetValue(alarm.Id, out var cts))
        {
            cts.Cancel();
            _scheduledAlarms.Remove(alarm.Id);
            Debug.WriteLine($"DÉSACTIVATION : {alarm.Name} à {DateTime.Now}.");
        }
    }
    
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

    public void StopSound()
    {
        _player?.Stop();
    }
    
}