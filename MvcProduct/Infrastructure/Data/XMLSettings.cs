using NuGet.Configuration;
using System.Xml.Linq;

namespace MvcProduct.Infrastructure.Data
{
    public class XMLSettings
    {
        public static void LoadSettings()
        {
            XDocument doc = XDocument.Load("Infrastructure/Data/settings.xml");

        }
        public static void SaveSettings() 
        {
            XDocument doc = new XDocument( new XElement("Child", new XElement("GrandChild", "Mistletoe is dangerous")
                ) 
                );
            doc.Save("Infrastructure/Data/settings.xml");
        }

    }
}
