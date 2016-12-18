module GenesisTools

open System.Drawing

open HeightMap  
open MidpointDisplacement

let heightMapToTxt (heightMap:HeightMap) (filename:string) =
    let out = Array.init (heightMap.Size * heightMap.Size) (fun e -> heightMap.Map.[e].ToString())
    System.IO.File.WriteAllLines(filename, out)

let heightMapToPng (heightMap:HeightMap) (filename:string) =
    let png = new Bitmap(heightMap.Size, heightMap.Size)
    for x in [0..heightMap.Size-1] do
        for y in [0..heightMap.Size-1] do
            let red, green, blue = convertFloatToRgb (heightMap.Get x y) 
            png.SetPixel(x, y, Color.FromArgb(255, red, green, blue))
    
    png.Save(filename, Imaging.ImageFormat.Png) |> ignore

[<EntryPoint>]
let main argv =
    let map = newHeightMap 8
    generate map 0.3 0.5
    heightMapToPng map "out.png"
    heightMapToTxt map "out.txt"  
    0 
