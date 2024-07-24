using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Provides methods for performing Reciprocal Rank Fusion on search results.
/// </summary>
public class ReciprocalRankFusion
{
    /// <summary>
    /// Performs Reciprocal Rank Fusion (RRF) on the given search results.
    /// </summary>
    /// <param name="searchResultsDict">A dictionary where the key is a query string and the value is another dictionary mapping document IDs to their scores for that query.</param>
    /// <param name="k">A constant used in the RRF formula to dampen the impact of ranks. Defaults to 60.</param>
    /// <returns>A dictionary mapping document IDs to their fused scores, sorted in descending order of scores.</returns>
    public static Dictionary<string, double> ReciprocalRankFusionAlgorithm(Dictionary<string, Dictionary<string, double>> searchResultsDict, int k = 60)
    {
        Dictionary<string, double> fusedScores = new Dictionary<string, double>();
        Console.WriteLine("Initial individual search result ranks:");

        // Print initial individual search result ranks for debugging purposes
        foreach (var query in searchResultsDict.Keys)
        {
            var docScores = searchResultsDict[query];
            Console.WriteLine($"For query '{query}': {string.Join(", ", docScores.Select(x => $"({x.Key}: {x.Value})"))}");
        }

        // Perform Reciprocal Rank Fusion
        foreach (var query in searchResultsDict.Keys)
        {
            var docScores = searchResultsDict[query];
            var sortedDocScores = docScores.OrderByDescending(x => x.Value).ToList();
            for (int rank = 0; rank < sortedDocScores.Count; rank++)
            {
                var doc = sortedDocScores[rank].Key;
                if (!fusedScores.ContainsKey(doc))
                {
                    fusedScores[doc] = 0;
                }
                double previousScore = fusedScores[doc];
                fusedScores[doc] += 1.0 / (rank + k);
                Console.WriteLine($"Updating score for {doc} from {previousScore} to {fusedScores[doc]} based on rank {rank} in query '{query}'");
            }
        }

        // Sort the fused scores in descending order and return the result
        var rerankedResults = fusedScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        Console.WriteLine("Final reranked results: " + string.Join(", ", rerankedResults.Select(x => $"({x.Key}: {x.Value})")));
        return rerankedResults;
    }
}
