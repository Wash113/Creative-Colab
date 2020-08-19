using UnityEngine;

namespace ExamineSystem
{
    public class ExamineInputManager : MonoBehaviour
    {
        [Header("Raycast Pickup Input")]
        public KeyCode interactKey;

        [Header("Rotation Key Inputs")]
        public KeyCode rotateKey;
        public KeyCode dropKey;
        public KeyCode left;
        public KeyCode right;
        public KeyCode up;
        public KeyCode down;

        public static ExamineInputManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
