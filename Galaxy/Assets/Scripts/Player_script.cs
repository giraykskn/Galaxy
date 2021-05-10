using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _originalSpeed = 5.5f;
    private float _speed = 5.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _nextFire = 0f;
    [SerializeField]
    private int _lives = 3;
    private Spawn_script _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField] 
    private GameObject _playerHurt;
    [SerializeField]
    private GameObject _playerHurt2;
    private int _score = 0;
    [SerializeField]
    private AudioClip _laserShotAudio;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _powerUpAudio;
    [SerializeField]
    private AudioSource _powerUpAudioSource;

    private UI_script _UIManager;
    void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UI_script>();

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<Spawn_script>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is null");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio source is null");
        }
        else
        {
            _audioSource.clip = _laserShotAudio;
        }
        if (_powerUpAudioSource == null)
        {
            Debug.LogError("Audio source is null");
        }
        else
        {
            _powerUpAudioSource.clip = _powerUpAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement_Update();
        Firing_Update();
    }

    void Movement_Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5, 0), transform.position.z);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }

    void Firing_Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            if (_isTripleShotActive)
            {
                Instantiate(_tripleLaserPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }

            _audioSource.Play();
            
        }
    }

    public void Damage()
    {
        if(_isShieldActive)
        {
            _isShieldActive = false;
            _shield.SetActive(false);
            return;
        }
        _lives--;
        _UIManager.UpdateLives(_lives);

        if (_lives <= 0)
        {
            _spawnManager.KillPlayer();
            Destroy(this.gameObject);
        }
        else if (_lives == 2)
        {
            _playerHurt.SetActive(true);
        }
        else if (_lives == 1)
        {
            _playerHurt2.SetActive(true);
        }
            
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(DisableTripleShot());
        _powerUpAudioSource.Play();
    }

    IEnumerator DisableTripleShot()
    {

        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;

    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
        _powerUpAudioSource.Play();
        StartCoroutine(DisableSpeed());
    }

    IEnumerator DisableSpeed()
    {

        yield return new WaitForSeconds(5.0f);
        _speed = _originalSpeed;

    }

    public void ActivateShield()
    {
        _powerUpAudioSource.Play();
        _isShieldActive = true;
        _shield.SetActive(true);
    }

    public void AddScore(int value)
    {
        _score += value;
        _UIManager.UpdateScore(_score);
    }
}
