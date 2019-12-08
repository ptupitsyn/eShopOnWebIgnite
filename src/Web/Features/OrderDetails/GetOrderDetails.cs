using System;
using MediatR;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.OrderDetails
{
    public class GetOrderDetails : IRequest<OrderViewModel>
    {
        public string UserName { get; private set; }
        public Guid OrderId { get; private set; }

        public GetOrderDetails(string userName, Guid orderId)
        {
            UserName = userName;
            OrderId = orderId;
        }
    }
}
