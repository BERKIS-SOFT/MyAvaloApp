using MyAvaloApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAvaloApp.Services
{
    public interface IEmployeService
    {
        List<Employe> GetEmployeList();

        void Add(Employe employe);
        void Update(Employe employe);
        void Delete(int employeId);
        Employe GetById(int id);
    }
}
