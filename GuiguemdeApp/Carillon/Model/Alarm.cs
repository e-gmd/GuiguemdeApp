using System.ComponentModel;

namespace GuiguemdeApp.Carillon.Model;

internal class Alarm(DateTime date, string? name) : INotifyPropertyChanged
{
    public DateTime Date { get; set; } = date; //les =date c'est pour les constructors
    public string? Name { get; set; } = name;
    
    private static int _nextId = 0;
    public int Id { get; } = Interlocked.Increment(ref _nextId);
    
    private bool _actif;
    public bool Actif {
        get => _actif;
        set {
            if (_actif == value)
                return;

            _actif = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

