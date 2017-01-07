module NameGenTests

open System.Collections.Generic

open TestFramework

// modules under test
open NameGenerator

// tests included in run
let nameGenTests = 
    [
        "countOcccurrence will add a new entry for a new string occurrence and init to 1",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdefg" map 1
            assertAreEqual 1.0 result.["a"];

        "countOcccurrence will return 0 for a string that is not in the input",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdefg" map 1
            assertIsFalse (result.ContainsKey "z");    

        "countOcccurrence will return 2 occurrences if a string occures twice",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "aabcdefg" map 1
            assertAreEqual 2.0 result.["a"];

        "countOcccurrence will add a new entry for a new 2 character string occurrence and init to 1",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdefg" map 2
            assertAreEqual 1.0 result.["ab"];

        "countOcccurrence will return 2 occurrences if a 2 character string occures twice",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdeabfg" map 2
            assertAreEqual 2.0 result.["ab"];

        "countOcccurrence will return 0 for a string with 2 characters that is not in the input",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdefg" map 2
            assertIsFalse (result.ContainsKey "ba");    

        "countOcccurrence will add a new entry for a new 3 character string occurrence and init to 1",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdefg" map 3
            assertAreEqual 1.0 result.["abc"];

        "countOcccurrence will return 2 occurrences if a 3 character string occures twice",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdeabcfg" map 3
            assertAreEqual 2.0 result.["abc"];

        "countOcccurrence will return 0 for a string with 3 characters that is not in the input",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdefg" map 3
            assertIsFalse (result.ContainsKey "bca");

        "countOcccurrence will add a new entry for a new 4 character string occurrence and init to 1",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdefg" map 4
            assertAreEqual 1.0 result.["abcd"];

        "countOcccurrence will return 2 occurrences if a 4 character string occures twice",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdeabcdfg" map 4
            assertAreEqual 2.0 result.["abcd"];

        "countOcccurrence will return 0 for a string with 4 characters that is not in the input",
        fun() ->
            let map = Map.empty
            let result = countOccurrences "abcdefg" map 4
            assertIsFalse (result.ContainsKey "bcaa");

        "buildFrenquencyTable will correctly compute values with 1 letter substrings",
        fun() ->
            let map = Map.empty
            let probabilities = countOccurrences " James John Max Gary Jess Gilles Mary " map 1 
                                |> buildFrenquencyTable
            assertAreEqual 0.210526 probabilities.[" "]            
            
        "buildFrenquencyTable will correctly compute values with 1 letter substrings (2)",
        fun() ->
            let map = Map.empty
            let probabilities = countOccurrences " James John Max Gary Jess Gilles Mary " map 1 
                                |> buildFrenquencyTable
            assertAreEqual 0.026316 probabilities.["m"]

        "buildFrenquencyTable will correctly compute values with 1 letter substrings (3)",
        fun() ->
            let map = Map.empty
            let probabilities = countOccurrences " James John Max Gary Jess Gilles Mary " map 1 
                                |> buildFrenquencyTable
            assertAreEqual 0.078947 probabilities.["J"]

        "buildFrenquencyTable will correctly compute values with 2 letter substrings",
        fun() ->
            let map = Map.empty
            let probabilities = countOccurrences " James John Max Gary Jess Gilles Mary " map 2
                                |> buildFrenquencyTable
            assertAreEqual 0.054054 probabilities.["ar"]       

        // "buildCumulativeFrenquencyTable will correctly compute cumulative properties for substrings",
        // fun() ->
        //     let map = Map.empty
        //     let probabilities = countOccurrences " James John Max Gary Jess Gilles Mary " map 2
        //                         |> buildFrenquencyTable
        //                         |> buildCumulativeFrenquencyTable
        //     assertAreEqual 0.428571 probabilities

    ]        
