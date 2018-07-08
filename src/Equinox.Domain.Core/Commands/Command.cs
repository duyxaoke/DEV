using System;
using Equinox.Domain.Core.Events;
using FluentValidation.Results;

namespace Equinox.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public const int Enable = 1;
        public const int Disable = 0;
        public const bool Active = true;
        public const bool InActive = false;
        public const string MessageSuccess = "Thành công!";
        public const string MessageError = "Thất bại!";

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}