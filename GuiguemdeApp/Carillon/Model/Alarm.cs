namespace GuiguemdeApp.Carillon.Model;

internal class Alarm(DateTime date, string? name, int id) //
{
    public DateTime Date { get; set; } = date; //les =date c'est pour les constructors
    public string? Name { get; set; } = name;
    public int Id { get; set; } = id;

    // pour la simulation 
}

