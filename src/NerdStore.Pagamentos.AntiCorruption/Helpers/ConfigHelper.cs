using System;
using System.Linq;

namespace NerdStore.Pagamentos.AntiCorruption.Helpers
{
    public static class ConfigHelper
    {
        #region Public Methods

        public static string GetValue(string element)
            => new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(x => x[new Random().Next(x.Length)]).ToArray());

        #endregion
    }
}
