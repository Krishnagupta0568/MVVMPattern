using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MVVMPattern.Model
{
    public class EmployeeService
    {
        private static List<Employee> ObjEmployeesList;

        public EmployeeService()
        {
            ObjEmployeesList = new List<Employee>()
            {
                new Employee{ Id=101,  Name="Krishna Gupta", Age=21 }
            }; 
        }

        public List<Employee> GetAll()
        {
            return ObjEmployeesList;
        }

        public bool Add(Employee objNewEmployee)
        {
            if (objNewEmployee.Age < 21 || objNewEmployee.Age > 58)
                throw new ArgumentException("Invalid Age For Employee");
            ObjEmployeesList.Add(objNewEmployee);
            return true;
        }

        public bool Update(Employee objEmployeeToUpdate)
        {
            bool IsUpdated = false;
            for(int i =0; i<ObjEmployeesList.Count; i++)
            {
                if (ObjEmployeesList[i].Id == objEmployeeToUpdate.Id)
                {
                    ObjEmployeesList[i].Name = objEmployeeToUpdate.Name;
                    ObjEmployeesList[i].Age = objEmployeeToUpdate.Age;
                    IsUpdated = true;
                    break;
                }
            }
            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;
            for(int i = 0; i < ObjEmployeesList.Count; i++)
            {
                if(ObjEmployeesList[i].Id == id)
                {
                    ObjEmployeesList.RemoveAt(i);
                    IsDeleted = true;
                    break;
                }
            }
            return IsDeleted;
        }

        public Employee Search(int id)
        {
            return ObjEmployeesList.FirstOrDefault(e => e.Id == id);
        }
    }
}
