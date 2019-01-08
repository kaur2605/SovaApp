using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;
using DataService.DTO;


namespace DataService.DataAccessLayer
{
    public interface IRepository
    {



        //Post
        PostDTO GetPostById(int id);
        ICollection<PostDTO> GetPosts();
        ICollection<PostDTO> GetAllPostsByUserId(int id);
        int CountPosts();
        int? GetScoreByPostId(int id);

        //PostType
        PostTypeDTO GetPostTypeByPostId(int id);
        

        //Answer
        ICollection<Post> GetAnswers(int page, int pageSize);
        AnswerDTO GetAnswerById(int id);
        ICollection<AnswerDTO> GetAllAnswersByUserId(int id, int page, int pageSize);
        ICollection<AnswerDTO> GetAllAnswersByQuestionId(int id);
        int CountAnswers();
        int CountAnswersByUserId(int id);
        int CountAnswersByQuestionId(int id);
        //LinkedPosts
        ICollection<QuestionDTO> GetLinkedPosts(int id);
        //Question
        QuestionDTO GetQuestionById(int id);
        QuestionDTO GetQuestionByAnswerId(int id);
        ICollection<Post> GetQuestions(int page, int pageSize);
        ICollection<QuestionDTO> GetQuestionsByUserID(int id, int page, int pageSize);
        int CountQuestions();
        int CountQuestionsByUserId(int id);

        //Comment
        CommentDTO GetCommentById(int id);
        ICollection<CommentDTO> GetComments();
        ICollection<CommentDTO> GetCommentsByPostId(int postId, int page, int pageSize);
        ICollection<CommentDTO> GetCommentsByUserId(int userId, int page, int pageSize);
        int CountComments();
        int CountCommentsByPostId(int id);
        int CountCommentsByUserId(int id);

        //Tags
        TagsDTO GetTagByPostTagId(int id);
        TagsDTO GetTagByID(int id);
        ICollection<TagsDTO> GetTags();
        int CountTags();


        //PostTags
        ICollection<PostTagsDTO> GetPostTagsByPostId(int id);
        int CountPostTags();


        //UserInfo
        ICollection<UserInfo> GetUsers(int page, int pageSize);
        UserInfoDTO GetUserById(int id);
        UserInfoDTO GetUserByPostId(int id);
        UserInfoDTO GetUserByCommentId(int id);
        int CountUsers();
        bool UserHasQuestion(int id);

        //Markings
        Boolean AddMarking(int postId);
        bool AddMarkingWithAnnotation(int postId, string text, int from, int to);
        Boolean RemoveMarking(int id);
        MarkingDTO GetMarkingById(int id);
        ICollection<MarkingDTO> GetMarkings(int page, int pageSize);
        int CountMarkings();

        //Annotations
        AnnotationsDTO AddAnnotation(int primaryKey, string text,int from, int to);
        AnnotationsDTO EditAnnotation(int id, string EditedText);
        Boolean DeleteAnnotation(int id);
        Boolean DeleteAnnotationsByMarkingId(int id);
        AnnotationsDTO GetAnnotationById(int id);
        ICollection<AnnotationsDTO> GetAnnotationsByMarkingId(int id);
        ICollection<AnnotationsDTO> GetAnnotations();
        int CountAnnotations();

        //Searching
        ICollection<SearchResultDTO> Search(string searchText,int page, int pageSize);

        //SearchHistory
        void AddSearchHistory(String SearchText);
        bool RemoveSearchHistory(int id);
        SearchHistoryDTO GetSearchHistoryById(int id);
        ICollection<SearchHistoryDTO> GetSearchHistories(int page, int pageSize);
        int GetNumberOfSearches();

        //FavoriteTags
        Boolean AddFavoriteTags();
        Boolean RemoveFavoriteTags(int id);
        ICollection<FavoriteTagsDTO> GetFavoriteTagsByCustomeId(int id);
        int CountFavoriteTags();

        //UserCustomeField
        void AddUserCustomeField(int postLimit, string tags);
        Boolean DeleteUserCustomeField(int id);
        UserCustomeFieldDTO GetUserCustomeFieldById(int id);
        UserCustomeFieldDTO GetLatestUserCustomeField();
        ICollection<UserCustomeFieldDTO> GetUserCustomeFields();
        int CountUserCustomeFields();

        //RecommendedPosts
        ICollection<CustomePostsDTO> ShowCustomePosts();


        //Co_Occurrent
        ICollection<CoOccurrenceDTO> GetCoOccurrences(int page, int pageSize);
        ICollection<CoOccurrenceDTO> GetCoOccurrencesByWord(string word, int page, int pageSize);
        int CountCoOccurrences();
        int CountCoOccurrencesByWord(string word);

        //Term_As_Result
        int CountTermsAsResult(string text);
      //ICollection<TermAsResultDTO> GetTermsAsResult(string text , int page, int pageSize);
        ICollection<TermAsResultDTO> GetTermsByPostId(int id);

        //Term_Network_Maker
        TermNetworkMakerDTO GenerateTermNetworkCode(string word);




    }
}
