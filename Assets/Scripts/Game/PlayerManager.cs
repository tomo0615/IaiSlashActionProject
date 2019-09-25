using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public GameObject player;

    public Transform playerTransform { get; private set; }

    protected override void Awake()
    {
        playerTransform = player.transform;
    }
}
