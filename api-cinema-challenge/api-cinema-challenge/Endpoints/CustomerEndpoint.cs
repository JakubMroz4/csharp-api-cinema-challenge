using api_cinema_challenge.DTOs.Customer;
using api_cinema_challenge.Factories;
using api_cinema_challenge.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.Endpoints
{
    public static class CustomerEndpoint
    {
        public static void ConfigureCustomerEndpoint(this WebApplication app)
        {
            string groupName = "customer";
            string contentType = "application/json";

            var customerGroup = app.MapGroup(groupName);

            customerGroup.MapGet("/", GetAllCustomers);
            customerGroup.MapPost("/", CreateCustomer).Accepts<CustomerPostDto>(contentType);
            customerGroup.MapPut("/{customerId}", UpdateCustomer).Accepts<CustomerPutDto>(contentType);
            customerGroup.MapDelete("/{customerId}", DeleteCustomer);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetAllCustomers(ICustomerRepository repository)
        {
            var customers = await repository.GetAllAsync();

            List<CustomerDto> dtos = new List<CustomerDto>();
            foreach (var customer in customers)
            {
                dtos.Add(CustomerFactory.DtoFromCustomer(customer));
            }

            return TypedResults.Ok(dtos);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> CreateCustomer(ICustomerRepository repository, HttpRequest request)
        {

            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> UpdateCustomer(ICustomerRepository repository, HttpRequest request)
        {

            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> DeleteCustomer(ICustomerRepository repository, HttpRequest request)
        {

            return TypedResults.Created();
        }
    }
}
