using Application.Snippets.Queries.GetSnippets;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Application.Snippets.Queries;

public static class GetSnippetsExtensions
{
    public static GetSnippetsQuery GetSnippetsQueryWithParsedParams(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new GetSnippetsQuery { SearchTerm = "" };

        var inputs = input.Split('|');

        var parameters = new GetSnippetsQuery
        {
            SearchTerm = inputs[0],
        };

        if (inputs.Length == 1)
            return parameters;

        foreach (PropertyInfo prop in typeof(GetSnippetsQuery).GetProperties())
        {
            var nameAttribute = (DisplayNameAttribute)prop.GetCustomAttribute(typeof(DisplayNameAttribute));

            if (nameAttribute == null)
                continue;

            string pattern = $@"{nameAttribute.DisplayName}:(.*?)(?=\s|$)";
            var value = Regex.Match(inputs[1], pattern).Groups[1].Value;
            if (prop.PropertyType == typeof(string[]))
            {
                // If property is a string array, split the value by commas
                if (!string.IsNullOrWhiteSpace(value))
                    prop.SetValue(parameters, value.Split(',').Select(tag => tag.Trim()).ToArray());
            }
            else
            {
                // Otherwise, set the property value directly
                prop.SetValue(parameters, value);
            }

        }

        return parameters;
    }
}
