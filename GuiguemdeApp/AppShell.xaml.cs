using GuiguemdeApp.Carillon.View;

namespace GuiguemdeApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("SirenView", typeof(SirenView));
    }
}