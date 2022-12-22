using Assets.Script.Event;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas= FindObjectOfType<Canvas>();
        
    }

    private void OnEnable() // make multiple time
    {
        CharacterEvent.characterDamaged +=(CharacterTookDamage);
        CharacterEvent.characterHealed +=(CharacterHealed);
    }
    private void OnDisable()
    {
        CharacterEvent.characterDamaged -=(CharacterTookDamage);
        CharacterEvent.characterHealed -=(CharacterHealed);
    }

    public void CharacterTookDamage(GameObject character,int damageReceived)
    {
        //create text at character hit
        Vector3 spwanPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //instance copy text
        TMP_Text tmpText = Instantiate(damageTextPrefab, spwanPosition, Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text= damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, int healthRestored) 
    {
        //create text at character hit
        Vector3 spwanPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //instance copy text
        TMP_Text tmpText = Instantiate(healthTextPrefab, spwanPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }
    public void OnExitGame (InputAction.CallbackContext context)
    {//end Game method
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + ":" + this.GetType() + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if (UNITY_EDITOR) // run on editor
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) //run on pc or mac
            Application.Quit(); 
#elif (UNITY_WEBGL) // run on web
SceneManager.LoadScene("QuitScene")
#endif
        }
    }
}
