using Exchange;
using NUnit.Framework;
using Reward;
using UnityEngine;

namespace Tests.Edit
{
    public class RewardTest : MonoBehaviour
    {
        [Test]
        public void RewardAddTest()
        {
            RewardManager.Instance.AddReward(new ExchangeData(1,1,1));
            RewardManager.Instance.AddReward(new ExchangeData(1,1,1));
            RewardManager.Instance.AddReward(new ExchangeData(1,1,2));
            var result = RewardManager.Instance.GetRewards()[0].Value;
            Debug.Log("Result: " + result + "\nExpected: " + 4);
            Assert.AreEqual(result, 4);
        }
    }
}
