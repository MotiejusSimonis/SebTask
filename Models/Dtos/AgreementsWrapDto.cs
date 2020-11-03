using SEBtask.Models.Entities;
using System.Collections.Generic;

namespace SEBtask.Models.Dtos
{
    public class AgreementsWrapDto
    {
        public IEnumerable<Agreement> Agreements { get; set; }
    }
}
