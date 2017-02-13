using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cryro_control : MonoBehaviour {

    Rigidbody2D Borracho;
    public float dificulty;
    public float speed;
    public Transform camera;


    void Start()
    {
#if UNITY_ANDROID
            Input.gyro.enabled = true;
#endif

        Borracho = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
       //Borracho's walk function
        Borracho.AddForce(new Vector2(dificulty * Input.gyro.rotationRate.y, speed));

        camera.localPosition = new Vector3(0,transform.localPosition.y+2.5f,-20);

    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "" + transform.localPosition.y);
        GUI.Label(new Rect(10, 30, 100, 20), "" + Input.gyro.rotationRate.x);
    }
}
