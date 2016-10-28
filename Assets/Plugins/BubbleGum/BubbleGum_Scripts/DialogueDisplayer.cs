using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplayer : MonoBehaviour {


    #region Display Internal Variables
        [HideInInspector] public string textToDisplay = "TEXT DISPLAY ERROR";
        [HideInInspector] public bool skipThisLine = false;

        string[] currentWordsToDisplay;

        //Positioning & Line status
        [HideInInspector] public RectTransform previousWordRectTransform;
        [HideInInspector] public RectTransform bubbleBackRectTransform;
        [HideInInspector] public RectTransform firstWordFromPreviousLine;
        [HideInInspector] public Image bubbleBackImage;

        [HideInInspector] public bool m_bReachedLineEnd = false;
        [HideInInspector] public bool finishedLine = false;
        bool firstWordMustSpawn = true;

        float targetYPos;
    #endregion

    #region External Components
    //[Tooltip ("If you live this space blank, BubbleGum will automatically get the first active Canvas it can find.")]
        [HideInInspector]
        public Canvas canvas;
        GameObject NextLineLogo;
        [HideInInspector]
        public Image[] AllImages;
    #endregion

    #region Inspector Variables
        [Header("Required prefabs")]
        public GameObject wordPrefab;


        [Space(10)]
        [Header("Display customization")]
        public float displaySpeedRate = 1f;

        [Space(5)]
        public float xMargin = 0;
        public float yMargin = 0;

        [Space(5)]
        [Range(0, 1)]
        [Tooltip("Avoid the bubbles to get out of the screen, by adding a margin. Number represents percentage of the screen's X size.")]
        public float screenMarginPercentage = .1f;
        public float spaceBetweenLines = .2f;

        [Space(5)]
        public Color textColor = Color.white;
    #endregion

    #region Misc
        int iterator = 0;
        float nextCharacterTimer;
    #endregion

    // Use this for initialization
    void Start ()
    {
        //Getting external components and objects
        canvas = gameObject.GetComponent<Canvas>();

        NextLineLogo = GameObject.Find("ContinueSpeechLogo").gameObject;
        bubbleBackImage = gameObject.GetComponentInChildren<Image>();
        bubbleBackRectTransform = bubbleBackImage.gameObject.GetComponent<RectTransform>();
        AllImages = transform.GetComponentsInChildren<Image>();

        //At game start, we want to hide the bubbles
        NextLineLogo.SetActive(false);
        
        foreach (Image image in AllImages)
        {
            image.enabled = false;
        }

        Dialogue.dialogueDisplayObject = this.gameObject; 
        gameObject.SetActive(false);
    }

    //Spawn a word and return the created gameObject
    GameObject SpawnWord ()
    {
        GameObject spawnedWord = GameObject.Instantiate(wordPrefab) as GameObject;
        Text newWordText = spawnedWord.GetComponent<Text>();
        newWordText.text += currentWordsToDisplay[iterator];

        newWordText.color = textColor;

        spawnedWord.transform.name = currentWordsToDisplay[iterator];
        newWordText.text += " ";

        return spawnedWord;
    }

    //Set the word initial position right after being spawned
    //This also parents the word to the dialogueDisplayer, so we make sure it'll follow the bubble dynamically
    void setWordStartPos (bool isFirstWord, GameObject currentWord)
    {
        WordNextToFirst currentWordScript = currentWord.GetComponent<WordNextToFirst>();
        currentWordScript.parentDialogue = this;

        if (isFirstWord) //We have to mark the word if it is the first one to spawn for this line, as its position will drive the rest of the words' positions.
            currentWordScript.firstWord = true;
        else
        {
            currentWordScript.firstWord = false;
            currentWordScript.previousWord = previousWordRectTransform; //This will be useful as every word that spawns after the first one will use the previously spawned word
                                                                        //position in order to place itself.
        }
    }

    //Must be called each time we need to clear the bubble (Most of the time, it's to prepare the next line to be displayed).
    //This will also immediately start to display the next line of dialogue.
    public void ResetDialogueBubble ()
    {
        WordNextToFirst[] everySingleWord = GameObject.FindObjectsOfType<WordNextToFirst>();

        foreach (WordNextToFirst word in everySingleWord)
            GameObject.Destroy(word.gameObject);

        firstWordMustSpawn = true;
        DisplayNewText(textToDisplay);
        iterator = 0;

        finishedLine = false;
        NextLineLogo.SetActive(false);
    }

	// Update is called once per frame
	void Update ()
    {
        //This is used when the player press "Next Line" when the line is still being in the process of being displayed
        if (skipThisLine && !finishedLine)
        {
            nextCharacterTimer = displaySpeedRate + 1;
        }
        else if (skipThisLine && finishedLine)
        {
            skipThisLine = false;
        }


       nextCharacterTimer += Time.deltaTime;

        //Each time the Timer is greater than the set display speed rate, we create the next word to be displayed.
        if (nextCharacterTimer > displaySpeedRate && iterator < currentWordsToDisplay.Length)
        {
            GameObject spawnedNewWord = SpawnWord();
            setWordStartPos(firstWordMustSpawn, spawnedNewWord);
            firstWordMustSpawn = false;
            spawnedNewWord.transform.SetParent(bubbleBackRectTransform.transform);
            previousWordRectTransform = spawnedNewWord.GetComponent<RectTransform>();

            iterator++;
            nextCharacterTimer = 0;
        }

        //If the iterator is greater than the number of words to display, then we're done with this line, let's prepare ourselves to display the next one (Or to close the dialogue...)
        if (iterator >= currentWordsToDisplay.Length)
        {
            finishedLine = true;
            NextLineLogo.SetActive(true);
        }
	}
    
    //This method actually separate each words of the line into a string array that will be used to spawn each word's gameObject individually
    public void DisplayNewText(string newText)
    {
        currentWordsToDisplay = textToDisplay.Split(' ');
    }
}
