using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using WheatGrainClassifierWpfApp.Models;

namespace WheatGrainClassifierWpfApp.Helpers
{
    public class CsvUtils
    {
        public static  List<Grain> Load(string filename)
        {
            List<Grain> grains = new List<Grain>();

            try
            {
                if (!File.Exists(filename))
                {
                    throw new FileNotFoundException("Fichier non existant", filename);
                }

                // CsvHelper config
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ";"
                };

                using (var reader = new StreamReader(filename))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<Grain>();


                    foreach (var record in records)
                    {

                        grains.Add(record);
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception($"Error: Echec du chargement du fichier csv - {ex.Message}");
            }

            return grains;
        }

    }
}
