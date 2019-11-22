using projecten3_1920_backend_klim03.Domain.Models.Domain.enums;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain
{
    public class Project
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }
        public string ProjectDescr { get; set; }
        public string ProjectImage { get; set; }
        public decimal ProjectBudget { get; set; }
        public ESchoolGrade ESchoolGrade { get; set; }
        public bool Closed { get; set; }

        public long ClassRoomId { get; set; }
        public ClassRoom ClassRoom { get; set; }

        public long ApplicationDomainId { get; set; }
        public ApplicationDomain ApplicationDomain { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<EvaluationCriterea> EvaluationCritereas { get; set; } = new List<EvaluationCriterea>();


        public Project()
        {

        }


        public Project(ProjectDTO dto, long schoolId)
        {
            ProjectName = dto.ProjectName;
            ProjectDescr = dto.ProjectDescr;
            ProjectImage = dto.ProjectImage;
            ProjectBudget = dto.ProjectBudget;
            ESchoolGrade = dto.ESchoolYear;

            Closed = dto.Closed;

            ApplicationDomainId = dto.ApplicationDomainId;
            if(dto.Products != null)
            {
                dto.Products.ToList().ForEach(g => AddProduct(new Product(g)));
            }
            if(dto.Groups != null)
            {
                dto.Groups.ToList().ForEach(g => AddGroup(new Group(g, schoolId)));
            }

            if (dto.EvaluationCritereas != null)
            {
                dto.EvaluationCritereas.ToList().ForEach(g =>
                {
                    var ec = new EvaluationCriterea(g);
                    Groups.ToList().ForEach(j =>
                    {
                        j.AddEvaluation(new Evaluation { 
                            Group = j,
                            EvaluationCriterea = ec
                        });
                    });
                    AddEvaluationCriterea(ec);
                }     
                );
            }
        }

        public Project(ProjectTemplate pt)
        {
            ProjectDescr = pt.ProjectDescr;
            ProjectImage = pt.ProjectImage;
            ESchoolGrade = ESchoolGrade.ALGEMEEN;

            ApplicationDomain = pt.ApplicationDomain;

            pt.ProductTemplateProjectTemplates.ToList().ForEach(g => AddProduct(new Product(g.ProductTemplate)));
        }


        public void AddEvaluationCriterea(EvaluationCriterea p)
        {
            EvaluationCritereas.Add(p);
        }

        public void AddProduct(Product p)
        {
            Products.Add(p);
        }

        public void RemoveProduct(Product p)
        {
            Products.Remove(p);
        }

        public void AddGroup(Group g)
        {
            Groups.Add(g);
        }


        public void RemoveGroup(Group g)
        {
            Groups.Remove(g);
        }


        public void UpdateProducts(ICollection<ProductDTO> prs)
        {
            foreach (var item in Products.ToList())
            {
                var productMatch = prs.FirstOrDefault(g => g.ProductId == item.ProductId);
                if (productMatch == null) // the product has been removed by the user
                {
                    RemoveProduct(item);
                } else // the product is still present in both arrays so update the product
                {
                    item.ProductName = productMatch.ProductName;
                    item.Description = productMatch.Description;
                    item.Price = productMatch.Price;
                    item.ProductImage = productMatch.ProductImage;

                    item.CategoryId = productMatch.CategoryId;
                }
            }

            foreach (var item in prs) // adds products that have not been assigned an ID yet (long is default 0)
            {
                if(item.ProductId == 0)
                {
                    AddProduct(new Product(item));
                }
            }
        }

        public void UpdateGroups(ICollection<GroupDTO> grs, long schoolId)
        {

            foreach (var item in Groups.ToList())
            {
                var groupMatch = grs.FirstOrDefault(g => g.GroupId == item.GroupId);
                if (groupMatch == null) // the group has been removed by the user
                {
                    RemoveGroup(item);
                } else // the product is still present in both arrays so update the product
                {
                    item.GroupName = groupMatch.GroupName;
                }
            }

            foreach (var item in grs) //adds groups that have not been assigned an ID yet(long is default 0)
            {
                if (item.GroupId == 0)
                {
                    AddGroup(new Group(item, schoolId));
                }
            }
        }



    }
}
