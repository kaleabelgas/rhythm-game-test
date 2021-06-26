using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public void Hit()
    {
        gameObject.SetActive(false);
        return;
    }
    private void Update()
    {
        transform.Translate(Vector2.down * LevelManager.FallSpeed * Time.deltaTime, Space.World);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
