using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
}