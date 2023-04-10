using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BttnSceneChanger : MonoBehaviour
{
    public void SceneChanger(string name)
    {
        SceneManager.LoadScene(name);
    }
}
