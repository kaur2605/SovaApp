using System;
using Xunit;
using DataService.DomainModel;
using DataService.DTO;
using DataService;
using DataService.DataAccessLayer;
using System.Linq;
namespace UnitTests
{
    public class DataAccessLayerTests
    {
        [Fact]
        
        public void CountPosts_ReturnsPostsNumbers()
        {
            var db = new RepositoryBody();
            int postCount = db.CountPosts();
            Assert.Equal(13629, postCount);
        }
        
        [Fact]

        public void PostType_GetPostTypeByPostId()
        {
            var db = new RepositoryBody();
            var PostType = db.GetPostTypeByPostId(19);
            Assert.Equal(1, PostType.Id);
        }

        [Fact]

        public void UserInfo_GetUserByCommentId()
        {
            var db = new RepositoryBody();
            var UserInfo = db.GetUserByCommentId(69759);
            Assert.Equal("Jeff Atwood", UserInfo.DisplayName);
            Assert.Equal("El Cerrito, CA", UserInfo.Location);

        }

        [Fact]

        public void Comments_GetCommentsByPostId()
        {
            var db = new RepositoryBody();
            var Comments = db.GetCommentsByPostId(52002,0,20);
            Assert.Equal(9, Comments.Count());
            Assert.Equal("A man, a plan, a canal, Panama", Comments.First().Body);
            Assert.Equal(15, Comments.First().Score);
        }


        [Fact]

        public void PostTags_GetPostTagsByPostId()
        {
            var db = new RepositoryBody();
            var PostTags = db.GetPostTagsByPostId(26583319);
            var PostTag = db.GetPostTagsByPostId(18332611);
            Assert.Equal(4, PostTags.Count());
            Assert.Equal("android", PostTag.First().Tag.Tag);
            Assert.Equal(1920, PostTag.First().TagId);
        }


        [Fact]

        public void FavoriteTags_GetEachTag()
        {
            var db = new RepositoryBody();
            var tagsCount = db.GetFavoriteTagsByCustomeId(1).Count;
            var firstTag = db.GetFavoriteTagsByCustomeId(1).First().Tag.Tag;
            Assert.Equal(3, tagsCount);
            Assert.Equal("java", firstTag);

        }
        
        [Fact]

        public void QuestionByAnswerId_returnsListOf_Its_Answers()
        {
            var db = new RepositoryBody();
            var parentId = db.GetQuestionByAnswerId(71).Answers.First().ParentId;
            var SameParentId = db.GetQuestionByAnswerId(71).Id;
            Assert.Equal(parentId, SameParentId);


        }



         
       [Fact]

        public void UserCustomField_GetUserCustomeFieldById()

        {
            var db = new RepositoryBody();
            var Ftag = db.GetUserCustomeFieldById(2);
            Assert.Equal(3, Ftag.FavoriteTags.Count());
        }

        [Fact]

        public void FavoriteTag_GetFavoriteTagsByCustomeId()

        {
            var db = new RepositoryBody();

            var TotalFavtTags = db.GetFavoriteTagsByCustomeId(2).Count;

            var FavtTag = db.GetFavoriteTagsByCustomeId(1);

            Assert.Equal(3, TotalFavtTags);

            Assert.Equal(2726, FavtTag.First().TagId);

        }
        

   
      

        [Fact]
        public void AddCustomeField_Adds_FavoriteTags()
        {

            var db = new RepositoryBody();
            db.AddUserCustomeField(11, "assembly,robots");
            var id = db.GetUserCustomeFields().Last().Id;
            var favoriteTags = db.GetFavoriteTagsByCustomeId(id);
            Assert.Equal(favoriteTags.First().Tag.Tag, "assembly");
            Assert.Equal(db.DeleteUserCustomeField(id), true);
            Assert.Empty(db.GetFavoriteTagsByCustomeId(id));  

        }

        [Fact]
        public void Custome_FieldMakes_CustomPostLimit()
        {
            var db = new RepositoryBody();
            db.AddUserCustomeField(5, "C#, java");
            var id = db.GetUserCustomeFields().Last().Id;
            var CustomePost = db.ShowCustomePosts();
            Assert.Equal(CustomePost.Count(), 5);
            Assert.Equal(db.DeleteUserCustomeField(id), true);
        }




        // IR tests

        [Fact]
        public void Search_MustReturnValidValues_andSavedHistory()
        {
            var db = new RepositoryBody();
            var foundTitle = db.Search("best gtk programming ide", 0, 5).Last().Title;
            Assert.Equal(foundTitle, "The best ide for gtk+ programming");
            var searchHistory = db.GetSearchHistories(0, 3);
            Assert.Equal("best gtk programming ide", searchHistory.First().SearchContent);
        }

        [Fact]
        public void Search_MustBeOrdered_By_Rank()
        {
            var db = new RepositoryBody();
            string searchString = "the best most common design patterns used for games";
            var FirstPostScore = db.Search(searchString, 0, 5).First().Rank;
            var LastPostScore = db.Search(searchString, 0, 5).Last().Rank;
            Assert.True(FirstPostScore > LastPostScore);
        }

        [Fact]
        public void CoOccurrence_ShouldReturn_ValidValues()
        {
            var db = new RepositoryBody();
            var CoOccur = db.GetCoOccurrencesByWord("class", 0, 3).First();
            var word1 = CoOccur.Word;
            var word2 = CoOccur.Word2;
            var grade = CoOccur.Grade;
            Assert.Equal(word1,  "class");
            Assert.Equal(word2, "public");
            Assert.Equal(81, grade);
        }






    }


}

