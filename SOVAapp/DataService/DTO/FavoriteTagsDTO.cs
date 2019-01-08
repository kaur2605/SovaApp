using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace DataService.DTO
{
    public class FavoriteTagsDTO
    {
        [Key]
        public int Id { get; set; }
        public int User_CustomeField_Id { get; set; }
        public int TagId { get; set; }
        public virtual UserCustomeField UserCustomeField { get; set; }
        public virtual TagsDTO Tag { get; set; }

        public FavoriteTagsDTO (int User_CustomeField_Id, int TagId, UserCustomeField UserCustomeField, TagsDTO Tag)
        {

            this.User_CustomeField_Id = User_CustomeField_Id;
            this.TagId = TagId;
            this.UserCustomeField = UserCustomeField;
            this.Tag = Tag;
        
        }
    }
}
