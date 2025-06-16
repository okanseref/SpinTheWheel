using UnityEngine;
using Utils;
using Zone;

namespace Game
{
    public class GameInfoManager : Singleton<GameInfoManager>
    {
        public ZoneSettingsCollection ZoneSettingsCollection { get; private set; }
        public GameSettings GameSettings { get; private set; }
        
        private void Awake()
        {
            ZoneSettingsCollection = Resources.Load<ZoneSettingsCollection>("ScriptableObjects/ZoneSettingsCollection");
            GameSettings = Resources.Load<GameSettings>("ScriptableObjects/GameSettings");
        }

        public ZoneSettings GetZoneLooped(int zoneIndex)
        {
            zoneIndex %= ZoneSettingsCollection.ZoneSettingsList.Count;
            return ZoneSettingsCollection.ZoneSettingsList[zoneIndex];
        }

        public ZoneType GetZoneType(int zoneIndex)
        {
            if (zoneIndex != 0 && zoneIndex % GameSettings.GoldenSpinPeriod  == 0)
                return ZoneType.Golden;
            
            if (zoneIndex != 0 && zoneIndex % GameSettings.SilverSpinPeriod  == 0)
                return ZoneType.Silver;

            return ZoneType.Basic;
        }
    }
}