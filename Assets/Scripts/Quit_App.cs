using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_App : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        QuitApplication();
    }

    void QuitApplication()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
