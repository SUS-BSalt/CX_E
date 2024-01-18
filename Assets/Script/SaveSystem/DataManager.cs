using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManager{
    public class DataManager : MonoBehaviour
    {
    
    }
    public class ConfigData
    {
        public LanguageEnum language = LanguageEnum.CN;

        public float MasterVolume = 1;
        public float MusicVolume = 1;
        public float EffectVolme = 1;
        public bool isFullScreen = false;
    }
    public enum LanguageEnum
    {
        CN, EN
    }
}
