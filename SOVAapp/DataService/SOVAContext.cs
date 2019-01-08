using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DataService.DomainModel;
namespace DataService
{

    public class SovaContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<PostType> PostTypes { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<SearchHistory> SearchHistory { get; set; }
        public DbSet<Marking> Markings { get; set; }
        public DbSet<Annotations> Annotations { get; set; }
        public DbSet<FavoriteTags> FavoriteTags { get; set; }
        public DbSet<UserCustomeField> UserCustomeField { get; set; }
        public DbSet<TermScore> TermScore { get; set; }
        public DbSet<CoOccurrence> CoOccurrence { get; set; }
        public DbSet<SearchResult> SearchResult { get; set; }
        public DbSet<TermAsResult> TermAsResult { get; set; }
        public DbSet<TermNetworkMaker> TermNetworkMaker { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
                 optionsBuilder.UseMySql("server=wt-220.ruc.dk;database=raw5;uid=raw5;pwd=raw5");
            //   optionsBuilder.UseMySql("server=wt-220.ruc.dk;database=aabedin;uid=aabedin;pwd=aMyVAMyt");
             //  optionsBuilder.UseMySql("server=localhost;database=raw5;uid=root;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Posts
            modelBuilder.Entity<Post>().ToTable("posts");
            modelBuilder.Entity<Post>().Property(x => x.PostTypeId).HasColumnName("posttypeid");
            modelBuilder.Entity<Post>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Post>().Property(x => x.ParentId).HasColumnName("parentId");
            modelBuilder.Entity<Post>().Property(x => x.AcceptedAnswerId).HasColumnName("acceptedanswerid");
            modelBuilder.Entity<Post>().Property(x => x.LinkPostId).HasColumnName("linkpostid");
            modelBuilder.Entity<Post>().Property(x => x.OwnerUserId).HasColumnName("owneruserId");
            modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Post>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Post>().Property(x => x.CreationDate).HasColumnName("creationDate");
            modelBuilder.Entity<Post>().Property(x => x.ClosedDate).HasColumnName("closedDate");

            // Table Comment
            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.CommentId).HasColumnName("commentid");
            modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("postid");
            modelBuilder.Entity<Comment>().Property(x => x.CommentText).HasColumnName("commenttext");
            modelBuilder.Entity<Comment>().Property(x => x.CommentScore).HasColumnName("commentscore");
            modelBuilder.Entity<Comment>().Property(x => x.CommentCreateDate).HasColumnName("commentcreatedate");
            modelBuilder.Entity<Comment>().Property(x => x.OwnerUserId).HasColumnName("owneruserid");



            // Table UserInfo
            modelBuilder.Entity<UserInfo>().ToTable("userinfo");
            modelBuilder.Entity<UserInfo>().Property(x => x.OwnerUserId).HasColumnName("userid");
            modelBuilder.Entity<UserInfo>().Property(x => x.OwnerUserAge).HasColumnName("owneruserage");
            modelBuilder.Entity<UserInfo>().Property(x => x.OwnerUserDisplayName).HasColumnName("owneruserdisplayname");
            modelBuilder.Entity<UserInfo>().Property(x => x.OwnerUserLocation).HasColumnName("owneruserlocation");
            modelBuilder.Entity<UserInfo>().Property(x => x.CreationDate).HasColumnName("ownerusercreationdate");

            // Table Tags
            modelBuilder.Entity<Tags>().ToTable("tags");
            modelBuilder.Entity<Tags>().Property(x => x.Id).HasColumnName("tagid");
            modelBuilder.Entity<Tags>().Property(x => x.Tag).HasColumnName("tag");

            // Table PostTags
            modelBuilder.Entity<PostTag>().ToTable("posttags");
            modelBuilder.Entity<PostTag>().Property(x => x.PostTagId).HasColumnName("posttag_id");
            modelBuilder.Entity<PostTag>().Property(x => x.PostId).HasColumnName("postid");
            modelBuilder.Entity<PostTag>().Property(x => x.TagId).HasColumnName("tagid");

            // Table SearchHistory
            modelBuilder.Entity<SearchHistory>().ToTable("searchhistory");
            modelBuilder.Entity<SearchHistory>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<SearchHistory>().Property(x => x.SearchContent).HasColumnName("searchcontent");
            modelBuilder.Entity<SearchHistory>().Property(x => x.SearchDate).HasColumnName("searchdate");


            // Table Marking
            modelBuilder.Entity<Marking>().ToTable("marking");
            modelBuilder.Entity<Marking>().Property(m => m.MarkedPostId).HasColumnName("markedpostid");
            modelBuilder.Entity<Marking>().Property(x => x.MarkingDate).HasColumnName("markingdate");

            // Table Annotation
            modelBuilder.Entity<Annotations>().ToTable("annotations");
            modelBuilder.Entity<Annotations>().Property(x => x.AnnotationId).HasColumnName("annotationid");
            modelBuilder.Entity<Annotations>().Property(x => x.MarkedPostId).HasColumnName("markedpostid");
            modelBuilder.Entity<Annotations>().Property(x => x.Annotation).HasColumnName("annotation");
            modelBuilder.Entity<Annotations>().Property(x => x.From).HasColumnName("from");
            modelBuilder.Entity<Annotations>().Property(x => x.To).HasColumnName("to");



            // Table UserCustomeField
            modelBuilder.Entity<UserCustomeField>().ToTable("usercustomefield");
            modelBuilder.Entity<UserCustomeField>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<UserCustomeField>().Property(x => x.Postlimit).HasColumnName("postlimit");
            modelBuilder.Entity<UserCustomeField>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<UserCustomeField>().HasKey(x => x.Id);



            // Table FavoriteTags
            modelBuilder.Entity<FavoriteTags>().ToTable("favoritetags");
            modelBuilder.Entity<FavoriteTags>().Property(x => x.Id).HasColumnName("favourite_id");
            modelBuilder.Entity<FavoriteTags>().Property(x => x.UserCustomeFieldId).HasColumnName("user_customeField_id");
            modelBuilder.Entity<FavoriteTags>().Property(x => x.TagId).HasColumnName("tagid");
            modelBuilder.Entity<FavoriteTags>().HasKey(x => x.Id);




            // Table PostType

            modelBuilder.Entity<PostType>().ToTable("posttypes");
            modelBuilder.Entity<PostType>().Property(x => x.Id).HasColumnName("Posttypeid");
            modelBuilder.Entity<PostType>().Property(x => x.Type).HasColumnName("posttype");


            // Table TermScore
            modelBuilder.Entity<TermScore>().ToTable("termscore");
            modelBuilder.Entity<TermScore>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<TermScore>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<TermScore>().Property(x => x.Tf).HasColumnName("tf");
            modelBuilder.Entity<TermScore>().Property(x => x.Idf).HasColumnName("idf");
            modelBuilder.Entity<TermScore>().Property(x => x.TfIdf).HasColumnName("tfidf");
            modelBuilder.Entity<TermScore>().HasKey(x => new { x.Id, x.Word });


            // TermAsResult

            modelBuilder.Entity<TermAsResult>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<TermAsResult>().Property(x => x.Score).HasColumnName("tfidf");


            // TermNetworkMaker

            modelBuilder.Entity<TermNetworkMaker>().Property(x => x.Raw).HasColumnName("raw");
            

            // Table CoOccurrence
            modelBuilder.Entity<CoOccurrence>().ToTable("co_occurrence");
            modelBuilder.Entity<CoOccurrence>().Property(t => t.Word).HasColumnName("word1");
            modelBuilder.Entity<CoOccurrence>().Property(x => x.Word2).HasColumnName("word2");
            modelBuilder.Entity<CoOccurrence>().Property(x => x.Grade).HasColumnName("grade");
            modelBuilder.Entity<CoOccurrence>()
           .HasKey(c => new { c.Word, c.Word2 });
            // SearchResults
            
            modelBuilder.Entity<SearchResult>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<SearchResult>().Property(x => x.Title).HasColumnName("title");
          //modelBuilder.Entity<SearchResult>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<SearchResult>().Property(x => x.Rank).HasColumnName("rank");


        }
    }
}
