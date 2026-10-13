using System.ComponentModel;

namespace GuiguemdeApp.Carillon.Model;

internal class Alarm(TimeSpan time, string? name) : INotifyPropertyChanged
{
    public TimeSpan Time { get; set; } = time; //les =date c'est pour les constructors
    public string? Name { get; set; } = name;
    
    private static int _nextId = 0;
    public int Id { get; } = Interlocked.Increment(ref _nextId);
    
    private bool _isActive { get; set; }= true;
    public bool IsActive {
        get => _isActive;
        set {
            if (_isActive == value)
                return;

            _isActive = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

