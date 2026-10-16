namespace GuiguemdeApp.Carillon.View;

public partial class CarillonShell : Shell
{
    public CarillonShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("SirenView", typeof(SirenView));
        Routing.RegisterRoute("TimerView", typeof(TimerView));
    }
    
    private void OnHomeClicked(object sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new MainPage();
    }
}
