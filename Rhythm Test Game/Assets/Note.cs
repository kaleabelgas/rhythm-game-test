using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private ObjectLists noteType;

    public void Hit()
    {
        GameManager.Instance.RemoveObjectFromList(noteType, gameObject);
        gameObject.SetActive(false);
        return;
    }
    private void Update()
    {
        transform.Translate(Vector2.down * LevelManager.FallSpeed * Time.deltaTime, Space.World);
    }

    private void OnBecameInvisible()
    {
        GameManager.Instance.RemoveObjectFromList(noteType, gameObject);
        gameObject.SetActive(false);
    }
}
