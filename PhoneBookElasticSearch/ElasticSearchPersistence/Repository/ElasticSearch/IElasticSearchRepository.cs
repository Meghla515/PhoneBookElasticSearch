using ElasticSearchPersistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchPersistence.Repository.ElasticSearch
{
    public interface IElasticSearchRepository : IDisposable
    {
        void CreateIndex(string IndexName);
        void Insert(DocumentModel document);
        void Update(DocumentModel document);
        void Delete(DocumentModel document);
        IEnumerable<dynamic> Search(string searchTerm);
    }    
}
