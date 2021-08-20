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



namespace StudentApplication.Controllers
{
    public class StudentController : ApiController
    {

        StudentService studentService = new StudentService();

        // GET 
        [HttpGet]
        [Route("api/Student")]
        public HttpResponseMessage Get()
        {
            List<Students> student = studentService.GetStudents();
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no students");
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        // GET by id
        [HttpGet]
        [Route("api/Student/{Student_id}")]
        public HttpResponseMessage GetById(int student_id)
        {
            Students student = studentService.GetStudentById(student_id);

            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no student with id " + Convert.ToString(student_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, student);
        }


        // POST 
        [HttpPost]
        [Route("api/Student")]
        public HttpResponseMessage Post([FromBody] Students student)
        {
            bool feedback = studentService.CreateStudent(student);
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
        public HttpResponseMessage Put(int student_id, [FromBody] Students student)
        {
            bool feedback = studentService.UpdateStudent(student_id, student);

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
        public HttpResponseMessage Delete(int student_id)
        {
            bool feedback = studentService.DeleteStudent(student_id);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no student with id " + Convert.ToString(student_id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Student with id " + student_id + " is deleted");
        }
    }
}

        