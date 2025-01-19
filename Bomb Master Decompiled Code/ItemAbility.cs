// Decompiled with JetBrains decompiler
// Type: ItemAbility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemAbility : MonoBehaviour
{
  private bool doBounce = true;
  private float time;
  private float size = 2f;
  private float upSizeTime = 0.2f;

  private void FixedUpdate()
  {
    if (!this.doBounce)
      return;
    if ((double) this.time < (double) this.upSizeTime)
      this.transform.localScale = Vector3.one * (float) (1.0 + (double) this.size * (double) this.time);
    else if ((double) this.time < (double) this.upSizeTime * 2.0)
    {
      this.transform.localScale = Vector3.one * (float) (1.0 + (double) this.size * (double) this.upSizeTime * 2.0 - (double) this.size * (double) this.time);
    }
    else
    {
      this.transform.localScale = Vector3.one;
      this.doBounce = false;
    }
    this.time += Time.deltaTime;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      if (this.name == "Bomb Power Up(Clone)" && GameManager.instance.bombPower < 4)
        ++GameManager.instance.bombPower;
      else if (this.gameObject.name == "Bomb Power Down(Clone)" && GameManager.instance.bombPower > 1)
        --GameManager.instance.bombPower;
      else if (this.gameObject.name == "Heart + 1(Clone)" && GameManager.instance.heart < 3)
        ++GameManager.instance.heart;
      else if (this.gameObject.name == "Heart - 1(Clone)" && GameManager.instance.heart > 0)
        --GameManager.instance.heart;
      else if (this.gameObject.name == "Bomb + 1(Clone)" && GameManager.instance.maxNum < 8)
        ++GameManager.instance.maxNum;
      else if (this.gameObject.name == "Bomb - 1(Clone)" && GameManager.instance.maxNum > 1)
        --GameManager.instance.maxNum;
      else if (this.gameObject.name == "Square bomb + 1(Clone)" && GameManager.instance.squareBomb < 4)
        ++GameManager.instance.squareBomb;
      else if (this.gameObject.name == "Square bomb - 1(Clone)" && GameManager.instance.squareBomb > 0)
        --GameManager.instance.squareBomb;
      Object.Destroy((Object) this.gameObject, 0.0f);
    }
    else if (collision.gameObject.tag == "PlayerTwo")
    {
      if (this.name == "Bomb Power Up(Clone)" && GameManager.instance.bombPower2 < 4)
        ++GameManager.instance.bombPower2;
      else if (this.gameObject.name == "Bomb Power Down(Clone)" && GameManager.instance.bombPower2 > 1)
        --GameManager.instance.bombPower2;
      else if (this.gameObject.name == "Heart + 1(Clone)" && GameManager.instance.heart2 < 3)
        ++GameManager.instance.heart2;
      else if (this.gameObject.name == "Heart - 1(Clone)" && GameManager.instance.heart2 > 0)
        --GameManager.instance.heart2;
      else if (this.gameObject.name == "Bomb + 1(Clone)" && GameManager.instance.maxNum2 < 8)
        ++GameManager.instance.maxNum2;
      else if (this.gameObject.name == "Bomb - 1(Clone)" && GameManager.instance.maxNum2 > 1)
        --GameManager.instance.maxNum2;
      else if (this.gameObject.name == "Square bomb + 1(Clone)" && GameManager.instance.squareBomb2 < 4)
        ++GameManager.instance.squareBomb2;
      else if (this.gameObject.name == "Square bomb - 1(Clone)" && GameManager.instance.squareBomb2 > 0)
        --GameManager.instance.squareBomb2;
      Object.Destroy((Object) this.gameObject, 0.0f);
    }
    else
    {
      if (!(collision.gameObject.tag == "Explosion"))
        return;
      Object.Destroy((Object) this.gameObject, 0.1f);
    }
  }
}
