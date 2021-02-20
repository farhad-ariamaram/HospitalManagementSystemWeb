using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;

namespace HospitalManagementSystem.ViewModel
{
    public class RoomVM
    {
        public List<Room> rooms { get; set; }
        public Room room { get; set; }
    }
}