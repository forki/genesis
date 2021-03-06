module MapConverter

open System
open System.Collections.Generic 
open Newtonsoft.Json  

// http://stackoverflow.com/a/30836070/153797
type MapConvert<'f,'t when 'f : comparison>() =
    static member ReadJson (reader:JsonReader, serializer:JsonSerializer) =
        serializer.Deserialize<Dictionary<'f, 't>> (reader)
        |> Seq.map (fun kv -> kv.Key, kv.Value)
        |> Map.ofSeq

let internal mapConverter = {
  new JsonConverter() with
    override __.CanConvert (t:Type) =
      t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<Map<_, _>>

    override __.WriteJson (writer, value, serializer) =
      serializer.Serialize(writer, value)

    override __.ReadJson (reader, t, _, serializer) =
      let converter = 
        typedefof<MapConvert<_,_>>.MakeGenericType (t.GetGenericArguments())

      let readJson =
        converter.GetMethod("readJson")

      readJson.Invoke(null, [| reader; serializer |])
}