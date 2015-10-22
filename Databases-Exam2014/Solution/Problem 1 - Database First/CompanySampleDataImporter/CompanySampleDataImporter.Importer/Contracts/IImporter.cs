namespace CompanySampleDataImporter.Importer.Contracts
{
    using System;
    using System.IO;
    using CompanySampleDataImporter.Data;

    internal interface IImporter
    {
        string Message { get; }

        int Order { get; }

        Action<CompanyEntities, TextWriter> Get { get; }
    }
}
