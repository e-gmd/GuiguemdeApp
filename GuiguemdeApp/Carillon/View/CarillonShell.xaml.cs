namespace GuiguemdeApp.Carillon.View;

public partial class CarillonShell : Shell
{
    /// <summary>
    /// Initialize every Page handled by this shell
    /// </summary>
    public CarillonShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("SirenView", typeof(SirenView));
        Routing.RegisterRoute("TimerView", typeof(TimerView));
    }
    /// <summary>
    /// Method to go back to the main page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnHomeClicked(object sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new MainPage();
    }
}
