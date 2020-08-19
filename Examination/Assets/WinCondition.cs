using ExamineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public GameObject Cube;
    public GameObject TempDirections;
    public GameObject ContinTxt;
    public GameObject DeathTxt;
    public Vector3 CurrentRotation;
    public bool Puz1;
    public bool Puz2;
    public bool Puz3;
    private bool Step1;
    private bool Step2;
    private bool Step3;
    private bool Step4;
    private Scene thisScene;
    private void Start()
    {
        thisScene = SceneManager.GetActiveScene();
    }
    void Update()
    {
        CurrentRotation.x = Cube.transform.localRotation.x;
        CurrentRotation.y = Cube.transform.localRotation.y;
        CurrentRotation.z = Cube.transform.localRotation.z;
        //Puzzle 1
        if (Puz1 == false && Puz2 == false && Puz3 == false)
        {
            if (CurrentRotation.x <= -0.985 && CurrentRotation.y < 0.1 && CurrentRotation.z < 0.1 && Puz1 == false && Puz2 == false && Puz3 == false)
            {

                ExamineDisableManager.instance.DisablePlayer(false);
                Cube.GetComponent<ExamineItemController>().ResetObject();
                Cube.GetComponent<ExamineItemController>().doOnce = false;
                Cube.GetComponent<ExamineItemController>().raycastManager.interacting = false;
                Puz1 = true;
                ContinTxt.GetComponent<Animator>().SetBool("FadeTxt", true);
            }
        }
        //Puzzle 2
        if(Puz1 == true && Puz2 == false && Puz3 == false)
        {
            if (CurrentRotation.x < 0.1 && CurrentRotation.y <= -0.7 && CurrentRotation.z < 0.1 && Step1 == false)
            {
                Step1 = true;
                Debug.Log("Step 1 Complete");
            }
            if (CurrentRotation.x >= 0.45 && CurrentRotation.y <= -0.45 && CurrentRotation.z >= 0.45 && Step1 == true && Step2 == false)
            {
                Step2 = true;
                Debug.Log("Step 2 Complete");
            }
            if (CurrentRotation.x >= 0.7 && CurrentRotation.y < 0.1 && CurrentRotation.z >= 0.7 && Step1 == true && Step2 == true && Step3 == false)
            {
                Step3 = true;
                Debug.Log("Step 3 Complete");
            }
            if (CurrentRotation.x >= 0.9 && CurrentRotation.y < 0.1 && CurrentRotation.z < 0.1 && Step1 == true && Step2 == true && Step3 == true && Step4 == false)
            {
                Step4 = true;
                Debug.Log("Step 4 Complete");
            }
            if (Step4 == true)
            {
                ExamineDisableManager.instance.DisablePlayer(false);
                Cube.GetComponent<ExamineItemController>().ResetObject();
                Cube.GetComponent<ExamineItemController>().doOnce = false;
                Cube.GetComponent<ExamineItemController>().raycastManager.interacting = false;
                Puz2 = true;
                ContinTxt.GetComponent<Animator>().SetBool("FadeTxt", true);
                Step1 = false; Step2 = false; Step3 = false; Step4 = false;
            }
            if (Input.GetKeyDown(ExamineInputManager.instance.dropKey))
            {
                Step1 = false; Step2 = false; Step3 = false; Step4 = false;
            }
        }
        //Puzzle 3
        if (Puz1 == true && Puz2 == true && Puz3 == false)
        {
            if (CurrentRotation.x == 0.0 && CurrentRotation.y == 0 && CurrentRotation.z == 0 && Step1 == false)
            {
                Step1 = true;
                Debug.Log("Step 1 Complete");
            }
            if (CurrentRotation.x < 0.1 && CurrentRotation.y >= 0.7 && CurrentRotation.z < 0.1 && Step1 == true && Step2 == false)
            {
                Step2 = true;
                Debug.Log("Step 2 Complete");
            }
            if (CurrentRotation.x <= -0.45 && CurrentRotation.y >= 0.45 && CurrentRotation.z >= 0.45 && Step1 == true && Step2 == true && Step3 == false)
            {
                Step3 = true;
                Debug.Log("Step 3 Complete");
            }
            if (CurrentRotation.x <= -0.65 && CurrentRotation.y <= -0.1 && CurrentRotation.z >= 0.65 && Step1 == true && Step2 == true && Step3 == true && Step4 == false)
            {
                Step4 = true;
                Debug.Log("Step 4 Complete");
            }
            if (Step4 == true)
            {
                ExamineDisableManager.instance.DisablePlayer(false);
                Cube.GetComponent<ExamineItemController>().ResetObject();
                Cube.GetComponent<ExamineItemController>().doOnce = false;
                Cube.GetComponent<ExamineItemController>().raycastManager.interacting = false;
                Puz3 = true;
                Step1 = false; Step2 = false; Step3 = false; Step4 = false;
            }
            if (Input.GetKeyDown(ExamineInputManager.instance.dropKey))
            {
                Step1 = false; Step2 = false; Step3 = false; Step4 = false;
            }
        }
        //Win Condition
        if (Puz1 == true && Puz2 == true && Puz3 == true)
        {
            Cube.GetComponent<Renderer>().enabled = false;
            Cube.GetComponent<BoxCollider>().enabled = false;
            TempDirections.SetActive(false);
        }
        #region TxtCode
        {
            if (Puz1 == true && Input.GetKey(KeyCode.KeypadEnter))
            {
                ContinTxt.GetComponent<Animator>().SetBool("FadeTxt", false);
            }
            if (Puz1 == true && Puz2 == true && Input.GetKey(KeyCode.KeypadEnter))
            {
                ContinTxt.GetComponent<Animator>().SetBool("FadeTxt", false);
            }
            if (Puz1 == true && Puz2 == true && Puz3 == false && ContinTxt.GetComponent<Animator>().GetBool("FadeTxt") == false)
            {
                DeathTxt.GetComponent<Animator>().SetBool("FadeTxt", true);
                if(Input.GetKey(KeyCode.Escape))
                {
                    //Run Bad VFX
                    //Wait
                    SceneManager.LoadScene("Examine System Demo");
                }
                
            }
            if (Puz3 == true)
            {
                DeathTxt.GetComponent<Animator>().SetBool("FadeTxt", false);
            }
        }
        #endregion
    }
}
