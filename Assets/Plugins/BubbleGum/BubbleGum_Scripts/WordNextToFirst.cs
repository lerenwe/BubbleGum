using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WordNextToFirst : MonoBehaviour {

    [HideInInspector]
    public RectTransform previousWord;
    [HideInInspector]
    public float targetYPos = 0;
    [HideInInspector]
    public bool firstWord = false;
    [HideInInspector]
    public DialogueDisplayer parentDialogue;

    RectTransform mahRectTransform;

    [HideInInspector]
    public Vector3 startPos;
    bool secondUpdateCall = false;
    bool begin = true;
    bool brokeLineOnce = false;



	// Use this for initialization
	void Start ()
    {
        mahRectTransform = gameObject.GetComponent<RectTransform>();
        mahRectTransform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        #region Set Word Start Position
        if (secondUpdateCall) //The positioning itself is getting done on the second frame rendered after this gameObject creation in order to avoid getting missing components
        {
            float targetxPos = 0f;
            float targetyPos = 0f;

            if (firstWord)
            {
                //We need to place the first word in the upper-left corner of the bubble, so let's get the corners first.
                Vector3[] corners = new Vector3[4];
                parentDialogue.bubbleBackRectTransform.GetWorldCorners(corners);

                targetxPos = corners[1].x + mahRectTransform.rect.width * parentDialogue.canvas.scaleFactor / 2 + parentDialogue.xMargin * parentDialogue.canvas.scaleFactor;
                targetyPos = corners[1].y - mahRectTransform.rect.height * parentDialogue.canvas.scaleFactor / 2 - parentDialogue.yMargin * parentDialogue.canvas.scaleFactor;

            if (begin) //If this is the start of our dialogue line, we immediately tell the dialogue script this is the first word.
                parentDialogue.firstWordFromPreviousLine = mahRectTransform;
            }
            else //Else we position this word according to the line's first word
            {
                targetxPos = previousWord.position.x + mahRectTransform.rect.width * parentDialogue.canvas.scaleFactor / 2 + previousWord.rect.width * parentDialogue.canvas.scaleFactor / 2;
                targetyPos = previousWord.position.y;
            }

            Vector2 targetStartPos = new Vector2(targetxPos, targetyPos);

            //Let's then check if our target position for this word gets out of the bubble, if yes, we'll break the line now
            if (IsLineOverflowingHorizontally(targetxPos, targetyPos))
            {
                transform.SetParent(parentDialogue.bubbleBackRectTransform.transform);
                startPos = BreakLine();
            }
            else
                startPos = targetStartPos; //Else, it's ok, we can set the calculated position
            #endregion

            mahRectTransform.position = startPos;

            begin = false;
        }
            secondUpdateCall = true;
    }

    //Check if the current word is overflowing out of the bubble
    bool IsLineOverflowingHorizontally(float targetXPos, float targetYPos)
    {
        float currentWordXBoundary = targetXPos + mahRectTransform.rect.width * parentDialogue.canvas.scaleFactor / 2;
        float currentBubbleXBoundary = parentDialogue.bubbleBackRectTransform.position.x + parentDialogue.bubbleBackRectTransform.rect.width * parentDialogue.canvas.scaleFactor / 2;


        if (currentWordXBoundary > currentBubbleXBoundary)
        {
            return true;
        }

        return false;
    }

    //Break this line by putting the current word on the line below
    Vector2 BreakLine ()
    {
        float targetXPos;
        float targetYPos;

        if (!brokeLineOnce)
        {
            Vector3[] corners = new Vector3[4];
            parentDialogue.bubbleBackRectTransform.GetWorldCorners(corners);

            targetXPos = parentDialogue.firstWordFromPreviousLine.position.x - (parentDialogue.firstWordFromPreviousLine.rect.width / 2) * parentDialogue.canvas.scaleFactor 
                            + (mahRectTransform.rect.width / 2) * parentDialogue.canvas.scaleFactor;

            targetYPos = parentDialogue.firstWordFromPreviousLine.position.y - (mahRectTransform.rect.height / 2) * parentDialogue.canvas.scaleFactor 
                            - parentDialogue.spaceBetweenLines * parentDialogue.canvas.scaleFactor;
        }
        else //If we already broke this line, we return the same value, we won't have to change the position of the word
        {
            targetXPos = mahRectTransform.position.x;
            targetYPos = mahRectTransform.position.y;
        }
        
        parentDialogue.firstWordFromPreviousLine = mahRectTransform;
        brokeLineOnce = true;

        return new Vector2(targetXPos, targetYPos);
    }
}
