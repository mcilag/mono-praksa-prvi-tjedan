using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentApplication.Controllers
{

    public class DictionaryofStudents
    {
        public static Dictionary<int, string> students = new Dictionary<int, string>();

    }

    public class StudentController : ApiController
    {
 
        // GET 
        [HttpGet]
        [Route("api/Student")]
        public HttpResponseMessage Get()
        {
            
            if(DictionaryofStudents.students.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no students");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, DictionaryofStudents.students);
            }
        }

        // GET with id
        [HttpGet]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Get(int id)
        {
            if(DictionaryofStudents.students.Count() < id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id: " + Convert.ToString(id));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, DictionaryofStudents.students[id]);
            }
        }

        // POST 
        [HttpPost]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Post(int id, [FromBody] string value)
        {
            if (DictionaryofStudents.students.Count() < id)
            {
                DictionaryofStudents.students.Add(id, value);
                return Request.CreateResponse(HttpStatusCode.OK, "Student with id " + Convert.ToString(id) + " is successfully added");
               
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with id " + Convert.ToString(id) + " already exists");
            }
        }

        // PUT 
        [HttpPut]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] string value)
        {
            if (DictionaryofStudents.students.Count() < id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id: " + Convert.ToString(id));
            }
            else
            {
                DictionaryofStudents.students[id] = value;
                return Request.CreateResponse(HttpStatusCode.OK, "Student with id " + Convert.ToString(id) + " is modified");
            }
        }

        // DELETE 
        [HttpDelete]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (DictionaryofStudents.students.Count() < id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id: " + Convert.ToString(id));
            }
            else
            {
                DictionaryofStudents.students.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Student with id " + Convert.ToString(id) + " is deleted");
            }
        }
    }

   


}

