using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.DomainServices
{
    public interface IStudentRepostory : IDisposable
    {
        Task<int> CreateStudent(Student newStudent);
        Task<int> DeleteStudent(int id);
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentById(int studentId);
        Task<int> UpdateStudent(Student newStudent);
    }
}
