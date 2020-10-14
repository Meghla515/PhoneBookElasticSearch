using ElasticSearchPersistence.Repository.ElasticSearch;
using ElasticSearchPersistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using ElasticSearchService.DataTransfer.Model;
using ElasticSearchService.DataTransfer.Mapper;

namespace ElasticSearchService.Service
{
    public class ESService : IESService
    {
        IElasticSearchRepository repo;
        public ESService(IElasticSearchRepository repo)
        {
            this.repo = repo;
        }

        public void DeleteIndexDocument(PhoneBookDTO dto)
        {
            repo.CreateIndex("phonebook");

            repo.Delete(dto.ToEntity());
        }

        public void InsertIndexDocument(PhoneBookDTO dto)
        {
            repo.CreateIndex("phonebook");

            repo.Insert(dto.ToEntity());
        }

        public IEnumerable<dynamic> SearchDocuments(string searchTerm)
        {
            return repo.Search(searchTerm);
        }

        public void UpdateIndexDocument(PhoneBookDTO dto)
        {
            repo.CreateIndex("phonebook");

            repo.Update(dto.ToEntity());
        }
    }
}
