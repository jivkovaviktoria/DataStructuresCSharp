namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new List<IWeapon>();
        }

        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon) => this.weapons.Add(weapon);
        
        public IWeapon GetById(int id)
        {

            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Id == id) return this.weapons[i];
            }

            return null;
        }

        public bool Contains(IWeapon weapon) => this.weapons.Contains(weapon);
        
        public int Refill(IWeapon weapon, int ammunition)
        {
            this.IsValidWeapon(weapon);

            if ((weapon.Ammunition + ammunition) > weapon.MaxCapacity) weapon.Ammunition = weapon.MaxCapacity;
            else weapon.Ammunition += ammunition;

            return weapon.Ammunition;
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            this.IsValidWeapon(weapon);

            if ((weapon.Ammunition - ammunition) < 0) return false;

            weapon.Ammunition -= ammunition;
            return true;
        }

        public IWeapon RemoveById(int id)
        {
            IWeapon weapon = null;
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Id == id)
                {
                    weapon = this.weapons[i];
                    this.weapons.RemoveAt(i);
                    break;
                }
            }

            if (weapon == null)
              throw new InvalidOperationException("Weapon does not exist in inventory!");

            return weapon;
        }

        public void Clear() => this.weapons.Clear();
        
        public List<IWeapon> RetrieveAll() => new List<IWeapon>(this.weapons);
        
        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int indexOfFirst = this.weapons.IndexOf(firstWeapon);
            this.IsValidIndex(indexOfFirst);
            int indexOfSecond = this.weapons.IndexOf(secondWeapon);
            this.IsValidIndex(indexOfSecond);

            if (firstWeapon.Category == secondWeapon.Category)
            {
                this.weapons[indexOfFirst] = secondWeapon;
                this.weapons[indexOfSecond] = firstWeapon;
            }
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            var result = new List<IWeapon>(this.weapons.Capacity);
            int lowerBoundIndex = (int)lower;
            int upperBoundIndex = (int)upper;

            for (int i = 0; i < this.Capacity; i++)
            {
                var current = this.weapons[i];
                int currentIndex = (int)current.Category;
                if (currentIndex >= lowerBoundIndex && currentIndex <= upperBoundIndex)
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Category == category)
                    this.weapons[i].Ammunition = 0;
            }
        }

        public int RemoveHeavy() => this.weapons.RemoveAll(w => w.Category == Category.Heavy);

        public IEnumerator GetEnumerator() => this.weapons.GetEnumerator();
        
        private void IsValidWeapon(IWeapon weapon)
        {
            var existing = this.GetById(weapon.Id);

            if (existing == null)
                throw new InvalidOperationException("Weapon does not exist in inventory!");
        }

        private void IsValidIndex(int index)
        {
            if (index == -1)
                throw new InvalidOperationException("Weapon does not exist in inventory!");
        }
    }
}
