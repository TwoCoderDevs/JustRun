using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DynamicRagdoll.Demo;

public class GameMenu : MonoBehaviour
{

    //public GameObject gameOverMenu;
    public Characters charactersObj;
    public GameObject panel;

    private GameObject character;
    public TextMeshProUGUI coinText;

    private void Awake() {

        character = (GameObject) Instantiate(charactersObj.displayCharacters[PlayerPrefs.GetInt("CharacterSelected")].character.gameObject,
        Vector3.zero, Quaternion.identity);

        coinText.text = PlayerPrefs.GetInt("Money").ToString();

        //DisablePanel();

    }

    public void PlayGame(){

        StartCoroutine(LoadScene()); 
    }

    IEnumerator LoadScene(){

        panel.GetComponent<Animator>().SetTrigger("out");
        yield return new WaitUntil(() => panel.GetComponent<CanvasGroup>().alpha > 0.8f);
        SceneManager.LoadScene("Game");      

    }

    public void SelectCharacter(){
        StartCoroutine(SC());
    }

    IEnumerator SC(){
        panel.GetComponent<Animator>().SetTrigger("out");
        yield return new WaitUntil(() => panel.GetComponent<CanvasGroup>().alpha > 0.95f);
        SceneManager.LoadScene("Character Selection");
        
    }

}
