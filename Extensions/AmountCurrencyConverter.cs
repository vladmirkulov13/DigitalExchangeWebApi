using System;
using DigitalExchangeWebApi.Models;

namespace DigitalExchangeWebApi.Extensions;

public class AmountCurrencyConverter
{
    /// <summary>
    /// Конвертация валют согласно курсу
    /// </summary>
    /// <param name="inputAmount"></param>
    /// <param name="currencyToConvertFrom"></param>
    /// <param name="ABRate"></param>
    /// <param name="commission"></param>
    /// <param name="course"> Курс А:Б</param>
    /// <returns></returns>
    public static decimal ConvertInputAmount(decimal inputAmount, Currency currencyToConvertFrom, decimal ABRate,
        decimal commission)
    {
        decimal currentCommission;
        switch (currencyToConvertFrom)
        {
            case Currency.A:
                currentCommission = inputAmount * commission;
                return inputAmount * ABRate - currentCommission;
            case Currency.B:
                currentCommission = inputAmount / ABRate * commission;
                return inputAmount / ABRate - currentCommission;
            default:
                throw new ArgumentOutOfRangeException(nameof(currencyToConvertFrom), currencyToConvertFrom,
                    $"Необрабатывая валюта {currencyToConvertFrom}");
        }
    }
}