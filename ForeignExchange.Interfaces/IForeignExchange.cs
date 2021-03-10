using System;

namespace ForeignExchange.Interfaces
{
	public interface IForeignExchange
	{
		IBankResult GetRate(string currencyFrom, DateTime date, string bmxToken = "");
	}
}
