using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapMovement : MonoBehaviour {
    public float speed = 2.0f;
    public float distance = 1.0f;
    public float waitTime = 1.0f;
    private Vector3 startPos;

    private void Start() {
        startPos = transform.position;
        StartCoroutine(MoveDownUp());
    }

    private IEnumerator MoveDownUp() {
        while (true) {
            yield return StartCoroutine(MoveDown());
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(MoveUp());
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveDown() {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - new Vector3(0f, distance, 0f);
        float time = 0f;
        while (time < 1f) {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
        transform.position = endPosition;
    }

    private IEnumerator MoveUp() {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0f, distance, 0f);
        float time = 0f;
        while (time < 1f) {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
        transform.position = endPosition;
    }
}