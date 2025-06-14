using System;

namespace Exchange
{
    [Serializable]
    public class ExchangeData
    {
        public ExchangeType Type;
        public int Subtype;
        public int Value;

        public ExchangeData(ExchangeType type, int subtype, int value)
        {
            Type = type;
            Subtype = subtype;
            Value = value;
        }

        public ExchangeData(int type, int subtype, int value)
        {
            Type = (ExchangeType) type;
            Subtype = subtype;
            Value = value;
        }
    }
}
