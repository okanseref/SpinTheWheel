using System;
using System.Collections;
using System.Collections.Generic;
using Exchange;
using Spin;
using UnityEditor;
using UnityEngine;
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
            
            for (int j = 0; j < 7; j++)
            {
                zoneSettings.Rewards.Add(GetRandomExchangeData());            
            }
            
            ZoneSettingsList.Add(zoneSettings);
        }

        var zoneSettingsCollection = Resources.Load<ZoneSettingsCollection>("ScriptableObjects/ZoneSettingsList");
        zoneSettingsCollection.ZoneSettingsList = ZoneSettingsList;
        
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(zoneSettingsCollection);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    private static ExchangeData GetRandomExchangeData()
    {
        return new ExchangeData(Random.Range(1, Enum.GetNames(typeof(ExchangeType)).Length + 1), 1, 1);
    }
}
