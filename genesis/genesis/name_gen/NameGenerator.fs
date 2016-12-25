module NameGenerator

open System
open System.Collections.Generic

// Open a file and read all lines into IEnumerable<string>
let readInputFile (filePath:string) = 
    System.IO.File.ReadAllText(filePath)

// Parses a string and count the total number of occurences of substrings of size length
let rec countOccurences (input:string) (occurenceTable:Dictionary<string, int>) length = 
    let adjLen = length - 1
    
    match input |> Seq.toList with
    | head :: tail when tail.Length >= adjLen -> 
        let other = Seq.take adjLen tail |> Seq.toList
        let occurence = (head :: other |> Array.ofList |> String)

        // add current occurence to the occurence table
        match occurenceTable.ContainsKey (occurence) with
        | true -> occurenceTable.[occurence] <- occurenceTable.[occurence] + 1
        | false -> occurenceTable.Add(occurence, 1)

        // call the function recursively with the rest of the string
        countOccurences (tail |> Array.ofList |> String) occurenceTable length
    | _ -> occurenceTable

// Given an input file create a probability table for the different letters in the file
let buildProbabilityTables (filePath:string) length =
    let input = readInputFile filePath
    let initialDictionary = new Dictionary<string, int>()

    countOccurences input initialDictionary length