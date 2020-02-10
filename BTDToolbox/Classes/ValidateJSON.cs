using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BTDToolbox.Classes
{
    class ValidateJSON
    {
        public static bool IsValidJson(string input)   //This method won't be used, but it shows how to get the line number of the error
        {
            input = input.Trim();
            if ((input.StartsWith("{") && input.EndsWith("}")) || //For object
                (input.StartsWith("[") && input.EndsWith("]"))) //For array
            {
                try
                {
                    //parse the input into a JObject
                    var jObject = JObject.Parse(input);

                    foreach (var jo in jObject)
                    {
                        string name = jo.Key;
                        JToken value = jo.Value;

                        //if the element has a missing value, it will be Undefined - this is invalid
                        if (value.Type == JTokenType.Undefined)
                        {
                            return false;
                        }
                    }
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    ConsoleHandler.appendLog(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    ConsoleHandler.appendLog(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
        public static bool IsValidJson2(string json)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                dynamic result = serializer.DeserializeObject(json);
                return true;
            }
            catch { return false; }
        }
    }
}
