using System.Collections.Generic;
using DigitalExchangeWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalExchangeWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    [HttpGet(Name = "GetCurrencies")]
    public IEnumerable<string> Get()
    {
        return new List<string> { Currency.A.ToString(), Currency.B.ToString() };
    }
}