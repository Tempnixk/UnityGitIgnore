using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if(Input.GetKeyDown(keyToPress))
          {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit(); 
                
                if(Mathf.Abs(transform.position.y)>0.25) //버튼과 노트 사이의 거리가 -0.25보다 크고 0.25보다 작을때 Hit
                { 
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if(Mathf.Abs(transform.position.y)>0.05f) //버튼과 노트 사이의 거리가 -0.05보다 크고 0.05보다 작을 때 Hit
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else //버튼과 노트 사이의 거리가 -0.25~0.25까지 일 때 Hit
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
            }
          }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;

                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);

            }
        }
        
    }
}
