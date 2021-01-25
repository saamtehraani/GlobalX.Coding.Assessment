using GlobalX.Coding.Assessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalX.Coding.Assessment.Infrastructure.IServices
{
    public interface IValidateService
    {
        Task<Result<List<Person>>> Validate();
    }
}
