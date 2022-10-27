using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearablePiece : MonoBehaviour
{
    public AnimationClip clearNormal;
    private bool isBeingCleared = false;
    public bool IsBeingCleared
    {
        get { return isBeingCleared; }
    }

    protected GridPiece piece;

    void Awake()
    {
        piece = GetComponent<GridPiece>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Clear()
    {
        piece.GridRef.level.OnPieceCleared(piece);
        isBeingCleared = true;
        StartCoroutine(ClearCoroutine());
    }

    private IEnumerator ClearCoroutine()
    {
        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            animator.Play(clearNormal.name);

            yield return new WaitForSeconds(clearNormal.length);
            Destroy(gameObject);
        }
    }

}
