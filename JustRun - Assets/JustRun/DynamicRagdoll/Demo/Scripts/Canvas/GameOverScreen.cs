using UnityEngine.SceneManagement;
using UnityEngine;
using DynamicRagdoll.Demo;
using System.Collections;
using TMPro;

public class GameOverScreen : MonoBehaviour
{

    public GameObject gameOverScreen;
    public PlayerControl player;
    public DemoSceneController demo;
    public GameObject panel;
    public GameObject score;

    private GameObject playerObj;
    public Characters characters;
    //public AudioSource gameplayLoop;

    public TextMeshProUGUI moneyText;
    private float orgFixedDelta;
    public static GameOverScreen instance;
    public AudioSource Swash;
    public AudioClip swashClip;
    //public GameObject tutorial;

    private void Awake() {

        

        if(instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);
        
        playerObj = (GameObject)Instantiate(characters.readyCharacters[PlayerPrefs.GetInt("CharacterSelected")], 
        new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);    

        orgFixedDelta = Time.fixedDeltaTime;    
        Admob.gameStarted = true;
    }

    

    IEnumerator disablePanel(){

        yield return new WaitForSeconds(0.3f);

        panel.gameObject.SetActive(false);

        yield break;
    
    }

    bool s = false;
    void Update()
    {
        if(Time.timeScale < 0.1){
            GameOver(); 
                       
        } 
        else{
            s = false;
        }       
    }

    void GameOver(){

        if(s) return;

        Swash.PlayOneShot(swashClip);

        Admob.instance.ShowInterstitial();

        gameOverScreen.SetActive(true);
        //gameOverScreen.GetComponent<Animator>().SetTrigger("fadein");

        score.SetActive(false);
        moneyText.text = ((score.GetComponent<Scores>().scores - prevScore)).ToString();
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + ((score.GetComponent<Scores>().scores - prevScore)));

        s = true;
    }


    int prevScore;
    public void Revive(){

        Admob.instance.ShowRewardedAd();
        
    }

    public void RewardResult(){
        Time.timeScale = 1;
        Time.fixedDeltaTime = orgFixedDelta * Time.timeScale;

        StartCoroutine(demo.CleanEnemy());

        prevScore = score.GetComponent<Scores>().scores;
        //Show Ads
        player.ragdollController.disableGetUp = false;
        gameOverScreen.SetActive(false);
        //gameOverScreen.GetComponent<Animator>().SetTrigger("fadeout");  
        score.SetActive(true);   
        GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

    }

    public void Retry(){

        StartCoroutine(RetryR());
        //PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + (score.GetComponent<Scores>().scores * 2));

    }

    public void MainMenu() {
        Admob.gameStarted = false;
        StartCoroutine(MainMenuR());
        //PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + (score.GetComponent<Scores>().scores * 2));
    }

    public IEnumerator MainMenuR(){

        Time.timeScale = 1;
        Time.fixedDeltaTime = orgFixedDelta * Time.timeScale;
        //gameOverScreen.GetComponent<Animator>().SetTrigger("fadeout"); 
        GoogleLeaderboards.instance.SetScore(PlayerPrefs.GetInt("Scores")); 
        panel.GetComponent<Animator>().SetTrigger("out");
        GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
        yield return new WaitUntil(() => panel.GetComponent<CanvasGroup>().alpha > 0.8f);
        SceneManager.LoadScene("Main Menu");      

    }

    public IEnumerator RetryR(){

        Time.timeScale = 1;
        Time.fixedDeltaTime = orgFixedDelta * Time.timeScale;
        //gameOverScreen.GetComponent<Animator>().SetTrigger("fadeout"); 
        GoogleLeaderboards.instance.SetScore(PlayerPrefs.GetInt("Scores"));  
        panel.GetComponent<Animator>().SetTrigger("out");
        GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
        yield return new WaitUntil(() => panel.GetComponent<CanvasGroup>().alpha > 0.8f);
        SceneManager.LoadScene("Game");      

    }
}
