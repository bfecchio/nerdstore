namespace NerdStore.Pagamentos.AntiCorruption.Gateways
{
    public interface IPayPalGateway
    {
        #region IPayPalGateway Members

        string GetPayPalServiceKey(string apiKey, string encriptionKey);
        string GetCardHashKey(string serviceKey, string cartaoCredito);
        bool CommitTransaction(string cardHashKey, string orderId, decimal amount);

        #endregion
    }
}
