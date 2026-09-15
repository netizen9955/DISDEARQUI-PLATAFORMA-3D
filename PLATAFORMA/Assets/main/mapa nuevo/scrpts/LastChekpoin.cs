using UnityEngine;

public class LastChekpoin : MonoBehaviour
{
    [SerializeField]
    private Vector3 lastCheckpoint;

    [SerializeField]
    private int health = 100;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Respawn();
        }
    }

    public void SetCheckpoint(Vector3 checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    private void Respawn()
    {
        if (_controller != null)
            _controller.enabled = false;

        transform.position = lastCheckpoint;

        if (_controller != null)
            _controller.enabled = true;

        health = 100;
    }
}