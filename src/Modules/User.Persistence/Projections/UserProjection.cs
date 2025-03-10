using Core.Domain.Primitives;

namespace User.Persistence.Projections
{
    public class UserProjection<TProjection>(IUserProjectionDbContext context) : Core.Persistence.Projection.Projection<TProjection>(context), IUserProjection<TProjection>
        where TProjection : IProjection
    {
    }
}
