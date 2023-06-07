using Moq;
using Xunit;
using SRS.Repositories.Interfaces;
using SRS.Domain.Entities;
using AutoMapper;
using SRS.Services.Interfaces;
using SRS.Services.Implementations;
using System.Collections.Generic;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using System.Threading.Tasks;
using SRS.Domain.Specifications;
using System.Linq;

namespace SRS.Testing
{
    public class FacultyServiceTests
    {
        private readonly Mock<IBaseRepository<Faculty>> _repo;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FacultyService _facultyService;

        public FacultyServiceTests()
        {
            _repo = new Mock<IBaseRepository<Faculty>>();
            _mapperMock = new Mock<IMapper>();
            _facultyService = new FacultyService(_repo.Object, _mapperMock.Object);
        }



        [Fact]
        public async Task GetAllAsync_ShouldReturnAllFacultiesAsync()
        {
            var faculties = new List<Faculty>
            {
                new Faculty { Id = 1, Name = "Faculty1" },
                new Faculty { Id = 2, Name = "Faculty2" },
                new Faculty { Id = 3, Name = "Faculty3" }
            };

            var expectedFacultyModels = new List<FacultyModel>
            {
                new FacultyModel { Id = 1, Name = "Faculty1" },
                new FacultyModel { Id = 2, Name = "Faculty2" },
                new FacultyModel { Id = 3, Name = "Faculty3" }
            };

            var baseFilter = new BaseFilterModel { Skip = 0, Take = 3, OrderBy = null, Desc = false, Search = null };

            _repo.Setup(x => x.GetAsync(It.IsAny<ISpecification<Faculty>>())).ReturnsAsync(faculties);
            _mapperMock.Setup(x => x.Map<IList<FacultyModel>>(faculties)).Returns(expectedFacultyModels);


            var result = await _facultyService.GetAllAsync(baseFilter);

            _repo.Verify(x => x.GetAsync(It.IsAny<ISpecification<Faculty>>()), Times.Once);

            Assert.Equal(expectedFacultyModels, result);
        }

        [Fact]
        public async Task CountAsync_ReturnAmountOfFaculties()
        {
            var faculties = new List<Faculty>
            {
                new Faculty { Id = 1, Name = "Faculty1" },
                new Faculty { Id = 2, Name = "Faculty2" },
                new Faculty { Id = 3, Name = "Faculty3" }
            };

            var expectedAmountOfFaculties = faculties.Count();


            var baseFilter = new BaseFilterModel { Skip = 0, Take = 3, OrderBy = null, Desc = false, Search = null };

            _repo.Setup(x => x.CountAsync(It.IsAny<ISpecification<Faculty>>())).ReturnsAsync(faculties.Count());

            var result = await _facultyService.CountAsync(baseFilter);

            _repo.Verify(x => x.CountAsync(It.IsAny<ISpecification<Faculty>>()), Times.Once);

            Assert.Equal(expectedAmountOfFaculties, result);
        }
    }
}
