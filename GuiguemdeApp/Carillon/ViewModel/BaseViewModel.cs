using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GuiguemdeApp.Carillon.ViewModel;

/// <summary>
/// Commune base of all App's ViewModels 
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    /// <summary>
    /// To link the view to the instances
    /// </summary>
    /// <param name="propertyName">instance that have been changed</param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName=null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}