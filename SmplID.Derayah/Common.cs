using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SmplID.Derayah
{
    public static class Common<T> where T : class
    {
        internal static void CreateCsvFile(string path, List<T> values)
        {
            using (var streamWriter = new StreamWriter(path))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    ShouldQuote = args => true
                };
                using (var csvWriter = new CsvWriter(streamWriter, config))
                {
                    csvWriter.WriteRecords(values);
                }
            }
        }
    }
}
