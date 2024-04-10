using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public ActionResult<IEnumerable<Student>> GetStudent()
        {
            return Ok(CollegeRepository.students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(200, Type = typeof(Student))] // can document the type here or else near the ActionResult.
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult GetStudentById(int id)
        {
            //Bad Request -400 bad request
            if (id <= 0)
            {
                return BadRequest();
            }
            var student = CollegeRepository.students.Where(n => n.ID == id).FirstOrDefault();
            //Not found - 404 - not found - client Error
            if (student == null)
            {
                return NotFound($"The student with id {id} not found");
            }

            return Ok(student);

        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        // instead of remembering each status of code they can also be written as below
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var student = CollegeRepository.students.Where(n => n.Name == name).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"The student with name {name} not found");
            }
            return Ok(student);

        }

        [HttpDelete("{id:int}", Name = "DeleteById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<bool> DeleteStudenById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var student = CollegeRepository.students.Where(n => n.ID == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"The student with the id {id} does not exist"); ;
            }
            CollegeRepository.students.Remove(student);
            return Ok(true);

        }


    }
}
