namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Attributes;

[ExcludeFromCodeCoverage]
public class FactOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

        foreach (TTestCase testCase in testCases)
        {
            int order = 0;

            // Get priority from the attribute
            var orderAttribute = testCase.TestMethod.Method
                .GetCustomAttributes(typeof(FactOrderAttribute))
                .FirstOrDefault();

            if (orderAttribute != null)
            {
                order = orderAttribute.GetNamedArgument<int>("Order");
            }

            // Add to the dictionary
            if (!sortedMethods.ContainsKey(order))
            {
                sortedMethods.Add(order, new List<TTestCase>());
            }

            sortedMethods[order].Add(testCase);
        }

        // Return test cases in order
        foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
        {
            list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(
                x.TestMethod.Method.Name,
                y.TestMethod.Method.Name));
            foreach (TTestCase testCase in list)
            {
                yield return testCase;
            }
        }
    }
}