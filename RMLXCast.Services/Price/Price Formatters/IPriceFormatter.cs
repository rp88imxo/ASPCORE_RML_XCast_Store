namespace RMLXCast.Services.Price.Price_Formatters
{
    public interface IPriceFormatter
    {
        string GetFormattedPrice(decimal price);
    }
}