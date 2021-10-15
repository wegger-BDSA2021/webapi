
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface IFooContext
    {
        DbSet<EntityT> Entities { get; set; }
    }
}