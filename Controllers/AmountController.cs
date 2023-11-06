using System;
using DigitalExchangeWebApi.Extensions;
using DigitalExchangeWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalExchangeWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AmountController : ControllerBase
{
    [HttpPost("ConvertInputAmount")]
    public decimal ConvertInputAmount(decimal inputAmount, decimal ABRate,
        decimal commission, [FromBody] string currencyToConvertFrom)
    {
        return AmountCurrencyConverter.ConvertInputAmount(inputAmount, Enum.Parse<Currency>(currencyToConvertFrom),
            ABRate, commission);
    }

    [HttpPost("AmountOutOfBorders")]
    public bool AmountOutOfBorders(decimal cashFlowAmountInput, [FromBody] string cashFlowCurrencyInput)
    {
        return AmountExtension.AmountOutOfBorders(cashFlowAmountInput, Enum.Parse<Currency>(cashFlowCurrencyInput));
    }
}