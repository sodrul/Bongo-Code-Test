using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParse
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Dictionary<string, string> nodes = new Dictionary<string, string>();            
            JObject rootObject = JObject.Parse("{ \"key1\": 1,\"key2\": { \"key3\": 1, \"key4\": { \"key5\": 4 } } } ");
            
            ParseJson(rootObject, nodes);
            
            Console.WriteLine("");
            Console.WriteLine("JSON:");
            foreach (string key in nodes.Keys)
            {
                Console.WriteLine(key + " = " + nodes[key]);
            }
            Console.ReadKey();
            
        }
        static bool ParseJson(JToken token, Dictionary<string, string> nodes, string parentLocation = "")
        {
            if (token.HasValues)
            {
                foreach (JToken child in token.Children())
                {

                    if (token.Type == JTokenType.Property)
                    {
                        if (parentLocation == "")
                        {
                            parentLocation = ((JProperty)token).Name;
                            nodes.Add(parentLocation, "1");
                        }
                        else
                        {
                            parentLocation += "." + ((JProperty)token).Name;
                            var depth = parentLocation.Split('.');
                            nodes.Add(depth[depth.Count() - 1], depth.Count().ToString());
                        }
                    }
                    ParseJson(child, nodes, parentLocation);
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
