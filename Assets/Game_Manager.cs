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
    public AudioClip eat_sound;
    public AudioClip hey_sound;
    AudioSource audio;
    public Image[] Drunkmeter;
    int current_number_image = 0;
    public Color desactivado;
    int multiplier = 1;




    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void drink () {
        score=score+multiplier;
        number_of_drinks++;
        score_text.text = ""+score;
        dificulty += 0.05f;
        Debug.Log("pinta+1");
        //sound
        audio.clip = drink_sound;
        audio.Play();

        if (number_of_drinks%2==0)
        {
            Drunkmeter [current_number_image].color= Color.white;
            current_number_image++;
        }

        if (number_of_drinks==8)
        {
            dificulty *= -1;
            //sound
            audio.clip = burp_sound;
            audio.Play();
            StartCoroutine(borrachera());
        }
    }


    public void eat()
    {
        //sound
        audio.clip = eat_sound;
        audio.Play();


        if (number_of_drinks>0)
        {
        number_of_drinks--;
            if (number_of_drinks % 2 != 0)
            {
                current_number_image--;
                Drunkmeter[current_number_image].color = desactivado;

            }
        }
    }

    public void bonus ()
    {
        multiplier=2;
        //sound
        audio.clip = hey_sound;
        audio.Play();
        StartCoroutine(bonus_time());

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
        current_number_image = 0;
    }

    IEnumerator bonus_time()
    {
        yield return new WaitForSeconds(5);
        multiplier = 1;
    }
}
