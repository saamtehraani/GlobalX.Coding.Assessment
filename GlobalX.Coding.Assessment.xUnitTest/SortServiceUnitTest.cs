using GlobalX.Coding.Assessment.Infrastructure;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Models;
using GlobalX.Coding.Assessment.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GlobalX.Coding.Assessment.xUnitTest
{
    public class SortServiceUnitTest
    {
        [Fact]
        public void Check_Sortability()
        {
            //Arrange
            ISortService sortService = new SortService();

            //Act
            Result<List<Person>> result = sortService.Sort(FakePeople());

            //Assert
            Assert.True(result.Value.First().LastName== "Archer");
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
