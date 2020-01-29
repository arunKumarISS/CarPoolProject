using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CarPool.Model;
using Newtonsoft.Json;
using System.Reflection;
using CarPool.Database;

namespace CarPool.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {


        public static IDictionary<string, string> ListOfPaths = new Dictionary<string, string>()
        {
            {"USER",@"D:\tasks\CarPool\Documents\users.json" },
            {"OFFER", @"D:\tasks\CarPool\Documents\offers.json"},
            {"BOOKING",  @"D:\tasks\CarPool\Documents\bookings.json"},
            {"OFFERREQUEST", @"D:\tasks\CarPool\Documents\offerRequests.json"},
            {"PAYMENT", @"D:\tasks\CarPool\Documents\payments.json"},
            {"LOCATION", @"D:\tasks\CarPool\Documents\locations.json"}
        };

        public static List<T> objects = GetDataFromJson(typeof(T));



        public void Add(T entity)
        {
            objects.Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            objects.Remove(entity);
            Save();
        }

        public List<T> GetList()
        {

            return objects;
        }

        public void Update(T entity)
        {

            Save();
        }

        public static List<T> GetDataFromJson(Type t)
        {
            string type = t.Name.ToString().ToUpper();
            string result;
            ListOfPaths.TryGetValue(type, out result);
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(result));
        }

        public void Save()
        {
            string type = typeof(T).Name.ToString().ToUpper();
            string result;
            ListOfPaths.TryGetValue(type, out result);

            File.WriteAllText(result, JsonConvert.SerializeObject(objects));
        }
    }
}
