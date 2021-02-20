using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace HMSDataLayer
{
    public class RoomDA
    {
        HospitalDBEntities context;

        public RoomDA()
        {
            context = new HospitalDBEntities();
        }

        public void Create(Room obj)
        {
            context.Rooms.Add(obj);
            context.SaveChanges();
        }

        public List<Room> Read()
        {
            return context.Rooms.ToList();
        }

        public void Update(Room obj)
        {
            Room main = context.Rooms.Where(a => a.Id == obj.Id).FirstOrDefault();
            main.Location = obj.Location;
            context.SaveChanges();
        }

        public void Delete(Room obj)
        {
            Room main = context.Rooms.Where(a => a.Id == obj.Id).FirstOrDefault();
            context.Rooms.Remove(main);
            context.SaveChanges();
        }
    }
}
