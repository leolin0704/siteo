using System.Data.Entity;

namespace Siteo.DAL
{
    public class BaseDBContext: DbContext
    {
        public BaseDBContext() : base("name=SiteoEntities") { }
    }
}
