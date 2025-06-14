using UnityEngine;
using Utils;
using Zone;

namespace Game
{
    public class GameInfoManager : Singleton<GameInfoManager>
    {
        public ZoneSettingsCollection ZoneSettingsCollection { get; private set; }
        public SpecialZoneSettings SpecialZoneSettings { get; private set; }
        
        private void Awake()
        {
            ZoneSettingsCollection = Resources.Load<ZoneSettingsCollection>("ScriptableObjects/ZoneSettingsCollection");
            SpecialZoneSettings = Resources.Load<SpecialZoneSettings>("ScriptableObjects/SpecialZoneSettings");
        }

        public ZoneSettings GetZoneLooped(int zoneIndex)
        {
            zoneIndex %= ZoneSettingsCollection.ZoneSettingsList.Count;
            return ZoneSettingsCollection.ZoneSettingsList[zoneIndex];
        }
    }
}