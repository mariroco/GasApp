using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GasApp.Extensions
{
    public static class GasExtensions
    {
        public static async void SafeFireAndForget(this Task gas,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await gas.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
