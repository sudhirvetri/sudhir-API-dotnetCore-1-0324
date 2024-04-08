using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testapiproject.Models;

namespace testapiproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All")]
        public IEnumerable<Student> GetStudent()
        {
            return CollegeRepository.students;
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.students.Where(n => n.ID == id).FirstOrDefault();

        }
        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        public Student GetStudentByName(string name)
        {
            return CollegeRepository.students.Where(n => n.Name == name).FirstOrDefault();

        }

        [HttpDelete("{id:int}", Name = "DeleteById")]
        public bool DeleteStudenById(int id)
        {
            var student = CollegeRepository.students.Where(n => n.ID == id).FirstOrDefault();
            CollegeRepository.students.Remove(student);
            return true;

        }


    }
}
