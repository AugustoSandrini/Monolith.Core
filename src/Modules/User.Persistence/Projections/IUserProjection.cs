using Core.Domain.Primitives;
using Core.Domain.Projection;

namespace User.Persistence.Projections
{
    public interface IUserProjection<TProjection> : IProjection<TProjection>
        where TProjection : IProjection
    { }
}
