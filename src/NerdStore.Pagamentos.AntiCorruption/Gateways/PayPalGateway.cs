using System;
using System.Linq;

namespace NerdStore.Pagamentos.AntiCorruption.Gateways
{
    public class PayPalGateway : IPayPalGateway
    {
        #region IPayPalGateway Members

        public bool CommitTransaction(string cardHashKey, string orderId, decimal amount)
            => new Random().Next(2) == 0;

        public string GetCardHashKey(string serviceKey, string cartaoCredito)
            => new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(x => x[new Random().Next(x.Length)]).ToArray());

        public string GetPayPalServiceKey(string apiKey, string encriptionKey)
            => new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(x => x[new Random().Next(x.Length)]).ToArray());

        #endregion
    }
}
