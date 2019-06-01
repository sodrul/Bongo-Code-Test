using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace JsonParseObject
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> nodes = new Dictionary<string, string>();
            var items = new List<KeyPair>();
            Person person_a = new Person("User", "1", "none");
            Person person_b = new Person("User", "1", person_a);
            var amend = new JavaScriptSerializer().Serialize(person_b);
            var jsonStr = "{ \"key1\": 1,\"key2\": { \"key3\": 1, \"key4\": { \"key5\": 4 ,\"user\":" + amend + "}} }";
            JObject rootObject = JObject.Parse(jsonStr);
            
            ParseJson(rootObject, items);
            Console.WriteLine("");
            Console.WriteLine("JSON:");
            foreach (var key in items)
            {
                Console.WriteLine(key.Key + " = " + key.Index);
            }
            Console.ReadKey();
        }
        static bool ParseJson(JToken token, List<KeyPair> keyPairs, string parentLocation = "")
        {
            if (token.HasValues)
            {
                foreach (JToken child in token.Children())
                {
                    var item = new KeyPair();
                    if (token.Type == JTokenType.Property)
                    {
                        if (parentLocation == "")
                        {
                            parentLocation = ((JProperty)token).Name;
                            item.Key = parentLocation;
                            item.Index = "1";
                            keyPairs.Add(item);
                        }
                        else
                        {
                            parentLocation += "." + ((JProperty)token).Name;
                            var depth = parentLocation.Split('.');

                            item.Key = depth[depth.Count() - 1];
                            item.Index = depth.Count().ToString();
                            keyPairs.Add(item);
                        }
                    }
                    ParseJson(child, keyPairs, parentLocation);
                }

                
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class KeyPair
    {
        public string Key { get; set; }
        public string Index { get; set; }
    }

    class Person
    {
        public string _first_name { get; set; }
        public string _last_name { get; set; }
        public object _father { get; set; }
        public Person(string first_name, string last_name, object father)
        {
            this._first_name = first_name;
            this._last_name = last_name;
            this._father = father;
        }

    }
}
