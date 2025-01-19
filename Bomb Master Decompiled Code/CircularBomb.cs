// Decompiled with JetBrains decompiler
// Type: CircularBomb
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CircularBomb : MonoBehaviour
{
  public GameObject canvasManager;
  public Transform[] prefab;
  public Transform ex;
  public Rigidbody2D rigid;
  public SpriteRenderer sprite;
  private RaycastHit2D rayHitUp;
  private RaycastHit2D rayHitDown;
  private RaycastHit2D rayHitLeft;
  private RaycastHit2D rayHitRight;
  private int layerMask;
  public float bombPower;
  public int bombOwner;
  public bool doExplosion = true;
  private float time;
  private float fallTime = 0.4f;
  private bool reduceFallTime = true;
  private float blinkTime = 0.3f;
  private bool reduceTime;

  private void Awake()
  {
    this.rigid = this.GetComponent<Rigidbody2D>();
    this.sprite = this.GetComponent<SpriteRenderer>();
    this.layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Breakable Wall"));
  }

  private void Start() => Object.Destroy((Object) this.gameObject, 2.5f);

  private void OnDestroy()
  {
    if (!this.doExplosion)
      return;
    if (this.canvasManager.gameObject.name == "CanvasManager")
      this.canvasManager.GetComponent<CanvasManager>().checkBomb[(int) this.transform.position.x - 1, -((int) this.transform.position.y + 1)] = false;
    else if (this.canvasManager.gameObject.name == "ModeManager")
      this.canvasManager.GetComponent<ModeManager>().checkBomb[(int) this.transform.position.x + 5, -((int) this.transform.position.y - 4)] = false;
    this.InstallExplosion();
  }

  private void FixedUpdate()
  {
    this.rayHitUp = Physics2D.Raycast((Vector2) (this.transform.position + Vector3.up * 0.4f), (Vector2) Vector3.up, this.bombPower, this.layerMask);
    this.rayHitDown = Physics2D.Raycast((Vector2) (this.transform.position + Vector3.down * 0.46f), (Vector2) Vector3.down, this.bombPower, this.layerMask);
    this.rayHitLeft = Physics2D.Raycast((Vector2) (this.transform.position + Vector3.left * 0.41f), (Vector2) Vector3.left, this.bombPower, this.layerMask);
    this.rayHitRight = Physics2D.Raycast((Vector2) (this.transform.position + Vector3.right * 0.41f), (Vector2) Vector3.right, this.bombPower, this.layerMask);
    if ((double) this.time < (double) this.fallTime && this.reduceFallTime)
      this.transform.localScale = new Vector3((float) (1.2000000476837158 - (double) this.time / 2.0), (float) (1.2000000476837158 - (double) this.time / 2.0), (float) (1.2000000476837158 - (double) this.time / 2.0));
    else
      this.reduceFallTime = false;
    if ((double) this.time < (double) this.blinkTime)
      this.sprite.color = new Color(1f, this.blinkTime * 2f, this.blinkTime * 2f, 1f);
    else if ((double) this.time < (double) this.blinkTime * 2.0)
      this.sprite.color = new Color(1f, 1f, 1f, 1f);
    else if (this.reduceTime)
    {
      this.reduceTime = false;
      this.blinkTime /= 1.5f;
    }
    else
    {
      this.reduceTime = true;
      this.time = 0.0f;
    }
    this.time += Time.deltaTime;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!(collision.gameObject.tag == "Explosion"))
      return;
    Object.Destroy((Object) this.gameObject, 0.0f);
  }

  private void InstallExplosion()
  {
    this.ex = Object.Instantiate<Transform>(this.prefab[6], this.transform.position, Quaternion.identity);
    Object.Destroy((Object) this.ex.gameObject, 0.2f);
    this.CalculateDistanceUD(this.rayHitUp, 2, 1);
    this.CalculateDistanceUD(this.rayHitDown, 3, -1);
    this.CalculateDistanceLR(this.rayHitLeft, 4, -1);
    this.CalculateDistanceLR(this.rayHitRight, 5, 1);
  }

  private void CalculateDistanceUD(RaycastHit2D rayhit, int index, int sign)
  {
    float num1 = 0.0f;
    if ((Object) rayhit.collider != (Object) null)
    {
      Vector2 position = rayhit.point + new Vector2(0.0f, (float) sign * -0.5f);
      if (position == this.rigid.position && rayhit.collider.gameObject.layer == 6)
        this.ex = (Transform) null;
      else if (sign > 0)
      {
        if (rayhit.collider.gameObject.layer == 6)
        {
          num1 = (float) ((double) position.y - (double) this.rigid.position.y - 1.0);
          this.ex = Object.Instantiate<Transform>(this.prefab[index], (Vector3) position, Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
        else if (rayhit.collider.gameObject.layer == 7)
        {
          if (this.bombOwner == 1)
            ++GameManager.instance.destroyNum;
          else if (this.bombOwner == 2)
            ++GameManager.instance.destroyNum2;
          num1 = position.y - this.rigid.position.y;
          this.ex = Object.Instantiate<Transform>(this.prefab[index], (Vector3) (position + Vector2.up), Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
        for (float y = num1; (double) y > 0.0; --y)
        {
          this.ex = Object.Instantiate<Transform>(this.prefab[0], this.transform.position + new Vector3(0.0f, y, 0.0f), Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
      }
      else
      {
        if (rayhit.collider.gameObject.layer == 6)
        {
          num1 = (float) ((double) this.rigid.position.y - (double) position.y - 1.0);
          this.ex = Object.Instantiate<Transform>(this.prefab[index], (Vector3) position, Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
        else if (rayhit.collider.gameObject.layer == 7)
        {
          if (this.bombOwner == 1)
            ++GameManager.instance.destroyNum;
          else if (this.bombOwner == 2)
            ++GameManager.instance.destroyNum2;
          num1 = this.rigid.position.y - position.y;
          this.ex = Object.Instantiate<Transform>(this.prefab[index], (Vector3) (position + Vector2.down), Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
        for (float num2 = num1; (double) num2 > 0.0; --num2)
        {
          this.ex = Object.Instantiate<Transform>(this.prefab[0], this.transform.position + new Vector3(0.0f, (float) sign * num2, 0.0f), Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
      }
    }
    else
    {
      if (!((Object) rayhit.collider == (Object) null))
        return;
      for (float num3 = this.bombPower - 1f; (double) num3 > 0.0; --num3)
      {
        this.ex = Object.Instantiate<Transform>(this.prefab[0], this.transform.position + new Vector3(0.0f, (float) sign * num3, 0.0f), Quaternion.identity);
        Object.Destroy((Object) this.ex.gameObject, 0.2f);
      }
      this.ex = Object.Instantiate<Transform>(this.prefab[index], this.transform.position + new Vector3(0.0f, (float) sign * this.bombPower, 0.0f), Quaternion.identity);
      Object.Destroy((Object) this.ex.gameObject, 0.2f);
    }
  }

  private void CalculateDistanceLR(RaycastHit2D rayhit, int index, int sign)
  {
    float num1 = 0.0f;
    if ((Object) rayhit.collider != (Object) null)
    {
      Vector2 position = rayhit.point + new Vector2((float) sign * -0.5f, 0.0f);
      if (position == this.rigid.position && rayhit.collider.gameObject.layer == 6)
        this.ex = (Transform) null;
      else if (sign > 0)
      {
        if (rayhit.collider.gameObject.layer == 6)
        {
          num1 = (float) ((double) position.x - (double) this.rigid.position.x - 1.0);
          this.ex = Object.Instantiate<Transform>(this.prefab[index], (Vector3) position, Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
        else if (rayhit.collider.gameObject.layer == 7)
        {
          if (this.bombOwner == 1)
            ++GameManager.instance.destroyNum;
          else if (this.bombOwner == 2)
            ++GameManager.instance.destroyNum2;
          num1 = position.x - this.rigid.position.x;
          this.ex = Object.Instantiate<Transform>(this.prefab[index], (Vector3) (position + Vector2.right), Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
        for (float x = num1; (double) x > 0.0; --x)
        {
          this.ex = Object.Instantiate<Transform>(this.prefab[1], this.transform.position + new Vector3(x, 0.0f, 0.0f), Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
      }
      else
      {
        if (rayhit.collider.gameObject.layer == 6)
        {
          num1 = (float) ((double) this.rigid.position.x - (double) position.x - 1.0);
          this.ex = Object.Instantiate<Transform>(this.prefab[index], (Vector3) position, Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
        else if (rayhit.collider.gameObject.layer == 7)
        {
          if (this.bombOwner == 1)
            ++GameManager.instance.destroyNum;
          else if (this.bombOwner == 2)
            ++GameManager.instance.destroyNum2;
          num1 = this.rigid.position.x - position.x;
          this.ex = Object.Instantiate<Transform>(this.prefab[index], (Vector3) (position + Vector2.left), Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
        for (float num2 = num1; (double) num2 > 0.0; --num2)
        {
          this.ex = Object.Instantiate<Transform>(this.prefab[1], this.transform.position + new Vector3((float) sign * num2, 0.0f, 0.0f), Quaternion.identity);
          Object.Destroy((Object) this.ex.gameObject, 0.2f);
        }
      }
    }
    else
    {
      if (!((Object) rayhit.collider == (Object) null))
        return;
      for (float num3 = this.bombPower - 1f; (double) num3 > 0.0; --num3)
      {
        this.ex = Object.Instantiate<Transform>(this.prefab[1], this.transform.position + new Vector3((float) sign * num3, 0.0f, 0.0f), Quaternion.identity);
        Object.Destroy((Object) this.ex.gameObject, 0.2f);
      }
      this.ex = Object.Instantiate<Transform>(this.prefab[index], this.transform.position + new Vector3((float) sign * this.bombPower, 0.0f, 0.0f), Quaternion.identity);
      Object.Destroy((Object) this.ex.gameObject, 0.2f);
    }
  }
}
