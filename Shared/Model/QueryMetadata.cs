using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace omnia_blazor_demo.Shared.Model
{
    public class QueryMetadata
    {
        public QueryMetadata() {}

        public QueryMetadata(IConfiguration configuration, string query)
        {
            Tenant = configuration["TenantCode"];
            Environment = "prd";
            Query = query;
            DataSource = "Default";
            Page = 1;
            PageSize = 25;
        }        
        public string Tenant { get; set; }

        public string Environment { get; set; } = "PRD";

        public string Query { get; set; }

        public string DataSource { get; set; } = "Default";

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 25;

        public List<QueryParameter> parameters = new List<QueryParameter>();
    }
}