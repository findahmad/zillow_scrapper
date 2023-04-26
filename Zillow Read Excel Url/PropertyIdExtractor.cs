using System;
using System.Collections.Generic;
using System.Text;
using IronXL;
using System.Data;
using System.Linq;
using System.Globalization;
using CsvHelper.Configuration;
using System.IO;
using CsvHelper;

namespace Zillow
{
    class PropertyIdExtractor
    {
        DAL dal = new DAL();

        public void ExtractIdFromUrl()
        {
            string File_Path = "D:\\Clients\\Career Op\\Zillow\\Zillow.xlsx";
            WorkBook workbook = WorkBook.Load(File_Path);
            WorkSheet workSheet = workbook.WorkSheets.First();
            for (int i = 1; i < workSheet.Rows.Count(); i++)
            {
                var PropertyUrl = workSheet.Rows[i].Columns[0].Value.ToString();
                var Id = PropertyUrl.Split("/");
                var PropertyId = Id[Id.Length - 2].Replace("_zpid", "");
                var insertion_query = "INSERT INTO[dbo].[PropertyId](PropertyUrl, PropertyId) values('" + PropertyUrl + "', '" + PropertyId + "')";
            //    Console.WriteLine(i + "going to insert");
                dal.ExecuteQuery(insertion_query);
                Console.WriteLine(i + "- Property Id generated");
            }
        }

        public void ExtractIdCSVFile()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false
            };

            using var streamReader = File.OpenText("D:\\Clients\\Career Op\\Zillow\\Zillow.csv");
            using var csvReader = new CsvReader(streamReader, csvConfig);

            string value;
            int z = 3;
            while (csvReader.Read())
            {
               
                for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
                {
                    value = value.Replace("'", "");
                    var Id = value.Split("/");
                    var PropertyId = Id[Id.Length - 2].Replace("_zpid", "");
                    var insertion_query = "INSERT INTO[dbo].[PropertyId](PropertyUrl, PropertyId) values('" + value + "', '" + PropertyId + "')";
                    //    Console.WriteLine(i + "going to insert");
                    dal.ExecuteQuery(insertion_query);
                    Console.WriteLine(z + "- Property Id generated");
                }

                z++;
            }
            Console.WriteLine("All Extracted!");
        }
    }
}