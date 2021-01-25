using System.Collections.Generic;

namespace GlobalX.Coding.Assessment.Infrastructure
{
    public class Result<T>
    {
        public IList<string> Errors { get; set; }
        public T Value { get; set; }

        public Result(T value)
        {
            Errors = new List<string>();
            Value = value;
        }

        public Result(string error)
        {
            Errors = new List<string>();
            AddError(error);
        }

        public Result(IList<string> errors)
        {
            Errors = new List<string>(errors);
        }

        public bool Success
        {
            get { return (Errors.Count == 0); }
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
