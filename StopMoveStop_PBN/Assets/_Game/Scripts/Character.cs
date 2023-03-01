using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public List<Character> targetListInRange = new List<Character>();

    public GameObject axePrefab;
    [SerializeField] protected float throwForce = 400f;

    //public Transform targetPosition;
    [SerializeField] protected Transform throwPoint;

    public virtual void Start()
    {
        LevelController.Instance.allAlivePosition.Add(this);
    }

    
    void Update()
    {

    }

    public virtual void Attack(Vector3 targetPosition) 
    {
        //GameObject newAxe = Instantiate(axePrefab, transform.position, transform.rotation);

        GameObject newAxe = ObjectPool.Instance.GetFromPool();
        newAxe.transform.position = throwPoint.position;
        newAxe.SetActive(true);

        Vector3 throwDirection = (targetPosition - throwPoint.position).normalized;
        newAxe.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce);

        newAxe.transform.rotation = Quaternion.LookRotation(throwDirection);
    }

}
