using UnityEngine;

namespace ExamineSystem
{
    public class ExamineItemController : MonoBehaviour
    {
        [Header("Zooming Object")]
        [SerializeField] private Transform examinePoint;

        [Header("Item Descriptors")]
        public string itemName;
        [Space(5)][TextArea] public string itemDescription;

        [Header("Item UI Type")]
        [SerializeField] private UIType _UIType;

        [Header("Emissive Highlight")]
        [SerializeField] private bool showHighlight;
        private Material thisMat;  

        [Header("Rotation Speed")]
        [SerializeField] private float horizontalSpeed = 5.0F;
        [SerializeField] private float verticalSpeed = 5.0F;

        Transform examinePosition;
        Vector3 originalPosition;
        Quaternion originalRotation;
        private Vector3 startPos;
        public bool doOnce;
        public bool canRotate;
        private bool Examined;
        private const string emissive = "_EMISSION";
        private const string mouseX = "Mouse X";
        private const string mouseY = "Mouse Y";
        private const string interact = "Interact";
        private const string examineLayer = "ExamineLayer";

        public ExamineRaycast raycastManager;

        public enum UIType { None, BasicLowerUI, RightSideUI }

        void Start()
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            startPos = gameObject.transform.localEulerAngles;

            thisMat = GetComponent<Renderer>().material; 
            thisMat.DisableKeyword(emissive);

            raycastManager = Camera.main.GetComponent<ExamineRaycast>();          
            doOnce = false;
        }

        public void MainHighlight(bool isHighlighted) 
        {
            if (showHighlight)
            {
                if (isHighlighted)
                {
                    thisMat.EnableKeyword(emissive);
                }
                else
                {
                    thisMat.DisableKeyword(emissive);
                }
            }
        }

        public void ResetObject()
        {
            gameObject.layer = LayerMask.NameToLayer(interact);
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            canRotate = false;
            ExamineObject(false);
        }

        public void ExamineObject(bool isExamined)
        {
            if (isExamined)
            {
                Examined = true;
                ExamineAudioManager.instance.Play("ExamineInteract");

                examinePosition = examinePoint.transform;
                transform.position = examinePosition.position;

                gameObject.layer = LayerMask.NameToLayer(examineLayer);
                thisMat.DisableKeyword(emissive);
                canRotate = true;
                examinePosition = null;

                if (Examined)
                {
                    switch (_UIType)
                    {
                        case UIType.BasicLowerUI:
                            ExamineUIManager.instance.basicItemNameUI.text = itemName;
                            ExamineUIManager.instance.basicItemDescriptionUI.text = itemDescription;
                            ExamineUIManager.instance.basicExamineUI.SetActive(true);
                            break;
                        case UIType.RightSideUI:
                            ExamineUIManager.instance.rightItemNameUI.text = itemName;
                            ExamineUIManager.instance.rightItemDescriptionUI.text = itemDescription;
                            ExamineUIManager.instance.rightExamineUI.SetActive(true);
                            break;
                    }
                }
            }
            else
            {
                switch(_UIType)
                {
                    case UIType.BasicLowerUI:
                        Examined = false;
                        ExamineUIManager.instance.basicItemNameUI.text = null;
                        ExamineUIManager.instance.basicExamineUI.SetActive(false);
                        break;
                    case UIType.RightSideUI:
                        Examined = false;
                        ExamineUIManager.instance.rightItemNameUI.text = null;
                        ExamineUIManager.instance.rightExamineUI.SetActive(false);
                        break;
                }
            }
        }

        void Update()
        {
            if (canRotate)
            {
                if (!doOnce)
                {
                    ExamineDisableManager.instance.DisablePlayer(true);
                    doOnce = true;

                    if (doOnce)
                    {
                        gameObject.transform.localEulerAngles = startPos;
                    }
                }

                float h = horizontalSpeed * Input.GetAxis(mouseX);
                float v = verticalSpeed * Input.GetAxis(mouseY);

                if (Input.GetKey(ExamineInputManager.instance.rotateKey))
                {
                    gameObject.transform.Rotate(v, h, 0);
                }

                else if (Input.GetKeyDown(ExamineInputManager.instance.dropKey))
                {
                    ExamineDisableManager.instance.DisablePlayer(false);
                    ResetObject();
                    doOnce = false;
                    raycastManager.interacting = false;
                }
            }
        }
    }
}