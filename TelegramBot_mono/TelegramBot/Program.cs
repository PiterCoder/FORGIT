using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using SimpleJSON;
using System.Collections.Specialized;
using System.IO;

namespace TelegramBot
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			while (true)
			{
				GetUpdates();
				Thread.Sleep(1000);
			}
		}
		static void GetUpdates()
		{
			using (var webClient = new WebClient())
			{
				Console.WriteLine("Запрос обновление {0}", TelegrammApi.LastUpdateID + 1);


				string response = webClient.DownloadString("https://api.telegram.org/bot" + TelegrammApi.Token + "/getUpdates" + "?offset=" + (TelegrammApi.LastUpdateID + 1));

				var N = JSON.Parse(response);

				foreach (JSONNode r in N["result"].AsArray)
				{
					TelegrammApi.LastUpdateID = r["update_id"].AsInt;

					Console.WriteLine("Пришло сообщение: {0}", r["message"]["text"]);

                    TelegrammApi.SendMessage("Я получил твоё сообщение", r["message"]["chat"]["id"].AsInt);
					//TelegrammApi.SendPhoto(r["message"]["chat"]["id"], "0.png").Wait();
				}
			}
		}




	}

}
