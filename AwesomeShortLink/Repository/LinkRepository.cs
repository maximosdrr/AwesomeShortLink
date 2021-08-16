using AwesomeShortLink.Models;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace AwesomeShortLink.Repository
{
    public class LinkRepository
    {
        private readonly ILiteDatabase Db;
        private readonly ILiteCollection<ShortLink> Collection;

        public LinkRepository(HttpContext Context)
        {
            Db = Context.RequestServices.GetService<ILiteDatabase>();
            Collection = Db.GetCollection<ShortLink>(BsonAutoId.Int32);
        }
        public void Insert(ShortLink link)
        {
            Collection.Insert(link);
        }

        public ShortLink FindOneById(int id)
        {
            return Collection.FindOne(docs => docs.Id == id);
        }
    }
}
