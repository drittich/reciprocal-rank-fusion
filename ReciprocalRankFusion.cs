using System;
using System.Collections.Generic;
using System.Linq;

public class ReciprocalRankFusion
{
    public static Dictionary<string, double> ReciprocalRankFusionAlgorithm(Dictionary<string, Dictionary<string, double>> searchResultsDict, int k = 60)
    {
        Dictionary<string, double> fusedScores = new Dictionary<string, double>();
        Console.WriteLine("Initial individual search result ranks:");

        foreach (var query in searchResultsDict.Keys)
        {
            var docScores = searchResultsDict[query];
            Console.WriteLine($"For query '{query}': {string.Join(", ", docScores.Select(x => $"({x.Key}: {x.Value})"))}");
        }

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

        var rerankedResults = fusedScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        Console.WriteLine("Final reranked results: " + string.Join(", ", rerankedResults.Select(x => $"({x.Key}: {x.Value})")));
        return rerankedResults;
    }
}
