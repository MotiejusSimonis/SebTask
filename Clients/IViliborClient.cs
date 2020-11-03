using SEBtask.Enums;
using SEBtask.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEBtask.Clients
{
    public interface IViliborClient
    {
        public Task<ViliborDto> GetViliborRate(BaseRateCode baseRateCode);
    }
}
