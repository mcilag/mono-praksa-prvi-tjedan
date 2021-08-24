using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student.Model;
using Student._Service;
using System.Threading.Tasks;
using StudentService.Common;




namespace StudentApplication.Controllers
{
    public class StudentController : ApiController
    {

        IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET 
        [HttpGet]
        [Route("api/Student")]
        public async Task<HttpResponseMessage> GetAsync()
        {
            List<Students> student = await _studentService.GetStudentsAsync();
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no students");
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        // GET by id
        [HttpGet]
        [Route("api/Student/{Student_id}")]
        public async Task<HttpResponseMessage> GetByIdAsync(int student_id)
        {
            Students student = await _studentService.GetStudentByIdAsync(student_id);

            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no student with id " + Convert.ToString(student_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, student);
        }


        // POST 
        [HttpPost]
        [Route("api/Student")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] Students student)
        {
            bool feedback = await _studentService.CreateStudentAsync(student);
            if(feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(student.Adress_id) + " or" +
                                                                        " student with given id already exists.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Student is succesfully created");
        }




        // PUT for given id modifies student info
        [HttpPut]
        [Route("api/Student/{student_id}")]
        public async Task<HttpResponseMessage> PutAsync(int student_id, [FromBody] Students student)
        {
            bool feedback = await _studentService.UpdateStudentAsync(student_id, student);

            if (feedback != true)
            {
               return Request.CreateResponse(HttpStatusCode.NotFound, "There is no student with id " + Convert.ToString(student_id) + " or" +
                                                                        " there is no adress with id " + Convert.ToString(student.Adress_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Student is successfully modified");
        }


        // DELETE 
        [HttpDelete]
        [Route("api/Student/{student_id}")]
        public async Task<HttpResponseMessage> DeleteAsync(int student_id)
        {
            bool feedback = await _studentService.DeleteStudentAsync(student_id);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no student with id " + Convert.ToString(student_id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Student with id " + student_id + " is deleted");
        }
    }
}

        