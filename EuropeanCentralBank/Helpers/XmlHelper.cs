using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace EuropeanCentralBank.Helpers
{
    public  class XmlHelper
    {
        public static T Deserialize<T>(string input) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(input))
            {
                return (T)serializer.Deserialize(sr);
            }
        }
    }
}
