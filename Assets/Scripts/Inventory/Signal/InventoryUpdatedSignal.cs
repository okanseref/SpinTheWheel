using Exchange;

namespace Inventory.Signal
{
    public class InventoryUpdatedSignal
    {
        public ExchangeType Type { get; private set; }
        public int SubType { get; private set; }
        public int Value { get; private set; }

        public InventoryUpdatedSignal(ExchangeType type, int subType, int value)
        {
            Type = type;
            SubType = subType;
            Value = value;
        }
    }
}