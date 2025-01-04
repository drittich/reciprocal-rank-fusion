# drittich.ReciprocalRankFusion

This package provides an implementation of the Reciprocal Rank Fusion (RRF) algorithm in C#. The RRF algorithm is used to combine search results from multiple sources, taking into account the ranks of documents to produce a fused ranking.

## Table of Contents

- [Introduction](#introduction)
- [Usage](#usage)
  - [Installation](#installation)
  - [Basic Usage](#basic-usage)
- [Complete Example](#complete-example)
- [Choosing the k Parameter](#choosing-the-k-parameter)
  - [For 1 to 10 Search Results](#for-1-to-10-search-results)
  - [For Up to 100 Search Results with Highly Relevant Top 10](#for-up-to-100-search-results-with-highly-relevant-top-10)
- [License](#license)
- [Acknowledgments](#acknowledgments)

## Introduction

Reciprocal Rank Fusion (RRF) is a simple yet effective method for merging ranked lists of documents. This algorithm assigns scores to documents based on their ranks across multiple queries, and then combines these scores to produce a final ranking.

## Usage

### Installation

You can install the `drittich.ReciprocalRankFusion` NuGet package using the NuGet Package Manager or the .NET CLI.

#### Using the NuGet Package Manager

1. Open your project in Visual Studio.
2. Right-click on your project in the Solution Explorer and select **Manage NuGet Packages**.
3. In the **Browse** tab, search for `drittich.ReciprocalRankFusion`.
4. Select the package from the search results and click **Install**.

#### Using the .NET CLI

Run the following command in your terminal:

```bash
dotnet add package drittich.ReciprocalRankFusion
```

### Basic Usage

After installing the package, you can use the `ReciprocalRankFusion` class to fuse your search results.

```csharp
        var searchResultsDict = ...

        var fusedResults = ReciprocalRankFusion.FuseSearchResults(searchResultsDict);
        foreach (var result in fusedResults)
        {
            Console.WriteLine($"Document: {result.Key}, Score: {result.Value}");
        }
    }
}
```

## Complete Example

Here is an example of how to use the `FuseSearchResults` method:

```csharp
using drittich.ReciprocalRankFusion;

var searchResultsDict = new Dictionary<string, Dictionary<string, double>>
{
    { "query1", new Dictionary<string, double> { 
        { "doc1", 3.0 }, 
        { "doc2", 1.5 }, 
        { "doc3", 2.0 } 
    }},
    { "query2", new Dictionary<string, double> { 
        { "doc4", 2.5 }, 
        { "doc5", 3.5 }, 
        { "doc6", 1.0 } 
    }}
};

var fusedResults = ReciprocalRankFusion.FuseSearchResults(searchResultsDict);
var top100Results = fusedResults
    .Take(100)
    .ToDictionary(x => x.Key, x => x.Value);
foreach (var result in fusedResults)
{
    Console.WriteLine($"Document: {result.Key}, Score: {result.Value}");
}

/*
Output:

Document: doc1, Score: 0.016666666666666666
Document: doc5, Score: 0.016666666666666666
Document: doc3, Score: 0.01639344262295082
Document: doc4, Score: 0.01639344262295082
Document: doc2, Score: 0.016129032258064516
Document: doc6, Score: 0.016129032258064516
*/
```

## Choosing the k Parameter

The **k** parameter in the Reciprocal Rank Fusion (RRF) algorithm plays a pivotal role in balancing the influence of document ranks on the final fused score. Selecting an appropriate value for **k** depends on the number of search results you aim to retrieve and the desired relevance distribution among them.

### For 1 to 10 Search Results

When your goal is to retrieve a **small set of highly relevant results (1 to 10)**, the choice of **k** should emphasize the top ranks more strongly to ensure that the most relevant documents are prioritized.

**Recommended k Values:**

- **k = 5 to 15**

  - **k = 5:** Provides strong emphasis on the top ranks, making the algorithm highly sensitive to the highest-ranked documents.
  - **k = 10:** Offers a balanced approach, still prioritizing top ranks while allowing some flexibility.
  - **k = 15:** Slightly less aggressive in emphasizing top ranks but still suitable for small result sets.

**Rationale:**

- **Lower k values** increase the weight difference between higher and lower ranks, ensuring that the top 1-10 results are highly relevant.
- **Higher k values** within this range provide a balance, preventing the top results from being too narrowly focused.

**Example Usage:**

```csharp
var fusedResults = ReciprocalRankFusion.FuseSearchResults(searchResultsDict, k: 10);

// Extract the top 10 results
var top10Results = fusedResults.Take(10).ToDictionary(x => x.Key, x => x.Value);
```

### For Up to 100 Search Results with Highly Relevant Top 10

When you aim to retrieve a **larger set of results (up to 100)** while ensuring that the **first 10 are highly relevant**, a different range of **k** values is appropriate to balance overall coverage with top-tier relevance.

**Recommended k Values:**

- **k = 30 to 50**

  - **k = 30:** Strong emphasis on top ranks while allowing meaningful contributions from ranks beyond 10.
  - **k = 40:** Provides a balanced approach, maintaining high relevance in the top 10 and comprehensive coverage up to 100 results.
  - **k = 50:** Slightly less emphasis on the very top ranks but ensures a diverse and comprehensive set of results.

**Rationale:**

- **Moderate k values** ensure that while the top 10 results are prioritized for high relevance, documents ranked up to 100 still contribute significantly to the final ranking.
- This range prevents the dilution of the top results' relevance while maintaining the algorithm's ability to consider a broader set of documents.

**Example Usage:**

```csharp
var fusedResults = ReciprocalRankFusion.FuseSearchResults(searchResultsDict, k: 40);

// Extract the top 100 results
var top100Results = fusedResults.Take(100).ToDictionary(x => x.Key, x => x.Value);
```

### General Guidelines for Selecting k

- **Start with Recommended Values:** Use the suggested ranges as a baseline.
- **Empirical Testing:** Evaluate different **k** values using your specific datasets and relevance metrics to find the optimal balance.
- **Adjust Based on Results:** Depending on the performance, you might fine-tune **k** upwards or downwards within the recommended ranges.
- **Consider Application Needs:** Align the choice of **k** with the specific requirements of your application, such as the importance of top results versus overall coverage.

## License

This project is licensed under the MIT License. See the LICENSE file for details. 

## Acknowledgments

This project contains code ported from the python source code in [rag-fusion](https://github.com/Raudaschl/rag-fusion).
