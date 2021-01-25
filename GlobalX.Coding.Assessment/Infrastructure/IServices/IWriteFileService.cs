using GlobalX.Coding.Assessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalX.Coding.Assessment.Infrastructure.IServices
{
    public interface IWriteFileService
    {
        Task<Result<bool>> Write(List<Person> people);
    }
}
