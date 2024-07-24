# Reciprocal Rank Fusion Algorithm

This project provides an implementation of the Reciprocal Rank Fusion (RRF) algorithm in C#. The RRF algorithm is used to combine search results from multiple sources, taking into account the ranks of documents to produce a fused ranking.

## Table of Contents

- [Introduction](#introduction)
- [Usage](#usage)
- [Example](#example)
- [Documentation](#documentation)
- [License](#license)

## Introduction

Reciprocal Rank Fusion (RRF) is a simple yet effective method for merging ranked lists of documents. This algorithm assigns scores to documents based on their ranks across multiple queries, and then combines these scores to produce a final ranking.

## Usage

To use the RRF algorithm, you need to call the `ReciprocalRankFusionAlgorithm` method from the `ReciprocalRankFusion` class. This method takes a dictionary of search results and an optional parameter `k` to control the impact of ranks.

### Method Signature

```csharp
public static Dictionary<string, double> ReciprocalRankFusionAlgorithm(
    Dictionary<string, Dictionary<string, double>> searchResultsDict, 
    int k = 60)
```

### Parameters

- **`searchResultsDict`**: A dictionary where the key is a query string and the value is another dictionary mapping document IDs to their scores for that query.
- **`k`**: A constant used in the RRF formula to dampen the impact of ranks. Defaults to 60.

### Returns
A dictionary mapping document IDs to their fused scores, sorted in descending order of scores.

## Example
Here is an example of how to use the ReciprocalRankFusionAlgorithm method:

```csharp
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var searchResultsDict = new Dictionary<string, Dictionary<string, double>>
        {
            { "query1", new Dictionary<string, double> { { "doc1", 3.0 }, { "doc2", 1.5 }, { "doc3", 2.0 } } },
            { "query2", new Dictionary<string, double> { { "doc1", 2.5 }, { "doc2", 3.5 }, { "doc3", 1.0 } } }
        };

        var fusedResults = ReciprocalRankFusion.ReciprocalRankFusionAlgorithm(searchResultsDict);
        foreach (var result in fusedResults)
        {
            Console.WriteLine($"Document: {result.Key}, Score: {result.Value}");
        }
    }
}
```

## Documentation
### Class: ReciprocalRankFusion
Provides methods for performing Reciprocal Rank Fusion on search results.

**Method: ReciprocalRankFusionAlgorithm**

- **Signature**: `public static Dictionary<string, double> ReciprocalRankFusionAlgorithm(Dictionary<string, Dictionary<string, double>> searchResultsDict, int k = 60)`
- **Parameters**:
    - `searchResultsDict: A dictionary where the key is a query string and the value is another dictionary mapping document IDs to their scores for that query.
    - `k`: A constant used in the RRF formula to dampen the impact of ranks. Defaults to 60.
- **Returns**: A dictionary mapping document IDs to their fused scores, sorted in descending order of scores.

## License
This project is licensed under the MIT License. See the LICENSE file for details. 
