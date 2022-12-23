using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
   public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            if (collision.gameObject)
            {
                Debug.Log("hit player");
                SceneManager.LoadScene("DeadScene");
            }
        
    }
}
