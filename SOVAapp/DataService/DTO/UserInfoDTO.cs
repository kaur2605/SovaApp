using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;

namespace DataService.DTO
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public String DisplayName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }

       

        public UserInfoDTO(int Id,  string OwnerUserDisplayName, DateTime CreationDate, string OwnerUserLocation)
        {

            this.Id = Id;
            this.DisplayName = OwnerUserDisplayName;
            this.CreationDate = CreationDate;
            this.Location = OwnerUserLocation;
         
        }
    }
}
