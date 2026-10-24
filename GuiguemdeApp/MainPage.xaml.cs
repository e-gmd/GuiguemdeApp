using GuiguemdeApp.Carillon.View;

namespace GuiguemdeApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    
    private void Carillon_Button_Clicked(object sender, EventArgs e)
    {
        var app = Application.Current;

        if (app is null || app.Windows.Count == 0)
            return;

        app.Windows[0].Page = new CarillonShell();
    }

}