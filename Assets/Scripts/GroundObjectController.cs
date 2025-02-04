using Unity.VisualScripting;
using UnityEngine;

public class GroundObjectController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 15f;


        Vector3 newRotation = transform.localEulerAngles;

        if (Input.GetKey(KeyCode.RightArrow)) {
            newRotation.z -= Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            newRotation.z += Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            newRotation.x += Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            newRotation.x -= Time.deltaTime * speed;
        }

        transform.localEulerAngles = newRotation;
    }
}
