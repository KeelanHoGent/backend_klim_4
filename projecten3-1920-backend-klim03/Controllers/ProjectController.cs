using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;

namespace projecten3_1920_backend_klim03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepo _projects;

        public ProjectController(IProjectRepo projects)
        {
            _projects = projects;
        }

        /// <summary>
        /// Get the project with given id
        /// </summary>
        /// <param name="projectId">the id of the project</param>
        /// <returns>The project</returns>
        [HttpGet("{projectId}")]
        public ActionResult<ProjectDTO> GetProject(long projectId)
        {          
            try
            {
                return new ProjectDTO(_projects.GetById(projectId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project niet gevonden"));
            }
        }

        /// <summary>
        /// Get the project with the its orders
        /// </summary>
        /// <param name="projectId">the id of the project</param>
        /// <returns>The groups of a project with their orders</returns>
        [HttpGet("groups/{projectId}")]
        public ActionResult<ICollection<GroupDTO>> GetGroups(long projectId)
        {
            try
            {
                return _projects.GetWithGroupsById(projectId).Groups.Select(g => new GroupDTO(g)).ToList();
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project niet gevonden"));
            }
            
        }


      
        /// <summary>
        /// Get the project with given id
        /// </summary>
        /// <param name="projectId">the id of the project</param>
        /// <returns>The project to diplay its progress</returns>
        [HttpGet("progress/{projectId}")]
        public ActionResult<ProjectDTO> GetProjectProgress(long projectId)
        {
            try
            {
                return new ProjectDTO(_projects.GetForProjectProgress(projectId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project niet gevonden"));
            }
        }

        /// <summary>
        /// Get product from a project
        /// </summary>
        /// <param name="projectId">the id of the project that contains the product</param>
        /// <param name="productId">the id of the expected product</param>
        /// <returns>The product</returns>
        [HttpGet("{projectId}/products/{productId}")]
        public ActionResult<ProductDTO> GetProductFromProject(long projectId, long productId)
        {
            try
            {
                return new ProductDTO(_projects.GetProductFromProject(projectId, productId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Product niet gevonden"));
            }
        }





        /// <summary>
        /// updates a project
        /// </summary>
        /// <param name="projectId">id of the project to be modified</param>
        /// <param name="dto">the modified project</param>
        [HttpPut("{projectId}")]
        public ActionResult<ProjectDTO> Put([FromBody] ProjectDTO dto, long projectId)
        {
            try
            {
                var p = _projects.GetById(projectId);

                p.ProjectName = dto.ProjectName;
                p.ProjectDescr = dto.ProjectDescr;
                p.ClassRoomId = dto.ClassRoomId;
                p.ApplicationDomainId = dto.ApplicationDomainId;

                p.UpdateProducts(dto.Products);
                p.UpdateGroups(dto.Groups, p.ClassRoom.SchoolId);

                _projects.SaveChanges();

                return new ProjectDTO(p);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project niet gevonden"));
            }
          
        }


        /// <summary>
        /// Deletes a project
        /// </summary>
        /// <param name="projectId">the id of the project to be deleted</param>
        [HttpDelete("{projectId}")]
        public ActionResult<ProjectDTO> DeleteProject(long projectId)
        {
            try
            {
                var delProject = _projects.GetById(projectId);
                _projects.Remove(delProject);
                _projects.SaveChanges();
                return new ProjectDTO(delProject);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project niet gevonden"));
            }
        }







    }
}
