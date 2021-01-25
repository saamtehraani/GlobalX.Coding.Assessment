using GlobalX.Coding.Assessment.Infrastructure;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Models;
using GlobalX.Coding.Assessment.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace GlobalX.Coding.Assessment.xUnitTest
{
    public class WriteFileServiceUnitTest
    {
        private IOptions<GlobalXConfig> _globalXConfig;

        public WriteFileServiceUnitTest()
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", false)
             .Build();

            _globalXConfig = Options.Create(configuration.GetSection("GlobalXConfig").Get<GlobalXConfig>());
        }

        [Fact]
        public async void Check_Writability()
        {
            //Arrange
            IWriteFileService sortService = new WriteFileService(_globalXConfig);

            //Act
            Result<bool> result = await sortService.Write(FakePeople());

            //Assert
            Assert.True(result.Success);
        }

        private List<Person> FakePeople()
        {
            return new List<Person>()
            {
                new Person()
                {
                    GivenNames="Janet",
                    LastName="Parsons"
                },
                new Person()
                {
                    GivenNames="Vaughn",
                    LastName="Lewis"
                },
                new Person()
                {
                    GivenNames="Adonis Julius",
                    LastName="Archer"
                }
            };
        }
    }
}
