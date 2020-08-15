using Kantor.Interfaces;
using Kantor.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog.Settings.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Logic
{
    public class NbpFile : INbpFile
    {
        private static NbpCurrency nbpCurrencyLogic;

        //static string folderPath = @"c:\Kantora\";
        //static string fileName = "jsonFile.txt";

        //private readonly IConfiguration Configuration;

        //public NbpFile (IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //string pathString = Path.Combine(folderPath, fileName);

        private readonly NbpFilePath _nbpFilePath;

        public NbpFile(IOptions<NbpFilePath> nbpFilePath)
        {
            _nbpFilePath = nbpFilePath.Value;
        }

        string json = JsonConvert.SerializeObject(nbpCurrencyLogic);

        public void SaveFile(NbpCurrency nbpCurrencyLogic)
        {
            var nbpFilePath = new NbpFilePath();
            string folderPath = _nbpFilePath.FolderPath;
            string filePath = _nbpFilePath.FilePath;

            // Configuration.GetSection(NbpFilePath.PathDirections).Bind(nbpFilePath);

            string pathString = Path.Combine(folderPath, filePath);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            } 
 
            if (!File.Exists(pathString))
            {
                using (File.Create(pathString)) { };
                File.WriteAllText(pathString, json);
            }
            else
            {
                string resultA = json;

                using (StreamReader streamReader = File.OpenText(pathString))
                {
                    ;
                    while ((json = streamReader.ReadLine()) == null)
                    {
                        resultA += json;
                    }
                }
                File.AppendAllText(pathString, resultA);
            }
        }
    }
}
