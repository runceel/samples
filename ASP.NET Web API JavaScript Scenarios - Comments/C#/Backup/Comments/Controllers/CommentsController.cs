using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Comments.Models;
using System.Web.Http;
using Comments.Filters;

namespace Comments.Controllers 
{
    //[Authorize]
    public class CommentsController : ApiController 
    {
        ICommentRepository repository;

        public CommentsController(ICommentRepository repository) 
        {
            this.repository = repository;
        }

        #region GET
        public IQueryable<Comment> GetComments()
        {
            return repository.Get().AsQueryable();
        }

        public Comment GetComment(int id)
        {
            Comment comment;
            if (!repository.TryGet(id, out comment))
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return comment;
        }
        #endregion

        #region POST
        public HttpResponseMessage<Comment> PostComment(Comment comment) 
        {
            comment = repository.Add(comment);
            var response = new HttpResponseMessage<Comment>(comment, HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/comments/" + comment.ID.ToString());
            return response;
        }
        #endregion

        #region DELETE
        public Comment DeleteComment(int id) 
        {
            Comment comment;
            if (!repository.TryGet(id, out comment))
                throw new HttpResponseException(HttpStatusCode.NotFound);
            repository.Delete(id);
            return comment;
        }
        #endregion

        #region Paging GET
        public IEnumerable<Comment> GetComments(int pageIndex, int pageSize)
        {
            return repository.Get().Skip(pageIndex * pageSize).Take(pageSize);
        }
        #endregion
    }
}