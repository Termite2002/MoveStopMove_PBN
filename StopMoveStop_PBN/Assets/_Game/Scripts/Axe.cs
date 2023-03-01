using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float rotareSpeed;

    private void OnEnable()
    {
        Invoke(nameof(DestroyAxe), 3f);

        // Reset status of axe
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.right, rotareSpeed);
    }
    void DestroyAxe()
    {
        ObjectPool.Instance.ReturnToPool(gameObject);
        gameObject.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
