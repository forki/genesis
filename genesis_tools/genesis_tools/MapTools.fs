module MapTools

open System.Drawing

open HeightMap  
open MidpointDisplacement

let heightMapToTxt (filename:string) (heightMap:HeightMap) =
    let out = Array.init (heightMap.Size * heightMap.Size) (fun e -> heightMap.Map.[e].ToString())
    System.IO.File.WriteAllLines(filename, out)

let heightMapToPng (filename:string) (heightMap:HeightMap) =
    let png = new Bitmap(heightMap.Size, heightMap.Size)
    for x in [0..heightMap.Size-1] do
        for y in [0..heightMap.Size-1] do
            let red, green, blue = convertFloatToRgb (heightMap.Get x y) 
            png.SetPixel(x, y, Color.FromArgb(255, red, green, blue))
    
    png.Save(filename, Imaging.ImageFormat.Png) |> ignore