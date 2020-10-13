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

        public void CreateIndex(string IndexName)
        {
            if (!elasticClient.Indices.Exists(IndexName).Exists)
            {
                var createIndexResponse = elasticClient.Indices.Create(IndexName,
                    index => index.Map<dynamic>(x => x.AutoMap())
                );
            }
        }

        public void Insert(DocumentModel documentModel)
        {
            string jsonpayload = JsonConvert.SerializeObject(documentModel.Document);
            var converter = new ExpandoObjectConverter();
            var document = JsonConvert.DeserializeObject<ExpandoObject>(jsonpayload, converter);
            var res = elasticClient.Index(document, i => i
                    .Index(documentModel.IndexName)
                    .Id(documentModel.Index)
                );
        }

        public void Update(DocumentModel documentModel)
        {
            string jsonpayload = JsonConvert.SerializeObject(documentModel.Document);
            var converter = new ExpandoObjectConverter();
            var document = JsonConvert.DeserializeObject<ExpandoObject>(jsonpayload, converter);
            var response = elasticClient.Update<dynamic, dynamic>(documentModel.Index, d => d
                      .Index(documentModel.IndexName)
                      .Doc(document));
        }

        public void Delete(DocumentModel documentModel)
        {
            string jsonpayload = JsonConvert.SerializeObject(documentModel.Document);
            var converter = new ExpandoObjectConverter();
            var document = JsonConvert.DeserializeObject<ExpandoObject>(jsonpayload, converter);
            var response = elasticClient.Delete<dynamic>(documentModel.Index, d => d
                      .Index(documentModel.IndexName));
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
