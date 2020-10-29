using ElasticSearchPersistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchPersistence.Repository.ElasticSearch
{
    public interface IElasticSearchRepository : IDisposable
    {
        void Insert(PhoneBook dto);
        void Update(PhoneBook dto);
        void Delete(PhoneBook dto);
        IEnumerable<dynamic> Search(string search);
    }    
}
