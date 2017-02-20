using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borracho_control : MonoBehaviour {

    Rigidbody2D Borracho;
   
    public Transform camera;

    Game_Manager gm;



    void Start()
    {
#if UNITY_ANDROID
            Input.gyro.enabled = true;
#endif
        gm = FindObjectOfType<Game_Manager>();
        Borracho = GetComponent<Rigidbody2D>();

        //initial force applied to the character 
        Borracho.AddForce(new Vector2(0, gm.speed));

    }

    void Update()
    {
        //Character controls 
        Borracho.AddForce(new Vector2(gm.dificulty * Input.gyro.rotationRate.y, 0));
        camera.localPosition = new Vector3(0,transform.localPosition.y+2.5f,-20);
    }

    //Obstacles
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "cop")
        {
            gm.game_over();
            gm.restart_game();
        }

        else if (coll.gameObject.tag == "hole")
        {
            gm.game_over();
        }

        else if (coll.gameObject.tag == "car")
        {
            StartCoroutine(hit_car());
        }

      

    }

    //Collectables and Bonus
    void OnTriggerEnter2D(Collider2D coll)
    {

        Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag == "pinta") {
            gm.drink();
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "food") {
            gm.eat();
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "buddie")
        {
            gm.bonus();
        }
    }


    //after character gets hit by a car 
    IEnumerator hit_car()
    {
        yield return new WaitForSeconds(2);
   
        Borracho.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(0.1f);

        Borracho.constraints = RigidbodyConstraints2D.None;
        Borracho.AddForce(new Vector2(0, gm.speed));
    }
}
