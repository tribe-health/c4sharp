using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using static C4Sharp.Sample.Orders.Structures;

namespace C4Sharp.Sample.Orders;
public class PackageByLayerDiagram
{
    public static ComponentDiagram Diagram => new()
    {
        Title = "Package By Layer Diagram",
        ShowLegend = false,
        Structures = new Structure[]
        {
            WebLayer,
            ServiceLayer,
            DataLayer,
        },
        Relationships = new[]
        {
            (OrdersController > IOrdersService)[Position.Down],
            (OrdersService > IOrdersRepository)[Position.Down],
        }
    };

    private static ContainerBoundary WebLayer => new("WebLayer", "web")
    {
        Components = new[]
        {
            OrdersController
        },
    };

    private static ContainerBoundary ServiceLayer => new("ServiceLayer", "service")
    {
        Components = new[]
        {
            IOrdersService,
            OrdersService
        },
        Relationships = new[]
        {
            (IOrdersService < OrdersService)["implements"],
        }
    };

    private static ContainerBoundary DataLayer => new("DataLayer", "data")
    {
        Components = new[]
        {
            IOrdersRepository,
            OrdersRepository
        },
        Relationships = new[]
        {
            (IOrdersRepository < OrdersRepository)["implements"],
        }
    };
}
