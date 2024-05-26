using Moq;
using SRS.Domain.Entities;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using SRS.Repositories.Interfaces;
using SRS.Services.Implementations;
using SRS.Domain.Specifications;
using SRS.Services.Interfaces;
using System.Linq.Expressions;

namespace SRS.Testing.Services
{
    public class CathedraServiceTests
    {
        private readonly Mock<IBaseRepository<Cathedra>> _repo;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CathedraService _cathedraService;

        public CathedraServiceTests()
        {
            _repo = new Mock<IBaseRepository<Cathedra>>();
            _mapperMock = new Mock<IMapper>();
            _cathedraService = new CathedraService(_repo.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCathedrasAsync()
        {
            var cathedras = new List<Cathedra>
            {
                new Cathedra { Id = 1, Name = "Cathedra1", FacultyId = 1, GenitiveCase = "Cathedra1Genetive" },
                new Cathedra { Id = 2, Name = "Cathedra2", FacultyId = 1, GenitiveCase = "Cathedra2Genetive" },
                new Cathedra { Id = 3, Name = "Cathedra3", FacultyId = 2, GenitiveCase = "Cathedra3Genetive" }
            };

            var expectedCathedraModels = new List<CathedraModel>
            {
                new CathedraModel { Id = 1, Name = "Cathedra1", FacultyId = 1, GenitiveCase = "Cathedra1Genetive" },
                new CathedraModel { Id = 2, Name = "Cathedra2", FacultyId = 1, GenitiveCase = "Cathedra2Genetive" },
                new CathedraModel { Id = 3, Name = "Cathedra3", FacultyId = 2, GenitiveCase = "Cathedra3Genetive" }
            };

            var facultyFilter = new FacultyFilterModel { Skip = 0, Take = 3, FacultyId = null, OrderBy = null, Desc = false, Search = null };

            _repo.Setup(x => x.GetAsync(It.IsAny<ISpecification<Cathedra>>())).ReturnsAsync(cathedras);
            _mapperMock.Setup(x => x.Map<IList<CathedraModel>>(cathedras)).Returns(expectedCathedraModels);


            var result = await _cathedraService.GetAllAsync(facultyFilter);

            _repo.Verify(x => x.GetAsync(It.IsAny<ISpecification<Cathedra>>()), Times.Once);

            Assert.Equal(expectedCathedraModels, result);
        }

        [Fact]
        public async Task CountAsync_ReturnAmountOfCathedras()
        {
            var cathedras = new List<Cathedra>
            {
                new Cathedra { Id = 1, Name = "Cathedra1", FacultyId = 1, GenitiveCase = "Cathedra1Genetive" },
                new Cathedra { Id = 2, Name = "Cathedra2", FacultyId = 1, GenitiveCase = "Cathedra2Genetive" },
                new Cathedra { Id = 3, Name = "Cathedra3", FacultyId = 2, GenitiveCase = "Cathedra3Genetive" }
            };

            var expectedAmountOfCathedras = cathedras.Count();

            var facultyFilter = new FacultyFilterModel { Skip = 0, Take = 3, FacultyId = null, OrderBy = null, Desc = false, Search = null };

            _repo.Setup(x => x.CountAsync(It.IsAny<ISpecification<Cathedra>>())).ReturnsAsync(cathedras.Count());

            var result = await _cathedraService.CountAsync(facultyFilter);

            _repo.Verify(x => x.CountAsync(It.IsAny<ISpecification<Cathedra>>()), Times.Once);

            Assert.Equal(expectedAmountOfCathedras, result);
        }

        [Fact]
        public async Task GetByFacultyAsync_FacultyIdIsNull()
        {
            var cathedras = new List<Cathedra>
            {
                new Cathedra { Id = 1, Name = "Cathedra1", FacultyId = 1, GenitiveCase = "Cathedra1Genetive" },
                new Cathedra { Id = 2, Name = "Cathedra2", FacultyId = 1, GenitiveCase = "Cathedra2Genetive" },
                new Cathedra { Id = 3, Name = "Cathedra3", FacultyId = 2, GenitiveCase = "Cathedra3Genetive" }
            };

            var expectedCathedraModels = new List<CathedraModel>
            {
                new CathedraModel { Id = 1, Name = "Cathedra1", FacultyId = 1, GenitiveCase = "Cathedra1Genetive" },
                new CathedraModel { Id = 2, Name = "Cathedra2", FacultyId = 1, GenitiveCase = "Cathedra2Genetive" },
                new CathedraModel { Id = 3, Name = "Cathedra3", FacultyId = 2, GenitiveCase = "Cathedra3Genetive" }
            };

            _repo.Setup(x => x.GetAllAsync()).ReturnsAsync(cathedras);
            _mapperMock.Setup(x => x.Map<IList<CathedraModel>>(cathedras)).Returns(expectedCathedraModels);

            var result = await _cathedraService.GetByFacultyAsync(null);

            _repo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.Equal(expectedCathedraModels, result);
        }

        [Fact]
        public async Task GetByFacultyAsync_FacultyIdIsNotNull()
        {
            var cathedras = new List<Cathedra>
            {
                new Cathedra { Id = 1, Name = "Cathedra1", FacultyId = 1, GenitiveCase = "Cathedra1Genetive" },
                new Cathedra { Id = 2, Name = "Cathedra2", FacultyId = 1, GenitiveCase = "Cathedra2Genetive" },
                new Cathedra { Id = 3, Name = "Cathedra3", FacultyId = 2, GenitiveCase = "Cathedra3Genetive" }
            };

            var expectedCathedraModels = new List<CathedraModel>
            {
                new CathedraModel { Id = 1, Name = "Cathedra1", FacultyId = 1, GenitiveCase = "Cathedra1Genetive" },
                new CathedraModel { Id = 2, Name = "Cathedra2", FacultyId = 1, GenitiveCase = "Cathedra2Genetive" }
            };

            var facultyId = 1;

            _repo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cathedra, bool>>>())).ReturnsAsync(cathedras);
            _mapperMock.Setup(x => x.Map<IList<CathedraModel>>(cathedras)).Returns(expectedCathedraModels);

            var result = await _cathedraService.GetByFacultyAsync(facultyId);

            _repo.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Cathedra, bool>>>()), Times.Once);

            Assert.Equal(expectedCathedraModels, result);
        }
    }
}
