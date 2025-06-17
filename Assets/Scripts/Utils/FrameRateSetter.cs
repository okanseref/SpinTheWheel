using UnityEngine;

namespace Utils
{
    public class FrameRateSetter : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}