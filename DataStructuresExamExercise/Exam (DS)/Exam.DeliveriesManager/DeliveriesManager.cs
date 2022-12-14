using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        private HashSet<Deliverer> deliverers = new HashSet<Deliverer>();
        private HashSet<Package> packages = new HashSet<Package>();

        private Dictionary<Deliverer, HashSet<Package>> packagesByDeliverer =
            new Dictionary<Deliverer, HashSet<Package>>();

        private HashSet<Package> assigned = new HashSet<Package>();
        public void AddDeliverer(Deliverer deliverer)
        {
            this.deliverers.Add(deliverer);
            this.packagesByDeliverer.Add(deliverer, new HashSet<Package>());
        }

        public void AddPackage(Package package)
        {
            this.packages.Add(package);
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if (this.deliverers.Contains(deliverer) == false || this.packages.Contains(package) == false)
                throw new ArgumentException();
            
            this.packagesByDeliverer[deliverer].Add(package);
            this.assigned.Add(package);
        }

        public bool Contains(Deliverer deliverer) => this.deliverers.Contains(deliverer);

        public bool Contains(Package package) => this.packages.Contains(package);

        public IEnumerable<Deliverer> GetDeliverers() => this.deliverers;

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
        {
            return this.packagesByDeliverer.Keys.OrderByDescending(x => this.packagesByDeliverer[x].Count).ThenBy(x => x.Name);
        }

        public IEnumerable<Package> GetPackages() => this.packages;

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver()
        {
            return this.packages.OrderByDescending(x => x.Weight).ThenBy(x => x.Receiver);
        }

        public IEnumerable<Package> GetUnassignedPackages()
        {
            return this.packages
                .Where(x => this.assigned.Contains(x) == false);
        }
    }
}
