using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public int health = 100;

    private void Start()
    {
        health = PlayerPrefs.GetInt("HealthValue", 100);
        print(health);
    }

    private void Update()
    {
        if(health == 0 || health < 0)
            {
                PlayerPrefs.DeleteKey("HealthValue");
                SceneManager.LoadScene("SampleScene");
                health = 100;
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RedObstacle")
        {
            health -= 50;
            PlayerPrefs.SetInt("HealthValue", health);
            print(health);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "BlueObstacle")
        {
            health -= 100;
            PlayerPrefs.SetInt("HealthValue", health);
            print(health);
        }
    }
}
