using ElasticSearchPersistence.Repository.ElasticSearch;
using ElasticSearchPersistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchService.Service
{
    public class ESService : IESService
    {
        IElasticSearchRepository repo;
        public ESService(IElasticSearchRepository repo)
        {
            this.repo = repo;
        }

        public void DeleteIndexDocument(DocumentModel documentModel)
        {
            repo.CreateIndex(documentModel.IndexName);

            repo.Delete(documentModel);
        }

        public void InsertIndexDocument(DocumentModel documentModel)
        {
            repo.CreateIndex(documentModel.IndexName);

            repo.Insert(documentModel);
        }

        public IEnumerable<dynamic> SearchDocuments(string searchTerm)
        {
            return repo.Search(searchTerm);
        }

        public void UpdateIndexDocument(DocumentModel documentModel)
        {
            repo.CreateIndex(documentModel.IndexName);

            repo.Update(documentModel);
        }
    }
}
