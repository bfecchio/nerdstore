using System;
using MediatR;
using FluentValidation.Results;

namespace NerdStore.Core.Messages
{
    public class Command : Message, IRequest<bool>
    {
        #region Public Properties

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        #endregion

        #region Constructors

        public Command()
        {
            Timestamp = DateTime.Now;
        }

        #endregion

        #region Behaviours

        public virtual bool EhValido()
            => throw new NotImplementedException();

        #endregion
    }
}
