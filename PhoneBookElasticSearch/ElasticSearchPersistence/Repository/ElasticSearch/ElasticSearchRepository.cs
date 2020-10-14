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

        public void CreateIndex(string index)
        {
            if (!elasticClient.Indices.Exists(index).Exists)
            {
                var createIndexResponse = elasticClient.Indices.Create(index,
                    index => index.Map<PhoneBook>(x => x.AutoMap())
                );
            }
        }

        public void Insert(PhoneBook dto)
        {
            string jsonpayload = JsonConvert.SerializeObject(dto);
            var converter = new ExpandoObjectConverter();
            var document = JsonConvert.DeserializeObject<ExpandoObject>(jsonpayload, converter);
            var res = elasticClient.Index(document, i => i
                    .Index("phonebook")
                    .Id(dto.id)
                );
            //var getTableData = elasticClient.Count<PhoneBook>(s => s.Index("sample").Type("rawdata"));
            //var indexResponse = elasticClient.IndexDocument(dto);

            //var res = elasticClient.Index(new IndexRequest<PhoneBook>(dto, "phonebook"));
            //var result = elasticClient.Index(dto, i => i
            //        .Index("phonebook")
            //        .Id(dto.id)
            //        .Refresh(Elasticsearch.Net.Refresh.True)
            //        );
        }

        public void Update(PhoneBook dto)
        {
            //string jsonpayload = JsonConvert.SerializeObject(documentModel.Document);
            //var converter = new ExpandoObjectConverter();
            //var document = JsonConvert.DeserializeObject<ExpandoObject>(jsonpayload, converter);
            //var response = elasticClient.Update<dynamic, dynamic>(dto.id, d => d
            //          .Index("phonebook")
            //          .Doc(dto));

            var task = elasticClient.Update(
                    new DocumentPath<PhoneBook>(dto), u =>
                        u.Index("phonebook").Doc(dto));
        }

        public void Delete(PhoneBook dto)
        {
            //string jsonpayload = JsonConvert.SerializeObject(documentModel.Document);
            //var converter = new ExpandoObjectConverter();
            //var document = JsonConvert.DeserializeObject<ExpandoObject>(jsonpayload, converter);
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
            var converter = new ExpandoObjectConverter();
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
