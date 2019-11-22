using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PupilController : ControllerBase
    {
        private readonly IPupilRepo _pupils;

        public PupilController(IPupilRepo pupils)
        {
            _pupils = pupils;
        }



        /// <summary>
        /// updates a pupil
        /// </summary>
        /// <param name="pupilId">id of the pupil to be modified</param>
        /// <param name="dto">the modified pupil</param>
        [HttpPut("{pupilId}")]
        public ActionResult<PupilDTO> Put([FromBody] PupilDTO dto, long pupilId)
        {
            try
            {
                var p = _pupils.GetById(pupilId);

                p.FirstName = dto.FirstName;
                p.Surname = dto.Surname;

                _pupils.SaveChanges();

                return new PupilDTO(p);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Leerling niet gevonden"));
            }

        }


        /// <summary>
        /// Deletes a pupil
        /// </summary>
        /// <param name="pupilId">the id of the pupil to be deleted</param>
        [HttpDelete("{pupilId}")]
        public ActionResult<PupilDTO> Delete(long pupilId)
        {
            try
            {
                var delPupil = _pupils.GetById(pupilId);
                _pupils.Remove(delPupil);
                _pupils.SaveChanges();
                return new PupilDTO(delPupil);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Leerling niet gevonden"));
            }
        }


        /// <summary>
        /// Get the project with given id
        /// </summary>
        /// <param name="pupilId">the id of the project</param>
        /// <returns>The project</returns>
        [HttpGet("{pupilId}")]
        public ActionResult<PupilDTO> GetPupil(long pupilId)
        {
            try
            {
                return new PupilDTO(_pupils.GetById(pupilId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Leerling niet gevonden"));
            }
        }









    }
}
