using ElasticSearchPersistence.Model;
using ElasticSearchService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchService.Service
{
    public interface IESService
    {
        void InsertPhonebook(PhoneBookDTO dto);
        void UpdatePhonebook(PhoneBookDTO dto);
        void DeletePhonebook(PhoneBookDTO dto);
        IEnumerable<dynamic> SearchPhonebook(string search);
    }
}
