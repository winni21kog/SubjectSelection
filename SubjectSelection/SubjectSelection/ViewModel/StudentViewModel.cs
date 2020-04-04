using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SubjectSelection.ViewModel
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthdayStr { get; set; }
        public string Email { get; set; }
    }
}