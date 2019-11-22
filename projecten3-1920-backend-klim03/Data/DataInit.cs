using Microsoft.AspNetCore.Identity;
using projecten3_1920_backend_klim03.Domain.Models;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data
{
    public class DataInit
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public DataInit(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }



        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {

                //seeding
                AppUser leraar = new AppUser
                {
                    UserName = "leraar",
                    Email = "leerkracht@school.be"
                };

                await CreateUser(leraar, "P@ssword1");

                

                // category template
                CategoryTemplate ct1 = new CategoryTemplate
                {
                    CategoryName = "default categorytemplate",
                    AddedByGO = true,
                    CategoryDescr = "default categorytemplate descr"
                };

                //application domain

                ApplicationDomain energie = new ApplicationDomain
                {
                    ApplicationDomainName = "Energie",
                    ApplicationDomainDescr = "Alles over energie"
                };
                
                ApplicationDomain informatie = new ApplicationDomain
                {
                    ApplicationDomainName = "Informatie & communicactie",
                    ApplicationDomainDescr = "Alles over informatie & communicactie"
                };
                
                ApplicationDomain constructie = new ApplicationDomain
                {
                    ApplicationDomainName = "Constructie",
                    ApplicationDomainDescr = "Alles over constructie"
                };
                
                ApplicationDomain transport = new ApplicationDomain
                {
                    ApplicationDomainName = "Transport",
                    ApplicationDomainDescr = "Alles over transport"
                };
                
                ApplicationDomain biochemie = new ApplicationDomain
                {
                    ApplicationDomainName = "Biochemie",
                    ApplicationDomainDescr = "Alles over biochemie"
                };


                //school
                School schoolGO = new School
                {
                    Name = "Go school",
                    Email = "go@school.be",
                    TelNum = "049746382",
                    Adres = new Adres
                    {
                        Straat = "straat",
                        Postcode = "8490",
                        Huisnummer = "5",
                        Plaats = "Brugge"
                    }
                };

                _dbContext.Add(ct1);
                _dbContext.Add(schoolGO);
                _dbContext.Add(energie);
                _dbContext.Add(informatie);
                _dbContext.Add(constructie);
                _dbContext.Add(transport);
                _dbContext.Add(biochemie);
                _dbContext.SaveChanges();

                //classroom
                ClassRoom cr = new ClassRoom
                {
                    Name = "Klas 1",
                    SchoolId = schoolGO.SchoolId
                };
                schoolGO.ClassRooms.Add(cr);

                #region ProductTemplates

                ProductTemplate pt1 = new ProductTemplate
                {
                    ProductName = "Karton",
                    Description = "Dit is karton",
                    AddedByGO = true,
                    ProductImage = "https://www.manutan.be/img/S/GRP/ST/AIG2166048.jpg",
                    Score = 9,
                    HasMultipleDisplayVariations = true,
                    CategoryTemplateId = ct1.CategoryTemplateId,
                    SchoolId = schoolGO.SchoolId,
                    ProductVariationTemplates = new List<ProductVariationTemplate>
                    {
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Algemene beschrijving karton",
                            ESchoolGrade = ESchoolGrade.ALGEMEEN
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Eerste graad beschrijving karton",
                            ESchoolGrade = ESchoolGrade.EERSTE
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Tweede graad beschrijving karton",
                            ESchoolGrade = ESchoolGrade.TWEEDE
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Derde graad beschrijving karton",
                            ESchoolGrade = ESchoolGrade.DERDE
                        }
                    }
                };
               
                ProductTemplate pt2 = new ProductTemplate
                {
                    ProductName = "Lijm",
                    Description = "Dit is lijm",
                    AddedByGO = true,
                    ProductImage = "https://www.manutan.be/img/S/GRP/ST/AIG2399133.jpg",
                    Score = 6,
                    HasMultipleDisplayVariations = true,
                    CategoryTemplateId = ct1.CategoryTemplateId,
                    SchoolId = schoolGO.SchoolId,
                    ProductVariationTemplates = new List<ProductVariationTemplate>
                    {
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Algemene beschrijving Lijm",
                            ESchoolGrade = ESchoolGrade.ALGEMEEN
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Eerste graad beschrijving Lijm",
                            ESchoolGrade = ESchoolGrade.EERSTE
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Tweede graad beschrijving Lijm",
                            ESchoolGrade = ESchoolGrade.TWEEDE
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Derde graad beschrijving Lijm",
                            ESchoolGrade = ESchoolGrade.DERDE
                        }
                    }
                };
                ProductTemplate pt3 = new ProductTemplate
                {
                    ProductName = "Plakband",
                    Description = "Dit is Plakband",
                    AddedByGO = true,
                    ProductImage = "https://www.manutan.be/img/S/GRP/ST/AIG2328457.jpg",
                    Score = 5,
                    HasMultipleDisplayVariations = true,
                    CategoryTemplateId = ct1.CategoryTemplateId,
                    SchoolId = schoolGO.SchoolId,
                    ProductVariationTemplates = new List<ProductVariationTemplate>
                    {
                         new ProductVariationTemplate
                        {
                            ProductDescr = "Algemene beschrijving Plakband",
                            ESchoolGrade = ESchoolGrade.ALGEMEEN
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Eerste graad beschrijving Plakband",
                            ESchoolGrade = ESchoolGrade.EERSTE
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Tweede graad beschrijving Plakband",
                            ESchoolGrade = ESchoolGrade.TWEEDE
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Derde graad beschrijving Plakband",
                            ESchoolGrade = ESchoolGrade.DERDE
                        }
                    }
                };

                ProductTemplate pt4 = new ProductTemplate
                {
                    ProductName = "Hout",
                    Description = "Dit is hout",
                    AddedByGO = true,
                    ProductImage = "https://cdn.webshopapp.com/shops/34832/files/96705407/800x600x1/van-gelder-hout-schuttingplanken.jpg",
                    Score = 8,
                    HasMultipleDisplayVariations = true,
                    CategoryTemplateId = ct1.CategoryTemplateId,
                    SchoolId = schoolGO.SchoolId,
                    ProductVariationTemplates = new List<ProductVariationTemplate>
                    {
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Algemene beschrijving hout",
                            ESchoolGrade = ESchoolGrade.ALGEMEEN
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Eerste graad beschrijving Hout",
                            ESchoolGrade = ESchoolGrade.EERSTE
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Tweede graad beschrijving Hout",
                            ESchoolGrade = ESchoolGrade.TWEEDE
                        },
                        new ProductVariationTemplate
                        {
                            ProductDescr = "Derde graad beschrijving Hout",
                            ESchoolGrade = ESchoolGrade.DERDE
                        }
                    }
                };

                schoolGO.AddProductTemplate(pt1);
                schoolGO.AddProductTemplate(pt2);
                schoolGO.AddProductTemplate(pt3);
                schoolGO.AddProductTemplate(pt4);
                _dbContext.SaveChanges();
                #endregion

                #region ProjectTemplate

                ProjectTemplate projectTemplate = new ProjectTemplate
                {
                    AddedByGO = true,


                    ProjectDescr = "Dit is een project voor energie",
                    ProjectImage = "image",
                    ProjectName = "Energie kennismaking",
                    SchoolId = schoolGO.SchoolId,
                    Budget = 20,
                    MaxScore = 25,
                    ApplicationDomainId = energie.ApplicationDomainId
                };

                projectTemplate.AddProductTemplate(pt2);
                projectTemplate.AddProductTemplate(pt3);
                projectTemplate.AddProductTemplate(pt4);

                schoolGO.AddProjectTemplate(projectTemplate);

                _dbContext.SaveChanges();
                #endregion



                Category cat1 = new Category
                {
                    CategoryName = "Bouwmaterialen",
                    CategoryDescr = "Zaken waarmee je kan bouwen"
                };

                Category cat2 = new Category
                {
                    CategoryName = "Diverse",
                    CategoryDescr = "Andere gevallen"
                };

                Product pr1 = new Product
                {
                    Category = cat1,
                    ProductName = "Hout",
                    Description = "Algemene beschrijving van hout",
                    Price = 5,
                    Score = 8,
                    ProductImage = "https://d16m3dafbknje9.cloudfront.net/imagescaler/9048544313374-500-500.jpg"
                };

                Product pr2 = new Product
                {
                    Category = cat1,
                    ProductName = "Papier",
                    Description = "Algemene beschrijving van papier",
                    Price = 3,
                    Score = 7,
                    ProductImage = "http://tmib.com/wp-content/uploads/2014/08/stack-of-paper.jpg"
                };

                Product pr3 = new Product
                {
                    Category = cat1,
                    ProductName = "Plastiek",
                    Description = "Algemene beschrijving van plastiek",
                    Price = 10,
                    Score = 2,
                    ProductImage = "https://img.etimg.com/thumb/msid-70477420,width-640,resizemode-4,imgsize-251889/the-most-recycled-plastic.jpg"
                };

                Product pr4 = new Product
                {
                    Category = cat2,
                    ProductName = "Plakband",
                    Description = "Algemene beschrijving van plakband",
                    Price = 10,
                    Score = 6,
                    ProductImage = "https://discountoffice.nl/productImages/8/large/Q800250-3.jpg"
                };

                Group groep1 = new Group
                {
                    GroupId = 1,
                    GroupName = "Groep 1",
                    GroupCode ="abcde"
                };

                Group groep2 = new Group
                {
                    GroupId = 2,
                    GroupName = "Groep 2",
                    GroupCode = "12345"
                };

                Group groep3 = new Group
                {
                    GroupId = 3,
                    GroupName = "Groep 3",
                    GroupCode = "azert"
                };
                

                groep2.InitOrder();
                groep3.InitOrder();

                //Projecten toevoegen

                //Project dat gestart is met 1 groep en 1 product
                Project project1 = new Project
                {
                    ProjectBudget = 200,
                    ProjectDescr = "Dit is een project over energie waar je iets leert over hechtingen, licht en schaduw via Reus en Dwerg.",
                    ProjectImage = "image",
                    ProjectName = "Ontdekdozen (hechtingen + licht/schaduw)",
                    ApplicationDomainId = energie.ApplicationDomainId,
                    ESchoolGrade = ESchoolGrade.ALGEMEEN,
                };

                project1.AddProduct(pr1);
                project1.AddGroup(groep1);
                project1.AddGroup(groep3);
                cr.AddProject(project1);
                

                project1.EvaluationCritereas.Add(new EvaluationCriterea
                {
                    EvaluationCritereaId = 1,
                    Title="Eerste ronde"
                });

                project1.EvaluationCritereas.Add(new EvaluationCriterea
                {
                    EvaluationCritereaId = 2,
                    Title = "Tweede ronde"
                });

                project1.EvaluationCritereas.Add(new EvaluationCriterea
                {
                    EvaluationCritereaId = 3,
                    Title = "Derde ronde"
                });


                _dbContext.SaveChanges();


                #region Add evaluations for group 1 and 3


                groep1.AddEvaluation(new Evaluation
                {
                    DescriptionPupil = "Een eerste evaluatie voor de leerling",
                    DescriptionPrivate = "Een eerste evaluatie voor de leerkracht",
                    Extra = false,
                    EvaluationCritereaId = 1
                });

                groep1.AddEvaluation(new Evaluation
                {
                    DescriptionPupil = "Een eerste evaluatie voor de leerling",
                    DescriptionPrivate = "Een eerste evaluatie voor de leerkracht",
                    Extra = false,
                    EvaluationCritereaId = 2
                });

                groep1.AddEvaluation(new Evaluation
                {
                    DescriptionPupil = "Een eerste evaluatie voor de leerling",
                    DescriptionPrivate = "Een eerste evaluatie voor de leerkracht",
                    Extra = false,
                    EvaluationCritereaId = 3
                });

                groep1.AddEvaluation(new Evaluation
                {
                    Title = "Extra evaluatie",
                    DescriptionPupil = "Evaluatie op het eindproduct voor de leerling",
                    DescriptionPrivate = "Evaluatie op het eindproduct voor de leerkracht",
                    Extra = true
                });




                groep3.AddEvaluation(new Evaluation
                {
                    DescriptionPupil = "",
                    DescriptionPrivate = "",
                    Extra = false,
                    EvaluationCritereaId = 1
                });

                groep3.AddEvaluation(new Evaluation
                {
                    DescriptionPupil = "",
                    DescriptionPrivate = "",
                    Extra = false,
                    EvaluationCritereaId = 2
                });

                groep3.AddEvaluation(new Evaluation
                {
                    DescriptionPupil = "",
                    DescriptionPrivate = "",
                    Extra = false,
                    EvaluationCritereaId = 3
                });


                #endregion




              



                _dbContext.SaveChanges();

                //Project dat gestart is met 1 groep en 3 producten
                Project project2 = new Project
                {
                    ProjectBudget = 100,
                    ProjectDescr = "Dit is een project over informatie en communicatie waarbij je leert seinen en blazen.",
                    ProjectImage = "image",
                    ProjectName = "Ontdekdozen (leren blazen + seinen)",
                    ApplicationDomainId = informatie.ApplicationDomainId,
                    ESchoolGrade = ESchoolGrade.ALGEMEEN,
                };

                project2.AddProduct(pr1);
                project2.AddProduct(pr2);
                project2.AddProduct(pr3);
                project2.AddProduct(pr4);
                project2.AddGroup(groep2);
                cr.AddProject(project2);
                _dbContext.SaveChanges();

                //Project dat gestart is met geen groepen en geen producten
                Project project3 = new Project
                {
                    ProjectBudget = 300,
                    ProjectDescr = "Dit is een project over constructie waarbij je een muurtje maakt via het verhaal van de 3 biggetjes",
                    ProjectImage = "image",
                    ProjectName = "Ontdekdozen (bouwen)",
                    ApplicationDomainId = constructie.ApplicationDomainId,
                    ESchoolGrade = ESchoolGrade.ALGEMEEN,
                };

                cr.AddProject(project3);
                _dbContext.SaveChanges();

                //Project dat gesloten is met geen groepen en geen producten
                Project project4 = new Project
                {
                    ProjectBudget = 300,
                    ProjectDescr = "Dit is een project over transport waarbij je een beetje maakt.",
                    ProjectImage = "image",
                    ProjectName = "Ontdekdozen (drijven/zinken)",
                    ApplicationDomainId = transport.ApplicationDomainId,
                    ESchoolGrade = ESchoolGrade.EERSTE,
                    Closed = true,
                };

                cr.AddProject(project4);
                _dbContext.SaveChanges();
                groep1.Order = new Order
                {
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = pr1.ProductId,
                            Amount = 2
                        }
                    }
                };
                



                _dbContext.SaveChanges();


                groep1.AddPupil(
                new Pupil
                {
                        FirstName = "Daan",
                        Surname = "Dedecker",
                        SchoolId = schoolGO.SchoolId
                        
                });

                groep1.AddPupil(
                new Pupil
                {
                    FirstName = "Rambo",
                    Surname = "Jansens",
                    SchoolId = schoolGO.SchoolId

                });

                groep1.AddPupil(
                new Pupil
                {
                    FirstName = "Piet",
                    Surname = "Petter",
                    SchoolId = schoolGO.SchoolId
                });



                _dbContext.SaveChanges();

            }
        }

        private async Task CreateUser(AppUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }
    }
}
