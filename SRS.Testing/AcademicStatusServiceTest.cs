using AutoMapper;
using Moq;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Implementations;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SRS.Test
{
    public class AcademicStatusServiceTest
    {
        private readonly Mock<IBaseRepository<AcademicStatus>> _repo;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AcademicStatusService _positionService;

        public AcademicStatusServiceTest()
        {
            _repo = new Mock<IBaseRepository<AcademicStatus>>();
            _mapperMock = new Mock<IMapper>();
            _positionService = new AcademicStatusService(_repo.Object, _mapperMock.Object);
        }



        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPositionsAsync()
        {
            var academicStatuses = new List<AcademicStatus>
            {
                new AcademicStatus { Id = 1, Value = "AcademicStatus1", SortOrder = 2 },
                new AcademicStatus { Id = 2, Value = "AcademicStatus2", SortOrder = 3 },
                new AcademicStatus { Id = 3, Value = "AcademicStatus3", SortOrder = 1 }
            };

            var academicStatusModels = new List<AcademicStatusModel>
            {
                new AcademicStatusModel { Id = 1, Value = "AcademicStatus1", SortOrder = 2  },
                new AcademicStatusModel { Id = 2, Value = "AcademicStatus2", SortOrder = 1  },
                new AcademicStatusModel { Id = 3, Value = "AcademicStatus3", SortOrder = 3  }
            };

            var baseFilter = new BaseFilterModel { Skip = 0, Take = 3, OrderBy = null, Desc = false, Search = null };

            _repo.Setup(x => x.GetAsync(It.IsAny<ISpecification<AcademicStatus>>())).ReturnsAsync(academicStatuses);
            _mapperMock.Setup(x => x.Map<IList<AcademicStatusModel>>(academicStatuses)).Returns(academicStatusModels);


            var result = await _positionService.GetAllAsync(baseFilter);

            _repo.Verify(x => x.GetAsync(It.IsAny<ISpecification<AcademicStatus>>()), Times.Never);

            Assert.Equal(academicStatusModels, result);
        }

        [Fact]
        public async Task CountAsync_ReturnAmountOfPositions()
        {
            var positions = new List<AcademicStatus>
            {
                new AcademicStatus { Id = 1, Value = "AcademicStatus1" },
                new AcademicStatus { Id = 2, Value = "AcademicStatus2" },
                new AcademicStatus { Id = 3, Value = "AcademicStatus3" },
            };

            var expectedAmountOfPositions = positions.Count();


            var baseFilter = new BaseFilterModel { Skip = 0, Take = 3, OrderBy = null, Desc = false, Search = null };

            _repo.Setup(x => x.CountAsync(It.IsAny<ISpecification<AcademicStatus>>())).ReturnsAsync(positions.Count());

            var result = await _positionService.CountAsync(baseFilter);

            _repo.Verify(x => x.CountAsync(It.IsAny<ISpecification<AcademicStatus>>()), Times.Once);

            Assert.Equal(expectedAmountOfPositions, result);
        }
    }
}
