using System.Collections.Generic;
using UnityEngine;

namespace Zone
{
    [CreateAssetMenu(fileName = "ZoneSettingsCollection", menuName = "ScriptableObjects/ZoneSettingsCollection", order = 1)]
    public class ZoneSettingsCollection : ScriptableObject
    {
        public List<ZoneSettings> ZoneSettingsList;
    }
}