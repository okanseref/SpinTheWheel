using System;
using System.Collections.Generic;
using Exchange;
using UnityEditor;
using UnityEngine;
using Zone;
using Random = UnityEngine.Random;

public class RandomZoneSettingsCreator
{
    [MenuItem("ZoneSettingsCreator/Create Dummy Settings")]
    static void CreateRandom()
    {
        List<ZoneSettings> ZoneSettingsList = new();
        
        for (int i = 0; i < 50; i++)
        {
            var zoneSettings = new ZoneSettings();
            
            for (int j = 0; j < 8; j++)
            {
                zoneSettings.Rewards.Add(GetRandomExchangeData());            
            }

            zoneSettings.BombIndex = Random.Range(0, 8);
            ZoneSettingsList.Add(zoneSettings);
        }

        var zoneSettingsCollection = Resources.Load<ZoneSettingsCollection>("ScriptableObjects/ZoneSettingsCollection");
        zoneSettingsCollection.ZoneSettingsList = ZoneSettingsList;
        
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(zoneSettingsCollection);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    private static ExchangeData GetRandomExchangeData()
    {
        var type = Random.Range(1, Enum.GetNames(typeof(ExchangeType)).Length + 1);
        var subType = 1;

        var value = type switch
        {
            (int)ExchangeType.Currency => Random.Range(1, 10) * 1000,
            (int)ExchangeType.WeaponProgress => Random.Range(1, 10),
            (int)ExchangeType.Case => Random.Range(1, 3),
            _ => 1
        };

        var subTypeCount = Resources.LoadAll<Sprite>("ExchangeVisuals/" + (ExchangeType)type).Length;
        subType = Random.Range(1, subTypeCount + 1);
        return new ExchangeData(type, subType, value);
    }
}
