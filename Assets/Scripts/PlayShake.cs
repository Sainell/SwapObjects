using UnityEngine;
using DG.Tweening;

public class PlayShake : MonoBehaviour
{
    private Sequence sequence;

    void Start()
    {
        sequence = DOTween.Sequence();
        ShakePlayButton();
    }

    private void ShakePlayButton()
    {
        sequence.Append(gameObject.transform.DORotate(new Vector3(0, 0, 20), 0.5f, RotateMode.Fast))
            .Append(gameObject.transform.DORotate(new Vector3(0, 0, -20), 1f, RotateMode.Fast))
            .Append(gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f, RotateMode.Fast))
            .SetLoops(-1)
            .AppendInterval(2f);
    }
}
