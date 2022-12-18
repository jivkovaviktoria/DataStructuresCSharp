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

        private Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
        private Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
        public void AddAirline(Airline airline)
        {
            this.airlines.Add(airline);
            this.flightsByAirline.Add(airline, new HashSet<Flight>());
            
            if(this.Airlines.ContainsKey(airline.Id) == false) this.Airlines.Add(airline.Id, airline);
        }
 
        public void AddFlight(Airline airline, Flight flight)
        {
            if (this.Airlines.ContainsKey(airline.Id) == false) throw new ArgumentException();
 
            if(this.Flights.ContainsKey(flight.Id) == false) this.Flights.Add(flight.Id, flight);
            flight.Airline = airline;
            airline.Flights.Add(flight);
        }
 
        public bool Contains(Airline airline) => this.Airlines.ContainsKey(airline.Id);
 
        public bool Contains(Flight flight) => this.Flights.ContainsKey(flight.Id);
 
        public void DeleteAirline(Airline airline)
        {
            if (this.Airlines.ContainsKey(airline.Id) == false) throw new ArgumentException();

            var toRemove = airline.Flights;

            this.Flights = this.Flights.Where(x => toRemove.Contains(x.Value) == false).ToDictionary(x =>x.Key, x => x.Value);

            this.Airlines.Remove(airline.Id);
            airline.Flights.Clear();
        }
 
        // public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        // {
        //     return this.airlines.OrderByDescending(x => x.Rating).ThenByDescending(x => this.flightsByAirline[x].Count)
        //         .ThenBy(x => x.Name);
        // }
 
        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
        {
            var item = this.Airlines.Values.Where(a => a.Flights.Any(f => f.IsCompleted == false
                                                                      && f.Origin == origin
                                                                      && f.Destination == destination)).ToArray();
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
