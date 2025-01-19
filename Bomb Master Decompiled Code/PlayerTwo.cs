// Decompiled with JetBrains decompiler
// Type: PlayerTwo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PlayerTwo : MonoBehaviour
{
  private Rigidbody2D rigid;
  private SpriteRenderer sprite;
  public AudioSource[] audioClip;
  public Transform prefab;
  public Transform[] bomb;
  public GameObject canvasManager;
  public float playerSpeed;
  private float h;
  private float v;
  private bool allowMove;
  public bool allowBomb = true;
  private int installindex;
  private int bombNum;
  private int doubleHit;
  private bool isDamaged;
  private int playMode;
  private float time;
  private float blinktime = 0.2f;
  private float backtime = 0.4f;

  private void Awake()
  {
    this.rigid = this.GetComponent<Rigidbody2D>();
    this.sprite = this.GetComponent<SpriteRenderer>();
  }

  private void Start()
  {
    this.playMode = GameManager.instance.playMode;
    this.transform.position = new Vector3(10f, -12f, 0.0f);
    this.EnterStage();
  }

  private void Update()
  {
    if (this.allowMove)
    {
      this.h = Input.GetAxisRaw("HorizontalTwo");
      this.v = Input.GetAxisRaw("VerticalTwo");
    }
    if ((double) this.h != 0.0)
      this.v = 0.0f;
    if ((double) this.v != 0.0)
      this.h = 0.0f;
    for (int index = 0; index <= 9; ++index)
      this.bombNum += (Object) this.bomb[index] == (Object) null ? 0 : 1;
    if (this.bombNum < GameManager.instance.maxNum2 && (Input.GetKeyDown(KeyCode.Slash) || Input.GetKeyDown(KeyCode.LeftControl)) && this.allowMove && this.allowBomb)
    {
      ++GameManager.instance.bombNumInstalled2;
      this.InstallBomb();
    }
    this.bombNum = 0;
    if (this.doubleHit >= 2)
      GameManager.instance.heart2 += this.doubleHit - 1;
    this.doubleHit = 0;
  }

  private void FixedUpdate()
  {
    this.rigid.velocity = new Vector2(this.h * this.playerSpeed, this.v * this.playerSpeed);
    if (this.isDamaged)
    {
      if ((double) this.time < (double) this.blinktime)
        this.sprite.color = new Color(1f, 1f, 1f, 0.5f);
      else if ((double) this.time < (double) this.backtime)
        this.sprite.color = new Color(1f, 1f, 1f, 1f);
      else
        this.time = 0.0f;
      this.time += Time.deltaTime;
    }
    else
      this.time = 0.0f;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Explosion")
    {
      ++this.doubleHit;
      this.SetImmortal();
      this.Invoke("SetMortal", 1f);
      --GameManager.instance.heart2;
    }
    if (!(collision.gameObject.tag == "Item"))
      return;
    ++GameManager.instance.itemNumUsed2;
    this.audioClip[0].Play();
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (!(collision.gameObject.tag == "Bomb"))
      return;
    collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
  }

  private void EnterStage()
  {
    if ((double) this.transform.position.y < -10.0)
    {
      this.transform.position = new Vector3(10f, this.transform.position.y + 1.2f * Time.deltaTime, 0.0f);
      this.Invoke(nameof (EnterStage), 0.01f);
    }
    else
      this.Invoke("AllowMove", 4.8f);
  }

  private void AllowMove() => this.allowMove = true;

  private void InstallBomb()
  {
    float x = Mathf.Round(this.rigid.position.x);
    float y = Mathf.Round(this.rigid.position.y);
    if (this.canvasManager.GetComponent<CanvasManager>().checkBomb[(int) x - 1, -((int) y + 1)])
      return;
    this.bomb[this.installindex++] = Object.Instantiate<Transform>(this.prefab, new Vector3(x, y, 0.0f), Quaternion.identity);
    this.canvasManager.GetComponent<CanvasManager>().checkBomb[(int) x - 1, -((int) y + 1)] = true;
    this.bomb[this.installindex - 1].GetComponent<CircularBomb>().canvasManager = this.canvasManager;
    this.bomb[this.installindex - 1].GetComponent<CircularBomb>().bombOwner = 2;
    this.bomb[this.installindex - 1].GetComponent<CircularBomb>().bombPower = (float) GameManager.instance.bombPower2;
    this.audioClip[1].Play();
    if (this.installindex <= 9)
      return;
    this.installindex = 0;
  }

  public void DestoryBomb()
  {
    for (int index = 0; index <= 9; ++index)
    {
      if ((Object) this.bomb[index] != (Object) null)
      {
        this.bomb[index].GetComponent<CircularBomb>().doExplosion = false;
        Object.Destroy((Object) this.bomb[index].gameObject);
      }
    }
  }

  private void SetImmortal()
  {
    this.isDamaged = true;
    this.gameObject.layer = 9;
  }

  private void SetMortal()
  {
    this.isDamaged = false;
    this.sprite.color = new Color(1f, 1f, 1f, 1f);
    this.gameObject.layer = 8;
  }
}
