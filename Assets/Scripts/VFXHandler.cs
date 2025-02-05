using UnityEngine;

public class VFXHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab to spaen when hit and destroyed")] GameObject _mainExplosionChunk;
    [SerializeField, Tooltip("Less common prefab when hit and destroyed")] GameObject _secondaryExplosionChunk;
    [SerializeField, Tooltip("Min explosion chunks to spawn")] int _minChunks = 10;
    [SerializeField, Tooltip("Max explosion chunks to spawn")] int _maxChunks = 20;
    [SerializeField, Tooltip("Force of explosion")] float _explosionForce = 1500f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnExplosion()
    {
        //spawn a random number of main chunks
        int rand = Random.Range(_minChunks,_maxChunks);
        if (_mainExplosionChunk)
            for (int i = 0; i < rand; i++)
                SpawnSubObject(_mainExplosionChunk);

        //no spawn the secondary object, but only half the amount
        rand /= 2;
        if(_secondaryExplosionChunk)
            for(int i = 0;i < rand; i++)
                SpawnSubObject(_secondaryExplosionChunk);
    }

    void SpawnSubObject(GameObject prefab)
    {
        //get a random piint around the object, should prevnt collsion with parent
        Vector3 pos = transform.position;
        pos += Random.onUnitSphere * 0.8f;

        GameObject newObj = Instantiate(prefab, pos, Quaternion.identity);

        //give the chunk a random velocity
        Rigidbody rb = newObj.GetComponent<Rigidbody>();
        rb?.AddExplosionForce(_explosionForce, transform.position, 1f);
    }
}
