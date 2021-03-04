using System.Collections.Generic;

namespace omnia_blazor_demo.Shared.Model
{
    public class OmniaQueryRequestBody
    {
        public List<QueryParameter> parameters {get;set;} = new List<QueryParameter>();
    }
}