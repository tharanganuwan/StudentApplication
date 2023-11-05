using System.ComponentModel.DataAnnotations;
using System;

namespace StudentApplication.Dtos
{
    public class CreateStudentDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public int NumberOfSubjects { get; set; }
    }
}
