using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    //private bool levelComplete = true;
    public GameObject finishMenuScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.Play("Finish");
        CompleteLevel();
        Time.timeScale = 0;
    }

    private void CompleteLevel()
    {
        finishMenuScreen.SetActive(true);
    }
}
