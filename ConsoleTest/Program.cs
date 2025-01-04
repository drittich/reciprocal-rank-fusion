using drittich.ReciprocalRankFusion;

var searchResultsDict = new Dictionary<string, Dictionary<string, double>>
{
	{ "query1", new Dictionary<string, double> {
		{ "doc1", 3.0 },
		{ "doc2", 1.5 },
		{ "doc3", 2.0 }
	}},
	{ "query2", new Dictionary<string, double> {
		{ "doc4", 0.25 },
		{ "doc5", 0.35 },
		{ "doc6", 0.10 }
	}}
};

var fuser = new SearchResultFuser();
var fusedResults = fuser.FuseSearchResults(searchResultsDict);
var top100Results = fusedResults
	.Take(100)
	.ToDictionary(x => x.Key, x => x.Value);
foreach (var result in fusedResults)
{
	Console.WriteLine($"Document: {result.Key}, Score: {result.Value}");
}