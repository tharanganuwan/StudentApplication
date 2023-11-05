using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public int NumberOfSubjects { get; set; }
        public int Status { get; set; }
    }
}
