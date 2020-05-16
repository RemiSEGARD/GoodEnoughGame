﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobProjectile : MonoBehaviour
{
    [SerializeField] private float time = 2f;
    [SerializeField] private float speed = 1f;

    //public AudioClip spawn;
    //AudioSource audio;

    private Damage damage;

    public int strength = 10;
    public string target = "";

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<Damage>();
        //audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(spawn, 0.8F);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            time -= Time.deltaTime;
            if (time <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && target == "player")
        {
            Debug.Log("touch");
            PlayerManager p = other.GetComponent<PlayerManager>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        if (other.CompareTag("MobShooter") && target == "mob")
        {
            Debug.Log(" mobtouch");
            MobShooter p = other.GetComponent<MobShooter>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        if (other.CompareTag("MobChaser") && target == "mob")
        {
            Debug.Log("mobtouch");
            MobChaser p = other.GetComponent<MobChaser>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
    }
}