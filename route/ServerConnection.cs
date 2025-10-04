using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace route
{
    internal class ServerConnection
    {
        HttpClient client = new HttpClient();
        string baseurl = "";

        public ServerConnection(string baseurl)
        {
            if (!baseurl.StartsWith("http://"))
            {
                throw new ArgumentException("hibas url");
            }
            this.baseurl = baseurl;
        }

        public async Task<List<cars>> getcars()
        {
            List<cars> result = new List<cars>();
            string url = baseurl + "/getcar";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                result = JsonSerializer.Deserialize<List<cars>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return result;
        }
        public async Task<List<owners>> getowner()
        {
            List<owners> result = new List<owners>();
            string url = baseurl + "/getowners";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                result = JsonSerializer.Deserialize<List<owners>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return result;
        }
        public async Task<List<manufacturers>> getmanu()
        {
            List<manufacturers> result = new List<manufacturers>();
            string url = baseurl + "/getowners";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                result = JsonSerializer.Deserialize<List<manufacturers>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return result;
        }
        public async Task<Message> postcars(int mmanufacturersid, string model,int power,int makeyear,int tyresize) {
            Message message = new Message();
            string url = baseurl + "/createcar";
            try
            {
                var jsondata = new
                {
                    mmanufacturersid = mmanufacturersid,
                    model = model,
                    power = power,
                    makeyear = makeyear,
                    tyresize = tyresize

                };
                string jsonstring = JsonSerializer.Serialize(jsondata);
                HttpContent content = new StringContent(jsonstring,Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); ;
            }
            return message;
        }
        public async Task<Message> postowner(int carsid, string name, string address, int birthyear)
        {
            Message message = new Message();
            string url = baseurl + "/createowner";
            try
            {
                var jsondata = new
                {
                    carsid = carsid,
                    name = name,
                    address = address,
                    birthyear = birthyear

                };
                string jsonstring = JsonSerializer.Serialize(jsondata);
                HttpContent content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); ;
            }
            return message;
        }
        public async Task<Message> postmanufct(string name, int launchyear, string country, string makeyear )
        {
            Message message = new Message();
            string url = baseurl + "/createmanufacturer";
            try
            {
                var jsondata = new
                {
                    name = name,
                    country = country,
                    launchyear = launchyear,
                    makeyear = makeyear

                };
                string jsonstring = JsonSerializer.Serialize(jsondata);
                HttpContent content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); ;
            }
            return message;
        }
        public async Task<Message> deletecar(int id)
        {
            Message message = new Message();
            string url = $"{baseurl}/deletecar/{id}";
            try
            {
                var jsondata = new
                {
                    id = id
                };
                string jsonstring = JsonSerializer.Serialize(jsondata) ;
                HttpContent content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync()) ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return message;
        }
        public async Task<Message> deleteowner(int id)
        {
            Message message = new Message();
            string url = $"{baseurl}/deletowner/{id}";
            try
            {
                var jsondata = new
                {
                    id = id
                };
                string jsonstring = JsonSerializer.Serialize(jsondata);
                HttpContent content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return message;
        }
        public async Task<Message> deletemanufact(int id)
        {
            Message message = new Message();
            string url = $"{baseurl}/deletemanufacturer/{id}";
            try
            {
                var jsondata = new
                {
                    id = id
                };
                string jsonstring = JsonSerializer.Serialize(jsondata);
                HttpContent content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return message;
        }
    }
}
