using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerCarCollider;
    private BoxCollider2D currentCollider;
    private bool ispParked = false;
    private void Start()
    {
        currentCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(currentCollider.bounds.Contains(playerCarCollider.bounds.min) && currentCollider.bounds.Contains(playerCarCollider.bounds.max))
        {
            if (!ispParked)
            {
                Debug.Log("Player Wins");
                GameManager.Instance.SetScore = ResultsEnum.Parked;
                ispParked = true;
            }
        }
    }
}
