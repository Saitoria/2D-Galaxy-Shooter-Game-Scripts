using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleshotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private bool _isTrippleShot = false;

    [SerializeField]
    private GameObject _shieldVisualizer;
    

    private SpawnManager _spawnManager;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    void Start()
    {
        transform.position = new Vector3(0,-3,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is null");
        }
        if(_spawnManager == null)
        {
            Debug.Log("Spawn manager is null");
        }

        if(_uiManager == null)
        {
            Debug.Log("UI Manager is null");
        }
        else{
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
        Fire();
        }
    }

    void CalculateMovement()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);

        transform.Translate(direction* Time.deltaTime* _speed);
       
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.95f,0),0);

        if(transform.position.x >= 11.27f)
        {
            transform.position = new Vector3(-11.27f,transform.position.y,0);
        }
        else if(transform.position.x <= -11.27f)
        {
            transform.position = new Vector3(11.27f,transform.position.y,0);
        }
    }

    void Fire()
    {
            _canFire = Time.time + _fireRate;
            if( _isTrippleShot == true)
            {
               Instantiate(_trippleshotPrefab,new Vector3(transform.position.x,transform.position.y,0),Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab,new Vector3(transform.position.x,transform.position.y+1.05f,0),Quaternion.identity);
            }
            _audioSource.Play();
        
    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return;
            
        }

        _lives -= 1;
        if(_lives<3)
        {
            _rightEngine.SetActive(true);
        }
        if(_lives<2)
        {
            _leftEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);
        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TrippleShotActive()
    {
        _isTrippleShot = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
        
    }

    IEnumerator TrippleShotPowerDownRoutine()
    {

            yield return new WaitForSeconds(5.0f);
            _isTrippleShot = false;
    }

    public void SpeedPowerupActive()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }

    public void ShieldPowerupActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldPowerDownROutine());
    }

    IEnumerator ShieldPowerDownROutine()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldVisualizer.SetActive(false);
        _isShieldActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
