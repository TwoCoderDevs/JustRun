using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class SelectCharStyled : MonoBehaviour {


    public bool testing = false;
	
	// The left marker out of visible scence
	public Transform markerLeft2;
	// The left marker of visible scence
	public Transform markerLeft;
	// The middle marker of visible scence
	public Transform markerMiddle;
	// The right marker of visible scence
	public Transform markerRight;
	// The right marker out of visible scence
	public Transform markerRight2;
	
	// The characters prefabs to pick
	public Transform[] charsPrefabs;
	// An aux array to be used on ShowSelectedChar.cs
	//public static Transform[] charsPrefabsAux;
	
	// The game objects created to be showed on screen
	private GameObject[] chars;
	
	// The index of the current character
	public int currentChar = 0;

	public TextMeshProUGUI characterNameText;
    public Characters charactersObj;

    public Slider stamina;
    public Slider skills;
    public Slider overAll;

    public GameObject buyButton;
    public GameObject selectButton;
    public GameObject priceLabel;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI coinText;

    public GameObject notEnoughMoney;
    public GameObject UIElements;
    public GameObject panel;
    public GameObject Tutorial;

    private void Awake() {

        if(charactersObj.firstRun){
            Tutorial.SetActive(true);
            //charactersObj.firstRun = false;
            StartCoroutine(DisableTut());
        }
        else{
            Tutorial.SetActive(false);
        }

        if(testing) PlayerPrefs.SetInt("Money", 1000);


        for(int i = 0; i < charactersObj.displayCharacters.Length; i++){
            charsPrefabs[i] = charactersObj.displayCharacters[i].character; 
        }

        coinText.text = PlayerPrefs.GetInt("Money").ToString();        
    }

    IEnumerator DisableTut(){
        yield return new WaitForSeconds(2.5f);
        Tutorial.SetActive(false);
        charactersObj.firstRun = false;
    }

    public void GotoMenu(){
        StartCoroutine(gM());
    }

    IEnumerator gM(){
        panel.GetComponent<Animator>().SetTrigger("out");
        yield return new WaitUntil(() => panel.GetComponent<CanvasGroup>().alpha > 0.8f);
        SceneManager.LoadScene("Main Menu"); 
    }
	
	void Start() {
		//charsPrefabsAux = charsPrefabs;
		// We initialize the chars array
		chars = new GameObject[charsPrefabs.Length];
		
		// We create game objects based on characters prefabs
		int index = 0;
		foreach (Transform t in charsPrefabs) {
			chars[index++] = GameObject.Instantiate(t.gameObject, markerRight2.position, Quaternion.identity) as GameObject;
		}

        Admob.instance.ShowInterstitial();
	}
	
	void ChangeChar() {
		// Here we create a button to choose a next char
		/*if (GUI.Button(new Rect(10, (Screen.height - 50) / 2, 100, 50), "Previous")) {
			
		}
		
		// Now we create a button to choose a previous char
		if (GUI.Button(new Rect(Screen.width - 100 - 10, (Screen.height - 50) / 2, 100, 50), "Next")) {
			
		}*/
		
		// Shows a label with the name of the selected character
		/*GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		
		string labelChar = selectedChar.name;
		GUI.Label(new Rect((Screen.width - 200) / 2, 20, 200, 50), labelChar);
		
		/*if (GUI.Button(new Rect((Screen.width - 100) / 2, Screen.height - 70, 100, 50), "Pick")) {
			Application.LoadLevel("main-styled-selected-char");
		}*/

		Transform selectedChar = charsPrefabs[currentChar];
		string labelChar = selectedChar.name;
		characterNameText.text = labelChar;

        stamina.value = charactersObj.displayCharacters[currentChar].stamina;
        skills.value = charactersObj.displayCharacters[currentChar].skills;
        overAll.value = charactersObj.displayCharacters[currentChar].overall;

        priceText.text = charactersObj.displayCharacters[currentChar].price.ToString();

        if(!charactersObj.displayCharacters[currentChar].unlocked){
            buyButton.SetActive(true);
            priceLabel.SetActive(true);
            selectButton.SetActive(false);
        }else if(!charactersObj.displayCharacters[currentChar].selected){
            selectButton.SetActive(true);
            priceLabel.SetActive(false);
            buyButton.SetActive(false);
        }else{
            buyButton.SetActive(false);
            selectButton.SetActive(false);
            priceLabel.SetActive(false);
        }
		
		// The index of the middle character
		int middleIndex = currentChar;	
		// The index of the left character
		int leftIndex = currentChar - 1;
		// The index of the right character
		int rightIndex = currentChar + 1;
		
		// For each character we set the position based on the current index
		for (int index = 0; index < chars.Length; index++) {
			Transform transf = chars[index].transform;
			
			// If the index is less than left index, the character will dissapear in the left side
			if (index < leftIndex) {
				transf.position = Vector3.Lerp(transf.position, markerLeft2.position, Time.deltaTime);
				
			// If the index is less than right index, the character will dissapear in the right side
			} else if (index > rightIndex) {
				transf.position = Vector3.Lerp(transf.position, markerRight2.position, Time.deltaTime);
				
			// If the index is equals to left index, the character will move to the left visible marker
			} else if (index == leftIndex) {
				transf.position = Vector3.Lerp(transf.position, markerLeft.position, Time.deltaTime);
				
			// If the index is equals to middle index, the character will move to the middle visible marker
			} else if (index == middleIndex) {
				transf.position = Vector3.Lerp(transf.position, markerMiddle.position, Time.deltaTime);
				
			// If the index is equals to right index, the character will move to the right visible marker
			} else if (index == rightIndex) {
				transf.position = Vector3.Lerp(transf.position, markerRight.position, Time.deltaTime);
			}
		}
	}

    public void SelectCharacter(){
        PlayerPrefs.SetInt("CharacterSelected", currentChar);
        
        for(int i = 0; i < charactersObj.displayCharacters.Length; i++){
            charactersObj.displayCharacters[i].selected = false; 
        }

        charactersObj.displayCharacters[currentChar].selected = true;
    }

    //Todo : Check for money
    public void BuyCharacter(){
        if(PlayerPrefs.GetInt("Money") > charactersObj.displayCharacters[currentChar].price){
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - charactersObj.displayCharacters[currentChar].price);
            charactersObj.displayCharacters[currentChar].unlocked = true;
            coinText.text = PlayerPrefs.GetInt("Money").ToString();
            //buyButton.SetActive(false);
        }
        else{
            UIElements.SetActive(false);
            notEnoughMoney.SetActive(true);
        }
    }


	private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            /*if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }*/
        }


		ChangeChar();
    }

    void checkSwipe()
    {
        /*//Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }*/

        //Check if Horizontal swipe
        if (horizontalValMove() > SWIPE_THRESHOLD)
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        /*else
        {
            //Debug.Log("No Swipe!");
        }*/
    }

    /*float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }*/

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    /*void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
    }*/

    void OnSwipeLeft()
    {
        currentChar++;
			
			if (currentChar >= chars.Length) {
				currentChar = chars.Length - 1;
			}
    }

    void OnSwipeRight()
    {
        currentChar--;
			
			if (currentChar < 0) {
				currentChar = 0;
			}
    }
	
}
