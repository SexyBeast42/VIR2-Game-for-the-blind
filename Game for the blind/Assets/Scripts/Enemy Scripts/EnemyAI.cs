using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    //Moving agent
    public NavMeshAgent agent;
    
    //Enemy attack animation
    public Animator animator;

    //Enemy noise
    public AudioSource source;
    public List<AudioClip> sounds; //need moving/spawning sounds, dying sounds

    //Enemy variables
    public Transform player;
    public float attackRange, health;
    private bool canPlayNoise;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        //Set bool for noise to true
        canPlayNoise = true;

        //Play a sound when they spawn
        int randomNumber = Random.Range(0, 3);
        source.PlayOneShot(sounds[randomNumber]);
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
        
        //Moves the zombie towards the player
        agent.SetDestination(player.position);
        
        //Check if they should make a noise when they move towards the player
        if (canPlayNoise)
        {
            StartCoroutine(PlayNoise());
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            UpdateHealth();
        }
    }

    public void UpdateHealth()
    {
        health--;
        //Play hurt sound
        int randomTrack = Random.Range(6, 8);
        source.PlayOneShot(sounds[randomTrack]);

        if (health <= 0)
        {
            //Play death sound
            source.PlayOneShot(sounds[3]);
            
            Destroy(gameObject);
        }
    }

    private void AttackPlayer()
    {
        //check if they are within attack distance
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            //Play attack sound
            int randomTrack = Random.Range(4, 6);
            source.PlayOneShot(sounds[randomTrack]);
            
            //Trigger attack animation
            //animator.Play("AnimationName");
        }
    }

    IEnumerator PlayNoise()
    {
        canPlayNoise = false;
        int randomTime = Random.Range(5, 11);
        
        //Play moving sound
        int randomTrack = Random.Range(0, 3);
        source.PlayOneShot(sounds[randomTrack]);
        
        yield return new WaitForSeconds(randomTime);

        canPlayNoise = true;
    }
}
