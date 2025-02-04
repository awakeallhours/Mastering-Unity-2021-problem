using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of the bullet")] private float _speed = 4f;
    [SerializeField, Tooltip("Normalized direction of this bullet")] private Vector3 _direction = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos += _direction * (_speed * Time.deltaTime);
        transform.position = newPos;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;

        transform.LookAt(transform.position + _direction);
    }

}
