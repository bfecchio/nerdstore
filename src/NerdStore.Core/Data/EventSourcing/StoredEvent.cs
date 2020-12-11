using System;

namespace NerdStore.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        #region Public Properties

        public Guid Id { get; private set; }
        public string Tipo { get; private set; }
        public DateTime DataOcorrencia { get; private set; }
        public string Dados { get; private set; }

        #endregion

        #region Constructors

        public StoredEvent(Guid id, string tipo, DateTime dataOcorrencia, string dados)
        {
            Id = id;
            Tipo = tipo;
            DataOcorrencia = dataOcorrencia;
            Dados = dados;
        }

        #endregion
    }
}
