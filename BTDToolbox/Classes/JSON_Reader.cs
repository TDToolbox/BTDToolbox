using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BTDToolbox.Classes
{
    class JSON_Reader
    {
        public static bool IsValidJson2(string input)   //This method won't be used, but it shows how to get the line number of the error

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
            catch (Exception ex)
            {
                return false;
            }
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
        public static void ValidateAllJsonFiles()
        {
            ConsoleHandler.appendLog("Beginning project validation. This may take up to a minute...");
            string projDir = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject;
            if(Directory.Exists(projDir))
            {
                string ignoreFiles = ".mask,README_subcompounds.txt,a9e50211ce5c11a0fe587b9ef452798f.json,333fd8efdbad97247c84556b100b1e74.json,brick_wall.path,battle_knot.path,19032012,005f0c8868851848d1269efd2d7e91a2.json,b7f1428b233718e0acaef609d317bad3.json,d4671aff849ceb62af5e9417f9db61d2.json";
                string[] ignoreFiles_split = ignoreFiles.Split(',');
                var allfiles = Directory.GetFiles(projDir, "*",SearchOption.AllDirectories);
                string badFiles = "";
                int totalFiles = 0;

                foreach (string file in allfiles)
                {
                    totalFiles++;
                }

                ConsoleHandler.appendLog("There are: " + totalFiles + " files to check");
                int bad = 0;
                string text = "";
                bool skip = false;
                foreach (string file in allfiles)
                {
                    skip = false;
                    foreach (string ig in ignoreFiles_split)
                    {
                        if (file.Contains(ig))
                        {
                            skip = true;
                        }
                    }
                    if(skip == false)
                    {
                        if (!IsValidJson(File.ReadAllText(file)))
                        {
                            bad++;
                            if (badFiles == "")
                                badFiles = file.Replace(projDir + "\\Assets\\JSON", "");
                            else
                            {
                                badFiles = badFiles + "," + file.Replace(projDir + "\\Assets\\JSON", "");
                            }
                        }
                    }
                }

                
                if(badFiles.Length > 0)
                {
                    ConsoleHandler.force_appendNotice("Found: " + bad + " invalid files");
                    string[] split = badFiles.Split(',');
                    foreach (string badFile in split)
                    {
                        ConsoleHandler.force_appendLog_CanRepeat(badFile);
                    }
                }
                else
                {
                    ConsoleHandler.force_appendLog_CanRepeat("All files are valid!");
                }
            }
            else
            {
                ConsoleHandler.appendLog("Project not found or does not exist. Open a project to continue...");
            }
        }
    }
}
