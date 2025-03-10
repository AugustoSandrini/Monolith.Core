using Core.Application.Messaging;
using Core.Shared.Results;
using User.Shared.Queries;
using User.Shared.Responses;
using MediatR;
using System.Runtime.CompilerServices;

namespace User.Application.UseCases.Queries
{
    public class ListEstablishmentWithOrderInProgressHandler(
        ISender sender) : IQueryHandler<ListEstablishmentsWithOrdersInProgressQuery, List<EstablishmentWithOrderInProgressResponse>>
    {
        public async Task<Result<List<EstablishmentWithOrderInProgressResponse>>> Handle(
            ListEstablishmentsWithOrdersInProgressQuery query,
            CancellationToken cancellationToken)
        {
            //var ordersResult = await sender.Send(new ListInProgressOrdersByUserIdQuery(query.UserId), cancellationToken);

            //if (ordersResult.IsFailure)
            //    return Result.Failure<List<EstablishmentWithOrderInProgressResponse>>(ordersResult.Error);

            //var responses = new List<EstablishmentWithOrderInProgressResponse>();

            //await foreach (var response in StreamEstablishmentWithOrderAsync(ordersResult.Value, cancellationToken))
            //    responses.Add(response);

            //return Result.Success(responses.OrderByDescending(x => x.CreatedAt).ToList());
            return default;
        }

        //private async IAsyncEnumerable<EstablishmentWithOrderInProgressResponse> StreamEstablishmentWithOrderAsync(
        //    //List<OrderResponse> orders,
        //    [EnumeratorCancellation] CancellationToken cancellationToken)
        //{
        //    foreach (var order in orders)
        //    {
        //        var establishmentResult = await sender.Send(new GetEstablishmentByIdQuery(order.EstablishmentId), cancellationToken);

        //        if (establishmentResult.IsFailure)
        //            continue;

        //        var establishment = establishmentResult.Value;

        //        yield return new EstablishmentWithOrderInProgressResponse(
        //            establishment.EstablishmentId,
        //            establishment.LegalName,
        //            establishment.TradeName,
        //            order.Id,
        //            order.Status,
        //            order.CreatedAt);
        //    }
        //}
    }
}
