using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    Player _player;

    private Animator _anim;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.LogError("Enemy AudioSource is null");
        }
        if(_player == null)
        {
            Debug.LogError("Player not found");
        }
        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animator not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down* _enemySpeed* Time.deltaTime);
        if(transform.position.y <= -7f)
        {
            float randomX = Random.Range(-9.3f,9.3f);
            transform.position = new Vector3(randomX,7f,0);
        }
    }

     private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player!=null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.35f);
        }
        if(other.transform.tag == "laser")
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.35f);
        }
        
    }
}
