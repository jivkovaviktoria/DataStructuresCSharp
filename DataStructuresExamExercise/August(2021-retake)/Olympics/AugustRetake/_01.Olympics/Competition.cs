using System.Collections.Generic;

public class Competition
{
    public Competition(string name, int id, int score)
    {
        this.Name = name;
        this.Id = id;
        this.Score = score;
    }
    public Dictionary<int, Competitor> Map {get;}= new Dictionary<int, Competitor>();

    public int Id { get; set; }

    public string Name { get; set; }

    public int Score { get; set; }

    public ICollection<Competitor> Competitors => this.Map.Values;
}
