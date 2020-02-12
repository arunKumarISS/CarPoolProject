using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CarPool.Model;
using Newtonsoft.Json;
using System.Reflection;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

//Data Source=DESKTOP-H7CBN3O;Initial Catalog=CarPool;Integrated Security=True

namespace CarPool.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        SqlCommand Command;
        SqlConnection Connection;
        SqlDataAdapter Data;
        public static List<T> objects;

        public Repository()
        {
            objects = GetFromDatabase(typeof(T));
        }


        public static IDictionary<string, string> ListOfPaths = new Dictionary<string, string>()
        {
            {"USER","SELECT * FROM User" },
            {"OFFER", "SELECT * FROM Offers"},
            {"BOOKING",  "SELECT * FROM Bookings"},
            {"OFFERREQUEST", "SELECT * FROM OfferRequests"},
            {"PAYMENT", "SELECT * FROM Payments"},
            {"LOCATION", "SELECT * FROM Locations"}
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

        public List<T> GetFromDatabase(Type t)
        {
            //string type = t.Name.ToString().ToUpper();
            //string result;
            //ListOfPaths.TryGetValue(type, out result);

            //var queryExample = context.BookingTables;
            //Booking user = new Booking();
            //var groups = context.OfferTables.ToList();
            //return TObjects;
        }

        //public DataTable ToDataTable(this List<T> data)
        //{
        //    PropertyDescriptorCollection props =
        //        TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        PropertyDescriptor prop = props[i];
        //        table.Columns.Add(prop.Name, prop.PropertyType);
        //    }
        //    object[] values = new object[props.Count];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item);
        //        }
        //        table.Rows.Add(values);
        //    }
        //    return table;
        //}

        public void Save()
        {
            string type = typeof(T).Name.ToString().ToUpper();
            string result;
            ListOfPaths.TryGetValue(type, out result);

            File.WriteAllText(result, JsonConvert.SerializeObject(objects));
        }

    }
}
