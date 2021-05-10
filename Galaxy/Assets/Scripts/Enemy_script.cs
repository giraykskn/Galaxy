using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Animator _enemyAnim;
    private bool _isAlive;
    // Start is called before the first frame update

    private Player_script _player;

    [SerializeField]
    private AudioClip _explosionAudio;
    [SerializeField]
    private AudioSource _audioSource;
    void Start()
    {
        _isAlive = true;
        _player = GameObject.Find("Player").GetComponent<Player_script>();
        _enemyAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio source is null");
        }
        else
        {
            _audioSource.clip = _explosionAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if(transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _isAlive)
        {
            Player_script player = other.transform.GetComponent<Player_script>();
            if(player != null)
            {
                player.Damage();
            }
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            _isAlive = false;
            Destroy(this.gameObject,2.6f);
        }

        if (other.tag == "Laser" && _isAlive)
        {
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            _isAlive = false;
            Destroy(other.gameObject);
            Destroy(this.gameObject,2.6f);
        }

        

    }
}
