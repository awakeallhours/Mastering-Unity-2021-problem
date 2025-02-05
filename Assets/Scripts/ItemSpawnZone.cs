using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnZone : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab to spawn in this zone.")]
    private GameObject _itemToSpawn;

    [SerializeField, Tooltip("Number of items to spawn.")]
    private float _itemCount = 30;

    [SerializeField, Tooltip("The area to spawn these items.")]
    private BoxCollider _spawnZone;

    // --- added for chapter 5 --
    [SerializeField, Tooltip("How should these objects be organized when spawned?")]
    private SpawnShape _spawnShape;
    private enum SpawnShape
    {
        Random,
        Circle,
        Grid,
        Count
    }

    [SerializeField, Tooltip("Speed that this group of objects will rotate.")]
    private Vector3 _rotationSpeed;

    void Start()
    {
        // instanciate the objects according to spawn shape
        if (_spawnShape == SpawnShape.Circle)
        {
            SpawnObjectsInCircle();
        }
        else
        {
            for (int i = 0; i < _itemCount; i++)
                SpawnItemAtRandomPosition();
        }

        
    }

    void Update()
    {
        // calculate the new rotation
        // by taking the old rotation
        // and applying the speed parameter
        Vector3 newRot = transform.localEulerAngles;
        newRot += _rotationSpeed * Time.deltaTime;
        transform.localEulerAngles = newRot;
    }

    void SpawnItemAtRandomPosition()
    {
        // start off with the position of the zone
        Vector3 randomPosition;

        // now randomize location based on the size of the associated BoxCollider
        randomPosition.x = Random.Range(_spawnZone.bounds.min.x, _spawnZone.bounds.max.x);
        randomPosition.y = Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y);
        randomPosition.z = Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z);

        // spawn the item prefab at this position
        Instantiate(_itemToSpawn, randomPosition, Quaternion.identity);
    }

    /// <summary>
    /// Go through all the objects and spawn them in a circle.
    /// Radius is determined by the size of the spawn zone collider.
    /// </summary>
    void SpawnObjectsInCircle()
    {
        //List<bool> seatTaken = new List<bool>();
        //Vector3 originalScale = _itemToSpawn.transform.localScale;

        float radius = _spawnZone.bounds.max.x / 2;
        Transform parent = this.gameObject.transform;

        for (int i = 0; i < _itemCount; i++)
        {
            // get the position on the circle to spawn this object
            float angle = i * Mathf.PI * 2 / _itemCount;
            Vector3 pos = Vector3.zero;
            pos.x = Mathf.Cos(angle);
            pos.z = Mathf.Sin(angle);
            pos *= radius;

            // spawn as a child of the parent object
            GameObject newObj = Instantiate(_itemToSpawn, parent);
            newObj.transform.localPosition = pos;

            //scale seems to be what is causing the issues 
            //_itemToSpawn.transform.localScale = originalScale;
        }
    }
}