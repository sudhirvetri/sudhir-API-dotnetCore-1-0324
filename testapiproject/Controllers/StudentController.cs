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
        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {
            //Introducing DTO here instead of returning the data directly from the database.
            var studentsdto = new List<StudentDTO>();

            // foreach (var student in CollegeRepository.students)
            // {
            //     StudentDTO obj = new StudentDTO()
            //     {
            //         ID = student.ID,
            //         Name = student.Name,
            //         Email = student.Email,
            //         Phone = student.Phone
            //     };
            //     studentsdto.Add(obj);
            // }

            // the same StudentDTO is implemented using the LINQ query
            studentsdto = CollegeRepository.students.Select(x => new StudentDTO()
            {
                Name = x.Name,
                ID = x.ID,
                Email = x.Email,
                Phone = x.Phone
            }).ToList();

            return Ok(studentsdto);//Returning StudentDTO object as the output for ths Action Result. 
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(200, Type = typeof(Student))] // can document the type here or else near the ActionResult.
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> GetStudentById(int id)
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
            // 
            var studentdto = new StudentDTO()
            {
                ID = student.ID,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone
            };

            return Ok(studentdto);

        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        // instead of remembering each status of code they can also be written as below
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
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
            var studentdto = new StudentDTO()
            {
                ID = student.ID,
                Name = student.Name,
                Phone = student.Phone,
                Email = student.Email
            };
            return Ok(studentdto);

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

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            // if (CollegeRepository.students?.Any() != true)
            // {
            //     return BadRequest("No existing students found.");
            // }
            //int newId = CollegeRepository.students.LastOrDefault().ID + 1;
            var lastStudent = CollegeRepository.students.LastOrDefault();
            // Calculate the new ID
            int newId = (lastStudent?.ID ?? 0) + 1;
            Student student = new Student
            {
                ID = newId,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone
            };

            CollegeRepository.students.Add(student);
            model.ID = student.ID;

            return Ok(model);

        }



    }
}
