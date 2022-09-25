using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using MVVMPattern.Model;
using MVVMPattern.Commands;
using System.Collections.ObjectModel;

namespace MVVMPattern.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implemantation
        //NOtify when Somthing Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
  
        EmployeeService ObjEmployeeService;

        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            currentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        #region DisplayOperation
        //Here i try with using normal list not working so need to change on Observable Collection
        //Built in Feture provided to change the notification
        private ObservableCollection<Employee> employeesList;
        public ObservableCollection<Employee> EmployeesList
        {
            get{ return employeesList; }
            set { employeesList = value;  OnPropertyChanged("EmployeesList"); }
        }

        private void LoadData()
        {
            EmployeesList = new ObservableCollection<Employee>(ObjEmployeeService.GetAll());
        }
        #endregion

        #region AddEmployeeOperation
        
        private Employee currentEmployee;

        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnPropertyChanged("CurrentEmployee"); }
        }

        #endregion

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        #region SaveEmployeeData
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                //List<Employee> newemp = new List<Employee>(); 
                //employeesList.Add(CurrentEmployee);
                var IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                {
                    Message = "Employee Saved";
                    LoadData();
                }
                else
                    Message = "Save Operation Failed";
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }

        }
        #endregion

        #region SearchEmployee

        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void Search()
        {
            try
            {
                var ObjEmployee = ObjEmployeeService.Search(currentEmployee.Id);
                if (ObjEmployee != null)
                {
                    //CurrentEmployee = ObjEmployee;
                    CurrentEmployee.Name = ObjEmployee.Name;
                    CurrentEmployee.Age = ObjEmployee.Age;
                }
                else
                {
                    Message = "Employee Not found";
                }
            }
            catch(Exception ex) { Message = ex.Message; }
        }
        #endregion

        #region Update Employee Data

        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                var IsUpdated = ObjEmployeeService.Update(CurrentEmployee);
                if(IsUpdated)
                {
                    Message = "Employee Updated";
                    LoadData();
                }
                else
                {
                    Message = "Employee Updated Failed";
                }
            }
            catch (Exception ex) { Message = ex.Message; }
        }

        #endregion

        #region Delete Employee Data

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        public void Delete()
        {
            try
            {
                var IsDelete = ObjEmployeeService.Delete(CurrentEmployee.Id);
                if (IsDelete)
                {
                    Message = "Employee Deleted";
                    LoadData();
                }
                else
                {
                    Message = "Employee Delete Process Failed";
                }
            }
            catch (Exception ex) { Message = ex.Message; }
        }

        #endregion

    }
}
