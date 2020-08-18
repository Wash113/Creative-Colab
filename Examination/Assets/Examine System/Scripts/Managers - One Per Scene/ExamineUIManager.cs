using UnityEngine;
using UnityEngine.UI;

namespace ExamineSystem
{
    public class ExamineUIManager : MonoBehaviour
    {
        [Header("Basic Example UI References")]
        public Text basicItemNameUI;
        public Text basicItemDescriptionUI;
        public GameObject basicExamineUI;

        [Header("Right Side Example UI References")]
        public Text rightItemNameUI;
        public Text rightItemDescriptionUI;
        public GameObject rightExamineUI;

        [Header("Help Panel Visibility")]
        [SerializeField] private GameObject examineHelpUI;
        [SerializeField] private bool showHelp;

        public static ExamineUIManager instance;

        private void Awake()
        {
            if (instance == null) { instance = this; }
        }

        private void Start()
        {
            if (showHelp)
            {
                examineHelpUI.SetActive(true);
            }
            else
            {
                examineHelpUI.SetActive(false);
            }
        }
    }
}
