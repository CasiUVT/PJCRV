using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    private bool shouldFollow = true;

    public void SetFollowing(bool follow)
    {
        shouldFollow = follow;
    }

    void Update()
    {
        if (shouldFollow)
        {
            transform.position = Player.position;
        }
    }
}