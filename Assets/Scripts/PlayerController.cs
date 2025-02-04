using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody _rigidBody;
    [SerializeField, Tooltip("How much acceleration is applied to this object when directional input is received.")]private float _movementAcceleration = 2;
    [SerializeField, Tooltip("The maximum velocity of this object - keeps the player from moving too fast.")]private float _movementVelocityMax = 2;
    [SerializeField, Tooltip("Upwards force applied when jump key is pressed.")] private float _jumpVelocity = 20;
    [SerializeField, Tooltip("Additional gravitational pull.")] private float _extraGravity = 40;
    private float _movementFriction = 0.1f;

    [SerializeField, Tooltip("The bullet prefab to fire")] private GameObject _bulletToSpawn;
    [SerializeField, Tooltip("The direction that the player is facing")] Vector3 _curFacing = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curSpeed = _rigidBody.linearVelocity;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            curSpeed.x += (_movementAcceleration * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            curSpeed.x -= (_movementAcceleration * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            curSpeed.z += (_movementAcceleration * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            curSpeed.z -= (_movementAcceleration * Time.deltaTime);
        }

        //strore the current facing
        if(curSpeed.x != 0 && curSpeed.y != 0)
            _curFacing = curSpeed.normalized;

        //fire the weapon
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject newBullet = Instantiate(_bulletToSpawn, transform.position, Quaternion.identity);
            Bullet bullet = newBullet.GetComponent<Bullet>();

            if (bullet) { 
            bullet.SetDirection(new Vector3(_curFacing.x, 0f, _curFacing.z));
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) == Input.GetKey(KeyCode.RightArrow))
        {
            curSpeed.x -= (_movementFriction * curSpeed.x);
        }

        if (Input.GetKey(KeyCode.UpArrow) == Input.GetKey(KeyCode.DownArrow))
        {
            curSpeed.z -= (_movementFriction * curSpeed.z);
        }

        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(curSpeed.y) < 1)
        {
            curSpeed.y += _jumpVelocity;
        }
        else
        {
            curSpeed.y -= _extraGravity * Time.deltaTime;
        }

        curSpeed.x = Mathf.Clamp(curSpeed.x, _movementVelocityMax * -1, _movementVelocityMax);
        curSpeed.z = Mathf.Clamp(curSpeed.z, _movementVelocityMax * -1, _movementVelocityMax);

        _rigidBody.linearVelocity = curSpeed;   
    }

    private void OnTriggerEnter(Collider collider)
    {
        //testing collision
        //Debug.Log(collider.gameObject.name + " hit");

        if (collider.gameObject.GetComponent<PickUpItem>())
        {
            PickUpItem item = collider.gameObject.GetComponent<PickUpItem>();
            item.onPickedUp(this.gameObject);
        }
    }
}
