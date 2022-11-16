using System;
using System.Collections.Generic;

namespace TripAdministrations
{
    public class Company
    {
        public Company(string name, int tripOrganizationLimit)
        {
            this.Name = name;
            this.TripOrganizationLimit = tripOrganizationLimit;
        }

        public string Name { get; set; }

        public int TripOrganizationLimit { get; set; }

        public int CurrentTrips { get; set; }
    }
}
