using GlobalX.Coding.Assessment.Models;
using System.Collections.Generic;

namespace GlobalX.Coding.Assessment.Infrastructure.IServices
{
    public interface ISortService
    {
        Result<List<Person>> Sort(List<Person> people);
    }
}
