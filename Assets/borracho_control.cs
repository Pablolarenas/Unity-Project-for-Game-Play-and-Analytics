﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borracho_control : MonoBehaviour {

    Rigidbody2D Borracho;
   
    public Transform camera;
    public GameObject Game_Over;
    Game_Manager gm;



    void Start()
    {
#if UNITY_ANDROID
            Input.gyro.enabled = true;
#endif
        gm = FindObjectOfType<Game_Manager>();
        Borracho = GetComponent<Rigidbody2D>();

        Borracho.AddForce(new Vector2(0, gm.speed));

    }

    void Update()
    {
        //Borracho's walk function
        Borracho.AddForce(new Vector2(gm.dificulty * Input.gyro.rotationRate.y, 0));
        camera.localPosition = new Vector3(0,transform.localPosition.y+2.5f,-20);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "cop")
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
            Game_Over.SetActive(true);
        }

        else if (coll.gameObject.tag == "hole")
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
            Game_Over.SetActive(true);
        }

        else if (coll.gameObject.tag == "car")
        {
            StartCoroutine(hit_car());
            //////Time.timeScale = 0;
            //////Debug.Log("Game Over");
            //////Game_Over.SetActive(true);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
   
        if (coll.gameObject.tag == "pinta")
        {
            gm.drink();
            Destroy(coll.gameObject);
        }

    }

    IEnumerator hit_car()
    {
        yield return new WaitForSeconds(2);
   
        Borracho.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(0.1f);

        Borracho.constraints = RigidbodyConstraints2D.None;
        Borracho.AddForce(new Vector2(0, gm.speed));
    }
}