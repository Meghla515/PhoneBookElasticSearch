using ElasticSearchPersistence.Model;
using ElasticSearchService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchService.Service
{
    public interface IESService
    {
        void InsertIndexDocument(PhoneBookDTO dto);
        void UpdateIndexDocument(PhoneBookDTO dto);
        void DeleteIndexDocument(PhoneBookDTO dto);
        IEnumerable<dynamic> SearchDocuments(string search);
    }
}
