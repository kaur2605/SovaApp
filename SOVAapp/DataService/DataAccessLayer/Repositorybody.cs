using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;
using DataService.DTO;
using DataService;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataService.DataAccessLayer
{
    public class RepositoryBody : IRepository
    {

        //////////////// Posts

        public PostDTO GetPostById(int id)
        {
            using (var db2 = new SovaContext())
            {
                var post = db2.Posts.FirstOrDefault(a => a.Id == id);

                return new PostDTO(post.Id, post.OwnerUserId, post.Body, post.PostTypeId, post.ParentId, post.Title, post.Score, post.CreationDate, post.ClosedDate, null, GetPostTypeByPostId(id), GetPostTagsByPostId(id), GetUserByPostId(id));
            }
        }

        public ICollection<PostDTO> GetPosts()
        {
            using (var db = new SovaContext())
            {
                var posts = db.Posts.ToList();
                List<PostDTO> postsDTO = new List<PostDTO>();
                foreach (var post in posts)
                {
                    var myPost = new PostDTO(post.Id, post.OwnerUserId, post.Body, post.PostTypeId, post.ParentId, post.Title, post.Score, post.CreationDate, post.ClosedDate, null, GetPostTypeByPostId(post.Id), GetPostTagsByPostId(post.Id), GetUserByPostId(post.Id));
                    postsDTO.Add(myPost);
                }
                return postsDTO;

            }
        }


        public ICollection<PostDTO> GetAllPostsByUserId(int id)
        {
            using (var db = new SovaContext())
            {
                var posts = db.Posts.Where(u => u.OwnerUserId == id);
                List<PostDTO> PostDTO = new List<PostDTO>();

                foreach (var post in posts)
                {

                    var newPost = new PostDTO(post.Id, post.OwnerUserId, post.Body, post.PostTypeId, post.ParentId, post.Title, post.Score, post.CreationDate,
                        post.ClosedDate, null, GetPostTypeByPostId(post.Id), GetPostTagsByPostId(post.Id), GetUserByPostId(post.Id));
                    PostDTO.Add(newPost);
                }
                return PostDTO;

            }

        }

        public int CountPosts()
        {
            using (var db = new SovaContext())
            {
                return db.Posts.Count();
            }
        }

        public PostTypeDTO GetPostTypeByPostId(int id)
        {
            using (var db = new SovaContext())
            {
                var post = db.Posts.FirstOrDefault(i => i.Id == id);
                var postType = db.PostTypes.Where(p => p.Id == post.PostTypeId).FirstOrDefault();
                var postTypeDTO = new PostTypeDTO(postType.Id, postType.Type);
                return postTypeDTO;
            }
        }

        public int? GetScoreByPostId(int id) {
            using (var db = new SovaContext())
            {
                try { 
                return db.Posts.Where(i => i.Id == id).First().Score;
                }
                catch
                {
                    return null;
                }



            }
        }
        ////////////////Answers
        public ICollection<Post> GetAnswers(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var Answers = db.Posts.Where(t => t.PostTypeId == 2).ToList();
                return Answers.OrderBy(x => x.Id)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }


        }

        public AnswerDTO GetAnswerById(int id)
        {

            using (var db = new SovaContext())
            {
                var post = GetPostById(id);
                if (post.PostType.Id == 2)
                {
                    return new AnswerDTO(post.Id, (int)post.ParentId, post.OwneruserId, post.Body, post.Title, post.Score, post.CreationDate,
                        post.ClosedDate, null, null, post.PostType, post.PostTags, GetUserByPostId(post.Id));
                }
                else
                {
                    return null;
                }
            }

        }


        public ICollection<AnswerDTO> GetAllAnswersByUserId(int id, int page, int pageSize)
        {

            using (var db = new SovaContext())
            {
                var answers = db.Posts.Where(i => i.PostTypeId == 2).ToList();
                var answersByUserId = answers.Where(u => u.OwnerUserId == id);
                List<AnswerDTO> answersDTO = new List<AnswerDTO>();
                foreach (var post in answersByUserId)
                {
                    var answer = new AnswerDTO(post.Id, (int)post.ParentId, post.OwnerUserId, post.Body, post.Title, post.Score, post.CreationDate,
    post.ClosedDate, null, null, GetPostTypeByPostId(post.Id), GetPostTagsByPostId(post.Id), GetUserByPostId(post.Id));
                    answersDTO.Add(answer);
                }
                return answersDTO.OrderBy(x => x.Id)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();

            }
        }

        public ICollection<AnswerDTO> GetAllAnswersByQuestionId(int id)
        {

            using (var db = new SovaContext())
            {
                var answers = db.Posts.Where(u => u.ParentId == id).ToList();
                List<AnswerDTO> answersDTO = new List<AnswerDTO>();
                foreach (var post in answers)
                {
                    var answer = new AnswerDTO(post.Id, (int)post.ParentId, post.OwnerUserId, post.Body, post.Title, post.Score, post.CreationDate,
    post.ClosedDate, null, null, GetPostTypeByPostId(post.Id), GetPostTagsByPostId(post.Id), GetUserByPostId(post.Id));
                    answersDTO.Add(answer);
                }
                return answersDTO.OrderBy(x => x.Id).ToList();

            }
        }

        public int CountAnswers()
        {
            using (var db = new SovaContext())
            {

                var answers = db.Posts.Where(t => t.PostTypeId == 2);
                return answers.Count();
            }
        }



        public int CountAnswersByUserId(int id)
        {
            using (var db = new SovaContext())
            {
                var answers = db.Posts.Where(i => i.PostTypeId == 2).ToList();
                return answers.Where(u => u.OwnerUserId == id).Count();

            }
        }
        public int CountAnswersByQuestionId(int id)
        {
            using (var db = new SovaContext())
            {
                var answers = db.Posts.Where(i => i.PostTypeId == 2).ToList();
                return answers.Where(u => u.ParentId == id).Count();

            }

        }

        ////////////////LinkedPosts
       public ICollection<QuestionDTO> GetLinkedPosts(int id)
        {
            using (var db = new SovaContext())
            {
                var linkedposts = db.Posts.Where(i => i.LinkPostId == id);
                List<QuestionDTO> LinkedPostsDto = new List<QuestionDTO>();
                foreach (var item in linkedposts)
                {
                    var newLinked = new QuestionDTO(item.Id, null, item.OwnerUserId, item.Body, item.Title, item.Score, item.CreationDate
                        ,null, null, null, null, null, null);
                    LinkedPostsDto.Add(newLinked);
                }
                return LinkedPostsDto;
            }
            }

        ////////////////Questions

        public QuestionDTO GetQuestionById(int id)
        {

            using (var db = new SovaContext())
            {
                var post = db.Posts.Where(i => i.Id == id).FirstOrDefault();
                if (post.PostTypeId == 1)
                {
                    return new QuestionDTO(post.Id, post.AcceptedAnswerId, post.OwnerUserId, post.Body, post.Title, post.Score, post.CreationDate,
                      post.ClosedDate, null, null, null, GetUserByPostId(post.Id), GetLinkedPosts(post.Id));
                }
                return null;

            }

        }
        public QuestionDTO GetQuestionByAnswerId(int id)
        {

            using (var db = new SovaContext())
            {
                var answer = db.Posts.Where(i => i.Id == id).FirstOrDefault();
                var q = db.Posts.Where(i => i.Id == answer.ParentId).FirstOrDefault();
                var answersOfq = db.Posts.Where(i => i.ParentId == q.Id).ToList();

                return new QuestionDTO(q.Id, q.AcceptedAnswerId, q.OwnerUserId, q.Body, q.Title, q.Score, q.CreationDate,
                                       q.ClosedDate, null, answersOfq, GetPostTagsByPostId(q.Id), GetUserByPostId(q.Id), GetLinkedPosts(q.Id));

            }
        }


        public ICollection<Post> GetQuestions(int page, int pageSize)
        {

            using (var db = new SovaContext())
            {
                var questions = db.Posts.Include(x => x.UserInfo).Where(i => i.PostTypeId == 1).Skip(page * pageSize)
                    .Take(pageSize).ToList();
                //foreach (var item in questions)
                //{
                //    item.UserInfo = db.UserInfo.Where(i => i.OwnerUserId == item.OwnerUserId).FirstOrDefault();
                //}
                return questions.OrderBy(x => x.Id).ToList();


            }
        }

        public ICollection<QuestionDTO> GetQuestionsByUserID(int id, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var questionsByUserId = db.Posts.Where(i => i.PostTypeId == 1).Where(u => u.OwnerUserId == id).Skip(page * pageSize)
                    .Take(pageSize).ToList();
                List<QuestionDTO> questionDTO = new List<QuestionDTO>();

                foreach (var post in questionsByUserId)
                {
                //    var answerCollection = db.Posts.Where(p => p.ParentId == post.Id).ToList();
                    var question = new QuestionDTO(post.Id, post.AcceptedAnswerId, post.OwnerUserId, post.Body, post.Title, post.Score, post.CreationDate,
    post.ClosedDate, null, null, GetPostTagsByPostId(post.Id), GetUserByPostId(post.Id), GetLinkedPosts(post.Id));
                    questionDTO.Add(question);
                }
                return questionDTO.OrderBy(x => x.Id).ToList();


            }
        }


        public int CountQuestionsByUserId(int id)
        {
            using (var db = new SovaContext())
            {
                var answers = db.Posts.Where(i => i.PostTypeId == 1).ToList();
                return answers.Where(u => u.OwnerUserId == id).Count();

            }
        }

        public int CountQuestions()
        {
            using (var db = new SovaContext())
            {

                return db.Posts.Where(i => i.PostTypeId == 1).Count();
            }
        }


        ////////////////Comments

        public CommentDTO GetCommentById(int id)
        {
            using (var db = new SovaContext())
            {
                var c = db.Comments.Where(i => i.CommentId == id).FirstOrDefault();
                var CommentedPost = db.Posts.Where(i => i.Id == c.PostId).FirstOrDefault();
                return new CommentDTO(c.CommentId, c.PostId, c.CommentText, c.CommentScore, c.CommentCreateDate, c.OwnerUserId, CommentedPost, GetUserByCommentId(c.CommentId));

            }

        }

        public ICollection<CommentDTO> GetCommentsByPostId(int postId, int page, int pageSize)
        {
            using (var db2 = new SovaContext())
            {
                var Comments = db2.Comments.Include(p => p.post).Where(p => p.PostId == postId);
                List<CommentDTO> CommentsDTO = new List<CommentDTO>();

                foreach (var item in Comments)
                {

                    var commentDTO = new CommentDTO(item.CommentId, item.PostId, item.CommentText, item.CommentScore, item.CommentCreateDate,
                        item.OwnerUserId, item.post, GetUserByPostId(postId));

                    CommentsDTO.Add(commentDTO);
                }
                return CommentsDTO.OrderBy(x => x.CommentId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public ICollection<CommentDTO> GetCommentsByUserId(int userId, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var Comments = db.Comments.Where(p => p.OwnerUserId == userId).ToList();
                List<CommentDTO> CommentsDTO = new List<CommentDTO>();

                foreach (var item in Comments)
                {

                    var commentDTO = new CommentDTO(item.CommentId, item.PostId, item.CommentText, item.CommentScore, item.CommentCreateDate,
                        item.OwnerUserId, null, GetUserByCommentId(item.CommentId));

                    CommentsDTO.Add(commentDTO);
                }
                return CommentsDTO.OrderBy(x => x.CommentId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }


        public ICollection<CommentDTO> GetComments()
        {
            using (var db = new SovaContext())
            {
                var comments = db.Comments.ToList();
                List<CommentDTO> commentsDTO = new List<CommentDTO>();

                foreach (var c in comments)
                {
                    var commentsPost = db.Posts.Where(i => i.Id == c.PostId).FirstOrDefault();
                    var newComment = new CommentDTO(c.CommentId, c.PostId, c.CommentText, c.CommentScore, c.CommentCreateDate, c.OwnerUserId
                        , commentsPost, GetUserByCommentId(c.CommentId));
                    commentsDTO.Add(newComment);
                }
                return commentsDTO;

            }
        }


        public int CountComments()
        {
            using (var db = new SovaContext())
            {
                return db.Comments.Count();
            }
        }

        public int CountCommentsByPostId(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Comments.Where(i => i.PostId == id).Count();
            }

        }

        public int CountCommentsByUserId(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Comments.Where(i => i.OwnerUserId == id).Count();
            }

        }


        ////////////////Tags

        public TagsDTO GetTagByPostTagId(int id)
        {
            using (var db = new SovaContext())
            {
                var tag = db.Tags.Where(t => t.Id == id).FirstOrDefault();
                TagsDTO tagsDTO = null;
                if (tag != null)
                {
                    tagsDTO = new TagsDTO(tag.Id, tag.Tag);
                }
                return tagsDTO;


            }
        }


        public TagsDTO GetTagByID(int id)
        {
            using (var db = new SovaContext())
            {
                var tag = db.Tags.Where(i => i.Id == id).FirstOrDefault();
                return new TagsDTO(tag.Id, tag.Tag);
            }

        }


        public ICollection<TagsDTO> GetTags()
        {
            using (var db = new SovaContext())
            {
                var tags = db.Tags.ToList();
                List<TagsDTO> tagsDTO = new List<TagsDTO>();

                foreach (var t in tags)
                {
                    var newtag = new TagsDTO(t.Id, t.Tag);
                    tagsDTO.Add(newtag);

                }
                return tagsDTO;

            }

        }

        public int CountTags()
        {
            using (var db = new SovaContext())
            {
                return db.Tags.Count();
            }
        }
        ////////////////PostTags

        public ICollection<PostTagsDTO> GetPostTagsByPostId(int id)
        {
            using (var db = new SovaContext())
            {
                var postTags = db.PostTags.Where(p => p.PostId == id);
                List<PostTagsDTO> PostTagsDTO = new List<PostTagsDTO>();

                foreach (var item in postTags)
                {
                    var postTagsDTO = new PostTagsDTO(item.PostId, item.TagId, GetTagByPostTagId(item.TagId));

                    PostTagsDTO.Add(postTagsDTO);
                }
                return PostTagsDTO;
            }
        }


        public int CountPostTags()
        {
            using (var db = new SovaContext())
            {
                return db.PostTags.Count();
            }
        }

        ////////////////UserInfo
        public ICollection<UserInfo> GetUsers(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                return db.UserInfo.Skip(page * pageSize)
                    .Take(pageSize).ToList();
                //List<UserInfoDTO> UsersDTO = new List<UserInfoDTO>();
                //foreach (var i in users)
                //{
                //    var newUser = new UserInfoDTO(i.OwnerUserId, i.OwnerUserDisplayName, i.CreationDate, i.OwnerUserLocation);
                //    UsersDTO.Add(newUser);
                //}
          
            }
        }
        public UserInfoDTO GetUserById(int id)
        {
            using (var db = new SovaContext())
            {
                var user = db.UserInfo.Where(i => i.OwnerUserId == id).FirstOrDefault();
                if (user != null)
                {
                    return new UserInfoDTO(user.OwnerUserId, user.OwnerUserDisplayName, user.CreationDate, user.OwnerUserLocation);
                }
                return null;
            }
        }
        public UserInfoDTO GetUserByPostId(int id)
        {
            using (var db2 = new SovaContext())
            {
                var post = db2.Posts.Where(i => i.Id == id).FirstOrDefault();
                var user = db2.UserInfo.Where(u => u.OwnerUserId == post.OwnerUserId).FirstOrDefault();
                if (user != null)
                {
                    var userDTO = new UserInfoDTO(user.OwnerUserId, user.OwnerUserDisplayName, user.CreationDate, user.OwnerUserLocation);
                    return userDTO;
                }
                return null;

            }
        }

        public UserInfoDTO GetUserByCommentId(int id)
        {
            using (var db = new SovaContext())
            {
                var comment = db.Comments.FirstOrDefault(c => c.CommentId == id);

                var user = db.UserInfo.FirstOrDefault(i => i.OwnerUserId == comment.OwnerUserId);
                return new UserInfoDTO(user.OwnerUserId, user.OwnerUserDisplayName, user.CreationDate, user.OwnerUserLocation);
            }

        }


        public int CountUsers()
        {
            using (var db = new SovaContext())
            {
                return db.UserInfo.Count();
            }
        }

        public bool UserHasQuestion(int id) {
            using (var db = new SovaContext())
            {
                return db.Posts.Where(x=> x.PostTypeId==1).Where(u => u.OwnerUserId == id).ToList().Count > 0;   
            }
        }


        ////////////////Marking

        public bool AddMarking(int postId)
        {
            using (var db = new SovaContext())
            {

                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn
                };

                cmd.Parameters.Add("@1", DbType.Int32);
                cmd.Parameters["@1"].Value = postId;
                cmd.CommandText = "call updateMarking(@1)";
                var executer = cmd.ExecuteNonQuery();
                return true;


            }

        }
        public bool AddMarkingWithAnnotation(int postId, string text, int from, int to)
        {
            using (var db = new SovaContext())
            {

                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn
                };

                cmd.Parameters.Add("@1", DbType.Int32);
                cmd.Parameters["@1"].Value = postId;
                cmd.CommandText = "call updateMarking(@1)";
                var executer = cmd.ExecuteNonQuery();
                AddAnnotation(postId, text,from , to);
                return true;


            }

        }
        public bool RemoveMarking(int id)
        {
            using (var db = new SovaContext())
            {
                var marking = db.Markings.FirstOrDefault(m => m.MarkedPostId == id);
               if (marking != null)
                {
                    DeleteAnnotationsByMarkingId(id);
                    db.Markings.Remove(marking);

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
       public MarkingDTO GetMarkingById(int id)
        {
            using (var db = new SovaContext())
            {
                var marking = db.Markings.FirstOrDefault(m => m.MarkedPostId == id);
                if (marking != null) { 
                return new MarkingDTO(marking.MarkedPostId, marking.MarkingDate, GetAnnotationById(id));
                } return null;
            }

        }
       public ICollection<MarkingDTO> GetMarkings(int page, int pageSize)
        {

            using (var db = new SovaContext())
            {
                var markings = db.Markings.OrderByDescending(x => x.MarkingDate).Skip(page * pageSize)
                    .Take(pageSize).ToList();
                List<MarkingDTO> markingsDTO = new List<MarkingDTO>();
                foreach (var item in markings)
                {
                    MarkingDTO newMarking = new MarkingDTO(item.MarkedPostId, item.MarkingDate, GetAnnotationById(item.MarkedPostId));
                    markingsDTO.Add(newMarking);
                }
                return markingsDTO.ToList();

            }
        }
        public int CountMarkings() {
            using (var db = new SovaContext())
            {
                return db.Markings.Count();
            }
        }

        ////////////////Annotations


        public AnnotationsDTO AddAnnotation(int primaryKey, string text, int from, int to)
        {

            using (var db = new SovaContext()) {
                Annotations a = new Annotations();
                a.MarkedPostId = primaryKey;
                a.Annotation = text;
                a.From = from;
                a.To = to;
                db.Add(a);
                db.SaveChanges();

                return new AnnotationsDTO(a.AnnotationId,a.MarkedPostId, a.Annotation,null, a.From, a.To);
            }

        }

        public AnnotationsDTO EditAnnotation(int id, string EditedText)
        {
            using (var db = new SovaContext())
            {
                var annotation = db.Annotations.FirstOrDefault(x => x.AnnotationId == id);
                if (annotation != null)
                {
                    annotation.Annotation = EditedText;
                   
                    db.SaveChanges();
                    return new AnnotationsDTO ( annotation.AnnotationId,annotation.MarkedPostId, annotation.Annotation ,null, annotation.From, annotation.To);
                }
                return null;
            }
        }


        public bool DeleteAnnotation(int id)
        {
            using (var db = new SovaContext())
            {
                var annotation = db.Annotations.FirstOrDefault(x => x.AnnotationId == id);
                if (annotation != null)
                {
                    db.Annotations.Remove(annotation);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        public bool DeleteAnnotationsByMarkingId(int id)
        {
            using (var db = new SovaContext())
            {
                var annotation = db.Annotations.Where(x => x.MarkedPostId == id);
                foreach (var item in annotation)
                {
                    db.Annotations.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
        }


        public AnnotationsDTO GetAnnotationById(int id)
        {
            using (var db = new SovaContext())
            {
                var annotation = db.Annotations.Where(i => i.AnnotationId == id).FirstOrDefault();
                if (annotation != null)
                {
                    return new AnnotationsDTO(annotation.AnnotationId, annotation.MarkedPostId, annotation.Annotation, annotation.Marking, annotation.From, annotation.To);
                }
                return new AnnotationsDTO(0, 0, "Empty", null, 0, 0);
            }
        }

        public ICollection<AnnotationsDTO> GetAnnotationsByMarkingId(int id)
        {
            using (var db = new SovaContext())
            {
                var annotation = db.Annotations.Where(i => i.MarkedPostId == id).ToList();
                List<AnnotationsDTO> annotationsDTO = new List<AnnotationsDTO>();

                foreach (var item in annotation)
                {

                    var newAnntoation = new AnnotationsDTO(item.AnnotationId, item.MarkedPostId, item.Annotation, item.Marking, item.From, item.To);
                    annotationsDTO.Add(newAnntoation);
                }


                return annotationsDTO;
            }
        }




        public ICollection<AnnotationsDTO> GetAnnotations()
        {
            using (var db = new SovaContext())
            {
                var annotations = db.Annotations.ToList();
                if (annotations != null)
                {
                    List<AnnotationsDTO> AnnotationsDTO = new List<AnnotationsDTO>();
                    foreach (var annotation in annotations)
                    {
                        var newAnn = new AnnotationsDTO(annotation.AnnotationId,annotation.MarkedPostId, annotation.Annotation, annotation.Marking, annotation.From, annotation.To);
                        AnnotationsDTO.Add(newAnn);

                    }
                    return AnnotationsDTO;

                }
                return null;

            }
        }



        public int CountAnnotations()
        {
            using (var db = new SovaContext())
            {
                return db.Annotations.Count();
            }
        }
        ////////////////Searching

        public ICollection<SearchResultDTO> Search(string keywords, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                //  var result = db.Posts.FromSql("call keysearch({0})", keywords);
                var result = db.SearchResult.FromSql("call SentenceSearch({0})", keywords);
                var result2 = result.OrderByDescending(x => x.Rank)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
                List <SearchResultDTO> ResultsDTO = new List<SearchResultDTO>();
                var totalResults= result.Count(); 
                foreach (var FoundItem in result2)
                {
                    var posttags = GetPostTagsByPostId(FoundItem.Id);
                    
                    var newItemDTO = new SearchResultDTO(FoundItem.Id, FoundItem.Title, FoundItem.Rank, posttags);

                    newItemDTO.totalResults = totalResults;
                    ResultsDTO.Add(newItemDTO);
                }

                return ResultsDTO;

            }

        }
        ////////////////SearchHistory

        public void AddSearchHistory(string SearchText)
        {
            using (var db = new SovaContext())
            {

                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn
                };

                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters["@1"].Value = SearchText;
                cmd.CommandText = "call updateSearchHistory(@1)";
                var executer = cmd.ExecuteNonQuery();

            }
        }

        public bool RemoveSearchHistory(int id)
        {
            using (var db = new SovaContext())
            {
                var searchedItem = db.SearchHistory.Where(i => i.Id == id).FirstOrDefault();
                if (searchedItem != null)
                {
                    db.SearchHistory.Remove(searchedItem);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }

        }



        public SearchHistoryDTO GetSearchHistoryById(int id)
        {
            using (var db = new SovaContext())
            {
                var s = db.SearchHistory.Where(i => i.Id == id).FirstOrDefault();
                return new SearchHistoryDTO(s.Id, s.SearchContent, s.SearchDate);

            }
        }


        public ICollection<SearchHistoryDTO> GetSearchHistories(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {

                var searchHistories = db.SearchHistory.GroupBy(x => x.SearchContent).Select(g => g.First()).OrderByDescending(x => x.SearchDate)
                    .Skip(page * pageSize)
                    .Take(pageSize);
                List<SearchHistoryDTO> SearchHistoriesDTO = new List<SearchHistoryDTO>();

                foreach (var s in searchHistories)
                {
                    var newSearchHistory = new SearchHistoryDTO(s.Id, s.SearchContent, s.SearchDate);
                    SearchHistoriesDTO.Add(newSearchHistory);

                }
                return SearchHistoriesDTO
                    .ToList();
            }

        }

        public int GetNumberOfSearches()
        {
            using (var db = new SovaContext())
            {
                var number = db.SearchHistory.Count();
                return number;

            }
        }


        ////////////////FavoriteTags

        public bool AddFavoriteTags()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFavoriteTags(int id)
        {
            throw new NotImplementedException();
        }



        public ICollection<FavoriteTagsDTO> GetFavoriteTagsByCustomeId(int id)
        {

            using (var db = new SovaContext())
            {
                var FavTags = db.FavoriteTags.Where(i => i.UserCustomeFieldId == id);
                List<FavoriteTagsDTO> FavTagsDTO = new List<FavoriteTagsDTO>();
                foreach (var t in FavTags)
                {
                    var Customfield = db.UserCustomeField.Where(i => i.Id == t.UserCustomeFieldId).FirstOrDefault();
                    var newTag = new FavoriteTagsDTO(t.UserCustomeFieldId, t.TagId, Customfield, GetTagByID(t.TagId));
                    FavTagsDTO.Add(newTag);
                }
                return FavTagsDTO;
            }
        }


        public int CountFavoriteTags()
        {
            using (var db = new SovaContext())
            {
                return db.FavoriteTags.Count();
            }
        }

        ////////////////UserCustomeField

        public void AddUserCustomeField(int postLimit, string tags)
        {
            using (var db = new SovaContext())
            {

                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn
                };

                cmd.Parameters.Add("@1", DbType.Int32);
                cmd.Parameters.Add("@2", DbType.String);
                cmd.Parameters["@1"].Value = postLimit;
                cmd.Parameters["@2"].Value = tags;
                cmd.CommandText = "call addUserCustomeField(@1,@2)";
                var reader = cmd.ExecuteNonQuery();


            }
        }


        public bool DeleteUserCustomeField(int id)
        {
            using (var db = new SovaContext())
            {
                var customeField = db.UserCustomeField.Where(i => i.Id == id).FirstOrDefault();

                if (customeField != null)
                {
                    var favoriteTagsOfC = db.FavoriteTags.Where(i => i.UserCustomeFieldId == id);
                    foreach (var item in favoriteTagsOfC)
                    {
                        db.FavoriteTags.Remove(item);
                    }
                    db.UserCustomeField.Remove(customeField);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }


        public UserCustomeFieldDTO GetUserCustomeFieldById(int id)
        {
            using (var db = new SovaContext())
            {
                var u = db.UserCustomeField.Where(i => i.Id == id).FirstOrDefault();

                return new UserCustomeFieldDTO(u.Id, u.Postlimit, u.CreationDate, GetFavoriteTagsByCustomeId(id));
            }
        }
        public UserCustomeFieldDTO GetLatestUserCustomeField()
        {
            using (var db = new SovaContext())
            {
                var u = db.UserCustomeField.OrderByDescending(x => x.Id).FirstOrDefault();

                return new UserCustomeFieldDTO(u.Id, u.Postlimit, u.CreationDate, GetFavoriteTagsByCustomeId(u.Id));
            }

        }


        public ICollection<UserCustomeFieldDTO> GetUserCustomeFields()
        {
            using (var db = new SovaContext())
            {
                var CustomeFields = db.UserCustomeField.ToList();
                List<UserCustomeFieldDTO> CustomeFieldsDTO = new List<UserCustomeFieldDTO>();
                foreach (var u in CustomeFields)
                {
                    var newCustomeField = new UserCustomeFieldDTO(u.Id, u.Postlimit, u.CreationDate, GetFavoriteTagsByCustomeId(u.Id));
                    CustomeFieldsDTO.Add(newCustomeField);
                }
                return CustomeFieldsDTO;

            }
        }


        public int CountUserCustomeFields()
        {
            using (var db = new SovaContext())
            {
                return db.UserCustomeField.Count();
            }
        }

        //RecommendedPosts
        public ICollection<CustomePostsDTO> ShowCustomePosts(){

            using (var db = new SovaContext())
            {
                var result = db.Posts.FromSql("call selectUserCustomePosts()");
                List<CustomePostsDTO> ResultsDTO = new List<CustomePostsDTO>();

                try{ 
                foreach (var FoundItem in result)
                {
                    var newItemDTO = new CustomePostsDTO(FoundItem.Id, FoundItem.Title, FoundItem.Body,FoundItem.OwnerUserId, FoundItem.PostTypeId);
                    ResultsDTO.Add(newItemDTO);

                }
                }
                catch
                {
                    var notFoundValue = new CustomePostsDTO(0, "not found", "not found", 1, 1);
                    var notFoundList = new List<CustomePostsDTO>();
                    notFoundList.Add(notFoundValue);
                    return notFoundList;

                }
                return ResultsDTO;

            }
        }




        //Co_Occurrent
        public ICollection<CoOccurrenceDTO> GetCoOccurrences(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var CoOccurrent = db.CoOccurrence.ToList();
                List<CoOccurrenceDTO> coOccurrenceDTO = new List<CoOccurrenceDTO>();
                    foreach(var item in CoOccurrent)
                {
                    var newItem = new CoOccurrenceDTO(item.Word, item.Word2 , item.Grade);
                    coOccurrenceDTO.Add(newItem);
                }
                return coOccurrenceDTO.OrderByDescending(x => x.Grade)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList(); 


        }
        }


        public ICollection<CoOccurrenceDTO> GetCoOccurrencesByWord(string word, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var CoOccurrent = db.CoOccurrence.Where(i => i.Word == word).ToList();
                List<CoOccurrenceDTO> coOccurrenceDTO = new List<CoOccurrenceDTO>();
                foreach (var item in CoOccurrent)
                {
                    var newItem = new CoOccurrenceDTO(item.Word, item.Word2, item.Grade);
                    coOccurrenceDTO.Add(newItem);
                }
                return coOccurrenceDTO.OrderByDescending(x => x.Grade)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();


            }

        }

        public int CountCoOccurrences()
        {
            using (var db = new SovaContext())
            {
                return db.CoOccurrence.Count();
            }
        }
        public  int CountCoOccurrencesByWord(string word)
        {
            using (var db = new SovaContext())
            {
                return db.CoOccurrence.Where(x=> x.Word == word).Count();
            }

        }

        ///////////Term_As_Result

        public int CountTermsAsResult(string text)
        {
            using (var db = new SovaContext())
            {
                return db.TermAsResult.FromSql("call bestmatch_terms({0})", text).Count();

            }
        }

       /* public ICollection<TermAsResultDTO> GetTermsAsResult(string text, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var terms = db.TermAsResult.FromSql("call bestmatch_terms({0})", text);

                List<TermAsResultDTO> termDto = new List<TermAsResultDTO>();

                foreach (var item in terms)
                {
                    var newItem = new TermAsResultDTO(item.Word, item.Score);
                    termDto.Add(newItem);

                }

                return termDto.OrderByDescending(x => x.Score)
                   .Skip(page * pageSize)
                   .Take(pageSize)
                   .ToList();

            }

        } */

       public ICollection<TermAsResultDTO> GetTermsByPostId(int id)
        {
            using (var db = new SovaContext())
            {
                var terms = db.TermScore.Where(x => x.Id == id).ToList();
                List<TermAsResultDTO> termsDto = new List<TermAsResultDTO>();
                foreach (var item in terms)
                {
                    var newTerm = new TermAsResultDTO(item.Word, item.Tf);
                    Console.WriteLine(item.Word);
                    termsDto.Add(newTerm);

                }
                return termsDto;

            }
        }

       public TermNetworkMakerDTO GenerateTermNetworkCode(string word)
        {
            using (var db = new SovaContext())
            {
                var result = db.TermNetworkMaker.FromSql("call term_network({0})", word);
                TermNetworkMakerDTO resultDTO = new TermNetworkMakerDTO();

                foreach (var item in result)
                {
                    resultDTO.Code += item.Raw;
                    resultDTO.Code += " ";
                }
                return resultDTO;

            }
        }




    }
}

