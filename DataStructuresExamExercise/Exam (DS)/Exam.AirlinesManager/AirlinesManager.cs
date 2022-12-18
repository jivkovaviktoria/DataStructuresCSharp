using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
          private HashSet<Airline> airlines = new HashSet<Airline>();
        private HashSet<Flight> flights = new HashSet<Flight>();
        private Dictionary<Airline, HashSet<Flight>> flightsByAirline = new Dictionary<Airline, HashSet<Flight>>();
        public void AddAirline(Airline airline)
        {
            this.airlines.Add(airline);
            this.flightsByAirline.Add(airline, new HashSet<Flight>());
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (this.airlines.Contains(airline) == false) throw new ArgumentException();

            this.flights.Add(flight);
            this.flightsByAirline[airline].Add(flight);
        }

        public bool Contains(Airline airline) => this.airlines.Contains(airline);

        public bool Contains(Flight flight) => this.flights.Contains(flight);

        public void DeleteAirline(Airline airline)
        {
            if (this.airlines.Contains(airline) == false) throw new ArgumentException();

            var toRemove = this.flightsByAirline[airline];

            this.flights = this.flights.Where(x => toRemove.Contains(x) == false).ToHashSet();

            this.airlines.Remove(airline);

            this.flightsByAirline.Remove(airline);
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        {
            return this.airlines.OrderByDescending(x => x.Rating).ThenByDescending(x => this.flightsByAirline[x].Count)
                .ThenBy(x => x.Name);
        }

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
        {
            return this.airlines.Where(x => this.flightsByAirline[x].Any(y => y.IsCompleted == false && y.Origin == origin && y.Destination == destination));
        }

        public IEnumerable<Flight> GetAllFlights() => this.flights;

        public IEnumerable<Flight> GetCompletedFlights() => this.flights.Where(x => x.IsCompleted);

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
        {
            return this.flights.OrderBy(x => x.IsCompleted).ThenBy(x => x.Number);
        }

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (this.airlines.Contains(airline) == false || this.flights.Contains(flight) == false || this.flightsByAirline[airline].Contains(flight) == false) throw new ArgumentException();

            flight.IsCompleted = true;
            return flight;
        }
    }
}