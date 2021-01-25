using GlobalX.Coding.Assessment.Infrastructure;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GlobalX.Coding.Assessment.Services
{
    public class ReadFileService : IReadFileService
    {
        private readonly string _filePath = string.Empty;
        public ReadFileService(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<Result<List<Person>>> ReadAsync()
        {
            try
            {
                string[] lines = await File.ReadAllLinesAsync(_filePath);
                List<Person> people = new List<Person>();

                foreach (string line in lines)
                {
                    string[] names = line.Split(' ');
                    string givenName = string.Join(" ", names[0..(names.Length - 1)]);
                    string lastName = names[names.Length - 1];

                    people.Add(new Person()
                    {
                        LastName = lastName,
                        GivenNames = givenName
                    });
                }

                return new Result<List<Person>>(people);
            }
            catch (Exception e)
            {
                return new Result<List<Person>>(e.Message);
            }
        }
    }
}
