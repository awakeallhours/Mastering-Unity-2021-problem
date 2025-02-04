using Unity.VisualScripting;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField, Tooltip("The object to follow")] private GameObject _camTarget;
    [SerializeField, Tooltip("Target offset")] private Vector3 _targetOffset;
    [SerializeField, Tooltip("The height off the ground to follow from")] private float _camHeight = 9;
    [SerializeField, Tooltip("The follow distance")] private float _camDistance = -16;
    [SerializeField, Tooltip("The follow distance")] private float _camRotationSpeed = 5;

    private float _currentRotationAngle;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make sure we have a valid target
        if (!_camTarget)
        {
            return;

        }

        // use the target object and offsets to calculate camera position
        Vector3 targetPos = _camTarget.transform.position + _targetOffset;
        targetPos.y += _camHeight;

        Quaternion currentRotation = Quaternion.Euler(0, _currentRotationAngle, 0);
        Vector3 cameraOffset = currentRotation * new Vector3(0, 0, _camDistance);
        Vector3 desiredPos = targetPos + cameraOffset;
        //move camera towards target position

        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * 5.0f);
        transform.LookAt(_camTarget.transform.position + _targetOffset);

        HandleRotationInput();
    }

        private void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            _currentRotationAngle -= _camRotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            _currentRotationAngle += _camRotationSpeed * Time.deltaTime;
        }
    }
    
}

