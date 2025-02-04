using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField, Tooltip("Magnitude of the shake effect")] float _shake = 0.05f;
    Vector3 _startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //store the starting position
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if enabled, give camera a little shake
        Vector3 newPosition = new Vector3();
        newPosition.x = _startPos.x + Random.Range(-_shake, _shake);
        newPosition.y = _startPos.y + Random.Range(-_shake, _shake);
        newPosition.z = _startPos.z + Random.Range(-_shake, _shake);

        transform.position = newPosition;
    }
}
