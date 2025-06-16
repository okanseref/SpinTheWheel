using System;
using System.Collections;
using Game;
using Game.Signal;
using NUnit.Framework;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using Utils;
using Zone;

namespace Tests.Edit
{
    public class SignalBusTest : MonoBehaviour
    {
        private int _counter = 0;
        [Test]
        public void SilverZoneTest()
        {
            SignalBus.Instance.Subscribe<GameResetSignal>(OnSignal);
            SignalBus.Instance.Fire(new GameResetSignal());
            SignalBus.Instance.Unsubscribe<GameResetSignal>(OnSignal);
            SignalBus.Instance.Fire(new GameResetSignal());
            Debug.Log("Result: " + _counter + "\nExpected: " + 1);
            Assert.AreEqual(_counter == 1, true);
        }

        private void OnSignal()
        {
            _counter++;
        }
    }
}