using Exercises.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Exercises.Services
{
    public class PunkApi
    {
        private readonly string apiUri = "https://api.punkapi.com/v2/";

        public PunkApi()
        {
        }


        public Beer GetById(int id)
        {
            if (GetBeers().Where(x => x.Id == id).FirstOrDefault() == null)
            {
                try
                {
                    SeedBeers(id);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return GetBeers().Where(x => x.Id == id).FirstOrDefault();
        }

        public void PutBeerRatings(int id, BeerUserRatings userRatings)
        {
            try
            {
                PutBeerUserRatings(id, userRatings);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void PutBeerUserRatings(int id, BeerUserRatings userRatings)
        {
            try
            {
                if (GetBeers().Where(x => x.Id == id).FirstOrDefault() == null)
                {
                    SeedBeers(id);
                }

                var beer = GetBeers().Where(x => x.Id == id).FirstOrDefault();
                if (beer != null && userRatings != null)
                {
                    if (beer.UserRatings == null)
                    {
                        beer.UserRatings = new List<BeerUserRatings>();
                    }
                    beer.UserRatings.Add(userRatings);

                    if (GetBeers().ToList().Count > 0)
                    {
                        PostBeers(AddBeersToJson(new List<Beer> { beer }, GetBeers().ToList().Where(x => x.Id != beer.Id).ToList()));
                    }
                    else
                    {
                        PostBeers(JsonConvert.SerializeObject(beer));
                    }
                }
                else
                {
                    throw new Exception("No Beer with id is found or no user Ratings are provided");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void SeedBeers(int id)
        {
            try
            {
                List<Beer> newBeers = new List<Beer>();
                using HttpClient client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
                HttpResponseMessage response = client.GetAsync(apiUri + $"beers/{id}").Result;
                response.EnsureSuccessStatusCode();
                string res = response.Content.ReadAsStringAsync().Result;
                newBeers = JsonConvert.DeserializeObject<List<Beer>>(res);
                if (GetBeers().ToList().Count > 0 && newBeers.Count > 0)
                {
                    PostBeers(AddBeersToJson(newBeers, GetBeers().ToList()));
                }
                else if(newBeers.Count > 0)
                {
                    PostBeers(JsonConvert.SerializeObject(newBeers));
                }
            }
            catch (Exception)
            {
                throw new Exception("No beer with id is found");
            }
        }

        public IQueryable<Beer> GetBeers()
        {
            try
            {
                if (File.Exists(@"Database\database.json"))
                {
                    using StreamReader r = new StreamReader(@"Database\database.json");
                    return JsonConvert.DeserializeObject<IList<Beer>>(r.ReadToEnd()).AsQueryable();
                }

                return new List<Beer>().AsQueryable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void PostBeers(string json)
        {
            File.WriteAllText(@"Database\database.json", json);
        }

        public string AddBeersToJson(List<Beer> beers, List<Beer> objects)
        {
            beers.AddRange(objects);
            return JsonConvert.SerializeObject(beers);
        }
    }
}
