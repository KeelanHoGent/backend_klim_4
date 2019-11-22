using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projecten3_1920_backend_klim03.Domain.Models.DTOs.CustomDTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using projecten3_1920_backend_klim03.helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PdfGenController : ControllerBase
    {
        private readonly IProjectRepo _projects;
        private readonly PdfGenerator _pdfGenerator;

        public PdfGenController(IProjectRepo projects, PdfGenerator pdfGenerator)
        {
            _projects = projects;
            _pdfGenerator = pdfGenerator;
        }




        /// <summary>
        /// Generate a pdf with evaluations
        /// </summary>
        /// <param name="projectId">the id of the project</param>
        /// <param name="showTeacher">if the teacher evaluation should be shown</param>
        /// <param name="showPupil">if the pupil evaluation should be shown</param>
        /// <param name="groupNums">the numbers of the groups to show in the pdf</param>
        /// <returns>The pdf file</returns>
        [HttpGet("custompdf/evaluaties")]
        public ActionResult GetCustompdf([FromQuery(Name = "projectId")] long  projectId, [FromQuery(Name = "showPupil")] bool showPupil
            , [FromQuery(Name = "showTeacher")] bool showTeacher, [FromQuery(Name = "groupNums")] string groupNums)
        {
            return File(_pdfGenerator.GenerateCustomPdf(_projects.GetForProjectProgress(projectId), new PdfSettings{
                GroupsToShow = JsonConvert.DeserializeObject<List<long>>(groupNums),
                ShowPupil = showPupil,
                ShowTeacher = showTeacher
            }), "application/pdf"); 
     
        }


        
       /* [HttpGet("custompdf/{projectId}")]
        public ActionResult GetExcel(long projectId)
        {

          






        }*/



    }
}
