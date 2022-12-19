using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class RapaxController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private AudioClip idleAudio;
    [SerializeField] private AudioClip screamAudio;
    [SerializeField] private AudioSource source;
    private bool isWalkingForward = false;
    private bool isChasing = false;

    private void Update()
    {
        if (isWalkingForward)
        {
            float vertical = walkSpeed;
            Vector3 move = transform.forward * vertical;
            characterController.Move(walkSpeed * Time.deltaTime * move);
        }
        else if (isChasing)
        {
            agent.SetDestination(ProgressManager.Instance.player.transform.position);
        }
    }

    public void WalkForward()
    {
        isWalkingForward = true;
    }

    public void AmbiantNoises()
    {
        source.clip = idleAudio;
        source.loop = true;
        source.Play();
    }

    public void StopAmbiantNoises()
    {
        source.loop = false;
        source.Stop();
    }

    public void Scream()
    {
        source.clip = screamAudio;
        source.loop = false;
        StartCoroutine(ScreamRoutine());
    }

    public void EnableChase(bool v)
    {
        isChasing = v;
        characterController.enabled = !v;
        agent.enabled = v;
        GetComponent<CapsuleCollider>().enabled = v;
        if (v)
        {
            GetComponent<Animator>().Play("Run");
            Scream();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    private IEnumerator ScreamRoutine()
    {
        source.Play();

        while(true)
        {
            float timer = 0.0f;
            float timeToScream = Random.Range(2.717f, 4.5f);

            while (timer <= timeToScream)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            source.Play();
        }
    }
}
