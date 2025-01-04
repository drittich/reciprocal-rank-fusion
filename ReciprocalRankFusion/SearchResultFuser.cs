using System.Collections.Generic;
using System.Linq;

namespace drittich.ReciprocalRankFusion
{
	/// <summary>
	/// Provides methods for performing Reciprocal Rank Fusion on search results.
	/// </summary>
	public class SearchResultFuser
	{
		/// <summary>
		/// Performs Reciprocal Rank Fusion (RRF) on the given search results.
		/// </summary>
		/// <param name="searchResultsDict">A dictionary where the key is a query string and the value is another dictionary mapping document IDs to their scores for that query.</param>
		/// <param name="k">A constant used in the RRF formula to dampen the impact of ranks. Defaults to 60.</param>
		/// <returns>A dictionary mapping document IDs to their fused scores, sorted in descending order of scores.</returns>

		public Dictionary<string, double> FuseSearchResults(Dictionary<string, Dictionary<string, double>> searchResultsDict, int k = 60)
		{
			// Initialize the fusedScores dictionary with an estimated capacity to reduce reallocations.
			var fusedScores = new Dictionary<string, double>(capacity: 10000);

			foreach (var queryKvp in searchResultsDict)
			{
				var docScores = queryKvp.Value;

				var sortedDocScores = docScores.OrderByDescending(x => x.Value);

				int rank = 0;
				foreach (var docScore in sortedDocScores)
				{
					var doc = docScore.Key;
					double increment = 1.0 / (rank + k);

					if (fusedScores.TryGetValue(doc, out double existingScore))
					{
						fusedScores[doc] = existingScore + increment;
					}
					else
					{
						fusedScores[doc] = increment;
					}

					rank++;
				}
			}

			// Convert the fusedScores dictionary to a list for efficient sorting.
			var sortedFusedList = fusedScores.ToList();

			// Sort the list in descending order based on the fused scores.
			sortedFusedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

			// Initialize the rerankedResults dictionary with the sorted order.
			// This preserves the sorted order in .NET Core and later versions.
			var rerankedResults = new Dictionary<string, double>(fusedScores.Count);
			foreach (var pair in sortedFusedList)
			{
				rerankedResults.Add(pair.Key, pair.Value);
			}

			return rerankedResults;
		}
	}
}
