using UnityEngine;
using UnityEngine.EventSystems;

public class DragEvent : MonoBehaviour
{
    Vector3 dragPos;
    TargetJoint2D joint;

    public void OnPointerDown(BaseEventData data)
    {
        var p = data as PointerEventData;
        var pos = p.pointerCurrentRaycast.worldPosition;
        Vector3 mousePos = p.position;
        this.dragPos = Camera.main.WorldToScreenPoint(pos) - mousePos;

        // ジョイントを作ってRigidbody2Dを制御する
        this.joint = gameObject.AddComponent<TargetJoint2D>();
        this.joint.autoConfigureTarget = false;

        this.joint.anchor = this.transform.InverseTransformPoint(pos);     // 接続元: ローカル座標で指定
        this.joint.target = pos;                                           // 接続先: ワールド座標で指定
    }

    public void OnDrag()
    {
        // ジョイントの接続先をドラッグで動かす
        this.joint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition + this.dragPos);
    }

    public void OnPointerUp()
    {
        // ジョイントを破棄
        Destroy(this.joint);
    }
}