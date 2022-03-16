using AutoMapper;
using EnsekMeterReadingsService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EnsekMeterReadingsService.Dto;

namespace EnsekMeterReadingsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly iAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountController(iAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
        public IActionResult getAccounts()
        {
            var accounts = _mapper.Map<List<AccountDto>>(_accountRepository.GetAccounts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(accounts);
            }
        }
    }
}


