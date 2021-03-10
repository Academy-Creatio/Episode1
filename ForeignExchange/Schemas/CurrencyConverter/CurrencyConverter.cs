namespace Terrasoft.Core.Process.Configuration
{
	using ForeignExchange.Interfaces;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;
	using Terrasoft.UI.WebControls.Controls;

	#region Class: CurrencyConverter

	/// <exclude/>
	public partial class CurrencyConverter
	{

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {

			IForeignExchange foreignExchange = Factories.ClassFactory.Get<IForeignExchange>();

			string currencyCode = GetShortCurrencyCode(Currency);

			IBankResult result = default;
			if (currencyCode == "MXN" && SysSettings.TryGetValue(UserConnection, "bmxToken", out object value))
			{
				string token = value.ToString();
				result = foreignExchange.GetRate(currencyCode, Date, value.ToString());
			}
			else
			{
				result = foreignExchange.GetRate(currencyCode, Date);
			}

			ExchangeRate = result.ExchangeRate;
			ObservationDate = result.RateDate;
			BankName = result.BankName;
			return true;
		}

		#endregion

		#region Methods: Public

		public override bool CompleteExecuting(params object[] parameters) {
			return base.CompleteExecuting(parameters);
		}

		public override void CancelExecuting(params object[] parameters) {
			base.CancelExecuting(parameters);
		}

		public override string GetExecutionData() {
			return string.Empty;
		}

		public override ProcessElementNotification GetNotificationData() {
			return base.GetNotificationData();
		}


		public string GetShortCurrencyCode(Guid CurrencyId)
		{
			EntitySchema currencySchema = UserConnection.EntitySchemaManager.GetInstanceByName("Currency");
			Entity currency = currencySchema.CreateEntity(UserConnection);
			currency.FetchFromDB(CurrencyId);
			var shortCode = currency.GetTypedColumnValue<string>("ShortName");

			return shortCode;
		}

		#endregion

	}

	#endregion

}

