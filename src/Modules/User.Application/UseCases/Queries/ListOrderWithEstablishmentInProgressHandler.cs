using Core.Application.Messaging;
using Core.Shared.Results;
using User.Shared.Queries;
using User.Shared.Responses;
using MediatR;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace User.Application.UseCases.Queries
{
    public class ListOrderWithEstablishmentInProgressHandler(
        ISender sender) : IQueryHandler<ListOrderWithEstablishmentInProgressQuery, List<OrderWithEstablishmentInProgressResponse>>
    {
        public async Task<Result<List<OrderWithEstablishmentInProgressResponse>>> Handle(
            ListOrderWithEstablishmentInProgressQuery query,
            CancellationToken cancellationToken)
        {
            //var ordersResult = await sender.Send(new ListInProgressOrdersByUserIdQuery(query.UserId), cancellationToken);

            //if (ordersResult.IsFailure)
            //    return Result.Failure<List<OrderWithEstablishmentInProgressResponse>>(ordersResult.Error);

            //var responses = new List<OrderWithEstablishmentInProgressResponse>();

            //await foreach (var response in StreamOrderWithEstablishmentAsync(ordersResult.Value, cancellationToken))
            //    responses.Add(response);

            //return Result.Success(responses.OrderByDescending(x => x.CreatedAt).ToList());
            return default;
        }

        //private async IAsyncEnumerable<OrderWithEstablishmentInProgressResponse> StreamOrderWithEstablishmentAsync(
        //    List<OrderResponse> orders,
        //    [EnumeratorCancellation] CancellationToken cancellationToken)
        //{
        //    foreach (var order in orders)
        //    {
        //        var establishmentResult = await sender.Send(new GetEstablishmentByIdQuery(order.EstablishmentId), cancellationToken);

        //        if (establishmentResult.IsFailure)
        //            continue;

        //        var establishment = establishmentResult.Value;

        //        yield return new OrderWithEstablishmentInProgressResponse(
        //            establishment.TradeName,
        //            order.Number,
        //            order.Id,
        //            order.RequestedAmount ?? order.OriginalRequestedAmount,
        //            establishment.EstablishmentId,
        //            OrderStatus.ConvertOnGoindOrderStatus(order.Status),
        //            order.CreatedAt);
        //    }
        //}
    }
}
