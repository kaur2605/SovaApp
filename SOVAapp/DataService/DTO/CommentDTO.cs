using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;
namespace DataService.DTO
{

    public class CommentDTO
    {

        public int CommentId { get; set; }
        public int PostId { get; set; }
        public String Body { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public int User { get; set; }

        public virtual Post post { get; set; }
        public virtual UserInfoDTO UserInfo { get; set; }

        public CommentDTO (int CommentId , int PostId ,String CommentText ,int CommentScore ,DateTime CommentCreateDate, int OwnerUserId, Post post , UserInfoDTO UserInfo )

        {
            this.CommentId = CommentId;
            this.Body = CommentText;
            this.PostId = PostId;
            this.Score = CommentScore;
            this.CreationDate = CommentCreateDate;
            this.User = OwnerUserId;
            this.post = post;
            this.UserInfo = UserInfo;

        }
    }
}

