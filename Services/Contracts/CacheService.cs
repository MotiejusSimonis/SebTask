using Microsoft.Extensions.Caching.Memory;
using SEBtask.Enums;
using SEBtask.Models.Dtos;
using SEBtask.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEBtask.Services
{
    public class CacheService : ICacheService
    {
        private readonly string _clientCacheKeyPrefix;
        private readonly string _agreementsCacheKeyPrefix;
        private readonly string _baseRateValueCacheKeyPrefix;
        private readonly MemoryCacheEntryOptions _defoultCacheOptions;
        private readonly MemoryCacheEntryOptions _baseRateCacheOptions;
        private IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
            _defoultCacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(20)
            };
            _baseRateCacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(12),
                Priority = CacheItemPriority.High,
            };
            _clientCacheKeyPrefix = "[Client]ClientPersonalId:";
            _agreementsCacheKeyPrefix = "[IEnumerable<Agreement>]ClientPersonalId:";
            _baseRateValueCacheKeyPrefix = "[decimal]BaseRateCode:";
        }

        public CacheDto<Client> GetClient(long clientPersonalId)
        {
            var cacheKey = GetClientCacheKey(clientPersonalId);
            var isSuccess = _cache.TryGetValue(cacheKey, out Client value);
            return new CacheDto<Client>
            {
                IsSuccess = isSuccess,
                Property = value
            };
        }

        public void SetClient(Client client)
        {
            var cacheKey = GetClientCacheKey(client.PersonalId);
            _cache.Set(cacheKey, client, _defoultCacheOptions);
        }
        
        public CacheDto<AgreementsWrapDto> GetAgreements(long clientPersonalId)
        {
            var cacheKey = GetAgreementsCacheKey(clientPersonalId);
            var isSuccess = _cache.TryGetValue(cacheKey, out AgreementsWrapDto values);
            return new CacheDto<AgreementsWrapDto>
            {
                IsSuccess = isSuccess,
                Property = values,
            };
        }
        
        public void SetAgreements(AgreementsWrapDto agreementsWrapDto, long clientPersonalId)
        {
            var cacheKey = GetAgreementsCacheKey(clientPersonalId);
            _cache.Set(cacheKey, agreementsWrapDto, _defoultCacheOptions);
        }

        public void SetAgreements(IEnumerable<Agreement> agreements, long clientPersonalId)
        {
            var cacheKey = GetAgreementsCacheKey(clientPersonalId);
            var agreementsWrapDto = new AgreementsWrapDto
            {
                Agreements = agreements,
            };
            _cache.Set(cacheKey, agreementsWrapDto, _defoultCacheOptions);
        }

        public CacheDto<Agreement> GetAgreement(long clientPersonalId, long agreementId)
        {
            var cacheKey = GetAgreementsCacheKey(clientPersonalId);
            var isSuccess = _cache.TryGetValue(cacheKey, out AgreementsWrapDto agreementsWrapDto);
            return new CacheDto<Agreement>
            {
                IsSuccess = isSuccess,
                Property = agreementsWrapDto?.Agreements?.FirstOrDefault(x => x.Id == agreementId),
            };
        }

        public void SetAgreement(Agreement agreement, long clientPersonalId)
        {
            var cacheKey = GetAgreementsCacheKey(clientPersonalId);
            if (!_cache.TryGetValue(cacheKey, out AgreementsWrapDto agreementsWrapDto))
            {
                agreementsWrapDto = new AgreementsWrapDto
                {
                    Agreements = new List<Agreement> { agreement },
                };
            }
            else if (agreementsWrapDto.Agreements.Any(x => x.Id == agreement.Id))
            {
                agreementsWrapDto.Agreements = agreementsWrapDto.Agreements.Where(x => x.Id != agreement.Id)
                    .Concat(new[] { agreement });
            }
            else
            {
                agreementsWrapDto.Agreements = agreementsWrapDto.Agreements.Concat(new[] { agreement });
            }

            _cache.Set(cacheKey, agreementsWrapDto, _defoultCacheOptions);
        }

        public CacheDto<decimal?> GetBaseRateValue(BaseRateCode baseRateCode)
        {
            var cacheKey = GetBaseRateCacheKey(baseRateCode);
            bool isSuccess = _cache.TryGetValue(cacheKey, out decimal value);
            return new CacheDto<decimal?>
            {
                IsSuccess = isSuccess,
                Property = value
            };
        }

        public void SetBaseRateValue(BaseRateCode baseRateCode, decimal baseRateValue)
        {
            var cacheKey = GetBaseRateCacheKey(baseRateCode);
            _cache.Set(cacheKey, baseRateValue, _baseRateCacheOptions);
        }

        private string GetBaseRateCacheKey(BaseRateCode baseRateCode)
        {
            return _baseRateValueCacheKeyPrefix + baseRateCode.ToString();
        }
        
        private string GetAgreementsCacheKey(long clientPersonalId)
        {
            return _agreementsCacheKeyPrefix + clientPersonalId.ToString();
        }

        private string GetClientCacheKey(long clientPersonalId)
        {
            return _clientCacheKeyPrefix + clientPersonalId.ToString();
        }
    }
}
