using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // Permite usar o novo sistema de Input (teclado, etc)

public class Movimento : MonoBehaviour // Classe responsável pelo movimento do foguetao
{
    [SerializeField] InputAction thrust; // Ação de impulso (para subir)
    [SerializeField] InputAction rotation; // Ação de rotação (esquerda/direita)
    [SerializeField] float thrustStrength = 100f; // Força do impulso
    [SerializeField] float rotationStrength = 100f; // Velocidade de rotação
    [SerializeField] AudioClip mainEngine; // Som do motor principal
    [SerializeField] ParticleSystem mainEngineParticles; // Partículas do motor

    Rigidbody rb; // Referência ao Rigidbody (componente de física do fuguetao)
    AudioSource audioSource; // Componente para tocar sons

    private void Start() // Isto corre quando o jogo começa.
    {
        rb = GetComponent<Rigidbody>(); // Obtém o componente Rigidbody do foguetao
        audioSource = GetComponent<AudioSource>(); // Obtém o componente AudioSource do foguetao
    }

    private void OnEnable() // Isto ativa os controlos( A , D , space )
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate() // Este método corre em intervalos fixos(ótimo para física).
    {
        ProcessThrust(); // para levantar o foguetao
        ProcessRotation(); // para rodar o foguetao
    }

    /* Verifica se o jogador está a carregar no botão de impulso.
     Se sim, utiliza o metodo start thrusting. Se não, utiliza o stopthrusting*/
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

    void StartThrusting() // Método para iniciar o impulso
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime); // Aplica uma força para cima ao Rigidbody, depende da taxa fixa de atualização do jogo
        if (!audioSource.isPlaying) // Verifica se o som do motor não está a tocar
        {
            audioSource.PlayOneShot(mainEngine); // toca o som mainEngine uma vez (sem repetir continuamente).
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play(); // Garante que as partículas do motor só começam a tocar se ainda não estiverem a tocar.
        }
    }
    void StopThrusting()
    {
        audioSource.Stop(); // Para o som do motor 
        mainEngineParticles.Stop(); // Para as partículas do motor
    }



    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>(); //  lê o valor atual da entrada de rotação
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
        rb.freezeRotation = true; // Congela a rotação do Rigidbody para evitar que a física o faça rodar

        //Roda o objeto manualmente em torno do eixo Z(direção Vector3.forward), multiplicando pela força de rotação e pelo tempo da física.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime); 
        
        rb.freezeRotation = false; // Descongela a rotação do Rigidbody para permitir que a física funcione normalmente
    }
}
