using GuiguemdeApp.Carillon.ViewModel;

namespace GuiguemdeApp.Carillon.View;

public partial class SirenView : ContentPage
{
    /// <summary>
    /// Initialize the Page and link it to the ViewModel
    /// </summary>
    /// <remarks>The ViewModel has a "special" constructor so it's written weird</remarks>
    /// <param name="viewModel"> a Siren View model </param>
    public SirenView(SirenViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
}