using ForeignExchange.Interfaces;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Terrasoft.Web.Common;

namespace ForeignExchange
{
	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class TestService : BaseService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
		public BankResult Test(string currency)
		{
			BankResult bResult = new BankResult();
			IForeignExchange foreignExchange = Terrasoft.Core.Factories.ClassFactory.Get<IForeignExchange>();
			Interfaces.IBankResult result = foreignExchange.GetRate(currency, DateTime.Now);

			bResult.BankName = result.BankName;
			bResult.ExchangeRate = result.ExchangeRate;
			bResult.HomeCurrency = result.HomeCurrency;
			bResult.RateDate = result.RateDate;

			return bResult;
		}
	}
}



