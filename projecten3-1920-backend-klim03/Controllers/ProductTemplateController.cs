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
    public class ProductTemplateController : ControllerBase
    {
        private readonly IProductTemplateRepo _productTemplates;

        public ProductTemplateController(IProductTemplateRepo productTemplates)
        {
            _productTemplates = productTemplates;
        }



        /// <summary>
        /// Get the availiable productTemplates for a given school id 
        /// </summary>
        /// <param name="schoolId">the id of the school</param>
        /// <returns>The product templates of the school and those that are added by the GO</returns>
        [HttpGet("getAllProductTemplatesForSchool/{schoolId}")]
        public ActionResult<ICollection<ProductTemplateDTO>> GetClassroomwithAllProductTemplates(long schoolId)
        {
            try
            {
                return _productTemplates.GetBySchoolIdWithTemplatesAndGoTemplates(schoolId).Select(g => new ProductTemplateDTO(g)).ToList();
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("School niet gevonden"));
            }
        }

        /// <summary>
        /// Get the product template with given id
        /// </summary>
        /// <param name="productTemplateId">the id of the project template</param>
        /// <returns>The product template</returns>
        [HttpGet("{productTemplateId}")]
        public ActionResult<ProductTemplateDTO> GetProjectTemplate(long productTemplateId)
        {
            try
            {
                return new ProductTemplateDTO(_productTemplates.GetById(productTemplateId));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Product concept niet gevonden"));
            }

        }


        /// <summary>
        /// updates a product template
        /// </summary>
        /// <param name="productTemplateId">id of the project template to be modified</param>
        /// <param name="dto">the modified project template</param>
        [HttpPut("{productTemplateId}")]
        public ActionResult<ProductTemplateDTO> Put([FromBody] ProductTemplateDTO dto, long productTemplateId)
        {
            try
            {
                var pt = _productTemplates.GetById(productTemplateId);

                pt.ProductImage = dto.ProductImage;
                pt.ProductName = dto.ProductName;
                pt.Description = dto.Description;
                pt.CategoryTemplateId = dto.CategoryTemplateId;

                pt.UpdateVariations(dto.ProductVariationTemplates);

                _productTemplates.SaveChanges();
                return new ProductTemplateDTO(pt);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("product concept niet gevonden"));
            }

        }



        /// <summary>
        /// Deletes a project template
        /// </summary>
        /// <param name="productTemplateId">the id of the project template to be deleted</param>
        [HttpDelete("{productTemplateId}")]
        public ActionResult<ProductTemplateDTO> DeleteProject(long productTemplateId)
        {
            try
            {
                var delProductTemplate = _productTemplates.GetById(productTemplateId);
                _productTemplates.Remove(delProductTemplate);
                _productTemplates.SaveChanges();
                return new ProductTemplateDTO(delProductTemplate);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Product concept niet gevonden"));
            }
        }





    }
}
