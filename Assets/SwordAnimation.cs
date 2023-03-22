using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    public int swingType;
    private Transform myTransform;
    private float initialRotation = 60f;
    private float backRotation = -180f;
    private float secondSwingRotation = 270f;

    private float timer = 0f;
    private bool isWaiting = false;
    private bool isRotatingBack = false;
    private bool isClockwise = true;
    private int swingCount = 0;

    void Start()
    {
        if(swingType == 2)
        {
            initialRotation = 300f;
            backRotation = -180f;
            secondSwingRotation = 90f;
        }
        Destroy(this.gameObject, 1.5f);
        myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (true)
        {
            if (isWaiting)
            {
                timer += Time.deltaTime;
                if (timer >= 0.2f)
                {
                    isRotatingBack = true;
                    isWaiting = false;
                    timer = 0f;
                    swingCount++;
                }
            }
            else if (isRotatingBack)
            {
                if (isClockwise)
                {
                    if (swingCount == 1)
                    {
                        myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, Quaternion.Euler(0, 0, secondSwingRotation), Time.deltaTime * (60 / 0.2f));
                    }
                }

                if ((isClockwise && (swingCount == 1 && Mathf.Abs(myTransform.rotation.eulerAngles.z - secondSwingRotation) < 0.1f || swingCount == 2 && Mathf.Abs(myTransform.rotation.eulerAngles.z - backRotation) < 0.2f)) ||
                    (!isClockwise && Mathf.Abs(myTransform.rotation.eulerAngles.z - initialRotation) < 0.1f))
                {
                    isClockwise = !isClockwise; // switch the swing direction
                    if (swingCount < 2)
                    {
                        isWaiting = true;
                        timer = 0f;
                    }
                    else
                    {
                        isRotatingBack = false; // end the swinging motion
                    }
                }
            }

            else
            {
                myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, Quaternion.Euler(0, 0, initialRotation), Time.deltaTime * (60 / 0.1f));

                if (Mathf.Abs(myTransform.rotation.eulerAngles.z - initialRotation) < 0.1f)
                {
                    isWaiting = true;
                    timer = 0f;
                }
            }
        }
    }
}
