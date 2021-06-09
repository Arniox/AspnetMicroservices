using System;

namespace Ordering.Application.Exceptions
{
    //Exception layer
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) 
            : base($"Entity \"{name}\" ({key}) was not found in the given database.")
        {
        }
    }
}
