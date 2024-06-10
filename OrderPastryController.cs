using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using probKol2.DTOs;
using probKol2.Models;
using probKol2.Repositories;

namespace probKol2.Controllers;

[ApiController]
[Route("api")]
public class OrderPastryController : ControllerBase
{
    private readonly IOrderPastryRepository _pastryRepository;

    public OrderPastryController(IOrderPastryRepository orderPastryRepository)
    {
        _pastryRepository = orderPastryRepository;
    }


    [HttpGet]
    [Route("/orders")]
    public async Task<IActionResult> GetList(String? clientLastName = null)
    {

        var tmp = await _pastryRepository.getOrders(clientLastName);

        return Ok(tmp.Select(e => new OrderDto()
        {
            ID = e.ClientId,
            FulfilledAt = e.FuffilledAt,
            acceptedAt = e.AcceptedAt,
            comments = e.Comments,
            Pastrys = e.OrderPastries.Select(p => new PastryDto
            {
                name = p.Pastry.Name,
                price = p.Pastry.Price,
                amount = p.Amount
            }).ToList()
        }));
    }


    // [HttpPost]
    // [Route("clietns/{idClient}/orders")]
    // public async Task<IActionResult> AddOrder(int idClient, AddOrder addOrder)
    // {
    //     // var order = new Order()
    //     // {
    //     //     AcceptedAt = addOrder.acceptedAt,
    //     //     ClientId = idClient,
    //     //     EmployeeID = addOrder.employeeId,
    //     //     Comments = addOrder.comments,
    //     //     
    //     // };
    //     //
    //     using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    //     {
    //         // var idOrder = await _pastryRepository.AddNewOrder(order);
    //
    //         var pastries = new List<OrderPastry>();
    //
    //         foreach (var pastry in addOrder.PastryDto2s)
    //         {
    //             var newPastry = await _pastryRepository.DoesPastryExist(pastry.name);
    //             if (newPastry is null)
    //             {
    //                 return NotFound("Given pastry doesn't exist");
    //             }
    //
    //             pastries.Add(new OrderPastry()
    //             {
    //                 PastryID = newPastry.ID,
    //                 Amount = pastry.amount,
    //                 Comme = pastry.comments,
    //             });
    //         }
    //
    //         // await _pastryRepository.AddOrderPastries(pastries);
    //        
    //         // return Created();
    //         // }
    //
    //         var order = new Order()
    //         {
    //             ID = 5,
    //             AcceptedAt = addOrder.acceptedAt,
    //             ClientId = idClient,
    //             EmployeeID = addOrder.employeeId,
    //             Comments = addOrder.comments,
    //             OrderPastries = new List<OrderPastry>(pastries)
    //         };
    //         await _pastryRepository.AddNewOrder(order);
    //          scope.Complete();
    //         return Ok();
    //
    //     }
    // }
    
    
    [HttpPost]
    [Route("clients/{idClient}/orders")]
    public async Task<IActionResult> AddOrder(int idClient, AddOrder addOrder)
    {
        var order = new Order()
        {
            AcceptedAt = addOrder.acceptedAt,
            ClientId = idClient,
            EmployeeID = addOrder.employeeId,
            Comments = addOrder.comments
        };

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // Najpierw dodaj i zapisz zamówienie
         var idOrder  = await _pastryRepository.AddNewOrder(order);

            var pastries = new List<OrderPastry>();

            foreach (var pastryDto in addOrder.PastryDto2s)
            {
                var pastry = await _pastryRepository.DoesPastryExist(pastryDto.name);
                Console.WriteLine(pastry);
                if (pastry == null)
                {
                    return NotFound("Given pastry doesn't exist");
                }

                var orderPastry = new OrderPastry()
                {
                    OrderID = idOrder, // Ustaw OrderID na ID nowo dodanego zamówienia
                    PastryID = pastry.ID,
                    Amount = pastryDto.amount,
                    Comme = pastryDto.comments,
                };

                pastries.Add(orderPastry);
                
            }

            // Dodaj wszystkie OrderPastry na raz
            await _pastryRepository.AddOrderPastries(pastries);

            scope.Complete();
        }

        return Ok(order);
    }
    
}
    