using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEBtask.Clients;
using SEBtask.Enums;
using SEBtask.Models.Dtos;
using SEBtask.Repositories;
using SEBtask.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SEBtask.Controllers
{
    [ApiController]
    [Route("api/v1/agreements")]
    public class AgreementsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ITokenService _tokenService;
        private readonly IViliborClient _viliborClient;
        private readonly ILogger<AgreementsController> _logger;
        private readonly ICacheService _cache;
        private readonly IResponseBuildService _responseService;
        private readonly IInterestRateService _interestService;

        public AgreementsController(
            IRepositoryWrapper repoWrapper,
            ITokenService tokenService,
            IViliborClient viliborClient,
            ILogger<AgreementsController> logger,
            ICacheService cache,
            IResponseBuildService responseService,
            IInterestRateService interestService
            )
        {
            _repoWrapper = repoWrapper;
            _tokenService = tokenService;
            _viliborClient = viliborClient;
            _logger = logger;
            _cache = cache;
            _responseService = responseService;
            _interestService = interestService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAllAgreements()
        {
            try
            {
                var clientPersonalId = _tokenService.GetClientPersonalId(User);
                var agreementsCacheDto = _cache.GetAgreements(clientPersonalId);
                var agreements = agreementsCacheDto.Property?.Agreements;

                if (!agreementsCacheDto.IsSuccess)
                {
                    agreements = _repoWrapper.Agreement.GetAgreements(clientPersonalId);

                    if (!agreements.Any())
                    {
                        NotFound();
                    }

                    _cache.SetAgreements(agreements, clientPersonalId);
                }

                var client = _cache.GetClient(clientPersonalId);
                var agreementsResponse = _responseService.GetAgreementsRespose(client.Property, agreements);

                return Ok(agreementsResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong in GetAllAgreements action");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult GetAgreementById(long id)
        {
            try
            {
                var clientPersonalId = _tokenService.GetClientPersonalId(User);
                var agreementCacheDto = _cache.GetAgreement(clientPersonalId, id);
                var agreement = agreementCacheDto.Property;

                if (!agreementCacheDto.IsSuccess)
                {
                    agreement = _repoWrapper.Agreement.GetAgreement(clientPersonalId, id);
                    if (agreement == null)
                    {
                        NotFound();
                    }

                    _cache.SetAgreement(agreement, clientPersonalId);
                }

                var client = _cache.GetClient(clientPersonalId);
                var agreementsResponse = _responseService.GetAgreementsRespose(client.Property, agreement);

                return Ok(agreementsResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong in GetAgreementById action");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpGet("{id}/interests")]
        public async Task<ActionResult> GetInterests(long id, BaseRateCode? baseRateCode)
        {
            try
            {
                var clientPersonalId = _tokenService.GetClientPersonalId(User);
                var client = _cache.GetClient(clientPersonalId).Property;
                var agreementCacheDto = _cache.GetAgreement(clientPersonalId, id);
                var agreement = agreementCacheDto.Property;

                if (!agreementCacheDto.IsSuccess)
                {
                    agreement = _repoWrapper.Agreement.GetAgreement(clientPersonalId, id);
                    if (agreement == null)
                    {
                        NotFound();
                    }

                    _cache.SetAgreement(agreement, clientPersonalId);
                }

                var currentBaseRateCode = ParseBaseRateCode(agreement.BaseRateCode);
                var viliborDto = await GetBaseRate(currentBaseRateCode);
                var newViliborDto = baseRateCode == null ? null : await GetBaseRate(baseRateCode ?? default);
                var interestDto = _interestService.GetInterestRates(agreement, viliborDto.BaseRateValue, newViliborDto.BaseRateValue);
                var interestsResponse = _responseService.GetInterestsRespose(client, agreement, interestDto);

                return Ok(interestsResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong in Login action");
                return StatusCode(500, "Internal Server Error");
            }
        }

        private BaseRateCode ParseBaseRateCode(string baseRateCodeString)
        {
            return (BaseRateCode)Enum.Parse(typeof(BaseRateCode), baseRateCodeString, true);
        }

        private async Task<ViliborDto> GetBaseRate(BaseRateCode baseRateCode)
        {
            var isSuccess = true;
            var cacheDto = _cache.GetBaseRateValue(baseRateCode);
            decimal? baseRateValue = cacheDto.Property;
            if (!cacheDto.IsSuccess)
            {
                var viliborDto = await _viliborClient.GetViliborRate(baseRateCode);
                if (!viliborDto.IsSuccess)
                {
                    isSuccess = false;
                }

                baseRateValue = viliborDto.BaseRateValue;

                _cache.SetBaseRateValue(baseRateCode, baseRateValue ?? 0);
            }

            return new ViliborDto
            {
                IsSuccess = isSuccess,
                BaseRateValue = baseRateValue,
            };
        }
    }
}
