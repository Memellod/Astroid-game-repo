using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundPlacer : MonoBehaviour
{
    // по часовой сверху 
    [SerializeField] teleport[] borderList;
    [SerializeField] float offset = 2f;

    // Start is called before the first frame update



    void Awake()
    {
        // Make border placement
        Vector3 upper_point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height, 15));

        Vector3 right_point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 15));

        Vector3 lower_point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 15));

        Vector3 left_point = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height/2, 15));

        borderList[0].transform.position = upper_point + new Vector3(0, offset, 0);

        borderList[1].transform.position = right_point + new Vector3(offset, 0, 0);

        borderList[2].transform.position = lower_point + new Vector3(0, -offset, 0);

        borderList[3].transform.position = left_point + new Vector3(-offset, 0, 0);


        // Set teleport distance for each border


        float width = (right_point - left_point).x;
        float height = (upper_point - lower_point).y;

        borderList[0].SetOffset(0, height + offset);
        borderList[1].SetOffset(width + offset, 0);
        borderList[2].SetOffset(0, height + offset);
        borderList[3].SetOffset(width + offset, 0);

    }
}
