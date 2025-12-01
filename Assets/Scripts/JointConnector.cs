using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
public class JointConnector : MonoBehaviour
{
    private DistanceJoint2D joint;

    // ★ 攻撃対象（追従させたくない特定の1体）
    [SerializeField] private Rigidbody2D attackTarget;

    private void Awake()
    {
        joint = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryConnectTo(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryConnectTo(collision.gameObject);
    }

    private void TryConnectTo(GameObject target)
    {
        if (joint == null) return;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning($"{target.name} に Rigidbody2D がありません。");
            return;
        }

        // ★ 敵（攻撃対象）は追従禁止！
        if (rb == attackTarget)
        {
            Debug.Log($"{target.name} は攻撃対象なので joint に接続しません。（攻撃処理のみ反応）");
            return;
        }

        // ★ それ以外のオブジェクトはすべて追従OK
        joint.connectedBody = rb;
        joint.enabled = true;

        float dist = Vector2.Distance(transform.position, rb.transform.position);
        joint.autoConfigureDistance = false;
        joint.distance = dist;

        Debug.Log($"Joint connected to {target.name}. distance={dist:F2}");
    }

    // A が何かを掴んでいるか確認したいとき用
    public bool HasConnectedBody()
    {
        return joint.connectedBody != null;
    }
}
