using ElasticSearchPersistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchService.Service
{
    public interface IESService
    {
        void InsertIndexDocument(DocumentModel document);
        void UpdateIndexDocument(DocumentModel document);
        void DeleteIndexDocument(DocumentModel document);
        IEnumerable<dynamic> SearchDocuments(string searchTerm);
    }
}
