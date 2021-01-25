using GlobalX.Coding.Assessment.Infrastructure;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalX.Coding.Assessment.Services
{
    public class ValidateService : IValidateService
    {
        private readonly string _filePath = string.Empty;
        private readonly IReadFileService _readFileService;

        public ValidateService(
            string filePath,
            IReadFileService readFileService
        )
        {
            _filePath = filePath;
            _readFileService = readFileService;
        }

        public async Task<Result<List<Person>>> Validate()
        {
            try
            {
                bool checkPath = CheckPath();
                if (!checkPath)
                    return new Result<List<Person>>("File does not exist.");

                var people = await _readFileService.ReadAsync();
                if (!people.Success)
                    return new Result<List<Person>>(people.Errors.First());

                bool invalidPeople = people.Value.Any(q => string.IsNullOrEmpty(q.LastName) || string.IsNullOrEmpty(q.GivenNames));
                if (invalidPeople)
                    return new Result<List<Person>>($"There are some invalid names in the file.");

                return new Result<List<Person>>(people.Value);
            }
            catch(Exception e)
            {
                return new Result<List<Person>>(e.Message);
            }
        }

        private bool CheckPath()
        {
            if (string.IsNullOrEmpty(_filePath) || !File.Exists(_filePath))
            {
                return false;
            }

            return true;
        }
    }
}
