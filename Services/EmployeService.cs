using MyAvaloApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyAvaloApp.Services
{
    public class EmployeService
    {
        private ObservableCollection<Employe> _employes = new ObservableCollection<Employe>();
        private int nextId = 1;

        // Renvoie directement la collection observable
        public ObservableCollection<Employe> GetEmployeList() => _employes;

        public void Add(Employe employe)
        {
            employe.Id = nextId++;
            _employes.Add(employe);
        }

        public void Delete(int employeId)
        {
            var employe = GetById(employeId);
            if (employe != null)
            {
                _employes.Remove(employe);
            }
        }

        public Employe GetById(int id)
        {
            return _employes.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Employe employe)
        {
            var existingEmploye = GetById(employe.Id);
            if (existingEmploye != null)
            {
                _employes.Remove(existingEmploye); // Supprime l'ancien employé
                _employes.Add(employe); // Ajoute l'employé mis à jour
            }
        }
    }
}
