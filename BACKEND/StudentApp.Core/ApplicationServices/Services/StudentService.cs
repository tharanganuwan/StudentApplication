using StudentApp.Core.DomainServices;
using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.ApplicationServices.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepostory _studentRepostory;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepostory studentRepostory, IUnitOfWork unitOfWork)
        {
            _studentRepostory = studentRepostory;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateStudent(int studentId, string firstName, string lastName, string dateOfBirth, int numberOfSubjects)
        {
            using (_unitOfWork)
            {
                var transaction = _unitOfWork.BeginTransaction();
                try
                {
                    await transaction.CreateSavepointAsync("S1");
                    var student = await _studentRepostory.GetStudentById(studentId);

                    if (student != null)
                    {
                        await transaction.RollbackToSavepointAsync("S1");
                        return 0;
                    }

                    Student newStudent = new Student
                    {
                        StudentId = studentId,
                        FirstName = firstName,
                        LastName = lastName,
                        DateOfBirth = dateOfBirth,
                        NumberOfSubjects = numberOfSubjects,
                        Status = 1
                    };

                    var result = await _studentRepostory.CreateStudent(newStudent);

                    if (result == 1)
                    {
                        await _unitOfWork.SaveChanges();
                        await transaction.CommitAsync();
                        return result;
                    }

                    await transaction.RollbackToSavepointAsync("S1");
                    return result;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackToSavepointAsync("S1");
                    throw ex;
                }
            }
        }

        public async Task<int> DeleteStudentById(int id)
        {
            using (_unitOfWork)
            {
                var transaction = _unitOfWork.BeginTransaction();
                try
                {
                    await transaction.CreateSavepointAsync("S2");
                    var student = await _studentRepostory.GetStudentById(id);

                    if (student == null)
                    {
                        await transaction.RollbackToSavepointAsync("S2");
                        return 0;
                    }

                    var result = await _studentRepostory.DeleteStudent(id);



                    if (result == 1)
                    {
                        await _unitOfWork.SaveChanges();
                        await transaction.CommitAsync();
                        return result;
                    }

                    await transaction.RollbackToSavepointAsync("S2");
                    return result;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackToSavepointAsync("S2");
                    throw ex;
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<Student>> GetAllStudents()
        {
            try
            {
                var students = await _studentRepostory.GetAllStudents();
                return students;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Student> GetStudentById(int id)
        {
            try
            {
                var student = await _studentRepostory.GetStudentById(id);
                return student;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateStudent(int id, string firstName, string lastName, string dateOfBirth, int numberOfSubjects)
        {
            using (_unitOfWork)
            {
                var transaction = _unitOfWork.BeginTransaction();
                try
                {
                    await transaction.CreateSavepointAsync("S3");
                    var student = await _studentRepostory.GetStudentById(id);

                    if (student == null)
                    {
                        await transaction.RollbackToSavepointAsync("S3");
                        return 0;
                    }

                    Student newStudent = new Student
                    {
                        StudentId = id,
                        FirstName = firstName,
                        LastName = lastName,
                        DateOfBirth = dateOfBirth,
                        NumberOfSubjects = numberOfSubjects,
                    };

                    var result = await _studentRepostory.UpdateStudent(newStudent);

                    if (result == 1)
                    {
                        await _unitOfWork.SaveChanges();
                        await transaction.CommitAsync();
                        return result;
                    }

                    await transaction.RollbackToSavepointAsync("S3");
                    return result;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackToSavepointAsync("S3");
                    throw ex;
                }
            }
        }
    }
}
