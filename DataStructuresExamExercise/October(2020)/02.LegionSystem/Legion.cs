using System.Collections;
using System.Collections.Concurrent;
using System.Linq;

namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private SortedSet<IEnemy> enemies = new SortedSet<IEnemy>();
        public int Size => this.enemies.Count();

        public bool Contains(IEnemy enemy)
        {
            return this.enemies.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            if(this.enemies.All(x => x.AttackSpeed != enemy.AttackSpeed)) this.enemies.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed) => this.enemies.FirstOrDefault(x => x.AttackSpeed == speed);

        public List<IEnemy> GetFaster(int speed)
            => this.enemies.Where(x => x.AttackSpeed > speed).ToList();

        public IEnemy GetFastest()
        {
            if (this.Size == 0) throw new InvalidOperationException("Legion has no enemies!");
            return this.enemies.First();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            return this.enemies.OrderByDescending(x => x.Health).ToArray();
        }
        public List<IEnemy> GetSlower(int speed) => this.enemies.Where(x => x.AttackSpeed < speed).ToList();

        public IEnemy GetSlowest()
        {
            if (this.Size == 0) throw new InvalidOperationException("Legion has no enemies!");
            return this.enemies.Last();
        }

        public void ShootFastest()
        {
            if(this.Size == 0) throw new InvalidOperationException("Legion has no enemies!");

            this.enemies.Remove(this.enemies.First());
        }

        public void ShootSlowest()
        {
            if(this.Size == 0) throw new InvalidOperationException("Legion has no enemies!");

            this.enemies.Remove(this.enemies.Last());
        }
        
        
    }
}
