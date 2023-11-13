using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PlayerMovement2 : MonoBehaviour
{
    float speed = 0.10f;
    bool tapToStart = false;
    public GameObject warrior;
    public GameObject bryce;
    // Start is called before the first frame update
    void Start()
    {
        
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
            print("space pressed");
            bryce.GetComponent<Animator>().SetBool("RunStart", true);
            bryce.GetComponent<Animator>().SetBool("Idle", false);
            tapToStart = true;
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
        if(warrior.transform.localPosition.z > 78)
        {
            print("finish");
            bryce.GetComponent<Animator>().SetBool("RunStart", false);
            bryce.GetComponent<Animator>().SetBool("Idle", true);
            tapToStart = false;
            SceneManager.LoadScene("NewLevel");
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
    }
/*    void Level2()
    {
        SceneManager.LoadScene(1);
    }*/
}