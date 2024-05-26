using AutoMapper;
using Moq;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Implementations;
using System.Threading.Tasks;
using Xunit;

namespace SRS.Testing.Services
{
    public class CathedraReportServiceTests
    {
        private readonly Mock<IBaseRepository<CathedraReport>> _repo;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CathedraReportService _cathedraReportService;

        public CathedraReportServiceTests(
            Mock<IBaseRepository<CathedraReport>> repo,
            Mock<IMapper> mapperMock,
            CathedraReportService cathedraReportService)
        {
            _repo = repo;
            _mapperMock = mapperMock;
            _cathedraReportService = cathedraReportService;
        }

        [Fact]
        public async Task GetForUserAsync_ShouldReturnAllCathedraReportsAsync()
        {

        }
    }
}
