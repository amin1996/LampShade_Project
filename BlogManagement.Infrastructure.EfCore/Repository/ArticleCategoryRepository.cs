﻿using _0_Framework.Application;
using _0_FrameWork.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _context.ArticleCategories.Select(x=>new ArticleCategoryViewModel 
            {
                Id = x.Id,
                Name = x.Name,

            }).ToList();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Name = x.Name,
                CanonicalAddress = x.CanonicalAddress,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Description = x.Description,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
            }).FirstOrDefault(x => x.Id == id);
        }

        public string GetSlugBy(long id)
        {
            return _context.ArticleCategories.Select(x => new { x.Id, x.Slug })
                .FirstOrDefault(x => x.Id == id).Slug;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories
                .Include(x=>x.Articles)
                .Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                ShowOrder = x.ShowOrder,
                Description = x.Description,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi(),
                ArticlesCount=x.Articles.Count,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.ShowOrder).ToList();

        }
    }
}
