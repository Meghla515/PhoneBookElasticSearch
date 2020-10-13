using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchService.DataTransfer.Model
{
    public class KafkaMessage
    {
        public string Payload { get; set; }
        public string EventName { get; set; }
        public KafkaMessage(string eventName, string payload)
        {
            Payload = payload;
            EventName = eventName;
        }
    }
}
