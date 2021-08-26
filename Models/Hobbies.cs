using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FieldLevel.Models
{
    public class Hobbies
    {
        private readonly Dictionary<string, string[]> hobbies = new Dictionary<string, string[]>();

        public void Add(string hobbyist, params string[] hobbies)
        {
            this.hobbies.Add(hobbyist, hobbies);
        }

        public List<string> FindAllHobbyists(string hobby)
        {
            List<string> retList = new List<string>();
            List<KeyValuePair<string, string[]>> hobbyPeople = hobbies.Where(e => e.Value.Contains(hobby)).ToList();

            foreach (var item in hobbyPeople)
                retList.Add(item.Key);

            return retList;
        }

        //public static void Main(string[] args)
        //{
        //    //Hobbies hobbies = new Hobbies();
        //    //hobbies.Add("Steve", "Fashion", "Piano", "Reading");
        //    //hobbies.Add("Patty", "Drama", "Magic", "Pets");
        //    //hobbies.Add("Chad", "Puzzles", "Pets", "Yoga");

        //    //hobbies.FindAllHobbyists("Yoga").ForEach(item => Console.WriteLine(item));
        //}
    }
}
