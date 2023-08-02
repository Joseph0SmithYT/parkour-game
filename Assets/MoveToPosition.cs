using UnityEngine;
using System.Collections;

public class MoveToPosition : MonoBehaviour
{
    Vector3 targetPosition;
    public Vector3 targetPos1;
    public Vector3 targetPos2;
    public float speed = 1f;
    public float threshold = 0.1f;

    private void Start()
    {
        transform.position = targetPos1;
        targetPosition = targetPos2;
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, targetPosition) > threshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        if (Vector3.Distance(transform.position, targetPosition) <= threshold)
        {
            if (targetPosition == targetPos1)
                targetPosition = targetPos2;
            else if (targetPosition == targetPos2)
                targetPosition = targetPos1;

            StartCoroutine(MoveToTarget());
        }
    }
}