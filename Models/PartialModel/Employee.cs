using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BrightWorks.Models
{
    public partial class Employee
    {
        [DisplayName("Full Name")]
        public string FullName { get { return Fname + " " + Lname; } }
    }
}