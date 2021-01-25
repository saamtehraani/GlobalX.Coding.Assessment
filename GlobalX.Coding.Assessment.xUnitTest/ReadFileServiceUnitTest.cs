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
    public class ReadFileServiceUnitTest
    {
        private GlobalXConfig _globalXConfig;
        public ReadFileServiceUnitTest()
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", false)
             .Build();

            _globalXConfig = Options.Create(configuration.GetSection("GlobalXConfig").Get<GlobalXConfig>()).Value;
        }

        [Fact]
        public async void Check_If_Cannot_Read()
        {
            //Arrange
            IReadFileService readFileService = new ReadFileService($"{_globalXConfig.FakeNames}\\fake.txt");

            //Act
            Result<List<Person>> result = await readFileService.ReadAsync();

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async void Check_If_Can_Read()
        {
            //Arrange
            IReadFileService readFileService = new ReadFileService(_globalXConfig.FakeNames);

            //Act
            Result<List<Person>> result = await readFileService.ReadAsync();

            //Assert
            Assert.True(result.Success);
        }
    }
}
