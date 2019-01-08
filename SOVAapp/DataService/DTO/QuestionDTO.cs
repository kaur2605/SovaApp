using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;

namespace DataService.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public int OwneruserId { get; set; }
        public String Body { get; set; }
        public String Title { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        public ICollection<CommentDTO> Comments;
        public ICollection<Post> Answers;
        public ICollection<QuestionDTO> LinkedPosts;
        public ICollection<PostTagsDTO> PostTags;
        public virtual UserInfoDTO UserInfo { get; set; }


        public QuestionDTO(int Id, int? AcceptedAnswerId, int OwneruserId, String Body, String Title, int Score, DateTime CreationDate, DateTime? ClosedDate
            , ICollection<CommentDTO> Comments, ICollection<Post> Asnwers, ICollection<PostTagsDTO> PostTags, UserInfoDTO UserInfo, ICollection<QuestionDTO> LinkedPosts)
        {
            this.Id = Id;
            this.OwneruserId = OwneruserId;
            this.AcceptedAnswerId = AcceptedAnswerId;
            this.Body = Body;
            this.Title = Title;
            this.Score = Score;
            this.CreationDate = CreationDate;
            this.ClosedDate = ClosedDate;
            this.Comments = Comments;
            this.Answers = Asnwers;
            this.PostTags = PostTags;
            this.UserInfo = UserInfo;
            this.LinkedPosts =  LinkedPosts;

    }


    }
}
