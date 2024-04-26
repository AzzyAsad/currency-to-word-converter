namespace CurrencyToWordConverter.Server.API.Services.IService
{
    public interface ICurrencyWordConverterService
    {
        public Task<string> CurrencyConversion(decimal currency);
    }
}
