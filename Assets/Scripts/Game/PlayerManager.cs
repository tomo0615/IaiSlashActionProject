using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public GameObject player;

    public Transform playerTransform { get; private set; }

    private void Awake()
    {
        playerTransform = player.transform;
    }
}
