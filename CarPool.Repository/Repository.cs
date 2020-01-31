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
        public static List<T> objects;

        public Repository()
        {
            objects = GetDataFromJson();
        }

        // This doesnt have to be a dictionary these can be constant strings
        // If using a dictionary you can rather map between Type and string instead of string and string
        // Please check the changes I made
        public static IDictionary<Type, string> ListOfPaths = new Dictionary<Type, string>()
        {
            {typeof(User),@"D:\tasks\CarPool\Documents\users.json" },
            {typeof(Offer), @"D:\tasks\CarPool\Documents\offers.json"},
            {typeof(Booking),  @"D:\tasks\CarPool\Documents\bookings.json"},
            {typeof(OfferRequest), @"D:\tasks\CarPool\Documents\offerRequests.json"},
            {typeof(Payment), @"D:\tasks\CarPool\Documents\payments.json"},
            {typeof(Location), @"D:\tasks\CarPool\Documents\locations.json"}
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
            
            GetDataFromJson();
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

        public List<T> GetDataFromJson()
        {
            string result;
            ListOfPaths.TryGetValue(typeof(T), out result);
            List<T> TObjects = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(result));
            return TObjects;
        }

        public void Save()
        {
            string type = typeof(T).Name.ToString().ToUpper();
            string result;
            ListOfPaths.TryGetValue(typeof(T), out result);

            File.WriteAllText(result, JsonConvert.SerializeObject(objects));
        }
    }
}
