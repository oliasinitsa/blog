using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.Repository;

namespace Blog.Services
{
    public class TagService : BaseService<Tag>
    {
        private IRepository<Tag> tags;

        public TagService()
        {
            tags = new BaseRepository<Tag>(context);
        }

        public void Create(Tag entity)
        {
            tags.Create(entity);
            Save();
        }

        public IEnumerable<Tag> FindByTag(String text)
        {
            return tags.Read().Where(tag => tag.Text.Equals(text)).AsEnumerable(); //Не совсем правильно...лучше выгружать сразу список постов с этим тегом
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return tags.Read().OrderByDescending(tag => tag.Text).AsEnumerable();
        }
    }
}