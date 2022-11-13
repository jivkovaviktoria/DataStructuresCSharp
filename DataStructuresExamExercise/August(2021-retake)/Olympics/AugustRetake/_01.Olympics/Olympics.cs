using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private Dictionary<int, Competitor> competitors = new Dictionary<int, Competitor>();
    private Dictionary<int, Competition> competitions = new Dictionary<int, Competition>();
    private HashSet<string> competitorNames = new HashSet<string>();

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (this.competitions.ContainsKey(id)) throw new ArgumentException();

        this.competitions.Add(id, new Competition(name, id, participantsLimit));
    }

    public void AddCompetitor(int id, string name)
    {
        if (this.competitors.ContainsKey(id)) throw new ArgumentException();

        this.competitors.Add(id, new Competitor(id, name));
        this.competitorNames.Add(name);
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!this.competitors.ContainsKey(competitorId) || !this.competitions.ContainsKey(competitionId))
            throw new ArgumentException();

        this.competitions[competitionId].Map.Add(competitorId, this.competitors[competitorId]);
        this.competitors[competitorId].TotalScore += this.competitions[competitionId].Score;
    }

    public int CompetitionsCount() => this.competitions.Count;

    public int CompetitorsCount() => this.competitors.Count;

    public bool Contains(int competitionId, Competitor comp)
    {
        if (this.competitions.ContainsKey(competitionId) == false) throw new ArgumentException();
        if (this.competitors.ContainsKey(comp.Id) == false) throw new ArgumentException();
        return this.competitions[competitionId].Map.ContainsKey(comp.Id);
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!this.competitions.ContainsKey(competitionId) || !this.competitors.ContainsKey(competitorId))
            throw new ArgumentException();

        if (!this.competitions[competitionId].Map.ContainsKey(competitorId))
            throw new ArgumentException();

        this.competitions[competitionId].Map.Remove(competitorId);
        this.competitors[competitorId].TotalScore -= this.competitions[competitionId].Score;

    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
     => this.competitors.Values.Where(x => x.TotalScore > min && x.TotalScore <= max).OrderBy(x => x.Id);

    public IEnumerable<Competitor> GetByName(string name)
    {
        if (!this.competitorNames.Contains(name)) throw new ArgumentException();

        return this.competitors.Values.Where(x => x.Name == name).OrderBy(x => x.Id);
    }

    public Competition GetCompetition(int id)
    {
        if (!this.competitions.ContainsKey(id)) throw new ArgumentException();

        return this.competitions[id];

    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
     => this.competitors.Values.Where(x => x.Name.Length >= min && x.Name.Length <= max).OrderBy(x => x.Id);
}