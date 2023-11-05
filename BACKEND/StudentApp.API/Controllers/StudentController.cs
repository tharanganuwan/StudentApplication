using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApp.API.Dtos;
using StudentApp.Core.ApplicationServices;
using StudentApplication.Dtos;

namespace StudentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        [Route("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto student)
        {
            try
            {
                string response;
                if (student.StudentId == 0 || student.FirstName == null)
                {
                    response = "student Data can not be null";
                    return BadRequest(response);
                }

                var result = await _studentService.CreateStudent(student.StudentId, student.FirstName, student.LastName, student.DateOfBirth, student.NumberOfSubjects);

                if (result == 0)
                {
                    response = "Student alrady use this Id";
                    return BadRequest(response);
                }
                if (result == 1)
                {
                    response = "Student create successfull...!";
                    return Ok(new
                    {
                        StatusCode = 200,
                        Response = response,
                    });
                }
                response = "somethig is wrong";
                return BadRequest(response);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("GetStudentById")]
        public async Task<IActionResult> GetStudent(int id)
        {
            try
            {
                string response;

                var student = await _studentService.GetStudentById(id);

                if (student == null)
                {
                    response = "No student in this ID..!";
                    return BadRequest(response);
                }
                return Ok(student);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                string response;
                var student = await _studentService.GetAllStudents();

                if (student.Count == 0)
                {
                    response = "No students...!";
                    return BadRequest(response);
                }
                return Ok(student);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto student)
        {
            try
            {
                string response;
                if (student == null || id == null)
                {
                    response = "student and ID Data can not be null";
                    return BadRequest(response);
                }

                var result = await _studentService.UpdateStudent(id, student.FirstName, student.LastName, student.DateOfBirth, student.NumberOfSubjects);

                if (result == 0)
                {
                    response = "Student not in the system..!";
                    return BadRequest(response);
                }
                if (result == 1)
                {
                    response = "Student update successfull...!";
                    return Ok(new
                    {
                        StatusCode = 200,
                        Response = response,
                    });
                }
                response = "somethig is wrong";
                return BadRequest(response);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut]
        [Route("DeleteStudent")]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            try
            {
                string response;

                var result = await _studentService.DeleteStudentById(id);

                if (result == 0)
                {
                    response = "Student is not in the system..!";
                    return BadRequest(response);
                }
                if (result == 1)
                {
                    response = "Student Delete successfull...!";
                    return Ok(new
                    {
                        StatusCode = 200,
                        Response = response,
                    });
                }
                response = "somethig is wrong";
                return BadRequest(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
