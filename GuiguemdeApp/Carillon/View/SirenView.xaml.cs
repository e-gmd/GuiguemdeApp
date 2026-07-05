using GuiguemdeApp.Carillon.ViewModel;

namespace GuiguemdeApp.Carillon.View;

public partial class SirenView : ContentPage
{
    public SirenView()
    {
        InitializeComponent();
        BindingContext = new SirenViewModel();

    }
}