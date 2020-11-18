using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace wpf_zadanie5
{
    public class Person : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string employee_name { get; set; }
        public string employee_salary { get; set; }
        public string employee_age { get; set; }
        public string profile_image { get; set; }

        public Person() { }

        public Person(string _id, string _name, string _salary, string _age)
        {
            id = _id;
            employee_name = _name;
            employee_salary = _salary;
            employee_age = _age;
        }
        public string Name
        {
            get { return employee_name; }
            set { employee_name = value; OnPropertyChanged("Info"); }
        }
        public string Salary
        {
            get { return employee_salary; }
            set { employee_salary = value; OnPropertyChanged("Info"); }
        }
        public string Age
        {
            get { return employee_age; }
            set { employee_age = value; OnPropertyChanged("Info"); }
        }
        public string Info
        {
            get
            {
                return Name + ", pensja: " + Salary + ", wiek: " + Age;
            }
        }
        public override string ToString()
        {
            return Name + ", pensja: " + Salary + ", wiek: " + Age;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                new PropertyChangedEventArgs(property));
        }
        public override bool Equals(object person)
        {
            Person obj = person as Person;
            if(obj!=null&&obj.employee_name==this.employee_name&&obj.employee_age==this.employee_age&&obj.employee_salary==this.employee_salary)
            {
                return true;
            }
            return false;
        }
    }
}
