using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using FieldLevel.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace FieldLevel
{
    public class BusinessLogic
    {
        //This function is called by the front end controller and returns a list containing the most recent post for each user
        public static List<UserPost> GetMostRecentUserPosts(IConfiguration configuration, IMemoryCache memoryCache)
        {
            List<UserPost> posts = new List<UserPost>();

            List<UserPost> allPosts = GetFullUserPostsDataSet(configuration, memoryCache).OrderBy(o => o.id).ToList();
            List<int> distinctUsers = allPosts.Select(x => x.userId).Distinct().ToList();
            
            foreach(int i in distinctUsers)
                posts.Add(allPosts.Where(e => e.userId == i).OrderByDescending(r => r.id).FirstOrDefault());

            return posts;
        }

        //This function is called by GetMostRecentUserPosts and gets the full list of posts
        private static List<UserPost> GetFullUserPostsDataSet(IConfiguration configuration, IMemoryCache memoryCache)
        {
            bool retrieveData = false;

            if (memoryCache.Get("PostsDataFeedTimeStamp") == null)
                retrieveData = true;
            else
            {
                DateTime feedTimeStamp = DateTime.Parse(memoryCache.Get("PostsDataFeedTimeStamp").ToString());
                if (feedTimeStamp <= DateTime.UtcNow.AddMinutes(-1)) retrieveData = true;
            }

            if (retrieveData)
                DownloadAndSavePostsData(configuration, memoryCache);

            string json = memoryCache.Get("PostsDataFeed").ToString();
            return JsonConvert.DeserializeObject<List<UserPost>>(json); 
        }

        private static void DownloadAndSavePostsData(IConfiguration configuration, IMemoryCache memoryCache)
        {
            string postsUrl = configuration.GetSection("AppSettings")["PostsUrl"].ToString();

            RESTRequest rItem = new RESTRequest();
            rItem.url = postsUrl;

            rItem.method = "GET";
            rItem.post = "";
            rItem.content = "text/plain";
            rItem.headers = new System.Net.WebHeaderCollection();

            string req = REST.GenerateRequest(rItem);
            if (!string.IsNullOrEmpty(req))
            {
                memoryCache.Set("PostsDataFeedTimeStamp", DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToShortTimeString());
                memoryCache.Set("PostsDataFeed", req);
            }
            else
                throw new Exception("No data returned from call");
        }
    }
}
