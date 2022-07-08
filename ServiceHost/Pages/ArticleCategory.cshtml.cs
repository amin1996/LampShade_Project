using _01_LampShadeQuery.Contracts.Article;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public List<ArticleQueryModel> LatestArticles;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string id)
        {
            ArticleCategory=_articleCategoryQuery.GetArticleCategory(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
            LatestArticles = _articleQuery.LatestArticles();
        }
    }
}
