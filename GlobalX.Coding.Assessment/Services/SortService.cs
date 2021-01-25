using GlobalX.Coding.Assessment.Infrastructure;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalX.Coding.Assessment.Services
{
    public class SortService : ISortService
    {
        public Result<List<Person>> Sort(List<Person> people)
        {
            try
            {
                List<Person> sortedPeople = people.OrderBy(q => q.LastName).ThenBy(q => q.GivenNames).ToList();
                return new Result<List<Person>>(sortedPeople);
            }
            catch (Exception e)
            {
                return new Result<List<Person>>(e.Message);
            }
        }
    }
}
