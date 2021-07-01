using System.Collections.Generic;

namespace MessageService.API.Data.Results
{
    public class ModelStateValidationResult
    {
        public ModelStateValidationResult()
        {
            Errors = new Dictionary<string, string>();
        }
        public bool Success { get; set; }
        public Dictionary<string,string> Errors { get; set; }
    }
}