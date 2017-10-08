using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Net.Http;
using System.IO;

namespace TelegramBot
{
    class TelegrammApi
    {
        public static string Token { set; get; } = @"452723579:AAFVaFR6RidJKHAy8j7xn1jAVJilGgY8PNU";
        public static int LastUpdateID { set; get; } = 0;

        static void SendMessage(string message, int chatid)
        {
            using (var webClient = new WebClient())
            {
                var pars = new NameValueCollection();

                pars.Add("text", message);
                pars.Add("chat_id", chatid.ToString());


                webClient.UploadValues("https://api.telegram.org/bot" + Token + "/sendMessage", pars);

            }
        }

        public async static Task SendPhoto(string chatId, string filePath)
        {
            var url = string.Format("https://api.telegram.org/bot{0}/sendPhoto", Token);
            var fileName = filePath.Split('\\').Last();

            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(chatId.ToString(), Encoding.UTF8), "chat_id");

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    form.Add(new StreamContent(fileStream), "photo", fileName);

                    using (var client = new HttpClient())
                    {
                        await client.PostAsync(url, form);
                    }
                }
            }
        }


    }
}
