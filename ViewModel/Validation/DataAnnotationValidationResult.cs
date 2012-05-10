using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModeler.Validation
{
    public class DataAnnotationValidationResult : IValidationResult
    {
        Dictionary<string, List<string>> propertyNamesToErrorMessages = new Dictionary<string, List<string>>();  

        public IEnumerable<String> GetErrorsForProperty(string propertyName)
        {
            if (propertyNamesToErrorMessages.ContainsKey(propertyName) && propertyNamesToErrorMessages[propertyName].Any())
            {
                return propertyNamesToErrorMessages[propertyName];
            }
            return new List<String>();
        }

        public bool IsModelValid()
        {
            return !propertyNamesToErrorMessages.Any();
        }

        public int GetErrorCount()
        {
            return propertyNamesToErrorMessages.Sum(kvp => kvp.Value.Count);
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (propertyNamesToErrorMessages.ContainsKey(propertyName))
            {
                propertyNamesToErrorMessages[propertyName].Add(errorMessage);
            }
            else
            {
                propertyNamesToErrorMessages.Add(propertyName, new List<string>() { errorMessage });
            }
        }
    }
}
