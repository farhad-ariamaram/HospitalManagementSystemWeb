using HospitalManagementSystem.Models.External_Models;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace HMSDataLayer
{
    public class SettingsDA
    {
        Settings context;

        public SettingsDA()
        {
            context = new Settings();
        }

        public Settings Read()
        {
            XmlSerializer serializer;
            serializer = new XmlSerializer(typeof(Settings));
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Settings.xml"));
            Settings a = (Settings)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        public void Write(Settings set)
        {
            XmlSerializer serializer;
            serializer = new XmlSerializer(typeof(Settings));
            StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/Settings.xml"));
            Settings a = new Settings();
            a.HospitalName = set.HospitalName;
            a.ManagerName = set.ManagerName;
            serializer.Serialize(writer, a);
            writer.Close();
        }

    }
}
