using Application.Common.Interfaces;
using Application.Services.CommentService.Dtos;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentService
{
    public class CommentService
    {
        private readonly IAppDbContext _appDbContext;

        public CommentService(
            IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public ICollection<Comment> AddToPost(string ownerID, Guid postID, CommentRequestAdd comment)
        { 
            var p = _appDbContext.Posts
                .Include(p=>p.Comments)
                .Single(p=>p.ID == postID) ;
            p?.Comments!.Add(new() {
                Content=comment.Content ,
                OwnerID = ownerID
            });
            _appDbContext.SaveChanges();
            return p.Comments!;
        }

        public void RemoveCommentFromPost(Guid postId, Guid commentId)
        {
            var comment = _appDbContext.Comments
                .Single(c => c.ID == commentId);
            _appDbContext.Comments
                .Remove(comment);
            _appDbContext.SaveChanges();
        }
    }
}
