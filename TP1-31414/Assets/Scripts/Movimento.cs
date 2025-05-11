using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // Permite usar o novo sistema de Input (teclado, etc)

public class Movimento : MonoBehaviour // Classe respons�vel pelo movimento do foguetao
{
    [SerializeField] InputAction thrust; // A��o de impulso (para subir)
    [SerializeField] InputAction rotation; // A��o de rota��o (esquerda/direita)
    [SerializeField] float thrustStrength = 100f; // For�a do impulso
    [SerializeField] float rotationStrength = 100f; // Velocidade de rota��o
    [SerializeField] AudioClip mainEngine; // Som do motor principal
    [SerializeField] ParticleSystem mainEngineParticles; // Part�culas do motor

    Rigidbody rb; // Refer�ncia ao Rigidbody (componente de f�sica do fuguetao)
    AudioSource audioSource; // Componente para tocar sons

    private void Start() // Isto corre quando o jogo come�a.
    {
        rb = GetComponent<Rigidbody>(); // Obt�m o componente Rigidbody do foguetao
        audioSource = GetComponent<AudioSource>(); // Obt�m o componente AudioSource do foguetao
    }

    private void OnEnable() // Isto ativa os controlos( A , D , space )
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate() // Este m�todo corre em intervalos fixos(�timo para f�sica).
    {
        ProcessThrust(); // para levantar o foguetao
        ProcessRotation(); // para rodar o foguetao
    }

    /* Verifica se o jogador est� a carregar no bot�o de impulso.
     Se sim, utiliza o metodo start thrusting. Se n�o, utiliza o stopthrusting*/
    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting() // M�todo para iniciar o impulso
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime); // Aplica uma for�a para cima ao Rigidbody, depende da taxa fixa de atualiza��o do jogo
        if (!audioSource.isPlaying) // Verifica se o som do motor n�o est� a tocar
        {
            audioSource.PlayOneShot(mainEngine); // toca o som mainEngine uma vez (sem repetir continuamente).
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play(); // Garante que as part�culas do motor s� come�am a tocar se ainda n�o estiverem a tocar.
        }
    }
    void StopThrusting()
    {
        audioSource.Stop(); // Para o som do motor 
        mainEngineParticles.Stop(); // Para as part�culas do motor
    }



    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>(); //  l� o valor atual da entrada de rota��o
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength); //roda para a esquerda(anti-horario)
        }

        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength); // roda para a direita(horario)
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Congela a rota��o do Rigidbody para evitar que a f�sica o fa�a rodar

        //Roda o objeto manualmente em torno do eixo Z(dire��o Vector3.forward), multiplicando pela for�a de rota��o e pelo tempo da f�sica.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime); 
        
        rb.freezeRotation = false; // Descongela a rota��o do Rigidbody para permitir que a f�sica funcione normalmente
    }
}
