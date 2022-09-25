using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("No SpawnManager found");
        }
    }


    void Update()
    {
        transform.Rotate(Vector3.forward*_speed*Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "laser")
        {
            Destroy(other.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject,0.25f);
        }
    }
}
