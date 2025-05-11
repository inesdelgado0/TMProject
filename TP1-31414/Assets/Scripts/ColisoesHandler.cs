using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ColisoesHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f; // Tempo de espera antes de mudar de nível ou reiniciar (em segundos)
    [SerializeField] AudioClip successAudio; // Som tocado ao completar o nível com sucesso
    [SerializeField] AudioClip crashAudio; // Som tocado ao colidir com um obstáculo

    [SerializeField] ParticleSystem successParticles; // Partículas tocadas ao completar o nível com sucesso
    [SerializeField] ParticleSystem crashParticles; // Partículas tocadas ao colidir com um obstáculo

    AudioSource audioSource; // Componente para tocar sons

    bool isControllable = true; // Se o jogador pode controlar o foguete
    bool isCollidable = true; // Se o foguete pode colidir com outros objetos

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>(); // Obtém o componente AudioSource do foguete
    }

    private void Update() 
    {
        RespondToDebugkeys(); // Responde a teclas de depuração (para testes)
    }

    void RespondToDebugkeys()
    {
       if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }


    private void OnCollisionEnter(Collision other) // Esta função é chamada automaticamente sempre que o foguetão colide com algo.
    {
        // Se o jogador já não puder controlar ou se as colisões estiverem desativadas, ignora a colisão.
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
        GetComponent<Movimento>().enabled = false; // Desativa o script Movimento, para impedir mais ações.
        Invoke("LoadNextLevel", levelLoadDelay); // Chama o método LoadNextLevel após um atraso definido.
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
