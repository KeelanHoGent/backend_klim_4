using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;

namespace projecten3_1920_backend_klim03.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ApplicationDomainController : ControllerBase
    {
        private readonly IApplicationDomainRepo _applicationDomains;

        public ApplicationDomainController(IApplicationDomainRepo applicationDomains)
        {
            _applicationDomains = applicationDomains;
        }

        /// <summary>
        /// Get all application domains
        /// </summary>
        /// <returns>list of application domains</returns>
        [HttpGet]
        public ActionResult<ICollection<ApplicationDomainDTO>> GetApplicationDomains()
        {

            return _applicationDomains.GetAll().Select(g => new ApplicationDomainDTO(g)).ToList();
        }


        /// <summary>
        /// Get the application domain with given id
        /// </summary>
        /// <param name="adId">the id of the application domain</param>
        /// <returns>The application domain</returns>
        [HttpGet("{adId}")]
        public ActionResult<ApplicationDomainDTO> GetProject(long adId)
        {
            try
            {
                return new ApplicationDomainDTO(_applicationDomains.GetById(adId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Toepassingsgebied niet gevonden"));
            }
            
        }


        /// <summary>
        /// Adding a application domain
        /// </summary>
        /// <param name="dto">The application domain details</param>
        /// <returns>The added application domain</returns>
        [HttpPost]
        public ActionResult<ApplicationDomainDTO> AddProject([FromBody]ApplicationDomainDTO dto)
        {

            ApplicationDomain ad = new ApplicationDomain { 
                ApplicationDomainName = dto.ApplicationDomainName,
                ApplicationDomainDescr = dto.ApplicationDomainDescr
            };
            _applicationDomains.Add(ad);
            _applicationDomains.SaveChanges();

            return new ApplicationDomainDTO(ad);
        }

        /// <summary>
        /// updates an application domain
        /// </summary>
        /// <param name="adId">id of the application domain to be modified</param>
        /// <param name="dto">the modified application domain</param>
        [HttpPut("{adId}")]
        public ActionResult<ApplicationDomainDTO> Put([FromBody]ApplicationDomainDTO dto, long adId)
        {
            try
            {
                var ad = _applicationDomains.GetById(adId);

                ad.ApplicationDomainName = dto.ApplicationDomainName;
                ad.ApplicationDomainDescr = dto.ApplicationDomainDescr;

                _applicationDomains.SaveChanges();
                return new ApplicationDomainDTO(ad);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Toepassingsgebied niet gevonden"));
            }
            
        }


        /// <summary>
        /// Deletes an application domain
        /// </summary>
        /// <param name="adId">the id of the application domain to be deleted</param>
        [HttpDelete("{adId}")]
        public ActionResult<ApplicationDomainDTO> DeleteProject(long adId)
        {
            try
            {
                var delAd = _applicationDomains.GetById(adId);
                _applicationDomains.Remove(delAd);
                _applicationDomains.SaveChanges();
                return new ApplicationDomainDTO(delAd);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Toepassingsgebied niet gevonden"));
            }
          
        }





    }
}
