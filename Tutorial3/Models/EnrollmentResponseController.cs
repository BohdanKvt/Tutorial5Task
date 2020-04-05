using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tutorial3.Models
{
    public class EnrollmentResponseController
    {
        public int Semester { get; set; }
        public string Studies { get; set; }
    }
}