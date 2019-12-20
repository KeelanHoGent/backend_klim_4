using projecten3_1920_backend_klim03.Domain.Models;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;
using projecten3_1920_backend_klim03.Tests.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace projecten3_1920_backend_klim03.Tests.Models
{
    public class GroupTest
    {
        private readonly DummyApplicationDbContext _dummyApplicationDbContext;
        private Group _group;
        private Pupil eddy;
        public GroupTest()
        {
            _dummyApplicationDbContext = new DummyApplicationDbContext();
            _group = _dummyApplicationDbContext.testGroup;
            eddy = new Pupil();
            eddy.FirstName = "Eddy";
            eddy.PupilId = 0;
            eddy.ClassRoomId = 99;
        }

        [Fact]
        public void newGroup_noPupils()
        {
            Group newGroup = new Group();
            Assert.Equal(0, newGroup.PupilGroups.Count);
        }

        [Fact]
        public void newGroup_AddPupil_addsPupil()
        {
            Group newGroup = new Group();
            newGroup.AddPupil(eddy);

            Assert.Equal(1, newGroup.PupilGroups.Count);

            newGroup.AddPupil(eddy);

            Assert.Equal(2, newGroup.PupilGroups.Count);
        }

        [Fact]
        public void newGroup_UpdatePupil_addsPupilIfItDoesntAlreadyHasHim()
        {
            Group newGroup = new Group();
            newGroup.UpdatePupil(eddy);

            Assert.Equal(1, newGroup.PupilGroups.Count);
        }

        [Fact]
        public void newGroup_UpdatePupil_updatesPupilDetails()
        {
            Group newGroup = new Group();
            newGroup.AddPupil(eddy);

            eddy.FirstName = "Freddy";

            newGroup.UpdatePupil(eddy);

            PupilGroup groupEddy = newGroup.PupilGroups.ToList().FirstOrDefault(g => g.PupilId == eddy.PupilId);
            Assert.Equal("Freddy", groupEddy.Pupil.FirstName);
        }

        [Fact]
        public void newGroup_AddEvaluation_addsEvaluation()
        {
            Group newGroup = new Group();
            Evaluation newEvaluation = new Evaluation();
            newEvaluation.Title = "Test evaluation";
            newEvaluation.DescriptionPrivate = "goed gewerkt";
            newEvaluation.DescriptionPupil = "niet goed gewerkt";

            newGroup.AddEvaluation(newEvaluation);

            Assert.Equal(1, newGroup.Evaluations.Count);
        }

        [Fact]
        public void newGroup_RemoveEvaluationById_removesEvaluation()
        {
            Group newGroup = new Group();
            Evaluation newEvaluation = new Evaluation();
            newEvaluation.Title = "Test evaluation";
            newEvaluation.DescriptionPrivate = "goed gewerkt";
            newEvaluation.DescriptionPupil = "niet goed gewerkt";
            newEvaluation.EvaluationId = 3;

            newGroup.AddEvaluation(newEvaluation);

            Assert.Equal(1, newGroup.Evaluations.Count);

            newGroup.RemoveEvaluationById(3);

            Assert.Equal(0, newGroup.Evaluations.Count);
        }

        [Fact]
        public void newGroup_RemoveEvaluationById_idNotInList()
        {
            Group newGroup = new Group();
            Evaluation newEvaluation = new Evaluation();
            newEvaluation.EvaluationId = 3;

            newGroup.AddEvaluation(newEvaluation);

            newGroup.RemoveEvaluationById(1);
            Assert.Equal(1, newGroup.Evaluations.Count);
        }

        
    }
}
