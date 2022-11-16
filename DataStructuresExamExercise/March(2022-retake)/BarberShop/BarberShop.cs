using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BarberShop
{
    public class BarberShop : IBarberShop
    {
        private readonly List<Barber> barbers = new List<Barber>();
        private readonly List<Client> clients = new List<Client>();
        
        public void AddBarber(Barber b)
        {
            if(this.barbers.Contains(b))  throw new ArgumentException();
            this.barbers.Add(b);
        }

        public void AddClient(Client c)
        {
            if (this.clients.Contains(c)) throw new ArgumentException();
            this.clients.Add(c);
        }

        public bool Exist(Barber b)
        {
            if (this.barbers.Contains(b)) return true;
            return false;
        }

        public bool Exist(Client c)
        {
            if (this.clients.Contains(c)) return true;
            return false;
        }

        public IEnumerable<Barber> GetBarbers() => this.barbers;

        public IEnumerable<Client> GetClients() => this.clients;

        public void AssignClient(Barber b, Client c)
        {
            if (!this.barbers.Contains(b)) throw new ArgumentException();
            if (!this.clients.Contains(c)) throw new ArgumentException();
            
            b.Clients.Add(c);
            c.Barber = b;
        }

        public void DeleteAllClientsFrom(Barber b)
        {
            if (!this.barbers.Contains(b)) throw new ArgumentException();
            b.Clients.Clear();
        }

        public IEnumerable<Client> GetClientsWithNoBarber()
            => this.clients.Where(x => x.Barber == null);

        public IEnumerable<Barber> GetAllBarbersSortedWithClientsCountDesc()
            => this.barbers.OrderByDescending(x => x.Clients.Count);

        public IEnumerable<Barber> GetAllBarbersSortedWithStarsDecsendingAndHaircutPriceAsc()
            => this.barbers.OrderByDescending(x => x.Stars).ThenBy(x => x.HaircutPrice);

        public IEnumerable<Client> GetClientsSortedByAgeDescAndBarbersStarsDesc()
            => this.clients.OrderByDescending(x => x.Age).ThenByDescending(x => x.Barber.Stars);
    }
}
