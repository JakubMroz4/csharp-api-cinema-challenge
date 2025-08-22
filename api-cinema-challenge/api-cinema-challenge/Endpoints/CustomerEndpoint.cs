using api_cinema_challenge.DTOs.Customer;
using api_cinema_challenge.Factories;
using api_cinema_challenge.Repository.Interfaces;
using api_cinema_challenge.Utils;
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
            CustomerPostDto inDto = await Utility.ValidateFromRequest<CustomerPostDto>(request);
            if (inDto is null)
            {
                return TypedResults.BadRequest();
            }

            var added = await repository.CreateCustomer(CustomerFactory.CustomerFromPostDto(inDto));
            if (added is null) 
            { 
                return TypedResults.Conflict(); 
            }

            var outDto = CustomerFactory.DtoFromCustomer(added);
            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> UpdateCustomer(ICustomerRepository repository, HttpRequest request, int id)
        {
            CustomerPutDto inDto = await Utility.ValidateFromRequest<CustomerPutDto>(request);
            if (inDto is null)
            {
                return TypedResults.BadRequest();
            }

            var entity = await repository.GetByIdAsync(id);
            if (entity is null)
            {
                return TypedResults.NotFound();
            }

            var updated = await repository.UpdateCustomer(CustomerFactory.CustomerFromPutDto(inDto, entity));
            if (updated is null)
            {
                return TypedResults.Conflict();
            }


            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> DeleteCustomer(ICustomerRepository repository, HttpRequest request)
        {

            return TypedResults.Created();
        }
    }
}
