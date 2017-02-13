using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cryro_control : MonoBehaviour {

    Rigidbody2D Borracho;
    Game_Manager gm; 

    void Start()
    {

        Borracho = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<Game_Manager>();
    }


    void FixedUpdate() //<----for physics and calculations, more accurate. 
    {
       //Borracho's walk function
        Borracho.AddForce(new Vector2(gm.borracho_sensibility * Input.gyro.rotationRate.y, gm.borracho_speed));

        //camera.localPosition = new Vector3(0,transform.localPosition.y+2.5f,-20);

    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "" + transform.localPosition.y);
        GUI.Label(new Rect(10, 30, 100, 20), "" + Input.gyro.rotationRate.x);
    }
}
