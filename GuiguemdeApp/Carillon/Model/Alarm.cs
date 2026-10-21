using System.ComponentModel;

namespace GuiguemdeApp.Carillon.Model;

/// <summary>
/// Main object of the App
/// </summary>
/// <param name="date"> Date when tthe alarm need to ring </param>
/// <param name="name"> Alarm's name</param>

public class Alarm(DateTime date, string? name) : INotifyPropertyChanged
{
    /// <summary>
    /// Date when tthe alarm need to ring </param>
    /// </summary>
    public DateTime Date { get; set; } = date; //les =date c'est pour les constructors
    /// <summary>
    /// Alarm's Name
    /// </summary>
    public string? Name { get; set; } = name;
    
    private static int _nextId = 0;
    /// <summary>
    /// Automaticly set alarm's id
    /// It's unique and starts at zero
    /// </summary>
    public int Id { get; } = Interlocked.Increment(ref _nextId);
    
    private bool _isActive { get; set; }= true;
    /// <summary>
    /// Boolean telling if the Alarm is active and can ring
    /// Directly related to the View
    /// </summary>
    public bool IsActive {
        get => _isActive;
        set {
            if (_isActive == value)
                return;

            _isActive = value;
            OnPropertyChanged();
        }
    }
    /// <summary>
    /// Handle the Changes from the View
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

