using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataService.DomainModel
{
    public class UserInfo
    {
        [Key]
        public int OwnerUserId { get; set; }
        public int? OwnerUserAge { get; set; }
        public String OwnerUserDisplayName { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerUserLocation { get; set; }
        public ICollection<Post> Posts;
        public ICollection<Comment> Comments;
    }
}
