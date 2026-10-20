using GuiguemdeApp.Carillon.ViewModel;

namespace GuiguemdeApp.Carillon.View;

public partial class SirenView : ContentPage
{
    public SirenView(SirenViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
}