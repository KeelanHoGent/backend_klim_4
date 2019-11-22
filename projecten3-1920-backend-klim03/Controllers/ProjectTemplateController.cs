using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
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
    public class ProjectTemplateController : ControllerBase
    {
        private readonly IProjectTemplateRepo _projectTemplates;

        public ProjectTemplateController(IProjectTemplateRepo projectTemplates)
        {
            _projectTemplates = projectTemplates;
        }

        /// <summary>
        /// Get the project template with given id
        /// </summary>
        /// <param name="projectTemplateId">the id of the project template</param>
        /// <returns>The project template</returns>
        [HttpGet("{projectTemplateId}")]
        public ActionResult<ProjectTemplateDTO> GetProjectTemplate(long projectTemplateId)
        {
            try
            {
                return new ProjectTemplateDTO(_projectTemplates.GetById(projectTemplateId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project concept niet gevonden"));
            }
            
        }

        /// <summary>
        /// Get the project templates with given schoolid
        /// </summary>
        /// <param name="schoolId">the id of the school</param>
        /// <returns>The project templates of the given school</returns>
        [HttpGet("projecttemplates/{schoolId}")]
        public ActionResult<ICollection<ProjectTemplateDTO>> GetProjectTemplates(long schoolId)
        {
            try
            {
                return _projectTemplates.GetAllBySchoolid(schoolId).Select(g => new ProjectTemplateDTO(g)).ToList();
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project concepten niet gevonden voor deze school"));
            }

        }

        /// <summary>
        /// updates a project template
        /// </summary>
        /// <param name="projectTemplateId">id of the project template to be modified</param>
        /// <param name="dto">the modified project template</param>
        [HttpPut("{projectTemplateId}")]
        public ActionResult<ProjectTemplateDTO> Put([FromBody] ProjectTemplateDTO dto, long projectTemplateId)
        {
            try
            {
                var pt = _projectTemplates.GetById(projectTemplateId);

                pt.ProjectName = dto.ProjectName;
                pt.ProjectDescr = dto.ProjectDescr;
                pt.ProjectImage = dto.ProjectImage;
                pt.ApplicationDomainId = dto.ApplicationDomainId;

                pt.UpdateProductTemplates(dto.ProductTemplates, true); // boolean(addedByGO) dependant on logged in user

                _projectTemplates.SaveChanges();
                return new ProjectTemplateDTO(pt);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project concept niet gevonden"));
            }
          
        }


        /// <summary>
        /// Deletes a project template
        /// </summary>
        /// <param name="projectTemplateId">the id of the project template to be deleted</param>
        [HttpDelete("{projectTemplateId}")]
        public ActionResult<ProjectTemplateDTO> DeleteProject(long projectTemplateId)
        {
            try
            {
                var delProjectTemplate = _projectTemplates.GetById(projectTemplateId);
                _projectTemplates.Remove(delProjectTemplate);
                _projectTemplates.SaveChanges();
                return new ProjectTemplateDTO(delProjectTemplate);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project concept niet gevonden"));
            }
        }


        /// <summary>
        /// gets a project based on a project template
        /// </summary>
        /// <param name="projectTemplateId">the id of the project template</param>
        [HttpGet("projectFromTemplate/{projectTemplateId}")]
        public ActionResult<ProjectDTO> GetProjectFromTemplate(long projectTemplateId)
        {
            try
            {
                return new ProjectDTO(new Project(_projectTemplates.GetById(projectTemplateId)));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Project concept niet gevonden"));
            }       
        }

    }
}
