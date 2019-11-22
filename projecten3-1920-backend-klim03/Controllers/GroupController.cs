using System;
using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.DTOs.AppDTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;

namespace projecten3_1920_backend_klim03.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepo _groups;

        public GroupController(IGroupRepo groups)
        {
            _groups = groups;
        }



        /// <summary>
        /// Get the group for a given id
        /// </summary>
        /// <param name="groupId">the id of the group</param>
        /// <returns>The project</returns>
        [HttpGet("{groupId}")]
        public ActionResult<GroupDTO> GetGroep(long groupId)
        {
            try
            {
                return new GroupDTO(_groups.GetById(groupId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Groep niet gevonden"));
            }
            
        }


        /// <summary>
        /// Get the group for a given groupCode with its order
        /// </summary>
        /// <param name="groupCode">the code of the group</param>
        /// <returns>The group with it's order</returns>
        [HttpGet("groupCode/{groupCode}")]
        public ActionResult<AppGroupDTO> GetGroepFromCode(string groupCode)
        {
            try
            {
                return new AppGroupDTO(_groups.GetByUniqueGroupCodeWithOrder(groupCode));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Groep niet gevonden"));
            }

        }

        /// <summary>
        /// Get the group with its project and order for a given groupCode
        /// </summary>
        /// <param name="groupCode">the code of the group</param>
        /// <returns>The group wit the project</returns>
        [HttpGet("project/{groupCode}")]
        public ActionResult<AppGroupDTO> GetGroupWithProjectAndOrder(string groupCode)
        {
            try
            {
                return new AppGroupDTO(_groups.GetByUniqueGroupCodeWithProjectAndOrder(groupCode));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Groep niet gevonden"));
            }

        }

        /// <summary>
        /// Adds an evaluation to a group
        /// </summary>
        /// <param name="dto">The evaluation to add</param>
        /// <param name="groupId">the id of the group</param>
        /// <returns>The added evaluation</returns>
        [HttpPost("addEvaluation/{groupId}")]
        public ActionResult<EvaluationDTO> AddEvaluation([FromBody]EvaluationDTO dto, long groupId)
        {
            try
            {
                dto.Extra = true; // evaluations that get addded this way are always extra

                Group s = _groups.GetByIdToAddEvaluation(groupId);
                Evaluation pt = new Evaluation(dto);

                pt.EvaluationCriterea = null;
                pt.EvaluationCritereaId = null;


                s.AddEvaluation(pt);
                _groups.SaveChanges();

                return new EvaluationDTO(pt);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Groep niet gevonden"));
            }
        }
        //GetByIdToEditEvaluation




        /// <summary>
        /// Edits an evaluation in a group
        /// </summary>
        /// <param name="dto">The evaluation to edit</param>
        /// <param name="groupId">the id of the group</param>
        /// <param name="evaluationId">the id of the evaluation</param>
        /// <returns>The edited evaluation</returns>
        [HttpPut("editEvaluation/{groupId}/{evaluationId}")]
        public ActionResult<EvaluationDTO> EditEvaluation([FromBody]EvaluationDTO dto, long groupId, long evaluationId)
        {
            try
            {
                Group s = _groups.GetByIdToEditEvaluation(groupId);
                var e = s.GetEvaluationById(evaluationId);
                if (e.Extra)
                {
                    e.Title = dto.Title;
                }
                e.DescriptionPupil = dto.DescriptionPupil;
                e.DescriptionPrivate = dto.DescriptionPrivate;

                _groups.SaveChanges();

                return new EvaluationDTO(e);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Groep niet gevonden"));
            }
        }


        /// <summary>
        /// Deletes an evaluation in a group
        /// </summary>
        /// <param name="groupId">the id of the group</param>
        /// <param name="evaluationId">the id of the evaluation</param>
        /// <returns>The deleted evaluation</returns>
        [HttpDelete("deleteEvaluation/{groupId}/{evaluationId}")]
        public ActionResult<EvaluationDTO> DeleteEvaluation(long groupId, long evaluationId)
        {
            try
            {
                Group s = _groups.GetByIdToEditEvaluation(groupId);
                var ev = s.GetEvaluationById(evaluationId);
                if (ev.Extra)
                {
                    s.RemoveEvaluationById(ev.EvaluationId);

                    _groups.SaveChanges();

                    return new EvaluationDTO(ev);
                } 
                return NotFound(new CustomErrorDTO("Een standaard evaluatie criteria kan je niet verwijderen!")); // dit moet een betere statuscode teruggeven
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Groep niet gevonden"));
            }
        }




    }
}
