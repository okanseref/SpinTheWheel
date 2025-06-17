using UnityEngine;

namespace Spin
{
    public class NormalSpinRule : ISpinRule
    {
        public int GetResult()
        {
            return Random.Range(0, 8);
        }
    }
}