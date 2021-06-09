using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Application.Exceptions
{
    //Exception layer
    public class ValidationException : ApplicationException
    {
        //Error
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        //Constructor for any Command Validator files.
        //FluentValidation will throw an error that will be captured here and processed into the Errors Disctionary
        public ValidationException(IEnumerable<ValidationFailure> failures)
            :this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        //Error Dictionary
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
