using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
        if(Input.GetButton("restart"))
        {

            GameState.Instance.currentPoints = 0;
            SceneManager.LoadScene(0);
        }   
    }
}
