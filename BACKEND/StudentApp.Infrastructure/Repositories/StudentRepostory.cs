using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentApp.Core.DomainServices;
using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Infrastructure.Repositories
{
    public class StudentRepostory : IStudentRepostory
    {
        private readonly DbContextCore _dbContext;

        public StudentRepostory(DbContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateStudent(Student newStudent)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@in_id",SqlDbType.Int),
                    new SqlParameter("@in_f_name",SqlDbType.NVarChar),
                    new SqlParameter("@in_l_name",SqlDbType.NVarChar),
                    new SqlParameter("@in_birthday",SqlDbType.Date),
                    new SqlParameter("@in_students",SqlDbType.Int),
                    new SqlParameter("@in_status",SqlDbType.Int),
                    new SqlParameter("@out_result",SqlDbType.Int)
                };
                parameters[0].Value = newStudent.StudentId;
                parameters[1].Value = newStudent.FirstName;
                parameters[2].Value = newStudent.LastName;
                parameters[3].Value = newStudent.DateOfBirth;
                parameters[4].Value = newStudent.NumberOfSubjects;
                parameters[5].Value = 1;
                parameters[6].Direction = ParameterDirection.Output;

                const string sql = "EXEC CREATE_STUDENT @in_id, @in_f_name, @in_l_name, @in_birthday,@in_students,@in_status,@out_result OUTPUT";


                await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);

                var result = parameters[6].Value;

                if (result == null || result == DBNull.Value)
                {
                    throw new InvalidOperationException("Repository level returns a null value");
                }
                return (int)result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteStudent(int id)
        {
            try
            {
                var idParam = new SqlParameter("@Id", SqlDbType.Int);
                idParam.Value = id;

                const string sql = "EXEC DELETE_STUDENT_FROM_ID @Id";

                await _dbContext.Database.ExecuteSqlRawAsync(sql, idParam);
                return 1;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<List<Student>> GetAllStudents()
        {
            try
            {
                const string sql = "EXEC GET_ALL_STUDENTS";

                return await _dbContext.Set<Student>().FromSqlRaw(sql).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            try
            {
                var idParam = new SqlParameter("@Id", SqlDbType.Int);
                idParam.Value = studentId;

                const string sql = "EXEC GET_STUDENT_FROM_ID @Id";

                return _dbContext.Set<Student>().FromSqlRaw(sql, idParam).AsEnumerable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateStudent(Student newStudent)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@in_id",SqlDbType.Int),
                    new SqlParameter("@in_f_name",SqlDbType.NVarChar),
                    new SqlParameter("@in_l_name",SqlDbType.NVarChar),
                    new SqlParameter("@in_birthday",SqlDbType.Date),
                    new SqlParameter("@in_students",SqlDbType.Int),
                    new SqlParameter("@in_status",SqlDbType.Int),
                    new SqlParameter("@out_result",SqlDbType.Int)
                };

                parameters[0].Value = newStudent.StudentId;
                parameters[1].Value = newStudent.FirstName;
                parameters[2].Value = newStudent.LastName;
                parameters[3].Value = newStudent.DateOfBirth;
                parameters[4].Value = newStudent.NumberOfSubjects;
                parameters[5].Value = 1;
                parameters[6].Direction = ParameterDirection.Output;

                const string sql = "EXEC UPDATE_STUDENT @in_id, @in_f_name, @in_l_name, @in_birthday,@in_students,@in_status,@out_result OUTPUT";


                await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);

                var result = parameters[6].Value;

                if (result == null || result == DBNull.Value)
                {
                    throw new InvalidOperationException("Repository level returns a null value");
                }
                return (int)result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
