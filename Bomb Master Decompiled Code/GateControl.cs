// Decompiled with JetBrains decompiler
// Type: GateControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class GateControl : MonoBehaviour
{
  private BoxCollider2D boxCollider;
  private SpriteRenderer sprite;
  private bool setActive;
  private float time;
  private float activeTime = 0.5f;

  private void Awake()
  {
    this.boxCollider = this.GetComponent<BoxCollider2D>();
    this.sprite = this.GetComponent<SpriteRenderer>();
  }

  private void Start() => this.Invoke("CloseGate", 1.2f);

  private void FixedUpdate()
  {
    if (!this.setActive)
      return;
    if ((double) this.time < (double) this.activeTime)
      this.sprite.color = new Color(1f, 1f, 1f, this.time * 2f);
    else
      this.setActive = false;
    this.time += Time.deltaTime;
  }

  private void CloseGate()
  {
    this.setActive = true;
    this.boxCollider.enabled = true;
  }
}
