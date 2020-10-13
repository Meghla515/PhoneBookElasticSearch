using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchPersistence.Model
{
    public class PhoneBook
    {
        public string username { get; set; }
        public int id { get; set; }
        public string phonenumber { get; set; }
    }

    public class DocumentModel
    {
        public Guid Index { get; set; }
        public string IndexName { get; set; }
        public dynamic Document { get; set; }
    }
}
