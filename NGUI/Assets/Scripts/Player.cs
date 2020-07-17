using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;     //플레이어의 이동속도

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up"))          //위키
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else if(Input.GetKey("down"))   //아래키
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }
}
