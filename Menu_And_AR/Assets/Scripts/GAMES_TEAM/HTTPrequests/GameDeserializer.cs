using SimpleJSON;
using System;
using System.Collections.Generic;

namespace GameRequest
{
    class GameDeserializer
    {
        private delegate T DeserializeSomething<T>(JSONNode node);

        private static List<T> DeserializeList<T>(JSONNode node, DeserializeSomething<T> deserialize)
        {
            var result = new List<T>();

            foreach (var i in node.AsArray)
            {
                result.Add(deserialize(i));
            }

            return result;
        }

        public static List<Photo> DeserializePhotoList(JSONNode node) => DeserializeList(node, DeserializePhotosPath);

        public static Photo DeserializePhotosPath(JSONNode node)
        {
            var photo = new Photo();

            photo.Path = node["path"];
            
            return photo;
        }

        public static List<JsonFile> DeserializeJsonList(JSONNode node) => DeserializeList(node, DeserializeJsonsPath);

        public static JsonFile DeserializeJsonsPath(JSONNode node)
        {
            var json = new JsonFile();

            json.Name = node["json_name"];

            return json;
        }
    }
}