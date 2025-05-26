using UnityEngine;

public class BalloonHint : MonoBehaviour
{
    public enum BalloonType { Question, Exclamation }
    public BalloonType type;

    public void HideBalloon()
    {
        gameObject.SetActive(false);
    }
}