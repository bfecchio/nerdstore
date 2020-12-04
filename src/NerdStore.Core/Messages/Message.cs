using System;

namespace NerdStore.Core.Messages
{
    public abstract class Message
    {
        #region Public Properties

        public Guid AggregateId { get; protected set; }
        public string MessageType { get; protected set; }

        #endregion

        #region Constructors

        public Message()
        {
            MessageType = GetType().Name;
        }

        #endregion
    }
}
