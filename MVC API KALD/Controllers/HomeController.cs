using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_API_KALD.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace MVC_API_KALD.Controllers
{
    public class HomeController : Controller
    {
       
            public List<Joke> JokeList = new List<Joke>();
            public async Task<ActionResult> Index()
            {
                ViewBag.Title = "Home Page";
                ViewModel vm = new ViewModel();
                vm.Dadjoke = await GetDadJoke();
                vm.ChuckNorris = await GetCNJoke();
                return View(vm);
            }

            [System.Web.Mvc.HttpPost]
            public async Task<ActionResult> StoreJokes(string Vote, ViewModel vm)
            {
                if (Vote == "Dadjoke")
                {
                    vm.Dadjoke.Votes++;
                }
                else
                {
                    vm.ChuckNorris.Votes++;
                }
                JokeList.Add(vm.Dadjoke);
                JokeList.Add(vm.ChuckNorris);
                return RedirectToAction("Index");
            }

            public async Task<Joke> GetDadJoke()
            {
                Joke joke = new Joke();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = await client.GetStringAsync("https://icanhazdadjoke.com/");
                JObject jsonContent = JObject.Parse(content);
                joke.ID = jsonContent["id"].ToString();
                joke.JokeContent = jsonContent["joke"].ToString();
                joke.Votes = 0;
                joke.Type = true;
                return joke;
            }

            public async Task<Joke> GetCNJoke()
            {
                Joke joke = new Joke();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = await client.GetStringAsync("https://api.chucknorris.io/jokes/random");
                JObject jsonContent = JObject.Parse(content);
                joke.ID = jsonContent["id"].ToString();
                joke.JokeContent = jsonContent["value"].ToString();
                joke.Votes = 0;
                joke.Type = false;
                return joke;
            }

        

        public ActionResult About()
        {
            Calendar c = new Calendar();
            DateTime today = DateTime.Today;
            int daysThisMonth = DateTime.DaysInMonth(today.Year, today.Month);
            c.DaysInMonth = new string[6, 7];
            int day = 1;
            int weekday;
            DateTime dayOfMonth = new DateTime(today.Year, today.Month, day);
            for (int i = 0; i < 6; i++)
            {
                int startweek = 1;
                for (int j = 0; j < 7; j++)
                {
                    weekday = (int)dayOfMonth.DayOfWeek;
                    if (startweek == 7)
                    {
                        startweek = 0;
                    }
                    if (day <= daysThisMonth)
                    {
                        if (startweek == weekday)
                        {
                            if (day == DateTime.Now.Day)
                            {
                                c.DaysInMonth[i, j] = "*";
                            }
                            else
                            {
                                c.DaysInMonth[i, j] = day.ToString();
                            }
                            dayOfMonth = dayOfMonth.AddDays(1);
                            day++;
                        }
                        startweek++;
                    }
                }
            }

            return View(c);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}