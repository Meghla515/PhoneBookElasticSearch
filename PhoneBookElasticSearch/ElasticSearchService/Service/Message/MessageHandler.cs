using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchService.Service.Message
{
    using ElasticSearchPersistence.Model;
    using ElasticSearchService.DataTransfer.Model;
    using Nest;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace PhoneBookReadService.Service.Message
    {       
        public class MessageConsumer
        {
            IElasticClient client;

            public MessageConsumer(IElasticClient client)
            {
                this.client = client;
            }

            public void HandlePhonebookMessage(string result)
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<KafkaMessage>(result);

                    switch (data.EventName)
                    {
                        case "phonebookDeleted":
                            using (var helper = new ServiceFactory(client))
                            {
                                var service = helper.esService;
                                var phonebookDTO = JsonConvert.DeserializeObject<DocumentModel>(data.Payload);
                                service.DeleteIndexDocument(phonebookDTO);
                            }
                            break;
                        case "phonebookCreated":
                            using (var helper = new ServiceFactory(client))
                            {
                                var service = helper.esService;
                                var phonebookDTO = JsonConvert.DeserializeObject<DocumentModel>(data.Payload);
                                service.InsertIndexDocument(phonebookDTO);
                            }
                            break;
                        case "phonebookUpdated":
                            using (var helper = new ServiceFactory(client))
                            {
                                var service = helper.esService;
                                var phonebookDTO = JsonConvert.DeserializeObject<DocumentModel>(data.Payload);
                                service.UpdateIndexDocument(phonebookDTO);
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }

            }
        }
    }

}
