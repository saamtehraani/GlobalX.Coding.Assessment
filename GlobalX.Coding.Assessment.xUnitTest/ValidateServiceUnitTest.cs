using GlobalX.Coding.Assessment.Infrastructure;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Models;
using GlobalX.Coding.Assessment.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace GlobalX.Coding.Assessment.xUnitTest
{
    public class ValidateServiceUnitTest
    {
        private GlobalXConfig _globalXConfig;
        public ValidateServiceUnitTest()
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", false)
             .Build();

            _globalXConfig = Options.Create(configuration.GetSection("GlobalXConfig").Get<GlobalXConfig>()).Value;
        }

        [Fact]
        public async void Check_If_File_Does_Not_Exist()
        {
            //Arrange
            Mock<IReadFileService> _readFileService = new Mock<IReadFileService>();
            IValidateService validateService = new ValidateService($"{_globalXConfig.FakeNames}\\fake.txt", _readFileService.Object);

            //Act
            Result<List<Person>> result = await validateService.Validate();

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async void Check_If_One_Person_Has_Just_LastName()
        {
            //Arrange
            Mock<IReadFileService> _readFileService = new Mock<IReadFileService>();
            List<Person> fakePeople = FakePeople();
            _readFileService.Setup(q => q.ReadAsync())
                .Returns(Task.FromResult(new Result<List<Person>>(fakePeople)));
            IValidateService validateService = new ValidateService(_globalXConfig.FakeNames, _readFileService.Object);

            //Act
            Result<List<Person>> result = await validateService.Validate();

            //Assert
            Assert.False(result.Success);
        }

        private List<Person> FakePeople()
        {
            return new List<Person>()
            {
                new Person()
                {
                    GivenNames="Saam",
                    LastName="Tehrani"
                },
                new Person()
                {
                    GivenNames="Test"
                }
            };
        }
    }
}
