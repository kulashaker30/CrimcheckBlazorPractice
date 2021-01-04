using System;
using System.Linq;
using System.Collections.Generic;

namespace OnlineClinic.Core.DTOs
{
    public class GenericResponseBase<T>
    {
        public GenericResponseBase(T value)
        {
            Value = value;
            Errors = new List<ValidationError>();
        }

        public GenericResponseBase(T value, List<ValidationError> errors)
        {
            Value = value;
            Errors = errors;
        }

        public T Value { get; private set; }

        public List<ValidationError> Errors { get; private set; }

        public bool Success { get => !Errors.Any(); }
    }
}