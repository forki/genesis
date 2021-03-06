module Blur

open HeightMap

type Filter = { Positions:int list; Kernel: float array }

// Applies a filter to a heightmap at position X Y
let private filter x y (heightMap:HeightMap) (filter:Filter) fposx fposy =
    let filtered = filter.Positions 
                   |> List.fold (fun acc p -> let posx = fposx x p
                                              let posy = fposy y p
                                              if posx >= 0 && posx < heightMap.Size &&
                                                 posy >= 0 && posy < heightMap.Size
                                              then acc + 
                                                   filter.Kernel.[p + 3] * heightMap.Get posx posy 
                                              else acc) 0.0

    heightMap.Set x y filtered

// Take a height map and blur it using the specified filter
let blur (heightMap:HeightMap) (blurFilter:Filter) =
    let size = heightMap.Size

    // do the filter on one dimension
    [2..size - 3] |> List.iter (fun i -> 
        [2.. size - 3] |> List.iter (fun j -> filter i j heightMap blurFilter (( + )) (fun y p -> y)))

    // do the filter on the other dimension
    [2..size - 3] |> List.iter (fun i -> 
        [2.. size - 3] |> List.iter (fun j -> filter i j heightMap blurFilter (fun x p -> x) (( + ))))

// Take a height map and blur it using a Gaussian blur
let gaussianBlur (heightMap:HeightMap) = 
    let gaussianFilter = { Positions = [-3..3]; 
                           Kernel = [| 0.0; 0.00135; 0.157305; 0.68269; 0.157305; 0.00135; 0.0 |]}

    blur heightMap gaussianFilter

// Take a height map and blur it using a Mean filter     
let meanFilter (heightMap:HeightMap) = 
    let meanFilter = { Positions = [-2..2]; 
                       Kernel = [| 0.2; 0.2; 0.2; 0.2; 0.2; 0.2 |]}

    blur heightMap meanFilter

