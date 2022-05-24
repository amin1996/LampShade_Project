using _0_Framework.Application;
using _0_FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly ShopContext _context;

        public CommentRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Include(x => x.Product).Select(x => new CommentViewModel
            {
                Id = x.Id,
                Email = x.Email,
                CommentDate = x.CreationDate.ToFarsi(),
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                Message = x.Message,
                Name = x.Name,
                ProductId = x.ProductId,
                ProductName=x.ProductId,

            });

            if(string.IsNullOrWhiteSpace(searchModel.Name))
                query=query.Where(x=>x.Name.Contains(searchModel.Name));

            if (string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
