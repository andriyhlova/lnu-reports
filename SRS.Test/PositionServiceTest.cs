using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Implementations;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models;
using Moq;
using AutoMapper;
using SRS.Domain.Specifications;

namespace SRS.Test
{
    public class PositionServiceTest
    {
        private readonly Mock<IBaseRepository<Position>> _repo;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PositionService _positionService;

        public PositionServiceTest()
        {
            _repo = new Mock<IBaseRepository<Position>>();
            _mapperMock = new Mock<IMapper>();
            _positionService = new PositionService(_repo.Object, _mapperMock.Object);
        }



        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPositionsAsync()
        {
            var positions = new List<Position>
            {
                new Position { Id = 1, Value = "Position1", SortOrder = 2 },
                new Position { Id = 2, Value = "Position2", SortOrder = 3 },
                new Position { Id = 3, Value = "Position3", SortOrder = 1 }
            };

            var expectedPositionModels = new List<PositionModel>
            {
                new PositionModel { Id = 1, Value = "Position1", SortOrder = 2  },
                new PositionModel { Id = 2, Value = "Position2", SortOrder = 1  },
                new PositionModel { Id = 3, Value = "Position3", SortOrder = 3  }
            };

            var baseFilter = new BaseFilterModel { Skip = 0, Take = 3, OrderBy = null, Desc = false, Search = null };

            _repo.Setup(x => x.GetAsync(It.IsAny<ISpecification<Position>>())).ReturnsAsync(positions);
            _mapperMock.Setup(x => x.Map<IList<PositionModel>>(positions)).Returns(expectedPositionModels);


            var result = await _positionService.GetAllAsync(baseFilter);

            _repo.Verify(x => x.GetAsync(It.IsAny<ISpecification<Position>>()), Times.Once);

            Assert.Equal(expectedPositionModels, result);
        }

        [Fact]
        public async Task CountAsync_ReturnAmountOfPositions()
        {
            var positions = new List<Position>
            {
                new Position { Id = 1, Value = "Position1" },
                new Position { Id = 2, Value = "Position2" },
                new Position { Id = 3, Value = "Position3" },
            };

            var expectedAmountOfPositions = positions.Count();


            var baseFilter = new BaseFilterModel { Skip = 0, Take = 3, OrderBy = null, Desc = false, Search = null };

            _repo.Setup(x => x.CountAsync(It.IsAny<ISpecification<Position>>())).ReturnsAsync(positions.Count());

            var result = await _positionService.CountAsync(baseFilter);

            _repo.Verify(x => x.CountAsync(It.IsAny<ISpecification<Position>>()), Times.Once);

            Assert.Equal(expectedAmountOfPositions, result);
        }
    }
}