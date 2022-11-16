using System;
using System.Collections.Generic;
using System.Linq;

namespace TripAdministrations
{
    public class TripAdministrator : ITripAdministrator
    {
        private readonly List<Company> companies = new List<Company>();
        private readonly List<Trip> trips = new List<Trip>();

        private readonly Dictionary<Company, List<Trip>> tMap = new Dictionary<Company, List<Trip>>();
        private readonly Dictionary<Trip, Company> cMap = new Dictionary<Trip, Company>();

        public void AddCompany(Company c)
        {
            if (this.companies.Contains(c)) throw new ArgumentException();

            this.companies.Add(c);
            this.tMap.Add(c, new List<Trip>());
        }

        public void AddTrip(Company c, Trip t)
        {
            if (this.companies.Contains(c) == false) throw new ArgumentException();

            this.trips.Add(t);
            this.tMap[c].Add(t);

            this.cMap.Add(t, c);
            c.CurrentTrips++;
        }

        public bool Exist(Company c) => this.companies.Contains(c);
        public bool Exist(Trip t) => this.trips.Contains(t);

        public void RemoveCompany(Company c)
        {
            if (this.companies.Contains(c))
            {
                this.companies.Remove(c);
                var toRemove = this.cMap.Where(x => x.Value == c);

                foreach (var x in toRemove)
                {
                    this.cMap.Remove(x.Key);
                    this.trips.Remove(x.Key);
                }

                this.tMap.Remove(c);
            }
            else throw new ArgumentException();
        }

        public IEnumerable<Company> GetCompanies() => this.companies;

        public IEnumerable<Trip> GetTrips() => this.trips;

        public void ExecuteTrip(Company c, Trip t)
        {
            if (this.companies.Contains(c) == false) throw new ArgumentException();
            if (this.trips.Contains(t) == false) throw new ArgumentException();
            if (this.tMap[c].Contains(t) == false) throw new ArgumentException();

            c.CurrentTrips--;
            this.trips.Remove(t);
            this.tMap[c].Remove(t);
            this.cMap.Remove(t);
        }

        public IEnumerable<Company> GetCompaniesWithMoreThatNTrips(int n)
            => this.tMap.Keys.Where(x => x.CurrentTrips > n);

        public IEnumerable<Trip> GetTripsWithTransportationType(Transportation t)
            => this.trips.Where(x => x.Transportation == t);

        public IEnumerable<Trip> GetAllTripsInPriceRange(int lo, int hi)
            => this.trips.Where(x => x.Price >= lo && x.Price <= hi);
    }
}