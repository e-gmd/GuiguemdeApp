namespace GuiguemdeApp;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void Siren_Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SirenView");
    }
}