using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEBtask.Models.Entities;
using SEBtask.Models.Requests;
using SEBtask.Repositories;
using SEBtask.Services;
using System;

namespace SEBtask.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<LoginController> _logger;
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ICacheService _cache;
        private readonly IResponseBuildService _responseService;

        public LoginController(
            ITokenService tokenService, 
            ILogger<LoginController> logger,
            IRepositoryWrapper repositoryWrapper,
            ICacheService cache,
            IResponseBuildService responseService
            )
        {
            _tokenService = tokenService;
            _logger = logger;
            _repoWrapper = repositoryWrapper;
            _cache = cache;
            _responseService = responseService;
        }

        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest == null)
                {
                    _logger.LogError("LoginRequest object sent from Client is null");
                    return BadRequest("LoginRequest is null");
                }

                Client client = null;

                if (_tokenService.HasClaims(User))
                {
                    var clientPersonalId = _tokenService.GetClientPersonalId(User);
                    var clientCacheDto = _cache.GetClient(clientPersonalId);
                    if (clientCacheDto.IsSuccess)
                    {
                        client = clientCacheDto.Property;
                    }
                }

                if (client == null)
                {
                    client = _repoWrapper.Client.GetClient(loginRequest);
                    if (client == null)
                    {
                        return Unauthorized();
                    }

                    _cache.SetClient(client);
                }

                var token = _tokenService.GenerateToken(client);
                var loginResponse = _responseService.GetLoginRespose(client, token);

                return Ok(loginResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong in Login action");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
