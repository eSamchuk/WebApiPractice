using System;
using System.Linq;

namespace NoMansSkyRecipies.Extensions
{
    public static class IsStringInStaticClassMembers
    {
        public static bool IsInStaticClass(this string input, Type staticClassType)
        {
            bool result = false;
            var fields = staticClassType.GetFields();
            var properties = staticClassType.GetProperties();

            foreach (var field in fields)
            {
                var value = field.GetValue(null).ToString();
                result = input == value;
                if (result) break;
            }

            if (result)
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(null).ToString();
                    result = input == value;
                    if (result) break;
                }
            }

            return result;
        }
    }
}
