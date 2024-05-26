using AutoMapper;
using Moq;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Implementations;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace SRS.Testing.Services
{
    public class BaseCrudServiceTests
    {
        private readonly Mock<IBaseRepository<Faculty>> _repoFaculty;
        private readonly Mock<IBaseRepository<Cathedra>> _repoCathedra;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BaseCrudService<Faculty, FacultyModel> _facultyCrudService;
        private readonly BaseCrudService<Cathedra, CathedraModel> _cathedraCrudService;


        public BaseCrudServiceTests()
        {
            _repoFaculty = new Mock<IBaseRepository<Faculty>>();
            _repoCathedra = new Mock<IBaseRepository<Cathedra>>();
            _mapperMock = new Mock<IMapper>();
            _facultyCrudService = new BaseCrudService<Faculty, FacultyModel>(_repoFaculty.Object, _mapperMock.Object);
            _cathedraCrudService = new BaseCrudService<Cathedra, CathedraModel>(_repoCathedra.Object, _mapperMock.Object);
        }

        [Fact]
        public void AddAsync_ShouldAddFacultyAsync()
        {
            var facultiesModel = new FacultyModel { Id = 1, Name = "Faculty1" };

            _facultyCrudService.AddAsync(facultiesModel);

            _repoFaculty.Verify(x => x.AddAsync(It.IsAny<Faculty>()), Times.Once);
        }

        [Fact]
        public void AddAsync_ShouldAddCathedraAsync()
        {
            var cathedraModel = new CathedraModel { Id = 1, Name = "Cathedra1", FacultyId = 2 };

            _cathedraCrudService.AddAsync(cathedraModel);

            _repoCathedra.Verify(x => x.AddAsync(It.IsAny<Cathedra>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ReturnsFacultyById()
        {
            // Arrange
            var facultyId = 1;
            var faculty = new Faculty { Id = facultyId, Name = "Faculty" };
            var expectedFacultyModel = new FacultyModel { Id = facultyId, Name = "Faculty" };
            _repoFaculty.Setup(x => x.GetAsync(facultyId)).ReturnsAsync(faculty);
            _mapperMock.Setup(x => x.Map<FacultyModel>(faculty)).Returns(expectedFacultyModel);

            // Act
            var result = await _facultyCrudService.GetAsync(facultyId);

            // Assert
            Assert.Equal(expectedFacultyModel, result);
        }

        [Fact]
        public async Task GetAsync_ReturnsCathedraById()
        {
            // Arrange
            var cathedraId = 1;
            var cathedra = new Cathedra { Id = cathedraId, Name = "Cathedra" };
            var expectedCathedraModel = new CathedraModel { Id = cathedraId, Name = "Cathedra" };
            _repoCathedra.Setup(x => x.GetAsync(cathedraId)).ReturnsAsync(cathedra);
            _mapperMock.Setup(x => x.Map<CathedraModel>(cathedra)).Returns(expectedCathedraModel);

            // Act
            var result = await _cathedraCrudService.GetAsync(cathedraId);

            // Assert
            Assert.Equal(expectedCathedraModel, result);
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

            _repoFaculty.Setup(x => x.GetAllAsync()).ReturnsAsync(faculties);
            _mapperMock.Setup(x => x.Map<IList<FacultyModel>>(faculties)).Returns(expectedFacultyModels);

            var result = await _facultyCrudService.GetAllAsync();

            _repoFaculty.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.Equal(expectedFacultyModels, result);
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

            _repoCathedra.Setup(x => x.GetAllAsync()).ReturnsAsync(cathedras);
            _mapperMock.Setup(x => x.Map<IList<CathedraModel>>(cathedras)).Returns(expectedCathedraModels);


            var result = await _cathedraCrudService.GetAllAsync();

            _repoCathedra.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.Equal(expectedCathedraModels, result);
        }

        [Fact]
        public void UpdateAsync_ShouldUpdateFacultyAsync()
        {
            var facultiesModel = new FacultyModel { Id = 1, Name = "Faculty1" };

            _facultyCrudService.UpdateAsync(facultiesModel);

            _repoFaculty.Verify(x => x.UpdateAsync(It.IsAny<Faculty>()), Times.Once);
        }

        [Fact]
        public void UpdateAsync_ShouldUpdateCathedraAsync()
        {
            var cathedraModel = new CathedraModel { Id = 1, Name = "Cathedra1", FacultyId = 2 };

            _cathedraCrudService.UpdateAsync(cathedraModel);

            _repoCathedra.Verify(x => x.UpdateAsync(It.IsAny<Cathedra>()), Times.Once);
        }
        [Fact]
        public void DeleteAsync_ShouldDeleteFacultyAsync()
        {
            var facultiesModel = new FacultyModel { Id = 1, Name = "Faculty1" };

            _facultyCrudService.DeleteAsync(facultiesModel.Id);

            _repoFaculty.Verify(x => x.DeleteAsync(facultiesModel.Id), Times.Once);
        }

        [Fact]
        public void DeleteAsync_ShouldDeleteCathedraAsync()
        {
            var cathedraModel = new CathedraModel { Id = 1, Name = "Cathedra1", FacultyId = 2 };

            _cathedraCrudService.DeleteAsync(cathedraModel.Id);

            _repoCathedra.Verify(x => x.DeleteAsync(cathedraModel.Id), Times.Once);
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

            _repoFaculty.Setup(x => x.CountAsync()).ReturnsAsync(faculties.Count());

            var result = await _facultyCrudService.CountAsync();

            _repoFaculty.Verify(x => x.CountAsync(), Times.Once);

            Assert.Equal(expectedAmountOfFaculties, result);
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

            _repoCathedra.Setup(x => x.CountAsync()).ReturnsAsync(cathedras.Count());

            var result = await _cathedraCrudService.CountAsync();

            _repoCathedra.Verify(x => x.CountAsync(), Times.Once);

            Assert.Equal(expectedAmountOfCathedras, result);
        }
    }
}
