using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

            if (player.transform.localScale.x > 0)
            {
                playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
            }
            else if (player.transform.localScale.x < 0)
            {
                playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
            }

            transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
        }
    }
}
