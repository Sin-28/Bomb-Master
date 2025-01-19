// Decompiled with JetBrains decompiler
// Type: Portal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public class Portal : MonoBehaviour
{
  public SpriteRenderer sprite;
  public GameObject blackBoard;
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
    this.Invoke("SlideDown", 0.5f);
    this.Invoke("ChangePlayMode", 1.5f);
    this.Invoke("EnterMotion", 1.5f);
    this.Invoke("ClearMoveScene", 1.5f);
  }

  public void SlideDown()
  {
    this.blackBoard.gameObject.GetComponent<Animator>().SetTrigger("doSlide");
  }

  public void ClearMoveScene()
  {
    this.ResetVariable();
    SceneManager.LoadScene("Select Character");
  }

  public void ChangePlayMode() => GameManager.instance.playMode = 1;

  public void EnterMotion() => GameManager.instance.doEnterMotion = true;

  private void ResetVariable()
  {
    GameManager.instance.bombPower = 1;
    GameManager.instance.heart = 3;
    GameManager.instance.maxNum = 1;
    GameManager.instance.squareBomb = 0;
    GameManager.instance.bombNumInstalled = 0;
    GameManager.instance.itemNumUsed = 0;
    GameManager.instance.destroyNum = 0;
    Time.timeScale = 1f;
  }
}
