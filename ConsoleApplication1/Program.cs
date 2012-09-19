using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            testAsync ins = new testAsync();
            ins.runAsync();


            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }

    public class testAsync 
    {
        public void runSync() 
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.google.com");
            request.Method = "Head";
            var response = (HttpWebResponse)request.GetResponse();
            string headersText = formatOutput(response.Headers);
            Console.WriteLine(headersText);

        }
        public void runAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.google.com");
            request.Method = "Head";
            request.BeginGetResponse(
                asyncResult => 
                {
                    var response = (HttpWebResponse)request.EndGetResponse(asyncResult);
                    string headersText = formatOutput(response.Headers);
                    Console.WriteLine(headersText);
                },null
                );
        }
        public string formatOutput(WebHeaderCollection input) 
        {
            
            var query = input.Keys.Cast<string>().Select(x => x+" >>>> "+input[x]).ToList();
            return string.Join(Environment.NewLine, query);
        
        }
    }
}
