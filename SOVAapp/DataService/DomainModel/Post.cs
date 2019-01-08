using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataService.DomainModel
{
    public class Post
    {
        public int Id { get; set; }
        public int PostTypeId { get; set; }
        public int? ParentId { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public int? LinkPostId { get; set; }
        public int OwnerUserId { get; set; }
        public String Body { get; set; }
        public String Title { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        public ICollection<Comment> Comments;
        public virtual PostType PostType { get; set; }
       
        public ICollection<PostTag> PostTags;
        public virtual UserInfo UserInfo { get; set; }


    }
}
