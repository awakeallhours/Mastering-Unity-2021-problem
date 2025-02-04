using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField, Tooltip("The speed this object rotates at.")] private float _rotationSpeed = 5;

    public static int s_objectsCollected = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y += (_rotationSpeed * Time.deltaTime);
        transform.eulerAngles = newRotation;
    }

    public void onPickedUp(GameObject whoPickedUp)
    {
        s_objectsCollected++;
        Debug.Log(s_objectsCollected + " items picked up.");
        Destroy(gameObject);
    }
}
