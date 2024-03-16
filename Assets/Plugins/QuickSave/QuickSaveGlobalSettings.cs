using CI.QuickSave.Core.Serialisers;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace CI.QuickSave
{
    public static class QuickSaveGlobalSettings
    {
        /// <summary>
        /// The path to save data to - defaults to Application.persistentDataPath
        /// </summary>
        public static string StorageLocation { get; set; } = Path.Combine(Application.streamingAssetsPath,"SaveData");

        /// <summary>
        /// Register a new json converter
        /// </summary>
        /// <param name="converter">The json converter to register</param>
        public static void RegisterConverter(JsonConverter converter) => JsonSerialiser.RegisterConverter(converter);
    }
}