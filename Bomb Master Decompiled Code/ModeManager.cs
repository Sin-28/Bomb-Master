// Decompiled with JetBrains decompiler
// Type: ModeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#nullable disable
public class ModeManager : MonoBehaviour
{
  public GameObject player;
  public GameObject pausePanel;
  public GameObject modeMenuPanel;
  public Image spriteImage;
  public Sprite[] playerSprite;
  private bool gamePaused;
  private bool isMenu;
  public bool[,] checkBomb = new bool[12, 10];

  private void Start()
  {
    this.ChangeSprite();
    this.spriteImage.sprite = this.playerSprite[GameManager.instance.campaignPlayerSprite];
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Tab) && !this.gamePaused)
    {
      if (!this.isMenu)
        this.MenuPanelUp();
      else
        this.MenuPanelDown();
    }
    if (!Input.GetButtonDown("Cancel"))
      return;
    if (!this.gamePaused)
    {
      if (this.isMenu)
        this.MenuPanelDown();
      else
        this.GamePause();
    }
    else
      this.GameResume();
  }

  public void ChangeSpriteNumber(int num)
  {
    if (GameManager.instance.campaignPlayerSprite == 4 && num > 0)
      GameManager.instance.campaignPlayerSprite = 0;
    else if (GameManager.instance.campaignPlayerSprite == 0 && num < 0)
      GameManager.instance.campaignPlayerSprite = 4;
    else
      GameManager.instance.campaignPlayerSprite += num;
    this.spriteImage.sprite = this.playerSprite[GameManager.instance.campaignPlayerSprite];
  }

  public void ChangeSprite()
  {
    this.player.gameObject.GetComponent<SpriteRenderer>().sprite = this.playerSprite[GameManager.instance.campaignPlayerSprite];
  }

  private void MenuPanelUp()
  {
    this.isMenu = true;
    this.player.GetComponent<PlayerMove>().allowBomb = false;
    Time.timeScale = 0.0f;
    this.modeMenuPanel.gameObject.SetActive(true);
  }

  private void MenuPanelDown()
  {
    this.isMenu = false;
    this.player.GetComponent<PlayerMove>().allowBomb = true;
    Time.timeScale = 1f;
    this.modeMenuPanel.gameObject.SetActive(false);
  }

  private void GamePause()
  {
    this.gamePaused = true;
    this.player.GetComponent<PlayerMove>().allowBomb = false;
    Time.timeScale = 0.0f;
    this.pausePanel.gameObject.SetActive(true);
  }

  private void GameResume()
  {
    this.gamePaused = false;
    this.player.GetComponent<PlayerMove>().allowBomb = true;
    Time.timeScale = 1f;
    this.pausePanel.gameObject.SetActive(false);
  }

  public void ClearMoveScene(string sceneName)
  {
    this.ResetVariable();
    SceneManager.LoadScene(sceneName);
  }

  public void ChangePlayMode(int playMode) => GameManager.instance.playMode = playMode;

  public void EnterMotion(bool check) => GameManager.instance.doEnterMotion = check;

  private void ResetVariable()
  {
    this.player.GetComponent<PlayerMove>().DestoryBomb();
    GameManager.instance.bombPower = 1;
    GameManager.instance.heart = 3;
    GameManager.instance.maxNum = 1;
    GameManager.instance.squareBomb = 0;
    GameManager.instance.bombNumInstalled = 0;
    GameManager.instance.itemNumUsed = 0;
    GameManager.instance.destroyNum = 0;
    Time.timeScale = 1f;
  }

  public void GameExit()
  {
    GameManager.instance.GameSave();
    Application.Quit();
  }
}
