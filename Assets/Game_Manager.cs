using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    //Game manager variables
    int countdown = 10;
    public GameObject Game_Over;
    int score = 0;
    int number_of_drinks = 0;
    public float dificulty;
    public float speed;
    public Text score_text;

//sounds slots
    public AudioClip drink_sound;
    public AudioClip burp_sound;
    public AudioClip back_normal_sound;
    public AudioClip eat_sound;
    AudioSource[] audio;

//drunkmeter variables
    public Image[] Drunkmeter;
    int current_number_image = 0;
    public Color desactivado;

//value of drinks for score
    int multiplier = 1;


 // Use this for initialization
    void Start () {
        audio = GetComponents<AudioSource>();
        InvokeRepeating("substact_time_to_countdown", 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //drinking effects 
    public void drink () {
        score=score+multiplier;
        number_of_drinks++;
        score_text.text = ""+score;
        dificulty += 0.05f;
        Debug.Log("pinta+1");
        //sound
        audio[0].clip = drink_sound;
        audio[0].Play();

        if (number_of_drinks%2==0)
        {
            Drunkmeter [current_number_image].color= Color.white;
            current_number_image++;
        }

        if (number_of_drinks==8)
        {
            dificulty *= -1;
            //sound
            audio[0].clip = burp_sound;
            audio[0].Play();
            StartCoroutine(borrachera());
        }
    }

    //eatting food effects
    public void eat()
    {
        //sound
        audio[0].clip = eat_sound;
        audio[0].Play();
        countdown += 5;

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

    //bonus effects 
    public void bonus ()
    {
        multiplier=2;
        //sound
        audio[1].Play();
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

// drunkmeter full
    IEnumerator borrachera()
    {
        yield return new WaitForSeconds(5);
        dificulty *= -1;
        audio[0].clip = back_normal_sound;
        audio[0].Play();
        number_of_drinks = 0;
        current_number_image = 0;
    }

// bonus timmer
    IEnumerator bonus_time()
    {
        yield return new WaitForSeconds(5);
        multiplier = 1;
    }

    // coutdown 
    void substact_time_to_countdown()
    {
        countdown--;
        Debug.Log(countdown);

         if (countdown<0)
        {
            game_over();
        }
    }

    //game over
    public void game_over()
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
        Game_Over.SetActive(true);
    }
}

