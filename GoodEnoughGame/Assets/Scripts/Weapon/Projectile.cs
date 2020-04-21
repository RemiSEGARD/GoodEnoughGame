﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float time = 2f;
    [SerializeField] private float speed;
    public Vector2 mousePos;
    public Rigidbody2D projectile;
    public float angle;

    public AudioClip spawn;
    AudioSource audio;

    public GameObject texture;
    public int strength = 10;
    public string target = "";
    
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Rigidbody2D>();

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector3 vect = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        Vector3 rotateVector = rotation * vect;

        projectile.AddForce(rotateVector * 30);

        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(spawn, 0.8F);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentVector = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        currentVector.Normalize();
        projectile.AddForce(currentVector * Time.deltaTime * speed);

        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        Vector2 dir = projectile.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        texture.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && target == "player")
        {
            Debug.Log("hit");
            PlayerManager p = other.GetComponent<PlayerManager>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        if(other.CompareTag("MobShooter") && target == "mob")
        {
            MobShooter p = other.GetComponent<MobShooter>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        if(other.CompareTag("MobChaser") && target == "mob")
        {
            MobChaser p = other.GetComponent<MobChaser>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
    }
}