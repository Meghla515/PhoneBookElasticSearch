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

        public void DeletePhonebook(PhoneBookDTO dto)
        {
            repo.Delete(dto.ToEntity());
        }

        public void InsertPhonebook(PhoneBookDTO dto)
        {
            repo.Insert(dto.ToEntity());
        }

        public IEnumerable<dynamic> SearchPhonebook(string searchTerm)
        {
            return repo.Search(searchTerm);
        }

        public void UpdatePhonebook(PhoneBookDTO dto)
        {
            repo.Update(dto.ToEntity());
        }
    }
}
