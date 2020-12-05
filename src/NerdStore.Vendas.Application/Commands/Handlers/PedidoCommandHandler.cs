using MediatR;
using System.Threading;
using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.Handlers
{
    public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        #region IRequestHandler Members

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;
            return true;
        }

        #endregion

        #region Private Methods

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                // lançar um evento de erro
            }

            return false;
        }

        #endregion
    }
}
