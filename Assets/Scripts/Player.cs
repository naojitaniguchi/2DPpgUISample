using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float speed;
    private Vector3 lastPosition;
    float lastMoveTime;

    [System.NonSerialized]
    public bool talking = false;

    public GameObject speechObject ;

    string lastEncount = "";
    private List<string> items;

    public string nextScene;

    bool upPushed = false;
    bool downPushed = false;
    bool leftPushed = false;
    bool rightPushed = false;


    // Use this for initialization
    void Start () {
        lastPosition = transform.position;
        items = new List<string>();

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

    void dispItem(GameObject triggerObjct)
    {
        GameObject go = triggerObjct.GetComponent<ItemStart>().ItemObject;
        if ( go == null)
        {
            return;
        }

        if ( go.activeSelf)
        {
            return;
        }
        go.SetActive(true);
        speechObject.SetActive(true);
        speechObject.GetComponent<Speech>().startSpeech(go.GetComponent<Item>().sentences);
        talking = true;

        items.Add(go.name);

    }
    void dispEncount(GameObject triggerObjct)
    {
        GameObject go = triggerObjct.GetComponent<EncountStart>().EncountObject;
        if (go == null)
        {
            return;
        }


        if (lastEncount != go.name)
        {
            if (talking == false)
            {
                go.SetActive(true);
                speechObject.SetActive(true);
                speechObject.GetComponent<Speech>().startSpeech(go.GetComponent<Encounter>().sentences);
                talking = true;
            }
        }

        lastEncount = go.name;

    }

    bool checkHasKey()
    {
        return items.Contains("key");
    }


    void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log(collider.gameObject.tag);

        if (collider.gameObject.tag == "ItemTrigger")
        {
            dispItem(collider.gameObject);
        }
        
        if ( collider.gameObject.tag == "EncountTrigger")
        {
            dispEncount(collider.gameObject);
        }
        if (collider.gameObject.tag == "ExitTrigger")
        {
            if (checkHasKey())
            {
                SceneManager.LoadScene(nextScene);
            }
        }

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
