using GlobalX.Coding.Assessment.Infrastructure;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GlobalX.Coding.Assessment.Services
{
    public class WriteFileService : IWriteFileService
    {
        private readonly GlobalXConfig _globalXConfig;
        public WriteFileService(
                IOptions<GlobalXConfig> globalXConfig
            )
        {
            _globalXConfig = globalXConfig.Value;
        }

        public async Task<Result<bool>> Write(List<Person> people)
        {
            try
            {
                var outPut = $"{_globalXConfig.Output}\\sorted-names-list.txt";

                if (File.Exists(outPut))
                {
                    File.Delete(outPut);
                }

                using StreamWriter file = new StreamWriter(outPut, true);
                foreach (Person person in people)
                {
                    string fullName = $"{person.GivenNames} {person.LastName}";
                    await file.WriteLineAsync(fullName);
                    Console.WriteLine(fullName);
                }

                return new Result<bool>(true);
            }
            catch (Exception e)
            {
                return new Result<bool>(e.Message);
            }
        }
    }
}
