using Application.Common.Interfaces;
using Application.Services.TagService.Dtos;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.TagService
{
    public class TagServices
    {
        private readonly IAppDbContext _context;

        public TagServices(IAppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> Tags => _context.Tags;

        public IQueryable<Tag> getTagsByName(ICollection<string> name)
        {
            return _context.Tags.Where(t => name.Contains(t.TagName));
        }

        public IQueryable<Tag> getTagsById(ICollection<int> ids)
        {
            return _context.Tags.Where(t => ids.Contains(t.ID));
        }

        public Tag AddTag(TagRequestAdd tag)
        {
            var t = new Tag()
            {
                TagName = tag.Name,
                TagColour = tag.ColorRGB ?? "EEEEEE"
            };
            _context.Tags.Add(t);
            _context.SaveChanges();
            return t;
        }
    }
}
