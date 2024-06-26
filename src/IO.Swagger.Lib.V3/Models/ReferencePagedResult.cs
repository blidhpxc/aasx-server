﻿using IO.Swagger.Models;
using System.Collections.Generic;

namespace IO.Swagger.Lib.V3.Models
{
    public class ReferencePagedResult : PagedResult
    {
        public new List<IReference>? result { get; set; }

        public new PagedResultPagingMetadata? paging_metadata { get; set; }
    }
}
