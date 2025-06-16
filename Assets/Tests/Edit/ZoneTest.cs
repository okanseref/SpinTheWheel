using System;
using System.Collections;
using Game;
using NUnit.Framework;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using Zone;

namespace Tests.Edit
{
    public class ZoneTest : MonoBehaviour
    {
        [Test]
        public void SilverZoneTest()
        {
            GameInfoManager.Instance.Awake();
            var silverZonePeriod = GameInfoManager.Instance.GameSettings.SilverSpinPeriod;
            Assert.AreEqual(GameInfoManager.Instance.GetZoneType(silverZonePeriod) == ZoneType.Silver, true);
        }
        
        [Test]
        public void GoldenZoneTest()
        {
            GameInfoManager.Instance.Awake();
            var goldenZonePeriod = GameInfoManager.Instance.GameSettings.GoldenSpinPeriod;
            Assert.AreEqual(GameInfoManager.Instance.GetZoneType(goldenZonePeriod) == ZoneType.Golden, true);
        }
    }
}
