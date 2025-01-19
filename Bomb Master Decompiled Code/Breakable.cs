// Decompiled with JetBrains decompiler
// Type: Breakable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Breakable : MonoBehaviour
{
  public SpriteRenderer sprite;
  private bool destroy;
  private float time;
  private float blinktime = 0.1f;

  private void Awake() => this.sprite = this.GetComponent<SpriteRenderer>();

  private void FixedUpdate()
  {
    if (!this.destroy)
      return;
    if ((double) this.time < (double) this.blinktime)
      this.sprite.color = new Color(1f, 1f, 1f, 0.5f);
    else if ((double) this.time < (double) this.blinktime * 2.0)
      this.sprite.color = new Color(1f, 1f, 1f, 1f);
    else
      this.time = 0.0f;
    this.time += Time.deltaTime;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!(collision.gameObject.tag == "Explosion"))
      return;
    this.destroy = true;
    Object.Destroy((Object) this.gameObject, 0.5f);
  }
}
