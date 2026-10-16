using GuiguemdeApp.Carillon.View;

namespace GuiguemdeApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Carillon_Button_Clicked(object sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new CarillonShell();
    }
}