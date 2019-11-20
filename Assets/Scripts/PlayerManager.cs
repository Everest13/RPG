using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singletone

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;    
    }

    #endregion

    public GameObject player;

    public void KillPlayer()
    {
        // reload activ scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
