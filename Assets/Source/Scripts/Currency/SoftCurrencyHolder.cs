public class SoftCurrencyHolder : CurrencyHolder
{
    protected override Currency InitCurrency()
        => new SoftCurrency();
    
}