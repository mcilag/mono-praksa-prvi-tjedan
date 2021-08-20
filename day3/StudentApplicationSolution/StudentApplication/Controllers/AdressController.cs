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
    public class AdressController : ApiController
    {

        AdressService adressService = new AdressService();

        // GET 
        [HttpGet]
        [Route("api/Adress")]
        public HttpResponseMessage Get()
        {
            List<Adress> adress = adressService.GetAdresses();

            if(adress == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no adresses");
            }
            return Request.CreateResponse(HttpStatusCode.OK, adress);
        }

        // GET by id
        [HttpGet]
        [Route("api/Adress/{Adress_id}")]
        public HttpResponseMessage GetById(int adress_id)
        {
            Adress adress = adressService.GetAdressById(adress_id);

            if (adress == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, adress);
        }


        // POST 
        [HttpPost]
        [Route("api/Adress")]
        public HttpResponseMessage Post([FromBody] Adress adress)
        {
            bool feedback = adressService.CreateAdress(adress);
            if(feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Adress with given id already exists.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Adress is succesfully created");
        }




        // PUT for given id modifies adress 
        [HttpPut]
        [Route("api/Adress/{adress_id}")]
        public HttpResponseMessage Put(int adress_id, [FromBody] Adress adress)
        {
            bool feedback = adressService.UpdateAdress(adress_id, adress);

            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Adress is successfully modified");
        }


        // DELETE 
        [HttpDelete]
        [Route("api/Adress/{adress_id}")]
        public HttpResponseMessage Delete(int adress_id)
        {
            bool feedback = adressService.DeleteAdress(adress_id);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id) + " or it is connected" +
                                                                         " to some student so it can not be deleted");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Adress with id " + adress_id + " is deleted");
        }
    }
}

