using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField] float teleportDistance_x = 9f;
    [SerializeField] float teleportDistance_y = 9f;
    [SerializeField] bool isRightOrUp = false;


    public void SetOffset(float x, float y)
    {
        teleportDistance_x = x;
        teleportDistance_y = y;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (!isRightOrUp)
        {
            other.transform.position += new Vector3(teleportDistance_x, teleportDistance_y, 0);
        }
        else
        {
            other.transform.position -= new Vector3(teleportDistance_x, teleportDistance_y, 0);
        }

    }
}
