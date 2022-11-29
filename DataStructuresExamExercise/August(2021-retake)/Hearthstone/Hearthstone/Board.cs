using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IBoard
{
    private HashSet<string> cardNames = new HashSet<string>();
    private HashSet<Card> cards = new HashSet<Card>();
    public bool Contains(string name) => this.cardNames.Contains(name);

    public int Count() => this.cardNames.Count;

    public void Draw(Card card)
    {
        if (this.Contains(card.Name)) throw new ArgumentException();
        this.cardNames.Add(card.Name);
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

        if (attacked.Health > 0)
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
        this.cardNames.Remove(name);
    }

    public void RemoveDeath()
    {
        var toRemove = this.cards.Where(x => x.Health <= 0);

        foreach (var card in toRemove)
        {
            this.cards.Remove(card);
            this.cardNames.Remove(card.Name);
        }
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return this.cards.Where(x => x.Level == level).OrderByDescending(x => x.Score);
    }
}