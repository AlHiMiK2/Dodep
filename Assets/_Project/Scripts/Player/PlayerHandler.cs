using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerHandler : MonoBehaviour
    {
        public static PlayerHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}