using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{

    public float Health;
    Animator Anim;
    float restartDelay = 5f;
    float restartTimer = 0f;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().Health;

        if (Health <= 0)
        {
            Anim.SetBool("IsDead", true);

            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if (restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}