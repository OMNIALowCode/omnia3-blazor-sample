using Microsoft.Extensions.Configuration;

namespace omnia_blazor_demo.Shared.Model
{
    public class EntityMetadata
    {
        public EntityMetadata() { }
        public EntityMetadata(IConfiguration configuration, string entity) { 
            Tenant = configuration["TenantCode"];
            Environment = "prd";
            Entity = entity;
            DataSource = "Default";
        }
        public string Tenant { get; set; }

        public string Environment { get; set; } = "PRD";

        public string Entity { get; set; }

        public string DataSource { get; set; } = "Default";

    }
}