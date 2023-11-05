using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.ApplicationServices
{
    public interface IStudentService : IDisposable
    {
        Task<int> CreateStudent(int studentId, string firstName, string lastName, string dateOfBirth, int numberOfSubjects);
        Task<int> DeleteStudentById(int id);
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
        Task<int> UpdateStudent(int id, string firstName, string lastName, string dateOfBirth, int numberOfSubjects);
    }
}
