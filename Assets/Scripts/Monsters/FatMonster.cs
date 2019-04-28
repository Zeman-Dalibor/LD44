﻿using Assets.Scripts.Character;
using UnityEngine;

public class FatMonster : MosterMovement
{
    public float Speed = 0.3f;

    public AudioClip[] ZombieMoanClips;
    private AudioClip[] hitClips;
    private AudioSource soundPlayer;

    private float soundEstimate;
    private readonly System.Random luck = new System.Random();

    // Start is called before the first frame update
    private void Start()
    {
        this.Init();
        this.soundPlayer = this.GetComponent<AudioSource>();
        this.hitClips = GameObject.Find("HitManager").GetComponent<HitManager>().HitClips;
    }

    // Update is called once per frame
    private void Update()
    {
        this.PlayMoan();

        Vector3 plPosition = this.Player.gameObject.transform.position;
        float x = plPosition.x;
        float y = plPosition.y;

        float thisX = this.gameObject.transform.position.x;
        float thisY = this.gameObject.transform.position.y;

        Vector2 dist = new Vector2(x - thisX, y - thisY).normalized;

        this.col.velocity = dist * this.Speed;

        this.SetAnimator(this.col.velocity);
    }

    private void PlayMoan()
    {
        if (soundEstimate < 0)
        {
            this.soundPlayer.clip = this.ZombieMoanClips.Length == 0 ? null : this.ZombieMoanClips[this.luck.Next(this.ZombieMoanClips.Length)];
            this.soundEstimate = this.soundPlayer.clip.length + 5 + this.luck.Next(5);

            this.soundPlayer.Play();
        }
        soundEstimate -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == this.Player)
        {
            float x = collision.gameObject.transform.position.x;
            float y = collision.gameObject.transform.position.y;

            float thisX = this.gameObject.transform.position.x;
            float thisY = this.gameObject.transform.position.y;

            Vector2 dist = new Vector2(thisX - x, thisY - y).normalized;

            this.Player.GetComponent<PlayerController>().BumpPlayerIntoDirection(-dist * 10.0f);
            PlayBump();
        }
    }

    private void PlayBump()
    {
        if (this.hitClips != null && this.hitClips.Length != 0)
        {
            this.soundPlayer.clip = this.hitClips.Length == 0 ? null : this.hitClips[this.luck.Next(this.hitClips.Length)];
            this.soundEstimate = this.soundPlayer.clip.length;

            this.soundPlayer.Play();
        }
    }
}