using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    private Vector3 lastPosition;
    float lastMoveTime;
    public GameObject[] encountCharactors;

    [System.NonSerialized]
    public bool talking = false;

    public GameObject speechObject ;

    string lastEncount = "";

    Dictionary<string, GameObject> encountDic;

    bool upPushed = false;
    bool downPushed = false;
    bool leftPushed = false;
    bool rightPushed = false;


    // Use this for initialization
    void Start () {
        lastPosition = transform.position;

        encountDic = new Dictionary<string, GameObject>();

        foreach ( GameObject g in encountCharactors)
        {
            encountDic.Add(g.tag, g);
        }
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (talking)
        {
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            GetComponent<Animator>().SetInteger("Direction", 1);
            lastMoveTime = Time.time;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * ( -1.0f * speed );
            GetComponent<Animator>().SetInteger("Direction", 2);
            lastMoveTime = Time.time;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            GetComponent<Animator>().SetInteger("Direction", 3);
            lastMoveTime = Time.time;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * (-1.0f * speed);
            GetComponent<Animator>().SetInteger("Direction", 4);
            lastMoveTime = Time.time;
        }

        if (upPushed)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            GetComponent<Animator>().SetInteger("Direction", 1);
            lastMoveTime = Time.time;
        }
        if (downPushed)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * (-1.0f * speed);
            GetComponent<Animator>().SetInteger("Direction", 2);
            lastMoveTime = Time.time;
        }
        if (rightPushed)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            GetComponent<Animator>().SetInteger("Direction", 3);
            lastMoveTime = Time.time;
        }
        if (leftPushed)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * (-1.0f * speed);
            GetComponent<Animator>().SetInteger("Direction", 4);
            lastMoveTime = Time.time;
        }

        if (this.transform.position == lastPosition && ( Time.time - lastMoveTime ) >  0.1f )
        {
            GetComponent<Animator>().SetInteger("Direction", 0);
        }


        lastPosition = transform.position;
    }


    void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log(collider.gameObject.tag);

        GameObject go = encountDic[collider.gameObject.tag];
        if ( go == null)
        {
            return;
        }
        go.SetActive(true);

        if (lastEncount != collider.gameObject.tag)
        {
            if (talking == false)
            {
                speechObject.SetActive(true);
                speechObject.GetComponent<Speech>().startSpeech(go.GetComponent<Encounter>().sentences);
                talking = true;
            }
        }

        lastEncount = collider.gameObject.tag;
    }

    public void upButtonPushed()
    {
        upPushed = true;
    }

    public void downButtonPushed()
    {
        downPushed = true;
    }

    public void leftButtonPushed()
    {
        leftPushed = true;
    }

    public void rightButtonPushed()
    {
        rightPushed = true;
    }

    public void buttonUpped()
    {
        upPushed = false;
        downPushed = false;
        leftPushed = false;
        rightPushed = false;
    }

}
