using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadPoint : MonoBehaviour
{
    [SerializeField] private Text pointText;
    [SerializeField] private Character owner;
    [SerializeField] private Vector3 offset;

    public void SetOwner(Character character)
    {
        owner = character;

    }

    public void ChangePointText(int point)
    {
        pointText.text = point.ToString();
    }
    private void Update()
    {
        transform.position = owner.headPoint.position + offset;
    }
}
