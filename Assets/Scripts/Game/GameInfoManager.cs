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

        public ZoneType GetZoneType(int zoneIndex)
        {
            if (zoneIndex != 0 && zoneIndex % SpecialZoneSettings.GoldenSpinPeriod  == 0)
                return ZoneType.Golden;
            
            if (zoneIndex != 0 && zoneIndex % SpecialZoneSettings.SilverSpinPeriod  == 0)
                return ZoneType.Silver;

            return ZoneType.Basic;
        }
    }
}