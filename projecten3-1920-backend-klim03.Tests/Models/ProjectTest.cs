using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.CustomExceptions;
using projecten3_1920_backend_klim03.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using projecten3_1920_backend_klim03.Domain.Models.Domain.enums;

namespace projecten3_1920_backend_klim03.Tests.Models
{
    public class ProjectTest : IDisposable
    {
        private readonly DummyApplicationDbContext _dummyApplicationDbContext;
        private Project testProject1;
        private ProjectTemplate testProjectTemplate1;
        private Product testProduct1;
        private Group testGroup1;
        public ProjectTest()
        {
            _dummyApplicationDbContext = new DummyApplicationDbContext();
            testProject1 = _dummyApplicationDbContext.testProject;
            testProjectTemplate1 = _dummyApplicationDbContext.projectTemplate1;
            testGroup1 = _dummyApplicationDbContext.testGroup;
            //testProject1 = _dummyApplicationDbContext.
        }
        [Fact]
        public void Project_NewProject_ByTemplateWithoutProductTemplates()
        {
            Project project = new Project(testProjectTemplate1);
            Assert.Equal(project.ProjectName, testProjectTemplate1.ProjectName);
            Assert.Equal(project.ProjectDescr, testProjectTemplate1.ProjectDescr);
            Assert.Equal(project.ProjectImage, testProjectTemplate1.ProjectImage);
            Assert.Equal(project.ApplicationDomain, testProjectTemplate1.ApplicationDomain);
            Assert.Equal(ESchoolGrade.ALGEMEEN, project.ESchoolGrade);
            Assert.Equal(0, project.Products.Count);
        }
        [Fact]
        public void Project_AddProduct_AddsProduct()
        {

            int amount = testProject1.Products.Count;
            testProject1.AddProduct(testProduct1);
            Assert.Equal(amount + 1, testProject1.Products.Count);
        }
        [Fact]
        public void Project_AddGroup_AddsGroup()
        {

            int amount = testProject1.Groups.Count;
            testProject1.AddGroup(testGroup1);
            Assert.Equal(amount + 1, testProject1.Groups.Count);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
