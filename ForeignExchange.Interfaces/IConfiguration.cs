using Terrasoft.Core;

namespace ForeignExchange.Interfaces
{
	/// <summary>
	/// Provides access to commonly used Terrasoft.Configuration functions
	/// </summary>
	public interface IConfiguration
	{

		/// <summary>
		/// Sends WebSocketMessage to a user identified by UserConnection
		/// </summary>
		/// <param name="userConnection"></param>
		/// <param name="senderName"></param>
		/// <param name="message"></param>
		void PostMessage(UserConnection userConnection, string senderName, string message);

		/// <summary>
		/// Sends WebSocketMessage to all users
		/// </summary>
		/// <param name="senderName"></param>
		/// <param name="message"></param>
		void PostMessageToAll(string senderName, string message);
	}
}
