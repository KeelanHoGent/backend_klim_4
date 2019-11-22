using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Domain.enums;
using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;
using System;
using System.Collections.Generic;
using System.Text;

namespace projecten3_1920_backend_klim03.Tests.Data
{
    public class DummyApplicationDbContext
    {
        public Order testOrder { get; }

        public OrderItem testOrderItem { get; }

        public Project testProject;
        public ProjectTemplate projectTemplate1;
        public ProjectTemplate projectTemplate2;
        public ProductTemplate producttemplate1;
        public ProductTemplate producttemplate2;
        public Group testGroup;

        public DummyApplicationDbContext()
        {
            Product testP1 = new Product
            {
                ProductId = 1,
                ProductName = "Karton",
                Description = "Karton beschrijving",
                Price = 5
            };

            Product testP2 = new Product
            {
                ProductId = 2,
                ProductName = "Plastiek",
                Description = "plastiek beschrijving",
                Price = 20
            };

            Product testP3 = new Product
            {
                ProductId = 3,
                ProductName = "Lijm",
                Description = "Lijm beschrijving",
                Price = 8.5
            };

            OrderItem testOrderItem1 = new OrderItem
            {
                OrderItemId = 1,
                Product = testP1,
                Amount = 2
            };

            OrderItem testOrderItem2 = new OrderItem
            {
                OrderItemId = 2,
                Product = testP2,
                Amount = 1
            };

            OrderItem testOrderItem3 = new OrderItem
            {
                OrderItemId = 3,
                Product = testP3,
                Amount = 3
            };

            testOrderItem = new OrderItem
            {
                OrderItemId = 4,
                Product = testP1,
                Amount = 5
            };

            testOrder = new Order
            {
                OrderId = 1,
                Approved = false,
                Submitted = false,
                OrderItems = new List<OrderItem>
                {
                    testOrderItem1,
                    testOrderItem2,
                    testOrderItem3
                }
            };
            ApplicationDomain applicationDomainTest = new ApplicationDomain
            {
                ApplicationDomainId = 1,
                ApplicationDomainName = "naam",
                ApplicationDomainDescr = "niet veel speciaals"

            };
            testGroup = new Group
            {
                GroupId = 1,
                GroupName = "groepsnaam",
                Order = testOrder

        };

            testProject = new Project
            {
                ProjectName = "testproject",
                ProjectDescr = "ceci est une beschrijving",
                ProjectImage = "url",
                ProjectBudget = 20,
                ESchoolGrade = ESchoolGrade.ALGEMEEN,
                Closed = false,
                ApplicationDomainId = 1
            };

            projectTemplate1 = new ProjectTemplate
            {
                ProjectName = "DitIsEenTest",
                ProjectDescr = "Dit is een test voor rojecttemplate",
                ProjectImage = "ceci n'est pas une url",
                AddedByGO = true,
                ApplicationDomainId = 1,
                ApplicationDomain = applicationDomainTest,
                ProductTemplateProjectTemplates = new List<ProductTemplateProjectTemplate>()
        };
            projectTemplate2 = new ProjectTemplate
            {
                ProjectName = "DitIsEenAndereTest",
                ProjectDescr = "Dit is 2e een test voor projecttemplate",
                ProjectImage = "ceci n'est aussie pas une url",
                AddedByGO = false,
                ApplicationDomainId = 2
            };
            producttemplate1 = new ProductTemplate
            {
                ProductName = "DitIsEenProductTemplateTest",
                Description = "dit is een beschrijving voor een producttemplate",
                ProductImage = "dit is ook geen url",
                AddedByGO = true
            };
            producttemplate2 = new ProductTemplate
            {
                ProductName = "DitIsEentweedeProductTemplateTest",
                Description = "dit is een beschrijving voor een andere producttemplate",
                ProductImage = "dit is ook geen url, zot eh",
                AddedByGO = false
            };
        }
    }
}
