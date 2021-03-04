using System.Collections.Generic;

namespace Spike_ExternalWebApp.Shared.Model
{
    public class ErrorResponse
    {
        public List<Error> errors { get; set; } 
        public string code { get; set; } 
        public string message { get; set; } 
    }
}