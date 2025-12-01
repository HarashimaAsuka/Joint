using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D specificTarget; 
    [SerializeField] private Rigidbody2D attackTarget;
    [SerializeField] private float knockbackPower = 3f;

    private DistanceJoint2D joint;

    private void Awake()
    {
        joint = GetComponent<DistanceJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (joint.connectedBody != specificTarget) return;
        if (collision.rigidbody != attackTarget) return;

        Debug.Log("攻撃対象にヒット！ノックバック処理開始");

        // ---- キネマティック用 ノックバック処理 ----
        Vector2 dir = (attackTarget.transform.position - transform.position).normalized;

        // Rigidbody2D が Kinematic の場合は MovePosition が最適
        attackTarget.MovePosition(attackTarget.position + dir * knockbackPower);

        Debug.Log($"Knockback dir={dir}, power={knockbackPower}");
    }
}
