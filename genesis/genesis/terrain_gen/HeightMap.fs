module HeightMap

// contains the height map types and common functions that can be re-used for 
// different generation algorithms

type HeightMap = {Size:int; Map:float array} with     
    member this.Get x y = this.Map.[x * this.Size + y]      
        
    member this.Set x y value = this.Map.[x * this.Size + y] <- value

    member this.Add x y value = this.Set x y ((this.Get x y) + value)

    member this.Substract x y value = this.Set x y ((this.Get x y) - value)

    // return if a x, y coordinates fits inside the bounds of the map
    member this.CoordValid x y = 
        match (x, y) with
        | (x, _) when x < 0 -> false
        | (_, y) when y < 0 -> false
        | (x, _) when x >= this.Size -> false
        | (_, y) when y >= this.Size -> false
        | _ -> true

// returns a square matrix of size 2^n + 1 HeightMap object
let newHeightMap n : HeightMap =
    let size = ( pown 2 n ) + 1
    {Size = size; Map = Array.zeroCreate (size * size)}  

// returns an HeightMap object initialized with the given parameters
let newHeightMap' n array : HeightMap =
    {Size = n; Map = array}  

// normalize a single value to constrain it's value between 0.0 and 1.0
let normalizeValue v =
    match v with
    | v when v < 0.0 -> 0.0
    | v when v > 1.0 -> 1.0
    | _ -> v

// converts a floating point ranging from 0.0 to 1.0 to an int value ranging from 0 to 255
let convertFloatToInt (pct:float) : int =
    int (255.0 * pct)

// converts a floating point ranging from 0.0 to 1.0 to a rgb value
// 0.0 represents black and 1.0 white. The conversion is in greyscale 
let convertFloatToRgb (pct:float) : int * int * int =
    let greyscale = convertFloatToInt pct
    (greyscale, greyscale, greyscale)
    
// returns the average between two values    
let inline avg (a:^n) (b:^n) : ^n =
    (a + b) / (LanguagePrimitives.GenericOne + LanguagePrimitives.GenericOne)
    
// returns a floating number which is generated using bounds as a control of the range of possible values
let randomize (rnd:System.Random) (bound:float) : float =   
    (rnd.NextDouble() * 2.0 - 1.0) * bound
