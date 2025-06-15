using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zone
{
    [Serializable]
    [CreateAssetMenu(fileName = "ZoneSettingsCollection", menuName = "ScriptableObjects/ZoneSettingsCollection", order = 1)]
    public class ZoneSettingsCollection : ScriptableObject
    {
        public List<ZoneSettings> ZoneSettingsList;
    }
}