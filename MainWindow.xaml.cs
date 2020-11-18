using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace wpf_zadanie5
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Collection<Person> ProgramPersons { get; set; }
        public List<Person> APIPersons { get; set; }
        public Collection<string> Operations { get; set; } = new ObservableCollection<string>();
        RestClient restClient = new RestClient("http://dummy.restapiexample.com/api/v1");
        public APIObject apiObject { get; set; } = new APIObject();
        public MainWindow()
        {
            APIGetEmployees();
            InitializeComponent();
        }
        private void WindowLoad(object sender, RoutedEventArgs e)
        {
            PeopleList.ItemsSource = ProgramPersons;
            OperationList.ItemsSource = Operations;
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (PeopleList.SelectedIndex >= 0)
                ProgramPersons.RemoveAt(PeopleList.SelectedIndex);
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            ProgramPersons.Add(new Person());
            PeopleList.SelectedIndex = ProgramPersons.Count - 1;
            TextName.Text = "Name";
            TextSalary.Text = "0";
            TextAge.Text = "0";
            TextName.SelectAll();
            TextSalary.SelectAll();
            TextAge.SelectAll();
            TextName.Focus();
        }
        private void APIGetEmployees()
        {

            var request = new RestRequest(Method.GET);
            request.Resource = "employees";
            try
            {
                var responseData = restClient.ExecuteAsync(request);
                var response = responseData.Result;
                apiObject = JsonSerializer.Deserialize<APIObject>(response.Content);
                APIPersons = new List<Person>();
                foreach (Person person in apiObject.data)
                {
                    APIPersons.Add(new Person(person.id, person.employee_name, person.employee_salary, person.employee_age));
                }
                ProgramPersons = new ObservableCollection<Person>(apiObject.data);
                //var people = APIPersons.GetType().GetMethod("MemberwiseClone");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void APIAddEmployee(Person person)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "create";
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddJsonBody(person);
            try
            {
                var response = restClient.Execute(request);
                //MessageBox.Show(response.Content);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void APIUpdateEmployee(Person person)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = "update/{id}";
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddParameter("{id}", person.id);
            request.AddJsonBody(person);
            try
            {
                var response = restClient.Execute(request);
                //MessageBox.Show(response.Content);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void APIDeleteEmployee(string PersonID)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = "delete/{id}";
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddParameter("{id}", PersonID);
            try
            {
                var response = restClient.Execute(request);
                //MessageBox.Show(response.Content);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            Operations.Clear();
            Person tmpAPIPerson;
            foreach (Person tmpProgramPerson in ProgramPersons)
            {
                tmpAPIPerson = APIPersons.FirstOrDefault(y => y.id == tmpProgramPerson.id);
                if (tmpAPIPerson != null)
                {
                    if (!tmpProgramPerson.Equals(tmpAPIPerson))
                    {
                        //edycja
                        Operations.Add(tmpProgramPerson.ToString() + " | Operacja: PUT");
                        APIUpdateEmployee(tmpProgramPerson);
                    }
                }
                else
                {
                    //dodanie
                    Operations.Add(tmpProgramPerson.ToString() + " | Operacja: POST");
                    APIAddEmployee(tmpProgramPerson);
                }
                tmpAPIPerson = null;
            }
            Person tmpProgramPerson2;
            foreach (Person tmpAPIPerson2 in APIPersons)
            {
                tmpProgramPerson2 = ProgramPersons.FirstOrDefault(y => y.id == tmpAPIPerson2.id);
                if (tmpProgramPerson2 == null)
                {
                    //usuwanie
                    Operations.Add(tmpAPIPerson2.ToString() + " | Operacja: DELETE");
                    APIDeleteEmployee(tmpAPIPerson2.id);
                }
            }
            APIPersons.Clear();

            foreach (Person person in ProgramPersons)
            {
                APIPersons.Add(new Person(person.id, person.employee_name, person.employee_salary, person.employee_age));
            }
        }
    }
}

