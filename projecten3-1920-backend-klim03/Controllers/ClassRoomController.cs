using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")] // misschien niet nodig als je vanuit een groep gaat
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ClassRoomController : ControllerBase
    {
        private readonly IClassRoomRepo _classRooms;

        public ClassRoomController(IClassRoomRepo classRooms)
        {
            _classRooms = classRooms;
        }

        [AllowAnonymous]
        [HttpGet("{classroomId}")]
        public ActionResult<ClassRoomDTO> GetClassroom(long classroomId)
        {
            try
            {
                return new ClassRoomDTO(_classRooms.GetById(classroomId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("klas niet gevonden"));
            }

        }

        /// <summary>
        /// Get the classRoom with its projects for given id
        /// </summary>
        /// <param name="classRoomId">the id of the classroom</param>
        /// <returns>The classroom with its projects</returns>
        [AllowAnonymous]
        [HttpGet("withProjects/{classRoomId}")]
        public ActionResult<ClassRoomDTO> GetClassRoomWithProjects(long classRoomId)
        {
            try
            {
                return new ClassRoomDTO(_classRooms.GetByIdWithProjects(classRoomId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Klas niet gevonden"));
            }

        }

        /// <summary>
        /// Get the project from a classroom
        /// </summary>
        /// <param name="classRoomId">the id of the classroom</param>
        /// <returns>the projects of a classroom</returns>
        [AllowAnonymous]
        [HttpGet("projects/{classRoomId}")]
        public ActionResult<ICollection<ProjectDTO>> ProjectFromClassroom(long classRoomId)
        {
            try
            {
                return _classRooms.GetByIdWithProjects(classRoomId).Projects.Select(g => new ProjectDTO(g)).ToList();
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Klas niet gevonden"));
            }

        }


        /// <summary>
        /// Adding a project to a given classroom
        /// </summary>
        /// <param name="dto">The project details</param>
        /// <param name="classRoomId">the id of the classroom</param>
        /// <returns>The added project</returns>
        [AllowAnonymous]
        [HttpPost("addProject/{classRoomId}")]
        public ActionResult<ProjectDTO> AddProject([FromBody]ProjectDTO dto, long classRoomId)
        {
            try
            {
                ClassRoom cr = _classRooms.GetById(classRoomId);
                Project p = new Project(dto, cr.SchoolId);


                cr.AddProject(p);


                _classRooms.SaveChanges();
                return new ProjectDTO(p);
            }
            catch (ArgumentNullException)
            {

                return NotFound(new CustomErrorDTO("Klas niet gevonden"));
            }
           
        }

        /// <summary>
        /// Deletes a classroom
        /// </summary>
        /// <param name="classroomId">the id of the classroom to be deleted</param>
        [AllowAnonymous]
        [HttpDelete("{classroomId}")]
        public ActionResult<ClassRoomDTO> DeleteProject(long classroomId)
        {
            try
            {
                var delClassroom = _classRooms.GetById(classroomId);
                _classRooms.Remove(delClassroom);
                _classRooms.SaveChanges();
                return new ClassRoomDTO(delClassroom);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Product concept niet gevonden"));
            }

        }

        [AllowAnonymous]
        [HttpPost("addPupil/{classRoomId}")]
        public ActionResult<PupilDTO> AddPupil([FromBody]PupilDTO pupil, long classroomId)
        {
            try
            {
                ClassRoom cls = _classRooms.GetById(classroomId);
                Pupil pup = new Pupil(pupil, classroomId);
                cls.addPupil(pup);

                _classRooms.SaveChanges();

                return new PupilDTO(pup);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("klas niet gevonden"));
            }
        }
    }


}
