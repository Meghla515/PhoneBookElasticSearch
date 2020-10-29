using ElasticSearchPersistence.Model;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ElasticSearchPersistence.Repository.ElasticSearch
{
    public class ElasticSearchRepository : IElasticSearchRepository
    {
        IElasticClient elasticClient { get; set; }

        public ElasticSearchRepository(IElasticClient client)
        {
            elasticClient = client;
        }

        public void Insert(PhoneBook dto)
        {
            var result = elasticClient.Index(dto, i => i
                    .Index("phonebook")
                    .Id(dto.id)
                    .Refresh(Elasticsearch.Net.Refresh.True)
                    );
        }

        public void Update(PhoneBook dto)
        {
            var test = elasticClient.Update<PhoneBook>(dto.id, u => u.Index("phonebook").Doc(dto));
        }

        public void Delete(PhoneBook dto)
        {
            var response = elasticClient.Delete<PhoneBook>(dto.id, d => d
                      .Index("phonebook"));
        }

        public IEnumerable<dynamic> Search(string searchTerm)
        {
            var rows = new List<Dictionary<string, object>>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                return rows;
            }
            var search = elasticClient.Search<dynamic>(s =>
                            s.Query(sq =>
                                sq.MultiMatch(mm => mm
                                    .Query(searchTerm)
                                    .Fuzziness(Fuzziness.Auto)
                                )
                            )
                        );

            foreach (var hit in search.Hits)
            {
                var row = new Dictionary<string, object>();
                foreach (var keyValuePair in hit.Source)
                {
                    row[keyValuePair.Key] = keyValuePair.Value;
                }
                rows.Add(row);
            }
            return rows;
        }

        public void Dispose()
        {
        }
    }
}
