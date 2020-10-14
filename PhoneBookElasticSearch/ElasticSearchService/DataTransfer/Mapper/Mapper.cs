using ElasticSearchPersistence.Model;
using ElasticSearchService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchService.DataTransfer.Mapper
{
    public static class Mapper
    {
        public static PhoneBook ToEntity(this PhoneBookDTO dto)
        {
            return new PhoneBook()
            {
                id = dto.id,
                username = dto.username,
                phonenumber = dto.phonenumber
            };
        }

        public static void ToEntity(this PhoneBookDTO dto, PhoneBook entity)
        {
            entity.username = dto.username;
            entity.phonenumber = dto.phonenumber;
        }

        public static PhoneBookDTO ToDTO(this PhoneBook entity)
        {
            return new PhoneBookDTO()
            {
                id = entity.id,
                username = entity.username,
                phonenumber = entity.phonenumber
            };
        }
    }
}
