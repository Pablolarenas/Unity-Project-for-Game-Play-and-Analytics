using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    //Game manager variables
    int countdown = 20;
    float countdown_borrachera = 5.0f;
    public GameObject Game_Over;
    int score = 0;
    int number_of_drinks = 0;
    public float dificulty;
    public float speed;
    public Text score_text;
    public Text countdown_text;
    public Text count_down_borrachera;
    public Animator Borracho_state_camera;

    bool is_borrachera = false;

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
    float borrachera_contador = 0;

 // Use this for initialization
    void Start () {
        audio = GetComponents<AudioSource>();
        InvokeRepeating("substact_time_to_countdown", 1, 1);
    }
	
	// Update is called once per frame
	void Update () {

        if (is_borrachera) {
            borrachera_contador += Time.deltaTime;

            count_down_borrachera.text = "" + (int)(countdown_borrachera - borrachera_contador);
        }

        //Debug.Log((int)(countdown_borrachera-Time.time));
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

        if (!is_borrachera)
        {
            if (number_of_drinks % 2 == 0)
            {
                Drunkmeter[current_number_image].color = Color.white;
                current_number_image++;
            }

            if (number_of_drinks == 8)
            {
                dificulty *= -1;
                Borracho_state_camera.SetBool("start", true);

                //sound
                audio[0].clip = burp_sound;
                audio[0].Play();
                is_borrachera = true;
                StartCoroutine(borrachera());
            }

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


    //void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 10, 100, 20), "" + score);

    //    GUI.Label(new Rect(10, 20, 100, 20), "" + Time.time);
    //}


public void restart_game ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }

// drunkmeter full
    IEnumerator borrachera()
    {
        yield return new WaitForSeconds(5);
        is_borrachera = false;
        countdown_borrachera = 5;
        count_down_borrachera.text = "";
        dificulty *= -1;
        audio[0].clip = back_normal_sound;
        audio[0].Play();
        number_of_drinks = 0;
        current_number_image = 0;
        Drunkmeter[0].color = desactivado;
        Drunkmeter[1].color = desactivado;
        Drunkmeter[2].color = desactivado;
        Drunkmeter[3].color = desactivado;
        Borracho_state_camera.SetBool("start", false);
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
        countdown_text.text=""+countdown;

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

