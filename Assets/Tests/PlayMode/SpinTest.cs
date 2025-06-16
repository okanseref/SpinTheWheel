using System.Collections;
using NUnit.Framework;
using UI.SpinArea;
using UI.SpinArea.Signal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Utils;

namespace Tests.PlayMode
{
    public class SpinTest
    {
        private bool isRollCompleted = false;
        [UnityTest]
        public IEnumerator SpinCompleteTest()
        {
            #region Start Game

            if (SceneManager.GetActiveScene().name != "MainScene")
                SceneManager.LoadScene("MainScene");
            
            yield return new WaitUntil(()=> SceneManager.GetActiveScene().name.Equals("MainScene"));

            #endregion
            
            SignalBus.Instance.Subscribe<RollCompletedSignal>(OnRollCompleted);
            SignalBus.Instance.Fire<SpinClickedSignal>();
            yield return new WaitForSeconds(3);
            Assert.AreEqual(isRollCompleted, true);
        }

        private void OnRollCompleted()
        {
            isRollCompleted = true;
        }
    }
}