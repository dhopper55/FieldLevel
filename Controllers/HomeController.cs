using FieldLevel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace FieldLevel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private IMemoryCache _cache;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _logger = logger;
            _configuration = configuration;
            _cache = memoryCache;
        }

        public IActionResult Test2()
        {
            List<string> strs = new List<string>();
            strs.Add("David 1");
            strs.Add("David 1");
            strs.Add("David 1");
            strs.Add("David 2");
            strs.Add("David 2");
            strs.Add("David 3");
            strs.Add("David 4");
            strs.Add("David 5");
            strs.Add("David 8");
            strs.Add("David 8");
            Dictionary<string, int> ret = Utilities.freqCnt(strs);
            Dictionary<string, int> ret2 = Utilities.FrequencyCount<string>(strs);

            return View();
        }

        public IActionResult Test()
        {
            //List<string> prodList = new List<string>();
            //prodList.Add("Prod 1");
            //prodList.Add("Prod 1");
            //prodList.Add("Prod 2");
            //prodList.Add("Prod 2");
            //prodList.Add("Prod 3");
            //prodList.Add("Prod 3");
            //prodList.Add("Prod 4");
            //prodList.Add("Prod 4");

            //for (int i = 5; i < 500000; i++)
            //{
            //    prodList.Add("Prod " + i.ToString());
            //    prodList.Add("Prod " + i.ToString());
            //}
            //prodList.Add("Prod 500001");
            //prodList.Add("Prod 500002");
            //prodList.Add("Prod 500002");


            //int timer = Environment.TickCount;
            //string s = FieldLevel.Models.Product.FirstUniqueProduct(prodList.ToArray());
            //timer = Environment.TickCount - timer;




            //Hobbies hobbies = new Hobbies();
            //hobbies.Add("Steve", "Fashion", "Piano", "Reading");
            //hobbies.Add("Patty", "Drama", "Magic", "Pets");
            //hobbies.Add("Chad", "Puzzles", "Pets", "Yoga");
            //List<string> sList = hobbies.FindAllHobbyists("Yoga");






            return View();
        }

        public IActionResult Index()
        {
            List<UserPost> postList = FieldLevel.BusinessLogic.GetMostRecentUserPosts(_configuration, _cache);

            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Utilities
    {
        public static Dictionary<String, Int32> freqCnt(List<String> data)
        {
            Dictionary<String, Int32> result = new Dictionary<String, Int32>();



            for (Int32 i = 0; i < data.Count; i++)
            {
                if (result.ContainsKey(data[i]))
                {
                    Int32 value = 0;
                    result.TryGetValue(data[i], out value);
                    result.Remove(data[i]); // -- otherwise an exception on the next line?!?
                    result.Add(data[i], value + 1);
                }
                else
                    result.Add(data[i], 1);
            }

            return result;
        }

        public static Dictionary<T, Int32> FrequencyCount<T>(IEnumerable<T> data)
        {
            var result = new Dictionary<T, Int32>();
            foreach (var word in data)
            {
                if (result.ContainsKey(word))
                {
                    result[word] = result[word] + 1;
                }
                else
                {
                    result[word] = 1;
                }
            }

            return result;
        }

    }

}
