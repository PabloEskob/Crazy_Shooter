using UnityEngine;

namespace Source.Scripts.Data
{
    public static class DataExtensions
    {
        public static int IntToBool(bool value) => value == true ? 1 : 0;
        public static bool BoolToInt(int value) => value == 1;
        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
    }
}