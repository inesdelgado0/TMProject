using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ColisoesHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f; // Tempo de espera antes de mudar de n�vel ou reiniciar (em segundos)
    [SerializeField] AudioClip successAudio; // Som tocado ao completar o n�vel com sucesso
    [SerializeField] AudioClip crashAudio; // Som tocado ao colidir com um obst�culo

    [SerializeField] ParticleSystem successParticles; // Part�culas tocadas ao completar o n�vel com sucesso
    [SerializeField] ParticleSystem crashParticles; // Part�culas tocadas ao colidir com um obst�culo

    AudioSource audioSource; // Componente para tocar sons

    bool isControllable = true; // Se o jogador pode controlar o foguete
    bool isCollidable = true; // Se o foguete pode colidir com outros objetos

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>(); // Obt�m o componente AudioSource do foguete
    }

    private void Update() 
    {
        RespondToDebugkeys(); // Responde a teclas de depura��o (para testes)
    }

    void RespondToDebugkeys()
    {
       if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }


    private void OnCollisionEnter(Collision other) // Esta fun��o � chamada automaticamente sempre que o foguet�o colide com algo.
    {
        // Se o jogador j� n�o puder controlar ou se as colis�es estiverem desativadas, ignora a colis�o.
        if (!isControllable || !isCollidable)
        {
            return;
        }

        switch (other.gameObject.tag) //analisa a tag do objecto com que o foguetao colide 
        {
            case "Amigavel":
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop(); 
        audioSource.PlayOneShot(successAudio); 
        successParticles.Play(); 
        GetComponent<Movimento>().enabled = false; // Desativa o script Movimento, para impedir mais a��es.
        Invoke("LoadNextLevel", levelLoadDelay); // Chama o m�todo LoadNextLevel ap�s um atraso definido.
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        GetComponent<Movimento>().enabled = false; 
        Invoke("ReloadLevel", levelLoadDelay);

    }

    void LoadNextLevel()
    {
         int currentScene = SceneManager.GetActiveScene().buildIndex;
         int nextScene = currentScene + 1;

         if (nextScene == SceneManager.sceneCountInBuildSettings)
         {
              nextScene = 0; // Loop back to the first scene
         }
            
         SceneManager.LoadScene(nextScene);

    }

    void ReloadLevel()
    {
         int currentScene = SceneManager.GetActiveScene().buildIndex;
         SceneManager.LoadScene(currentScene);
    }

}
