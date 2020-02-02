using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToContinue : MonoBehaviour
{
    public int SceneIndexToLoad;
    public float TimeBeforeActive;

    private bool canExit = false;

    private void Start() {
        StartCoroutine(ActivateTimer());
    }

    private IEnumerator ActivateTimer() {
        yield return new WaitForSeconds(TimeBeforeActive);
        canExit = true;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0) && canExit) {
            SceneManager.LoadScene(SceneIndexToLoad);
        }
    }

}
