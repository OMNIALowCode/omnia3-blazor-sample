using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace omnia_blazor_demo.Shared.Model
{
    public class Temporary
    {
        public string id { get; set; }
        
        public JsonPatchDocument data { get; set; }
    }
}