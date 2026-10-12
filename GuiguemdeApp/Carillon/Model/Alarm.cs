namespace GuiguemdeApp.Carillon.Model;

internal class Alarm(DateTime date, string? name) //
{
    public DateTime Date { get; set; } = date; //les =date c'est pour les constructors
    public string? Name { get; set; } = name;

    // pour la simulation 
}

