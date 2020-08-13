using Kantor.Interfaces;
using Kantor.Models;
using Newtonsoft.Json;
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

        static string folderPath = @"c:\Kantora\";
        static string fileName = "jsonFile.txt";

        string pathString = Path.Combine(folderPath, fileName);

        string json = JsonConvert.SerializeObject(nbpCurrencyLogic);

        public void SaveFile(NbpCurrency nbpCurrencyLogic)
        {

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
