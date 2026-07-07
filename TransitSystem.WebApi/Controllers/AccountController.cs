using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransitSystem.Core.DTOs;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) => _accountService = accountService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            var account = await _accountService.RegisterAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = account.AccountId }, account);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var account = await _accountService.GetAccountDetailsAsync(id);
            return Ok(account);
        }
    }
}