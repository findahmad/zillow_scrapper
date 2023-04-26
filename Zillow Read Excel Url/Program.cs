using System;

namespace Zillow
{
    class Program
    {
        static void Main(string[] args)
        {
            PropertyIdExtractor objExtract = new PropertyIdExtractor();
            objExtract.ExtractIdCSVFile();

          //  Properties obj_property = new Properties();
          //  obj_property.ZillowApiResponse();
        }
    }
}
