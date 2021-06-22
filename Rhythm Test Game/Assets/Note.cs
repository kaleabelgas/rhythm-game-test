using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private ObjectLists noteType;
    [SerializeField] private float duration = 5;

    private float _clock;
    private void OnEnable()
    {
        GameManager.Instance.AddObjectToList(noteType, gameObject);
        _clock = duration;
    }
    public void Hit()
    {
        GameManager.Instance.RemoveObjectFromList(noteType, gameObject);
        gameObject.SetActive(false);
        return;
    }

    private void Update()
    {
        _clock -= Time.deltaTime;

        if(_clock <= 0)
        {
            //gameObject.SetActive(false);
        }
    }
}
