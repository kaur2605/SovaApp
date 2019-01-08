using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;

namespace DataService.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public int LinkPostId { get; set; }
        public int OwneruserId { get; set; }
        public int PostTypeId { get; set; }
        public String Body { get; set; }
        public String Title { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        

        public ICollection<CommentDTO> Comments;
        public virtual PostTypeDTO PostType { get; set; }
        public ICollection<PostTagsDTO> PostTags;
        public virtual UserInfoDTO UserInfo { get; set; }


        public PostDTO(int Id, int OwneruserId, String Body, int PostTypeId, int? ParentId, String Title, int Score, DateTime CreationDate, DateTime? ClosedDate
            , ICollection<CommentDTO> Comments, PostTypeDTO PostType, ICollection<PostTagsDTO> PostTags, UserInfoDTO UserInfo)
        {
            this.Id = Id;
            this.OwneruserId = OwneruserId;
            this.Body = Body;
            this.PostTypeId = PostTypeId;
            this.ParentId = ParentId;
            this.Title = Title;
            this.Score = Score;
            this.CreationDate = CreationDate;
            this.ClosedDate = ClosedDate;
            this.Comments = Comments;
            this.PostType = PostType;
            this.PostTags = PostTags;
            this.UserInfo = UserInfo;
            
        }


    }
}
