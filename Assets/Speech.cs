using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour {
    public GameObject textObject;
    public GameObject playerObject;
    string[] speechArray;
    int speechCount;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startSpeech( string[] speechArrayArg)
    {
        speechArray = new string[speechArrayArg.Length];
        for ( int i = 0; i < speechArrayArg.Length; i ++)
        {
            speechArray[i] = speechArrayArg[i];
        }
        speechCount = 0;

        textObject.GetComponent<Text>().text = speechArray[speechCount];

    }

    public void nextText()
    {
        speechCount ++;

        if ( speechCount >= speechArray.Length)
        {
            textObject.GetComponent<Text>().text = "";
            gameObject.SetActive(false);
            playerObject.GetComponent<Player>().talking = false;
        }
        else
        {
            textObject.GetComponent<Text>().text = speechArray[speechCount];
        }
    }

}
