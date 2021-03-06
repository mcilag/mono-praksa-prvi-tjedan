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
using StudentApplication.Common;



namespace StudentApplication.Controllers
{
    public class AdressController : ApiController
    {

        IAdressService _adressService;

        public AdressController(IAdressService adressService)
        {
            _adressService = adressService;
        }

        // GET 
        [HttpGet]
        [Route("api/Adress")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Sorter sorter, [FromUri] Pager pager, [FromUri] AdressFilter adressFilter)
        {
            List<Adress> adress = await _adressService.GetAdressesAsync(sorter, pager, adressFilter);

            if(adress == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no adresses");
            }
            return Request.CreateResponse(HttpStatusCode.OK, adress);
        }

        // GET by id
        [HttpGet]
        [Route("api/Adress/{Adress_id}")]
        public async Task<HttpResponseMessage> GetByIdAsync(int adress_id)
        {
            Adress adress = await _adressService.GetAdressByIdAsync(adress_id);

            if (adress == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, adress);
        }


        // POST 
        [HttpPost]
        [Route("api/Adress")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] Adress adress)
        {
            bool feedback = await _adressService.CreateAdressAsync(adress);
            if(feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Adress with given id already exists.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Adress is succesfully created");
        }




        // PUT for given id modifies adress 
        [HttpPut]
        [Route("api/Adress/{adress_id}")]
        public async Task<HttpResponseMessage> PutAsync(int adress_id, [FromBody] Adress adress)
        {
            bool feedback = await _adressService.UpdateAdressAsync(adress_id, adress);

            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Adress is successfully modified");
        }


        // DELETE 
        [HttpDelete]
        [Route("api/Adress/{adress_id}")]
        public async Task<HttpResponseMessage> DeleteAsync(int adress_id)
        {
            bool feedback = await _adressService.DeleteAdressAsync(adress_id);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id) + " or it is connected" +
                                                                         " to some student so it can not be deleted");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Adress with id " + adress_id + " is deleted");
        }
    }
}

