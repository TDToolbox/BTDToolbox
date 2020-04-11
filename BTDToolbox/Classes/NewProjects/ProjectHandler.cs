using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox.Classes.NewProjects
{
    class ProjectHandler
    {
        public static ProjectClass.ProjectFile project;
        public static void CreateProject()
        {
            project = new ProjectClass.ProjectFile();
            CurrentProjectVariables.ResetProjectVariables();

            //SaveProject();            
        }
        public static ProjectClass.ProjectFile ReadProject(string projFile)
        {
            if(project == null)
                project = new ProjectClass.ProjectFile();

            if (File.Exists(projFile))
            {
                string json = File.ReadAllText(Environment.CurrentDirectory + "\\settings.json");
                project = JsonConvert.DeserializeObject<ProjectClass.ProjectFile>(json);
            }
            else
            {
                //Create new project
                ConsoleHandler.appendLog("Something went wrong when trying to read the" +
                    "project. Project not found...");
                //CreateProject();  //Commented out until im sure this is what needs to happen next
            }

            return project;
        }
        public static void SaveProject()
        {
            project.ProjectName = CurrentProjectVariables.ProjectName;
            project.PathToProjectFiles = CurrentProjectVariables.PathToProjectFiles;
            project.PathToProjectClassFile = CurrentProjectVariables.PathToProjectClassFile;
            project.GameName = CurrentProjectVariables.GameName;
            project.GamePath = CurrentProjectVariables.GamePath;
            project.GameVersion = CurrentProjectVariables.GameVersion;
            project.JetPassword = CurrentProjectVariables.JetPassword;
            project.ExportPath = CurrentProjectVariables.ExportPath;
            project.DateLastOpened = CurrentProjectVariables.DateLastOpened;
            project.JsonEditor_OpenedTabs = CurrentProjectVariables.JsonEditor_OpenedTabs;
            project.ModifiedFiles = CurrentProjectVariables.ModifiedFiles;

            string output_Cfg = JsonConvert.SerializeObject(project, Formatting.Indented);

            if(project.PathToProjectClassFile != "" && project.PathToProjectClassFile != null)
            {
                StreamWriter serialize = new StreamWriter(project.PathToProjectClassFile + "\\" + project.ProjectName + ".toolbox", false);
                serialize.Write(output_Cfg);
                serialize.Close();
            }
            else
            {
                ConsoleHandler.force_appendLog("Unknown error occured... Project path is invalid...");
            }            
        }
    }
}
