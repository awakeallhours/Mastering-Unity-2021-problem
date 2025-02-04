using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    [SerializeField, Tooltip("Change to health when applied to an object")] float _healthChange = 0;
    [SerializeField, Tooltip("The class of the object that should be damaged")] DamageTarget _applyToTarget = DamageTarget.Player;

    public enum DamageTarget
    {
        Player,
        Enemies,
        All,
        None
    }

    [SerializeField, Tooltip("Should object self destruct on collision")] bool _destroyOnCollision = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObj = collision.gameObject;
        
        //get the healthmanager of the object we've hit

        HealthManager healthManager = hitObj.GetComponent<HealthManager>();
        Debug.Log(hitObj.name);
        if (healthManager && IsValidTarget(hitObj))
        {
            //apply the damage as a negative heath to this object
            healthManager.AdjustCurHealth(_healthChange);

            //shold we self destruct after dealing damage
            if (_destroyOnCollision)
            {
                GameObject.Destroy(gameObject);
            }
        }

    }

    bool IsValidTarget(GameObject possibleTarget)
    {
        if (_applyToTarget == DamageTarget.All)
            return true;

        else if (_applyToTarget == DamageTarget.None)
            return false;

        else if (_applyToTarget == DamageTarget.Player && possibleTarget.GetComponent<PlayerController>())
        {
            Debug.Log("Player hit");
            return true;

        }

        else if (_applyToTarget == DamageTarget.Enemies && possibleTarget.GetComponent<AIBrain>())
            return true;

        //not a valid target
        Debug.Log("Invalid");
        return false;
    }
}
