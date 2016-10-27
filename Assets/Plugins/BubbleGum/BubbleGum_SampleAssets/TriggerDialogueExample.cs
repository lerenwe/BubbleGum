using UnityEngine;
using System.Collections;

public class TriggerDialogueExample : MonoBehaviour {

    Dialogue myDialogue;

	// Use this for initialization
	void Start ()
    {
        myDialogue = GameObject.Find("SampleDialogue").GetComponent<Dialogue>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown ("a"))
        {
            myDialogue.TriggerDialogue();
        }
	}
}
