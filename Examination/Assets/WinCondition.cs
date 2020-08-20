using ExamineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class WinCondition : MonoBehaviour
{
    [Header("Cube Tracking")]
    public Vector3 CurrentRotation;
    [Header("Game Objects")]
    public GameObject Player;
    public GameObject Cube;
    public GameObject CubeMesh;
    public GameObject Flower;
    public GameObject ContinTxt;
    public GameObject DeathTxt;
    public Text txt;
    [Header("Particles")]
    public ParticleSystem fail;
    public ParticleSystem reward;
    [Header("Audio")]
    public AudioClip winSound;
    public AudioClip failSound;
    public AudioClip step;
    public AudioClip Yoshi;
    [Header("Puzzles Completed")]
    public bool Puz1;
    public bool Puz2;
    public bool Puz3;
    [Header("In Puzzle Tracking")]
    private bool Step1;
    private bool Step2;
    private bool Step3;
    private bool Step4;
    void Update()
    {
        //Rotation Tracking for the cube
        CurrentRotation.x = Cube.transform.localRotation.x;
        CurrentRotation.y = Cube.transform.localRotation.y;
        CurrentRotation.z = Cube.transform.localRotation.z;

        ///<summary>
        ///Handles all functions related to puzzle completetion and progression
        ///</summary>
        #region Puzzle Code
        //Puzzle 1
        if (Puz1 == false && Puz2 == false && Puz3 == false)
        {
            if (CurrentRotation.x <= -0.985 && CurrentRotation.y < 0.2 && CurrentRotation.z < 0.2 && Puz1 == false && Puz2 == false && Puz3 == false)
            {
                Cube.GetComponent<ExamineItemController>().ResetObject();
                Cube.GetComponent<ExamineItemController>().doOnce = false;
                Cube.GetComponent<ExamineItemController>().raycastManager.interacting = false;
                reward.Play();
                Cube.GetComponent<AudioSource>().PlayOneShot(winSound);
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
                Cube.GetComponent<AudioSource>().PlayOneShot(step);
            }
            if (CurrentRotation.x >= 0.40 && CurrentRotation.y >= -0.6 && CurrentRotation.z >= 0.40 && Step1 == true && Step2 == false)
            {
                Step2 = true;
                Debug.Log("Step 2 Complete");
                Cube.GetComponent<AudioSource>().PlayOneShot(step);
            }
            if (CurrentRotation.x >= 0.7 && CurrentRotation.y < 0.1 && CurrentRotation.z >= 0.65 && Step1 == true && Step2 == true && Step3 == false)
            {
                Step3 = true;
                Debug.Log("Step 3 Complete");
                Cube.GetComponent<AudioSource>().PlayOneShot(step);
            }
            if (CurrentRotation.x >= 0.9 && CurrentRotation.y < 0.1 && CurrentRotation.z < 0.1 && Step1 == true && Step2 == true && Step3 == true && Step4 == false)
            {
                Step4 = true;
                Debug.Log("Step 4 Complete");
                //Cube.GetComponent<AudioSource>().PlayOneShot(step);
            }
            if (Step4 == true)
            {
                Cube.GetComponent<ExamineItemController>().ResetObject();
                Cube.GetComponent<ExamineItemController>().doOnce = false;
                Cube.GetComponent<ExamineItemController>().raycastManager.interacting = false;
                Puz2 = true;
                Cube.GetComponent<AudioSource>().PlayOneShot(winSound);
                reward.Play();
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
                //Cube.GetComponent<AudioSource>().PlayOneShot(step);
            }
            if (CurrentRotation.x < 0.1 && CurrentRotation.y >= 0.7 && CurrentRotation.z < 0.1 && Step1 == true && Step2 == false)
            {
                Step2 = true;
                Debug.Log("Step 2 Complete");
                Cube.GetComponent<AudioSource>().PlayOneShot(step);
            }
            if (CurrentRotation.x <= -0.40 && CurrentRotation.y <= 0.60 && CurrentRotation.z >= 0.40 && Step1 == true && Step2 == true && Step3 == false)
            {
                Step3 = true;
                Debug.Log("Step 3 Complete");
                Cube.GetComponent<AudioSource>().PlayOneShot(step);
            }
            if (CurrentRotation.x <= -0.65 && CurrentRotation.y != 0.0 && CurrentRotation.z >= 0.65 && Step1 == true && Step2 == true && Step3 == true && Step4 == false)
            {
                Step4 = true;
                Debug.Log("Step 4 Complete");
                //Cube.GetComponent<AudioSource>().PlayOneShot(step);
            }
            if (Step4 == true)
            {
                ExamineDisableManager.instance.DisablePlayer(false);
                Cube.GetComponent<ExamineItemController>().ResetObject();
                Cube.GetComponent<ExamineItemController>().doOnce = false;
                Cube.GetComponent<ExamineItemController>().raycastManager.interacting = false;
                Puz3 = true;
                Cube.GetComponent<AudioSource>().PlayOneShot(winSound);
                reward.Play();
                Step1 = false; Step2 = false; Step3 = false; Step4 = false;
            }
            if (Input.GetKeyDown(ExamineInputManager.instance.dropKey))
            {
                Step1 = false; Step2 = false; Step3 = false; Step4 = false;
            }
        }
        #endregion

        //Win Condition
        if (Puz1 == true && Puz2 == true && Puz3 == true)
        {
            Player.GetComponent<FirstPersonController>().m_RunSpeed = 3.0f; 
            Player.GetComponent<FirstPersonController>().m_WalkSpeed = 3.0f;
            CubeMesh.SetActive(false);
            Cube.GetComponent<BoxCollider>().enabled = false;
            Flower.transform.localPosition = new Vector3(0,0,0);
            txt.text = "Press ESCAPE to Restart";
            DeathTxt.GetComponent<Animator>().SetBool("FadeTxt", true);
            // Yoshi time
            if (Flower.GetComponent<AudioSource>().isPlaying == false)
            {
                Flower.GetComponent<AudioSource>().PlayOneShot(Yoshi);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("Examine System Demo");
            }
        }
        else { Player.GetComponent<FirstPersonController>().m_RunSpeed = 0.0f; Player.GetComponent<FirstPersonController>().m_WalkSpeed = 0.0f; }

        ///<summary>
        ///All code relating to Text Fade + Puzzle 3 Failure Code
        ///</summary>
        #region TxtCode
        {
            //Puz1
            if (Puz1 == true && Input.GetKey(KeyCode.Space))
            {
                ContinTxt.GetComponent<Animator>().SetBool("FadeTxt", false);
                ExamineDisableManager.instance.DisablePlayer(false);
            }
            //Puz2
            if (Puz1 == true && Puz2 == true && Input.GetKey(KeyCode.Space))
            {
                ContinTxt.GetComponent<Animator>().SetBool("FadeTxt", false);
                ExamineDisableManager.instance.DisablePlayer(false);
            }
            //Puz3
            if (Puz1 == true && Puz2 == true && Puz3 == false && ContinTxt.GetComponent<Animator>().GetBool("FadeTxt") == false)
            {
                DeathTxt.GetComponent<Animator>().SetBool("FadeTxt", true);
                if(Input.GetKey(KeyCode.Escape))
                {
                    Cube.GetComponent<AudioSource>().PlayOneShot(failSound);
                    fail.Play();
                    StartCoroutine("WaitForParticle");
                }
                
            }
        }
        #endregion
    }
    IEnumerator WaitForParticle()
    {
        //Allows a few seconds for the particle system to finish
        //Called in TxtCode, Puz3
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Examine System Demo");
    }
}
