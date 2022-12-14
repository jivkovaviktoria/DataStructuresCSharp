using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IBoard
{
    private HashSet<Card> cards = new HashSet<Card>();
    
    public bool Contains(string name) => this.cards.Any(x => x.Name == name);

    public int Count() => this.cards.Count;

    public void Draw(Card card)
    {
        if (this.Contains(card.Name)) throw new ArgumentException();
        this.cards.Add(card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        return this.cards.Where(x => x.Score >= start && x.Score <= end).OrderByDescending(x => x.Level);
    }

    public void Heal(int health)
    {
        this.cards.OrderBy(x => x.Health).First().Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        return this.cards.Where(x => x.Name.StartsWith(prefix)).OrderBy(x => (int)x.Name[x.Name.Length - 1])
            .ThenBy(x => x.Level);
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        var attacker = this.cards.FirstOrDefault(x => x.Name == attackerCardName);
        var attacked = this.cards.FirstOrDefault(x => x.Name == attackedCardName);

        if (attacker == null || attacked == null) throw new ArgumentException();
        if (attacked.Level != attacker.Level) throw new ArgumentException();

        if (attacked.Health > 0 && attacker.Health > 0)
        {
            attacked.Health -= attacker.Damage;

            if (attacked.Health <= 0)
            {
                attacked.CanPlay = false;
                attacker.Score += attacked.Level;
            }
        }
    }

    public void Remove(string name)
    {
        if (this.Contains(name) == false) throw new ArgumentException();

        var card = this.cards.FirstOrDefault(x => x.Name == name);
        this.cards.Remove(card);
    }

    public void RemoveDeath()
    {
        this.cards = this.cards.Where(x => x.Health > 0).ToHashSet();
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return this.cards.Where(x => x.Level == level).OrderByDescending(x => x.Score);
    }
}