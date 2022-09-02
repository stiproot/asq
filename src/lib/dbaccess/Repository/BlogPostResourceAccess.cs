using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace dbaccess.Repository
{
    public class BlogPostResourceAccess: BaseResourceAccess<BlogPostDto, TbBlogPost>, IBlogPostResourceAccess
    {
        private readonly IEnumerable<string> _includes = new List<string>()
        {
          "TbFocusBlogPostMappings.Focus",
          "Img",
          "CreationUser.Img",
          "CreationUser.TbFocusUserMappings.Focus",
        }; 

        public BlogPostResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<BlogPostDto> GetBlogPost(object id)
        {
            Expression<Func<TbBlogPost, bool>> predicate = null;
            TbBlogPost entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                //entity = await this._repo.FindByKey(longId);
                predicate = (TbBlogPost u) => u.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbBlogPost u) => u.UniqueId.Equals(id);
            }
            else
            {
                throw new NotImplementedException();
            }

            entity = await this._repo.FindSingleOrDefaultAsync(predicate, this._includes);

            if(entity == null)
            {
                return null;
            }
            return this._mapper.Map<TbBlogPost, BlogPostDto>(entity);
        }

        public async Task<IEnumerable<BlogPostSummaryDto>> GetBlogPostSummariesByFilter(BlogPostFilterConfigDto filter)
        {
            Expression<Func<TbBlogPost, bool>> predicate =
                (blogPost) => 
                    // Foci...
                    (!filter.Foci.Any() || blogPost.TbFocusBlogPostMappings.Any(mapping => filter.Foci.Contains(mapping.FocusId))) && 
                    // Creation user guid...
                    (filter.CreationUserUniqueId == null || blogPost.CreationUser.UniqueId == filter.CreationUserUniqueId.ToString()) &&
                    // Elastic...
                    (
                        (filter.Elastic == null || filter.Elastic == string.Empty) ||
                        (
                            blogPost.Title.Contains(filter.Elastic) ||
                            (
                                blogPost.CreationUser.Name.Contains(filter.Elastic) || 
                                blogPost.CreationUser.Surname.Contains(filter.Elastic) || 
                                blogPost.CreationUser.Username.Contains(filter.Elastic)
                            )
                        )
                    ) &&
                    // Creation user name...
                    (
                        (filter.CreationUserName == null || filter.CreationUserName == string.Empty) || 
                        (
                            blogPost.CreationUser.Name.Contains(filter.CreationUserName) || 
                            blogPost.CreationUser.Surname.Contains(filter.CreationUserName) ||
                            blogPost.CreationUser.Username.Contains(filter.CreationUserName)
                        )
                    );
            Expression<Func<TbBlogPost, object>> orderByFunc = null; 
            Expression<Func<TbBlogPost, object>> orderByDescFunc = (TbBlogPost b) => b.CreationDateUtc;
            var take = filter.Take ?? 10;
            
            var summaries = this._mapper.Map<IEnumerable<TbBlogPost>, IEnumerable<BlogPostSummaryDto>>( 
                await this._repo.FindToListAsync(predicate, this._includes, orderByFunc, orderByDescFunc, take));
            
            return summaries;
        }

        public async Task<BlogPostDto> AddBlogPost(BlogPostDto dto) 
        {
            return await this.Add(dto);
        }

        public async Task UpdateBlogPost(BlogPostDto dto) 
        {
            await this.Update(dto);
        }

        public async Task DeleteBlogPost(object id) 
        {
            await this.Delete(id);
        }
    }
}