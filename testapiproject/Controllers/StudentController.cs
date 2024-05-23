using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using testapiproject.Models;
using testapiproject.MyLogging;

namespace testapiproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("All")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {
            _logger.LogInformation("Inside the Get All method of student controller.");
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
                Phone = x.Phone,
                AdmissionDate = x.AdmissionDate
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            // use the below if condition if [APIController] is not avaialble. 
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);

            if (model == null)
            {
                return BadRequest();
            }

            // if (model.AdmissionDate < DateTime.Now)
            // {
            //     ModelState.AddModelError("AdmissionDate Error", "Admission date should be greater than or equal to Todays date");
            //     return BadRequest(ModelState);
            // }



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
                Phone = model.Phone,
                AdmissionDate = model.AdmissionDate
            };

            CollegeRepository.students.Add(student);
            model.ID = student.ID;

            //return Ok(model);// this return statement only returns 200 response however, if we need to return 201 we need to follow the below
            return CreatedAtRoute("GetStudentById", new { ID = model.ID }, model);

        }


        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.ID <= 0)
            {
                return BadRequest("No data found to update");
            }

            var existingStudent = CollegeRepository.students.Where(n => n.ID == model.ID).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound("No student with the provided ID exist in our repository");
            }

            existingStudent.Name = model.Name;
            existingStudent.Email = model.Email;
            existingStudent.Phone = model.Phone;

            return NoContent();

        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        //api/student/1/UpdatePartial
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                return BadRequest("No data found to update");
            }

            var existingStudent = CollegeRepository.students.Where(n => n.ID == id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound("No student with the provided ID exist in our repository");
            }

            var studentDTO = new StudentDTO
            {
                ID = existingStudent.ID,
                Name = existingStudent.Name,
                Email = existingStudent.Email,
                Phone = existingStudent.Phone
            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            existingStudent.Name = studentDTO.Name;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Phone = studentDTO.Phone;

            return NoContent();

        }
    }
}
