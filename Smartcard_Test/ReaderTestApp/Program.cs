/**
 * @author Olivier ROUIT
 * 
 * @license CPL, CodeProject license 
 */

using Core.Smartcard;
using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace ReaderTestApp
{

    class Program
    {
        public static bool configurado = false;
        public static string[] leitores;
        private string ip = "http://10.21.0.137";
        static void Main(string[] args)
        {
            try
            {
                string[] readerList = Reader.GetReaderList();
                Reader[] readers = new Reader[readerList.Length];

                for (int i = 0; i < readerList.Length; i++)
                {
                    Console.WriteLine(string.Format("Reader[{0}] = {1}", i, readerList[i]));

                    readers[i] = new Reader(readerList[i]);
                    readers[i].CardInserted += reader_CardInserted;
                    readers[i].CardRemoved += reader_CardRemoved;

                    Console.WriteLine("Reader: " + readerList[i] + " connected");
                    readers[i].StartCardEvents();
                }
                
                Console.WriteLine("Press any key to stop the application...");
                Console.ReadKey(true);

                for (int i = 0; i < readers.Length; i++)
                {
                    readers[i].StopCardEvents();
                    readers[i].Dispose();
                }

                Console.WriteLine("Card detection terminated");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
        }

        static void reader_CardRemoved(object sender, CardRemovedArgs args)
        {
            Console.WriteLine("A card has been removed from reader [" + args.Reader + "]");
        }


        static void reader_CardInserted(object sender, CardInsertedArgs args)
        {
            Console.WriteLine("A card has been inserted in reader [" + args.Reader + "]");
            

            var cmd = new APDUCommand(0xFF, 0xB0, 0x00, 0x03, null, (byte) 255);
            var response = args.Card.Transmit(cmd);

            if (response.Data != null)
            {
                Console.WriteLine(Encoding.ASCII.GetString(response.Data));
                PostAlgoAqui(new Models.SendNFC
                {
                    Leitor = args.Reader,
                    Leitura = Encoding.UTF8.GetString(response.Data)

                });
            }
            args.Card.Disconnect(DISCONNECT.Leave);
        }

        static void PostAlgoAqui(Models.SendNFC nfc)
        {
            HttpClient httpClient = new HttpClient();
            string ip = "http://192.168.1.107:8000";
            httpClient.BaseAddress = new Uri(ip);

            string s = JsonConvert.SerializeObject(nfc);

            var content = new StringContent(s, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync("", content);
            
        }
    }
}

