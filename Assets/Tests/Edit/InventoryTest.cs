using Exchange;
using Inventory;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Edit
{
    public class InventoryTest : MonoBehaviour
    {
        [Test]
        public void InventoryAddTest()
        {
            InventoryManager.Instance.ClearInventory();
            InventoryManager.Instance.AddExchangeData(new ExchangeData(1,1,1));
            InventoryManager.Instance.AddExchangeData(new ExchangeData(1,1,5));
            var result = InventoryManager.Instance.GetExchangeDataValue((ExchangeType)1, 1);
            Debug.Log("Result: " + result + "\nExpected: " + 6);
            Assert.AreEqual(result, 6);
        }
        
        [Test]
        public void InventoryRemoveTest()
        {
            InventoryManager.Instance.ClearInventory();
            InventoryManager.Instance.AddExchangeData(new ExchangeData(1,1,1));
            InventoryManager.Instance.AddExchangeData(new ExchangeData(1,1,5));
            InventoryManager.Instance.RemoveExchangeData(new ExchangeData(1,1,3));
            var result = InventoryManager.Instance.GetExchangeDataValue((ExchangeType)1, 1);
            Debug.Log("Result: " + result + "\nExpected: " + 3);
            Assert.AreEqual(result, 3);
        }
        
        [Test]
        public void InventoryHasTest()
        {
            InventoryManager.Instance.ClearInventory();
            InventoryManager.Instance.AddExchangeData(new ExchangeData(1,1,1));
            InventoryManager.Instance.AddExchangeData(new ExchangeData(2,2,5));
            InventoryManager.Instance.AddExchangeData(new ExchangeData(2,1,5));
            InventoryManager.Instance.AddExchangeData(new ExchangeData(1,1,5));
            InventoryManager.Instance.RemoveExchangeData(new ExchangeData(1,1,1));
            InventoryManager.Instance.AddExchangeData(new ExchangeData(1,1,5));
            var result = InventoryManager.Instance.GetExchangeDataValue((ExchangeType)1, 1);
            Debug.Log("Result: " + result + "\nExpected: " + 10);
            Assert.AreEqual(result, 10);
        }
    }
}
