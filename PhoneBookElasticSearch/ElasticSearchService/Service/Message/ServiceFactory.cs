using ElasticSearchPersistence.Repository.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchService.Service.Message
{
    public class ServiceFactory : IDisposable
    {
        public IElasticSearchRepository esRepo { get; set; }
        public IESService esService { get; set; }

        public ServiceFactory(IElasticClient client)
        {
            esRepo = new ElasticSearchRepository(client);
            esService = new ESService(esRepo);
        }

        public void Dispose()
        {
            if (esRepo != null)
            {
                esRepo.Dispose();
            }
        }
    }
}
