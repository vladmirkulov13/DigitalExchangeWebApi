using System;
using System.IO;
using DigitalExchangeWebApi.Models;
using Microsoft.Extensions.Configuration;

namespace DigitalExchangeWebApi.Extensions;

public class AmountExtension
{
    private readonly decimal _defCurrencyAmount;
    private readonly decimal _commission;
    private readonly decimal _ABRate;
    private static CurrencyLimited? CurrencyLimitedDict;

    public AmountExtension()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        IConfiguration config = builder.Build();

        _defCurrencyAmount = config.GetSection("DefaultCurrencyAmount").Get<decimal>();
        _commission = config.GetSection("Commission").Get<decimal>();
        _ABRate = config.GetSection("ABRate").Get<decimal>();
        CurrencyLimitedDict= config.GetSection("CurrencyLimited").Get<CurrencyLimited>();
    }

    /// <summary>
    /// Выходит ли заданное значение за границы
    /// </summary>
    /// <param name="cashFlowAmountInput"></param>
    /// <param name="cashFlowCurrencyInput"></param>
    /// <returns></returns>
    public static bool AmountOutOfBorders(decimal cashFlowAmountInput, Currency cashFlowCurrencyInput)
    {
        if (cashFlowCurrencyInput != Currency.A && cashFlowCurrencyInput != Currency.B)
        {
            throw new ArgumentOutOfRangeException(nameof(cashFlowCurrencyInput), cashFlowCurrencyInput,
                $"Необрабатывая валюта {cashFlowCurrencyInput}");
        }
        
        if (CurrencyLimitedDict != null && CurrencyLimitedDict.TryGetValue(cashFlowCurrencyInput.ToString(), out var limit))
        {
            return cashFlowAmountInput < limit.MinAllowToTransfer ||
                   cashFlowAmountInput > limit.MaxAllowToTransfer;
        }

        return false;
    }

    public static bool NotPositive(decimal cashFlowAmountInput) => cashFlowAmountInput <= 0;
}