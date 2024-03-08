using Application.Snippets.Queries;
using Application.Snippets.Queries.GetSnippets;

namespace Application.Tests.Snippets.Queries
{
    public class GetSnippetExtensionsTests
    {
        [TestCaseSource(nameof(TestData))]
        public void GetSnippetsQueryWithParsedParams_ValidInput_ReturnsExpectedResult(string input, GetSnippetsQuery excepted)
        {
            // Act
            var result = GetSnippetsExtensions.GetSnippetsQueryWithParsedParams(input);

            // Assert 
            Assert.Multiple(() =>
            {
                Assert.That(result.SearchTerm, Is.EqualTo(excepted.SearchTerm));
                Assert.That(result.Language, Is.EqualTo(excepted.Language));
                Assert.That(result.Tags, Is.EqualTo(excepted.Tags));
            });
        }

        private static readonly object[] TestData =
        {
            new TestCaseData(null,
                new GetSnippetsQuery
                {
                    SearchTerm = "",
                    Language = "",
                    Tags = [],
                }),

             new TestCaseData("",
                new GetSnippetsQuery
                {
                    SearchTerm = "",
                    Language = "",
                    Tags = [],
                }),

            new TestCaseData("|",
                new GetSnippetsQuery
                {
                    SearchTerm = ""
                }),

            new TestCaseData("searchTerm",
                new GetSnippetsQuery
                {
                    SearchTerm = "searchTerm"
                }),

           new TestCaseData("searchTerm|",
                new GetSnippetsQuery
                {
                    SearchTerm = "searchTerm"
                }),

            new TestCaseData("searchTerm|tags:1,2 lang:34,56,cd",
                new GetSnippetsQuery
                {
                    SearchTerm = "searchTerm",
                    Language = "34,56,cd",
                    Tags = ["1", "2"],
                })
        };
    }
}
