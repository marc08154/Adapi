using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Adapi.Models;

namespace Adapi.Domain
{
    public class SalesService
    {
        private readonly SalesRepository _salesRepository;

        public SalesService(IMongoClient mongoClient)
        {
            _salesRepository = new SalesRepository(mongoClient);
        }

        public void Insert(string articleNumber, decimal salesPrice)
        {
            if (!Regex.IsMatch(articleNumber, "^([A-Za-z0-9]){1,32}$"))
                throw new FormatException("The article number must only consist of alphanumerical characters and contain no more than 32 characters!");

            _salesRepository.Insert(new Sale(articleNumber, salesPrice));
        }
    }
}
