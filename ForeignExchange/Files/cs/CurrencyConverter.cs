using ForeignExchange.Interfaces;
using System;
using System.Threading.Tasks;
using Terrasoft.Core.Factories;

namespace ForeignExchange
{
	[DefaultBinding(typeof(IForeignExchange))]
	public class CurrencyConverter : IForeignExchange
	{
		public Interfaces.IBankResult GetRate(string currencyFrom, DateTime date, string bmxToken="")
		{

			IBankResult r = null;
			IBank bank = null;
			switch (currencyFrom)
			{
				case "CAD":
					bank = BankFactory.GetBank(BankFactory.SupportedBanks.BOC);
					break;
				case "RUB":
					bank = BankFactory.GetBank(BankFactory.SupportedBanks.CBR);
					break;
				case "UAH":
					bank = BankFactory.GetBank(BankFactory.SupportedBanks.NBU);
					break;
				case "EUR":
					bank = BankFactory.GetBank(BankFactory.SupportedBanks.ECB);
					break;
				case "MXN":
					bank = BankFactory.GetBank(BankFactory.SupportedBanks.BOMX, bmxToken);
					break;
				case "AUD":
					bank = BankFactory.GetBank(BankFactory.SupportedBanks.RBA);
					break;
				case "GBP":
					bank = BankFactory.GetBank(BankFactory.SupportedBanks.BOE);
					break;
				default:
					break;
			}

			Task.Run(async () =>
			{
				r = await bank.GetRateAsync("USD", date);
			}).Wait();

			var result = new Result
			{
				BankName = r.BankName,
				RateDate = r.RateDate,
				HomeCurrency = r.HomeCurrency
			};
			result.ExchangeRate = (currencyFrom=="GBP")? decimal.Round(1/r.ExchangeRate, 4): r.ExchangeRate;
			return result;
		}
	}

	public class Result : Interfaces.IBankResult
	{
		public string HomeCurrency { get; set; }
		public decimal ExchangeRate { get; set; }
		public DateTime RateDate { get; set; }
		public string BankName { get; set; }
	}
}
