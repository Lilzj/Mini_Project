using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Core.Services.Interfaces;
using Onboarding.DTOs.Request;

namespace Onboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _Service;

        public CustomerController(ICustomerService Service)
        {
            _Service = Service;
        }

        /// <summary>
        /// This Endpoint Onboards a new customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterCustomer([FromQuery] CustomerRequestDto model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var response = await _Service.AddCustomer(model);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// This Endpoint confirms a customer after registration. (default OTP = 546723)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpPatch("{customerId}")]
        public async Task<IActionResult> ConfirmCustomer([FromBody] OTPRequestDto model, string customerId)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var reaponse = await _Service.ConfirmOTP(model.OTP, customerId);

            return StatusCode(Response.StatusCode, reaponse);
        }

        /// <summary>
        /// This endpoint returns all registered customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = await _Service.GetCustomersAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
