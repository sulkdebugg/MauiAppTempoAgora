using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MauiAppTempoAgora.models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade) 
        {
            Tempo? t = null;

            string key = "c6af6d6603df58746b3a79a0d19753a6";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"q={cidade}&units=metric&appid={key}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    
                    var  rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        sunrise = sunrise.ToString("HH:mm:ss"),
                        sunset = sunset.ToString("HH:mm:ss"),
                        temp = (double)rascunho["main"]["temp"],
                        feels_like = (double)rascunho["main"]["feels_like"],
                        humidity = (int)rascunho["main"]["humidity"],
                        sea_level = (int?)rascunho["main"]["sea_level"],
                        speed = (double)rascunho["wind"]["speed"],
                        timezone = (int)rascunho["timezone"],
                    };


                } 

            }



            return t;
        }


    }
}
