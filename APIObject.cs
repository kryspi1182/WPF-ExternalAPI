using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_zadanie5
{
    public class APIObject
    {
        public string status { get; set; }
        public Collection<Person> data { get; set; }
    }
}
