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
    class JSON_Reader
    {
        public static bool IsValidJson_Old(string input)   //This method won't be used, but it shows how to get the line number of the error
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
        public static string GetJSON_Error(string input)   //This is being used to generate an exeption message for finding the subtask num
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
                            return null;
                        }
                    }
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    return jex.Message;
                }
                catch (Exception ex) //some other exception
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            return null;
        }

        public static bool IsValidJson(string json)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                dynamic result = serializer.DeserializeObject(json);
                return true;
            }
            catch { return false; }
        }
        public static string GetSubtaskNum(int posInText, string text)
        {
            char indexChar = text[posInText];

            //int index = text.LastIndexOf("[", posInText);
            int lastIndex = text.LastIndexOf(":", posInText);
            int firstIndex = text.IndexOf(":", posInText);

            int index = 0;
            if((posInText-lastIndex) < (firstIndex - posInText))
            {
                index = lastIndex;
            }
            else
            {
                index = firstIndex;
            }

            string badJson = text.Insert(index+1, ";;;");

            string error = GetJSON_Error(badJson);
            string subtask = "";
            if(error!=null)
            {
                string[] split = error.Split('[');
                foreach (string s in split)
                {
                    var isNumeric = int.TryParse(s[0].ToString(), out int n);
                    if (isNumeric)
                        subtask = subtask + " " + s[0] + ",";
                    
                }
                if (subtask.EndsWith(","))
                {
                    
                    subtask = subtask.Remove(subtask.Length - 1);
                }
                return subtask;
            }
            return null;
        }
    }
}
