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
                    CategoryName = "Bouwmaterialen",
                    AddedByGO = true,
                    CategoryDescr = "Alles voor constructies te bouwen"
                };
                CategoryTemplate ct2 = new CategoryTemplate
                {
                    CategoryName = "Divers",
                    AddedByGO = true,
                    CategoryDescr = "Overige materialen"
                };
                CategoryTemplate ct3 = new CategoryTemplate
                {
                    CategoryName = "Versiering",
                    AddedByGO = true,
                    CategoryDescr = "Extra versiering"
                };

                //application domain

                ApplicationDomain energie = new ApplicationDomain
                {
                    ApplicationDomainName = "Energie",
                    ApplicationDomainDescr = "Alles over energie"
                };
                
                ApplicationDomain informatie = new ApplicationDomain
                {
                    ApplicationDomainName = "Informatie & communicatie",
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
                _dbContext.Add(ct2);
                _dbContext.Add(ct3);
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
                    Name = "1A",
                    SchoolId = schoolGO.SchoolId
                };
                cr.addPupil(new Pupil
                 {
                     FirstName = "Freddie",
                     Surname = "Trump",
                     ClassRoomId = cr.ClassRoomId
                 });
                cr.addPupil(new Pupil
                {
                    FirstName = "Jonas",
                    Surname = "Bergmans",
                    ClassRoomId = cr.ClassRoomId
                });
                cr.addPupil(new Pupil
                {
                    FirstName = "Ron",
                    Surname = "Merkel",
                    ClassRoomId = cr.ClassRoomId
                });
                cr.addPupil(new Pupil
                {
                    FirstName = "Erin",
                    Surname = "Ceaser",
                    ClassRoomId = cr.ClassRoomId
                });
                cr.addPupil(new Pupil
                {
                    FirstName = "Lisa",
                    Surname = "Toek",
                    ClassRoomId = cr.ClassRoomId
                });
                schoolGO.ClassRooms.Add(cr);

                ClassRoom cr2 = new ClassRoom
                {
                    Name = "5B",
                    SchoolId = schoolGO.SchoolId
                };
                cr2.addPupil(new Pupil
                {
                    FirstName = "Thomas",
                    Surname = "Schuddinck",
                    ClassRoomId = cr2.ClassRoomId
                });
                cr2.addPupil(new Pupil
                {
                    FirstName = "Sofie",
                    Surname = "Seru",
                    ClassRoomId = cr2.ClassRoomId
                });
                cr2.addPupil(new Pupil
                {
                    FirstName = "Keelan",
                    Surname = "Savat",
                    ClassRoomId = cr2.ClassRoomId
                });
                cr2.addPupil(new Pupil
                {
                    FirstName = "Florian",
                    Surname = "Landuyt",
                    ClassRoomId = cr2.ClassRoomId
                });
                schoolGO.ClassRooms.Add(cr2);

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
                    AddedByGO = false,
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

                Category cat3 = new Category
                {
                    CategoryName = "Versiering",
                    CategoryDescr = "Extra versiering"
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


                Product pr5 = new Product
                {
                    Category = cat3,
                    ProductName = "Glitter",
                    Description = "Algemene beschrijving van glitter",
                    Price = 15,
                    Score = 3,
                    ProductImage = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Glitter_close_up.jpg"
                };

                Product pr6 = new Product
                {
                    Category = cat1,
                    ProductName = "Klei",
                    Description = "Algemene beschrijving van klei",
                    Price = 10,
                    Score = 9,
                    ProductImage = "https://static.dreamland.be/wcsstore/ColruytB2CCAS/JPG/JPG/646x1000/std.lang.all/32/01/asset-213201.jpg"
                };

                Product pr7 = new Product
                {
                    Category = cat3,
                    ProductName = "Rietjes",
                    Description = "Algemene beschrijving van rietjes",
                    Price = 20,
                    Score = 2,
                    ProductImage = "https://s.s-bol.com/imgbase0/imagebase3/large/FC/3/8/3/7/9200000056207383.jpg"
                };

                Product pr8 = new Product
                {
                    Category = cat2,
                    ProductName = "Batterijen",
                    Description = "Algemene beschrijving van batterijen",
                    Price = 25,
                    Score = 4,
                    ProductImage = "https://www.hoofdluizen.info/images/kingma/duracell_baterijen.jpg"
                };

                Product pr9 = new Product
                {
                    Category = cat1,
                    ProductName = "Touw",
                    Description = "Algemene beschrijving van touw",
                    Price = 10,
                    Score = 8,
                    ProductImage = "https://s.s-bol.com/imgbase0/imagebase3/large/FC/9/5/8/8/9200000040218859.jpg"
                };

                Product pr10 = new Product
                {
                    Category = cat1,
                    ProductName = "Houten staafjes",
                    Description = "Algemene beschrijving van houten staafjes",
                    Price = 15,
                    Score = 7,
                    ProductImage = "https://www.disposablediscounter.nl/media/catalog/product/cache/8/image/650x650/3e6dcc690df1ad912d04361a483c3855/w/o/wooden-stirrers-wood-houten-roerstaafjes-holz-ruhrstabchen-disposable-discounter-1.jpg"
                };

                Product pr11 = new Product
                {
                    Category = cat1,
                    ProductName = "Elastiekjes",
                    Description = "Algemene beschrijving van elastiekjes",
                    Price = 10,
                    Score = 3,
                    ProductImage = "https://media.margriet.nl/m/zbnr3gn6teh4.jpg/handig-met-elastiek.jpg"
                };



                Product pr12 = new Product
                {
                    Category = cat2,
                    ProductName = "Batterijen",
                    Description = "Algemene beschrijving van batterijen",
                    Price = 25,
                    Score = 4,
                    ProductImage = "https://www.hoofdluizen.info/images/kingma/duracell_baterijen.jpg"
                };

                Product pr13 = new Product
                {
                    Category = cat1,
                    ProductName = "Touw",
                    Description = "Algemene beschrijving van touw",
                    Price = 10,
                    Score = 8,
                    ProductImage = "https://s.s-bol.com/imgbase0/imagebase3/large/FC/9/5/8/8/9200000040218859.jpg"
                };

                Product pr14 = new Product
                {
                    Category = cat1,
                    ProductName = "Houten staafjes",
                    Description = "Algemene beschrijving van houten staafjes",
                    Price = 15,
                    Score = 7,
                    ProductImage = "https://www.disposablediscounter.nl/media/catalog/product/cache/8/image/650x650/3e6dcc690df1ad912d04361a483c3855/w/o/wooden-stirrers-wood-houten-roerstaafjes-holz-ruhrstabchen-disposable-discounter-1.jpg"
                };

                Product pr15 = new Product
                {
                    Category = cat1,
                    ProductName = "Elastiekjes",
                    Description = "Algemene beschrijving van elastiekjes",
                    Price = 10,
                    Score = 3,
                    ProductImage = "https://media.margriet.nl/m/zbnr3gn6teh4.jpg/handig-met-elastiek.jpg"
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

                Group groep4 = new Group
                {
                    GroupId = 4,
                    GroupName = "Groep 4",
                    GroupCode = "54321"
                };

                Group groep5 = new Group
                {
                    GroupId = 5,
                    GroupName = "Groep 5",
                    GroupCode = "678910"
                };

                Group groep6 = new Group
                {
                    GroupId = 6,
                    GroupName = "Groep 6",
                    GroupCode = "13579"
                };

                groep2.InitOrder();
                groep3.InitOrder();
                groep4.InitOrder();
                groep5.InitOrder();

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
                project1.AddProduct(pr2);
                project1.AddProduct(pr3);
                project1.AddProduct(pr4);
                project1.AddProduct(pr5);
                project1.AddProduct(pr6);
                project1.AddProduct(pr7);
                project1.AddProduct(pr8);
                project1.AddProduct(pr9);
                project1.AddProduct(pr10);
                project1.AddProduct(pr11);
                project1.AddGroup(groep1);
                project1.AddGroup(groep3);
                project1.AddGroup(groep6);
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

                
                project2.AddGroup(groep2);
                
                cr.AddProject(project2);
                _dbContext.SaveChanges();

                Project project3 = new Project
                {
                    ProjectBudget = 300,
                    ProjectDescr = "Dit is een project over constructie waarbij je een muurtje maakt via het verhaal van de 3 biggetjes",
                    ProjectImage = "image",
                    ProjectName = "Ontdekdozen (bouwen)",
                    ApplicationDomainId = constructie.ApplicationDomainId,
                    ESchoolGrade = ESchoolGrade.ALGEMEEN,
                };
                project3.AddGroup(groep4);
                project2.AddProduct(pr12);
                project2.AddProduct(pr13);
                project2.AddProduct(pr14);
                project2.AddProduct(pr15);
                cr.AddProject(project3);
                _dbContext.SaveChanges();

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
                project4.AddGroup(groep5);
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

                groep6.Order = new Order
                {
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = pr1.ProductId,
                            Amount = 200
                        }
                    }
                };




                _dbContext.SaveChanges();


                groep1.AddPupil(
                new Pupil
                {
                        FirstName = "Daan",
                        Surname = "Dedecker",
                        ClassRoomId = cr.ClassRoomId

                });

                groep1.AddPupil(
                new Pupil
                {
                    FirstName = "Rambo",
                    Surname = "Jansens",
                    ClassRoomId = cr.ClassRoomId

                });

                groep1.AddPupil(
                new Pupil
                {
                    FirstName = "Piet",
                    Surname = "Petter",
                    ClassRoomId = cr.ClassRoomId
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
