using System;
using NerdStore.Core.Messages;
using System.Collections.Generic;

namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        #region Private Fields

        private List<Event> _notificacoes;

        #endregion

        #region Public Properties

        public Guid Id { get; set; }
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        #endregion

        #region Constructors

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Behaviours

        public virtual bool EhValido()
            => throw new NotImplementedException();

        public void AdicionarEvento(Event evento)
        {
            _notificacoes = _notificacoes ?? new List<Event>();
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event evento)
            => _notificacoes?.Remove(evento);

        public void LimparEventos()
            => _notificacoes?.Clear();

        #endregion

        #region Overriden Methods

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        #endregion

        #region Operators Methods

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        #endregion
    }
}
