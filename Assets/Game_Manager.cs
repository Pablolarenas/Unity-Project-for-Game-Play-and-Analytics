using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    int score = 0;
    int number_of_drinks = 0;
    public float dificulty;
    public float speed;
    public Text score_text;
    public AudioClip drink_sound;
    public AudioClip burp_sound;
    public AudioClip back_normal_sound;
    AudioSource audio;



    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void drink () {
        score++;
        number_of_drinks++;
        score_text.text = "score: "+score;
        dificulty += 0.05f;
        Debug.Log("pinta+1");
        //sound
        audio.clip = drink_sound;
        audio.Play();

        if (number_of_drinks==5)
        {
            dificulty *= -1;
            //sound
            audio.clip = burp_sound;
            audio.Play();
            StartCoroutine(borrachera());

        }

    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "" + score);

        GUI.Label(new Rect(10, 20, 100, 20), "" + Time.time);
    }


public void restart_game ()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    IEnumerator borrachera()
    {
        yield return new WaitForSeconds(5);
        dificulty *= -1;
        audio.clip = back_normal_sound;
        audio.Play();
        number_of_drinks = 0;
    }
}
