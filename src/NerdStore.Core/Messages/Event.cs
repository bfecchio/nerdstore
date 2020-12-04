using System;
using MediatR;

namespace NerdStore.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        #region Public Properties

        public DateTime Timestamp { get; private set; }

        #endregion

        #region Constructors

        public Event()
        {
            Timestamp = DateTime.Now;
        }

        #endregion
    }
}
