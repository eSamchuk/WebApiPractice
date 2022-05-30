using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NoMansSkyRecipies.Helpers
{
    public static class ModelErrorsConverter
    {
        public static Dictionary<string, IEnumerable<string>> GetFormattedDictionary(
            this ModelStateDictionary dictionary)
        {
            return dictionary.ToDictionary(x => x.Key, y => y.Value.Errors.Select(z => z.ErrorMessage));
        }
    }
}
