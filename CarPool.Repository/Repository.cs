using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CarPool.Model;
using Newtonsoft.Json;
using System.Reflection;


namespace CarPool.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public static List<T> objects = new List<T>();

        public Repository()
        {
            objects = GetDataFromJson(typeof(T));
        }


        public static IDictionary<string, string> ListOfPaths = new Dictionary<string, string>()
        {
            {"USER",@"D:\tasks\CarPool\Documents\users.json" },
            {"OFFER", @"D:\tasks\CarPool\Documents\offers.json"},
            {"BOOKING",  @"D:\tasks\CarPool\Documents\bookings.json"},
            {"OFFERREQUEST", @"D:\tasks\CarPool\Documents\offerRequests.json"},
            {"PAYMENT", @"D:\tasks\CarPool\Documents\payments.json"},
            {"LOCATION", @"D:\tasks\CarPool\Documents\locations.json"}
        };

        


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
            
            GetDataFromJson(typeof(T));
            return objects;
        }

        public T GetById(string id)
        {
            
            
            foreach (var entity in objects)
            {
                if (string.Equals(entity.Id,id))
                    return entity;
            }
            return null;
        }

        public void Update(T entity)
        {
            Save();
        }

        public List<T> GetDataFromJson(Type t)
        {
            string type = t.Name.ToString().ToUpper();
            string result;
            ListOfPaths.TryGetValue(type, out result);
            List<T> TObjects = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(result));
            return TObjects;
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
