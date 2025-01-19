// Decompiled with JetBrains decompiler
// Type: CharacterManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#nullable disable
public class CharacterManager : MonoBehaviour
{
  public Text timer;
  public GameObject pausePanel;
  public Sprite[] playerSprite;
  public GameObject[] points;
  public GameObject[] playerSelection;
  private bool playerOneReady;
  private bool playerTwoReady;
  private bool lockOn;
  private float realtime = 60f;
  private int time;
  private bool isMenuUp;

  private void Start() => this.randomSprite();

  private void Update()
  {
    this.time = Mathf.RoundToInt(this.realtime);
    this.timer.text = this.time.ToString();
    if (this.playerOneReady && this.playerTwoReady && !this.lockOn)
    {
      this.realtime = 5f;
      this.lockOn = true;
      this.timer.color = new Color(0.0f, 0.0f, 0.0f, 1f);
    }
    if (this.time == 5)
      this.timer.color = new Color(1f, 0.0f, 0.0f, 1f);
    if (this.time != 0)
      this.realtime -= Time.deltaTime;
    if (this.time == 0)
      this.Invoke("MoveScene", 1f);
    if (Input.GetKeyDown(KeyCode.A))
      this.ChangeSpriteNumber(-1);
    else if (Input.GetKeyDown(KeyCode.D))
      this.ChangeSpriteNumber(1);
    else if (Input.GetKeyDown(KeyCode.Space))
      this.ChangeSprite();
    if (Input.GetKeyDown(KeyCode.LeftArrow))
      this.ChangeSpriteNumber2(-1);
    else if (Input.GetKeyDown(KeyCode.RightArrow))
      this.ChangeSpriteNumber2(1);
    else if (Input.GetKeyDown(KeyCode.Slash))
      this.ChangeSprite2();
    if (Input.GetButtonDown("Cancel") && !this.isMenuUp)
    {
      this.MenuUp();
    }
    else
    {
      if (!Input.GetButtonDown("Cancel") || !this.isMenuUp)
        return;
      this.MenuDown();
    }
  }

  public void randomSprite()
  {
    int num1 = Random.Range(0, 5);
    int num2 = Random.Range(0, 5);
    if (num1 == num2)
    {
      this.randomSprite();
    }
    else
    {
      GameManager.instance.playerSprite = num1;
      this.ChangeSpriteNumber(0);
      GameManager.instance.playerSprite2 = num2;
      this.ChangeSpriteNumber2(0);
    }
  }

  public void ChangeSpriteNumber(int num)
  {
    if (this.playerOneReady)
      this.ChangeSpriteBack();
    switch (num)
    {
      case -1:
        if (GameManager.instance.playerSprite == 0)
          return;
        if (GameManager.instance.playerSprite - 1 == GameManager.instance.playerSprite2)
        {
          if (GameManager.instance.playerSprite == 1)
            return;
          --GameManager.instance.playerSprite;
        }
        --GameManager.instance.playerSprite;
        break;
      case 1:
        if (GameManager.instance.playerSprite == 4)
          return;
        if (GameManager.instance.playerSprite + 1 == GameManager.instance.playerSprite2)
        {
          if (GameManager.instance.playerSprite == 3)
            return;
          ++GameManager.instance.playerSprite;
        }
        ++GameManager.instance.playerSprite;
        break;
    }
    switch (GameManager.instance.playerSprite)
    {
      case 0:
        this.playerSelection[0].transform.position = this.points[0].transform.position;
        break;
      case 1:
        this.playerSelection[0].transform.position = this.points[1].transform.position;
        break;
      case 2:
        this.playerSelection[0].transform.position = this.points[2].transform.position;
        break;
      case 3:
        this.playerSelection[0].transform.position = this.points[3].transform.position;
        break;
      case 4:
        this.playerSelection[0].transform.position = this.points[4].transform.position;
        break;
    }
  }

  public void ChangeSpriteNumber2(int num)
  {
    if (this.playerTwoReady)
      this.ChangeSpriteBack2();
    switch (num)
    {
      case -1:
        if (GameManager.instance.playerSprite2 == 0)
          return;
        if (GameManager.instance.playerSprite2 - 1 == GameManager.instance.playerSprite)
        {
          if (GameManager.instance.playerSprite2 == 1)
            return;
          --GameManager.instance.playerSprite2;
        }
        --GameManager.instance.playerSprite2;
        break;
      case 1:
        if (GameManager.instance.playerSprite2 == 4)
          return;
        if (GameManager.instance.playerSprite2 + 1 == GameManager.instance.playerSprite)
        {
          if (GameManager.instance.playerSprite2 == 3)
            return;
          ++GameManager.instance.playerSprite2;
        }
        ++GameManager.instance.playerSprite2;
        break;
    }
    switch (GameManager.instance.playerSprite2)
    {
      case 0:
        this.playerSelection[1].transform.position = this.points[0].transform.position;
        break;
      case 1:
        this.playerSelection[1].transform.position = this.points[1].transform.position;
        break;
      case 2:
        this.playerSelection[1].transform.position = this.points[2].transform.position;
        break;
      case 3:
        this.playerSelection[1].transform.position = this.points[3].transform.position;
        break;
      case 4:
        this.playerSelection[1].transform.position = this.points[4].transform.position;
        break;
    }
  }

  public void ChangeSprite()
  {
    if (this.playerOneReady)
      return;
    this.playerSelection[0].gameObject.SetActive(false);
    this.playerSelection[2].transform.position = this.playerSelection[0].transform.position;
    this.playerSelection[2].gameObject.SetActive(true);
    this.playerOneReady = true;
  }

  public void ChangeSpriteBack()
  {
    this.playerSelection[0].gameObject.SetActive(true);
    this.playerSelection[2].gameObject.SetActive(false);
    this.playerOneReady = false;
  }

  public void ChangeSprite2()
  {
    if (this.playerTwoReady)
      return;
    this.playerSelection[1].gameObject.SetActive(false);
    this.playerSelection[3].transform.position = this.playerSelection[1].transform.position;
    this.playerSelection[3].gameObject.SetActive(true);
    this.playerTwoReady = true;
  }

  public void ChangeSpriteBack2()
  {
    this.playerSelection[1].gameObject.SetActive(true);
    this.playerSelection[3].gameObject.SetActive(false);
    this.playerTwoReady = false;
  }

  public void MenuUp()
  {
    this.isMenuUp = true;
    this.pausePanel.gameObject.SetActive(true);
  }

  public void MenuDown()
  {
    this.isMenuUp = false;
    this.pausePanel.gameObject.SetActive(false);
  }

  public void MoveScene() => SceneManager.LoadScene("Local Mode");

  public void BackToSelectMode() => SceneManager.LoadScene("Select Mode");

  public void BackToHome() => SceneManager.LoadScene("Home");

  public void ChangePlayMode(int playMode) => GameManager.instance.playMode = playMode;
}
