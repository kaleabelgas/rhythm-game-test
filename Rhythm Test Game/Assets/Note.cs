using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int Index { get; set; }
    public void Hit()
    {
        GetComponent<Animator>().Play("NoteAnimation");
        GetComponentInChildren<ParticleSystem>().Play();
        return;
    }
    private void OnBecameInvisible()
    {
        NoteSpawner.ActiveNotes[Index].Remove(this);
        Disable();
    }
    private void Update()
    {
        transform.Translate(Vector2.down * LevelManager.FallSpeed * Time.deltaTime, Space.World);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
