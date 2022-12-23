using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeadStop : MonoBehaviour
{
    DamageAble playerDamageable;

    private void Awake()
    {
        playerDamageable =GetComponent<DamageAble>();
    }
    private void FixedUpdate()
    {
        if(playerDamageable.IsAlive == false)
        {
            Destroy (GameObject.FindWithTag("Audio"));
            SceneManager.LoadScene("DeadScene");
        }
    }    
}
