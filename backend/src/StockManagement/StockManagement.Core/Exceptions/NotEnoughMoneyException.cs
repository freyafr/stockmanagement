namespace StockManagement.Core.Exceptions
{
    public class NotEnoughMoneyException(string message) : Exception(message)
    {
    }
}
