using System;

namespace EventSourcing
{
    internal class BaseEvent
    {
        #region Public Properties

        public DateTime Timestamp { get; set; }

        #endregion
    }
}
