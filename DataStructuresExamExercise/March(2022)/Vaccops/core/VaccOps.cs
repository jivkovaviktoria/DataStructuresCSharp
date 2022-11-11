using System;
using System.Collections.Generic;
using System.Linq;

namespace VaccTests
{
    public class VaccOps
    {
        private readonly Dictionary<string, Doctor> _doctors = new Dictionary<string, Doctor>();
        private readonly Dictionary<string, Patient> _patients = new Dictionary<string, Patient>();
        private readonly Dictionary<string, HashSet<string>> _dMap = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, string> _pMap = new Dictionary<string, string>();

        public void AddDoctor(Doctor d)
        {
            if (this.Exist(d)) throw new ArgumentException();
            this._doctors[d.Name] = d;
            this._dMap[d.Name] = new HashSet<string>();
        }

        public void AddPatient(Doctor d, Patient p)
        {
            if (this.Exist(d) == false || this.Exist(p)) throw new ArgumentException();

            this._patients[p.Name] = p;
            this._dMap[d.Name].Add(p.Name);
            this._pMap[p.Name] = d.Name;
        }

        public IEnumerable<Doctor> GetDoctors() => this._doctors.Values;

        public IEnumerable<Patient> GetPatients() => this._patients.Values;

        public bool Exist(Doctor d) => this._doctors.ContainsKey(d.Name);

        public bool Exist(Patient p) => this._patients.ContainsKey(p.Name);

        public Doctor RemoveDoctor(string name)
        {
            if (this._doctors.ContainsKey(name) == false) throw new ArgumentException();

            var removedDoctor = this._doctors[name];
            var patientsToRemove = this._dMap[name];

            this._doctors.Remove(name);
            this._dMap.Remove(name);
            foreach (var p in patientsToRemove)
            {
                this._patients.Remove(p);
                this._pMap.Remove(p);
            }

            return removedDoctor;
        }

        public void ChangeDoctor(Doctor from, Doctor to, Patient p)
        {
            if (this.Exist(from) == false || this.Exist(to) == false || this.Exist(p) == false) throw new ArgumentException();
            this._dMap[from.Name].Remove(p.Name);
            this._dMap[to.Name].Add(p.Name);
            this._pMap[p.Name] = to.Name;
        }

        public IEnumerable<Doctor> GetDoctorsByPopularity(int populariry) => this.GetDoctors().Where(d => d.Popularity == populariry);

        public IEnumerable<Patient> GetPatientsByTown(string town) => this.GetPatients().Where(p => p.Town == town);

        public IEnumerable<Patient> GetPatientsInAgeRange(int lo, int hi) => this.GetPatients().Where(p => lo <= p.Age && p.Age <= hi);

        public IEnumerable<Doctor> GetDoctorsSortedByPatientsCountDescAndNameAsc() => this.GetDoctors().OrderByDescending(d => this._dMap[d.Name].Count).ThenBy(d => d.Name);

        public IEnumerable<Patient> GetPatientsSortedByDoctorsPopularityAscThenByHeightDescThenByAge() => this.GetPatients().OrderBy(p => this._doctors[this._pMap[p.Name]].Popularity).ThenByDescending(p => p.Height).ThenBy(p => p.Age);
    }
}
