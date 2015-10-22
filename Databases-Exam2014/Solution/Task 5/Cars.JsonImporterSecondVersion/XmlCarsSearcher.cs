namespace Cars.JsonImporterSecondVersion
{
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Cars.Data;

    public static class XmlCarsSearcher
    {
        public static void Search()
        {
            var xmlQueries = Directory
                          .GetFiles(Directory.GetCurrentDirectory() + "/XmlFiles/")
                          .Where(f => f.EndsWith("xml"))
                          .Select(f => File.ReadAllText(f))
                          .FirstOrDefault();

            var strReader = new StringReader(xmlQueries);
            var xmlSer = (Queries)new XmlSerializer(typeof(Queries)).Deserialize(strReader);

            foreach (var query in xmlSer.Query)
            {
                ProcessQuery(query);
            }
        }

        private static void ProcessQuery(QueriesQuery query)
        {
            var db = new CarsDbContext();

            var carsQuery = db.Cars.AsQueryable();

            foreach (var whereClause in query.WhereClauses)
            {
                switch (whereClause.PropertyName)
                {
                    case "Id":
                        switch (whereClause.Type)
                        {
                            case "Equals":
                                carsQuery = carsQuery.Where(c => c.Id == int.Parse(whereClause.Value));
                                break;
                            case "GreaterThan":
                                carsQuery = carsQuery.Where(c => c.Id > int.Parse(whereClause.Value));
                                break;
                            case "LessThan":
                                carsQuery = carsQuery.Where(c => c.Id < int.Parse(whereClause.Value));
                                break;
                            default:
                                break;
                        }

                        break;
                    case "Year":
                        break;
                    case "Price":
                        break;
                    case "Model":
                        break;
                    case "Manufacturer":
                        break;
                    case "Dealer":
                        break;
                    case "City":
                        break;
                    default:
                        break;
                }

                switch (query.OrderBy)
                {
                    case "Id": carsQuery = carsQuery.OrderBy(c => c.Id);
                        break;
                    case "Year": carsQuery = carsQuery.OrderBy(c => c.Year);
                        break;
                    case "Model": carsQuery = carsQuery.OrderBy(c => c.Model);
                        break;
                    case "Price": carsQuery = carsQuery.OrderBy(c => c.Price);
                        break;
                    case "Manufacturer": carsQuery = carsQuery.OrderBy(c => c.Manufacturer.Name);
                        break;
                    case "Dealer": carsQuery = carsQuery.OrderBy(c => c.Dealer.Name);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
