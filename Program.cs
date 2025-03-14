using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalShell
{
    internal class Program
    {
        public static string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static void Main(string[] args)
        {
            Console.WriteLine(documentsPath);
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path.GetDirectoryName(documentsPath + "\\command.txt");
            watcher.Filter = Path.GetFileName(documentsPath + "\\command.txt");
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
            while (Console.Read() != 'q') ;
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                string content = File.ReadAllText(e.FullPath);
                Excute(content);
            }
            catch (Exception ex)
            {

            }
        }
        private static void Excute(string command) {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + command;
            process.StartInfo.RedirectStandardOutput = true; 
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true; 
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            string filePath = documentsPath + "\\output.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(output);
            }
        }
    }
}
