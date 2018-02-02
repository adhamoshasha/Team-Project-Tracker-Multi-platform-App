
using Xamarin.Forms;
using Calendar.Model;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using WorkingWithFiles;

[assembly: Xamarin.Forms.Dependency(typeof(SaveAndLoad))]
namespace WorkingWithFiles
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public void SaveText(string filename, string text)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadText(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            if (File.Exists(filePath))
                return System.IO.File.ReadAllText(filePath);
            else return "no";
        }
    }
}