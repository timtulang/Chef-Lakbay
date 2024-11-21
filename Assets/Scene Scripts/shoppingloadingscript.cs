using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class shoppingloadingscript : MonoBehaviour
{
    
    void Start()
    {
        
        Invoke("LoadNextScene", 3f);
    }

    
    void Update()
    {
        
    }

    
    void LoadNextScene()
    {
        SceneManager.LoadScene("Stage(PLM)");
    }
}
