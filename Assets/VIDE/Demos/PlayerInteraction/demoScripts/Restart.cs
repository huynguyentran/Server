using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            if (System.IO.Directory.Exists(Application.dataPath + "/VIDE/saves"))
            {
                System.IO.Directory.Delete(Application.dataPath + "/VIDE/saves", true);
            }
            SceneManager.LoadScene(0);
        }
    }
}
