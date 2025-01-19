// Decompiled with JetBrains decompiler
// Type: Timer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class Timer : MonoBehaviour
{
  public Image initialTimerImage;
  public Image startImage;
  public Image warningImage;
  public Image showLeftTImeImage;
  public Sprite[] number;
  public GameObject canvasManager;
  public Vector3 randomPosition;
  public Transform[] bomb;
  public Transform bombPrefab;
  private int installindex;
  private float startTimer = 3f;
  private int timer;
  private bool timerCount;
  private bool startTime;
  private bool activeTimerOnce = true;
  private bool doWarning;
  public float randomTime = 3f;
  private int randomTimer;
  private int i = 1;

  private void Start() => this.Invoke("TurnTimerOn", 2f);

  private void Update()
  {
    if (this.startTime)
    {
      this.timer = Mathf.RoundToInt(this.startTimer);
      if ((double) this.startTimer > 0.0)
        this.startTimer -= Time.deltaTime;
      if (this.timer == 5 || this.timer == 4 || this.timer == 3 || this.timer == 2 || this.timer == 1)
      {
        if (this.timerCount && this.activeTimerOnce)
        {
          this.activeTimerOnce = false;
          this.TurnShowLeftTimeOn(this.timer);
        }
        else if (!this.timerCount)
          this.initialTimerImage.GetComponent<Image>().sprite = this.number[this.timer];
      }
      if (this.timer == 0)
      {
        if (!this.timerCount)
        {
          this.initialTimerImage.gameObject.SetActive(false);
          this.startImage.gameObject.SetActive(true);
          this.Invoke("TurnStartOff", 1f);
          this.timerCount = true;
          this.startTimer = 15f;
        }
        else
        {
          this.Invoke("TurnWarningOn", 0.5f);
          this.startTime = false;
          this.doWarning = true;
          this.Invoke("TurnWarningOff", 2f);
        }
      }
    }
    if (!this.doWarning)
      return;
    this.randomTimer = Mathf.RoundToInt(this.randomTime);
    this.randomTime -= Time.deltaTime;
    if (this.randomTimer != 0)
      return;
    this.makeRandomPosition();
    if (this.i < 20)
      ++this.i;
    this.randomTime = (float) (4.0 - (double) this.i * 0.15000000596046448);
  }

  public void makeRandomPosition()
  {
    int x = Random.Range(1, 11);
    int num = Random.Range(1, 11);
    this.randomPosition = new Vector3((float) x, (float) -num, 0.0f);
    if ((Object) Physics2D.OverlapCircle((Vector2) this.randomPosition, 0.1f) != (Object) null && !this.canvasManager.GetComponent<CanvasManager>().checkBomb[x - 1, num - 1])
    {
      this.makeRandomPosition();
    }
    else
    {
      this.bomb[this.installindex++] = Object.Instantiate<Transform>(this.bombPrefab, this.randomPosition, Quaternion.identity);
      this.canvasManager.GetComponent<CanvasManager>().checkBomb[x - 1, num - 1] = true;
      this.bomb[this.installindex - 1].GetComponent<CircularBomb>().canvasManager = this.canvasManager;
      this.bomb[this.installindex - 1].GetComponent<CircularBomb>().bombOwner = 3;
      this.bomb[this.installindex - 1].GetComponent<CircularBomb>().bombPower = 1f;
      this.bomb[this.installindex - 1].GetComponent<BoxCollider2D>().isTrigger = false;
      if (this.installindex <= 29)
        return;
      this.installindex = 0;
    }
  }

  public void DestoryBomb()
  {
    for (int index = 0; index <= 29; ++index)
    {
      if ((Object) this.bomb[index] != (Object) null)
      {
        this.bomb[index].GetComponent<CircularBomb>().doExplosion = false;
        Object.Destroy((Object) this.bomb[index].gameObject);
      }
    }
  }

  private void TurnTimerOn()
  {
    this.initialTimerImage.gameObject.SetActive(true);
    this.startTime = true;
  }

  private void TurnStartOff() => this.startImage.gameObject.SetActive(false);

  private void TurnWarningOn() => this.warningImage.gameObject.SetActive(true);

  private void TurnWarningOff() => this.warningImage.gameObject.SetActive(false);

  private void TurnShowLeftTimeOff()
  {
    this.showLeftTImeImage.gameObject.SetActive(false);
    this.activeTimerOnce = true;
  }

  private void TurnShowLeftTimeOn(int num)
  {
    switch (num)
    {
      case 10:
        this.showLeftTImeImage.GetComponent<Image>().sprite = this.number[6];
        break;
      case 20:
        this.showLeftTImeImage.GetComponent<Image>().sprite = this.number[7];
        break;
      case 30:
        this.showLeftTImeImage.GetComponent<Image>().sprite = this.number[8];
        break;
      default:
        this.showLeftTImeImage.GetComponent<Image>().sprite = this.number[this.timer];
        break;
    }
    this.showLeftTImeImage.gameObject.SetActive(true);
    this.Invoke("TurnShowLeftTimeOff", 1.1f);
  }
}
