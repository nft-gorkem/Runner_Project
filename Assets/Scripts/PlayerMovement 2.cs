using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using System.Threading;

public class PlayerMovement2 : MonoBehaviour
{
    float speed = 0.10f;
    public bool tapToStart = false;
    public GameObject warrior;
    public GameObject bryce;

    public bool death = false;
    public int health = 100;

    // Start is called before the first frame update
    private void Start()
    {
        health = PlayerPrefs.GetInt("HealthValue", 100);
        print(health);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tapToStart)
        transform.Translate(Vector3.forward * speed);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(death == false)
            {
                print("space pressed");
                bryce.GetComponent<Animator>().SetBool("RunStart", true);
                bryce.GetComponent<Animator>().SetBool("Idle", false);
                tapToStart = true;
            }
            if(death == true)
            {
                SceneManager.LoadScene("SampleScene");
                bryce.GetComponent<Animator>().SetBool("Idle", true);
                death = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))//if I press A, it will slide to left
        {
            if(warrior.transform.localPosition.x > -2)
            {
                warrior.transform.DOLocalMoveX(warrior.transform.localPosition.x - 2, 0.25f);
                bryce.GetComponent<Animator>().SetTrigger("RunLeft");
            }
        }

        if(Input.GetKeyDown(KeyCode.D))//if I press D, it will slide to right
        {
            
            if(warrior.transform.localPosition.x < 2)
            {
                warrior.transform.DOLocalMoveX(warrior.transform.localPosition.x + 2, 0.25f);
                bryce.GetComponent<Animator>().SetTrigger("RunRight");
            }
        }

        if(Input.GetKeyDown(KeyCode.G))//if I press D, it will slide to right
        {
            tapToStart = false;
            bryce.GetComponent<Animator>().SetTrigger("FallDown");
        }
        Scene currentScene = SceneManager.GetActiveScene ();

		// Retrieve the name of this scene.
		string sceneName = currentScene.name;

        if(warrior.transform.localPosition.z > 78 && sceneName == "SampleScene")
        {
            print("finish level 2");
            bryce.GetComponent<Animator>().SetBool("RunStart", false);
            bryce.GetComponent<Animator>().SetBool("Idle", true);
            tapToStart = false;
            SceneManager.LoadScene("NewLevel");
        }
        else if(warrior.transform.localPosition.z > 78 && sceneName == "NewLevel")
        {
            print("finish level 2, going back to level 1");
            bryce.GetComponent<Animator>().SetBool("RunStart", false);
            bryce.GetComponent<Animator>().SetBool("Idle", true);
            tapToStart = false;
            SceneManager.LoadScene("SampleScene");
        }

        if(health == 0 || health < 0)
            {
                PlayerPrefs.DeleteKey("HealthValue");
                bryce.GetComponent<Animator>().SetBool("RunStart", false);
                tapToStart = false;
                bryce.GetComponent<Animator>().SetTrigger("FallDown");
                health = 100;
                death = true;
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LevelFinish")
        {
            print("finish");
            bryce.GetComponent<Animator>().SetBool("RunStart", false);
            bryce.GetComponent<Animator>().SetBool("Idle", true);
            tapToStart = false;
            //Invoke(nameof(Level2), 2);
        }
        
        if(other.gameObject.tag == "RedObstacle")
        {
            health -= 50;
            PlayerPrefs.SetInt("HealthValue", health);
            print(health);
        }

        if(other.gameObject.tag == "BlueObstacle")
        {
            health -= 100;
            PlayerPrefs.SetInt("HealthValue", health);
            print(health);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
