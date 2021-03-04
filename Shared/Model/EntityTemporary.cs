using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace omnia_blazor_demo.Shared.Model
{
    public class EntityTemporary<T>
    {
        public string id { get; set; }
        
        public T data { get; set; }
    }
}