// Decompiled with JetBrains decompiler
// Type: CanvasManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#nullable disable
public class CanvasManager : MonoBehaviour
{
  public GameObject player;
  public GameObject player2;
  public GameObject timer;
  public GameObject[] oneHeartBar;
  public GameObject[] itemText;
  public GameObject[] twoHeartBar;
  public GameObject[] itemText2;
  public GameObject deathPanel;
  public GameObject pausePanel;
  public Sprite[] playerSprite;
  public Text playerText;
  public Text bombNumOne;
  public Text bombNumTwo;
  public Text itemNumOne;
  public Text itemNumTwo;
  public Text destroyNumOne;
  public Text destroyNumTwo;
  private bool gamePaused;
  public bool gameEnded;
  private int currentHeart = 3;
  private int bombPower = 1;
  private int maxNum = 1;
  private int squareBomb;
  private int currentHeart2 = 3;
  private int bombPower2 = 1;
  private int maxNum2 = 1;
  private int squareBomb2;
  public bool[,] checkBomb = new bool[10, 10];
  private float reduceTime = 0.5f;
  private float time;
  private bool doReduce;
  private bool doIncrease;
  private float time2;
  private bool doReduce2;
  private bool doIncrease2;

  private void Start()
  {
    this.player.gameObject.GetComponent<SpriteRenderer>().sprite = this.playerSprite[GameManager.instance.playerSprite];
    this.player2.gameObject.GetComponent<SpriteRenderer>().sprite = this.playerSprite[GameManager.instance.playerSprite2];
  }

  private void Update()
  {
    if (this.maxNum != GameManager.instance.maxNum)
      this.maxNum = GameManager.instance.maxNum;
    if (this.bombPower != GameManager.instance.bombPower)
      this.bombPower = GameManager.instance.bombPower;
    if (this.squareBomb != GameManager.instance.squareBomb)
      this.squareBomb = GameManager.instance.squareBomb;
    this.itemText[0].gameObject.GetComponent<Text>().text = this.maxNum.ToString();
    this.itemText[1].gameObject.GetComponent<Text>().text = this.bombPower.ToString();
    this.itemText[2].gameObject.GetComponent<Text>().text = this.squareBomb.ToString();
    if (this.maxNum2 != GameManager.instance.maxNum2)
      this.maxNum2 = GameManager.instance.maxNum2;
    if (this.bombPower2 != GameManager.instance.bombPower2)
      this.bombPower2 = GameManager.instance.bombPower2;
    if (this.squareBomb2 != GameManager.instance.squareBomb2)
      this.squareBomb2 = GameManager.instance.squareBomb2;
    this.itemText2[0].gameObject.GetComponent<Text>().text = this.maxNum2.ToString();
    this.itemText2[1].gameObject.GetComponent<Text>().text = this.bombPower2.ToString();
    this.itemText2[2].gameObject.GetComponent<Text>().text = this.squareBomb2.ToString();
    if (this.currentHeart > GameManager.instance.heart)
    {
      this.currentHeart = GameManager.instance.heart;
      this.doReduce = true;
    }
    else if (this.currentHeart < GameManager.instance.heart)
    {
      this.currentHeart = GameManager.instance.heart;
      this.doIncrease = true;
    }
    if (this.currentHeart2 > GameManager.instance.heart2)
    {
      this.currentHeart2 = GameManager.instance.heart2;
      this.doReduce2 = true;
    }
    else if (this.currentHeart2 < GameManager.instance.heart2)
    {
      this.currentHeart2 = GameManager.instance.heart2;
      this.doIncrease2 = true;
    }
    if (GameManager.instance.heart == 0 || GameManager.instance.heart2 == 0)
      this.Invoke("GameEnd", 1.1f);
    if (Input.GetButtonDown("Cancel") && !this.gameEnded)
    {
      if (!this.gamePaused)
        this.GamePause();
      else
        this.GameResume();
    }
    if (!this.gameEnded)
      return;
    this.ShowResult();
  }

  private void FixedUpdate()
  {
    if (this.doReduce)
    {
      if ((double) this.time < (double) this.reduceTime)
        this.oneHeartBar[this.currentHeart].gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, (float) (1.0 - (double) this.time * 2.0));
      else
        this.doReduce = false;
      this.time += Time.deltaTime;
    }
    else if (this.doIncrease)
    {
      if ((double) this.time < (double) this.reduceTime)
        this.oneHeartBar[this.currentHeart - 1].gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, this.time * 2f);
      else
        this.doIncrease = false;
      this.time += Time.deltaTime;
    }
    else
      this.time = 0.0f;
    if (this.doReduce2)
    {
      if ((double) this.time2 < (double) this.reduceTime)
        this.twoHeartBar[this.currentHeart2].gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, (float) (1.0 - (double) this.time2 * 2.0));
      else
        this.doReduce2 = false;
      this.time2 += Time.deltaTime;
    }
    else if (this.doIncrease2)
    {
      if ((double) this.time2 < (double) this.reduceTime)
        this.twoHeartBar[this.currentHeart2 - 1].gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, this.time2 * 2f);
      else
        this.doIncrease2 = false;
      this.time2 += Time.deltaTime;
    }
    else
      this.time2 = 0.0f;
  }

  private void ShowResult()
  {
    this.playerText.text = GameManager.instance.heart != 0 ? "Player 1 Has Won!" : "Player 2 Has Won!";
    if (GameManager.instance.bombNumInstalled > GameManager.instance.bombNumInstalled2)
      this.bombNumOne.color = new Color(1f, 0.0f, 0.0f, 1f);
    else if (GameManager.instance.bombNumInstalled < GameManager.instance.bombNumInstalled2)
      this.bombNumTwo.color = new Color(1f, 0.0f, 0.0f, 1f);
    if (GameManager.instance.itemNumUsed > GameManager.instance.itemNumUsed2)
      this.itemNumOne.color = new Color(1f, 0.0f, 0.0f, 1f);
    else if (GameManager.instance.itemNumUsed < GameManager.instance.itemNumUsed2)
      this.itemNumTwo.color = new Color(1f, 0.0f, 0.0f, 1f);
    if (GameManager.instance.destroyNum > GameManager.instance.destroyNum2)
      this.destroyNumOne.color = new Color(1f, 0.0f, 0.0f, 1f);
    else if (GameManager.instance.destroyNum < GameManager.instance.destroyNum2)
      this.destroyNumTwo.color = new Color(1f, 0.0f, 0.0f, 1f);
    this.bombNumOne.text = GameManager.instance.bombNumInstalled.ToString();
    this.bombNumTwo.text = GameManager.instance.bombNumInstalled2.ToString();
    this.itemNumOne.text = GameManager.instance.itemNumUsed.ToString();
    this.itemNumTwo.text = GameManager.instance.itemNumUsed2.ToString();
    this.destroyNumOne.text = GameManager.instance.destroyNum.ToString();
    this.destroyNumTwo.text = GameManager.instance.destroyNum2.ToString();
  }

  private void GameEnd()
  {
    this.gameEnded = true;
    Time.timeScale = 0.0f;
    this.deathPanel.gameObject.SetActive(true);
  }

  private void GamePause()
  {
    this.gamePaused = true;
    this.player.GetComponent<PlayerMove>().allowBomb = false;
    this.player2.GetComponent<PlayerTwo>().allowBomb = false;
    Time.timeScale = 0.0f;
    this.pausePanel.gameObject.SetActive(true);
  }

  public void GameResume()
  {
    this.gamePaused = false;
    this.player.GetComponent<PlayerMove>().allowBomb = true;
    this.player2.GetComponent<PlayerTwo>().allowBomb = true;
    Time.timeScale = 1f;
    this.pausePanel.gameObject.SetActive(false);
  }

  public void GameRestart()
  {
    this.ResetVariable();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    this.player2.GetComponent<PlayerTwo>().DestoryBomb();
    this.timer.GetComponent<Timer>().DestoryBomb();
    GameManager.instance.bombPower = 1;
    GameManager.instance.heart = 3;
    GameManager.instance.maxNum = 1;
    GameManager.instance.squareBomb = 0;
    GameManager.instance.bombNumInstalled = 0;
    GameManager.instance.itemNumUsed = 0;
    GameManager.instance.destroyNum = 0;
    GameManager.instance.bombPower2 = 1;
    GameManager.instance.heart2 = 3;
    GameManager.instance.maxNum2 = 1;
    GameManager.instance.squareBomb2 = 0;
    GameManager.instance.bombNumInstalled2 = 0;
    GameManager.instance.itemNumUsed2 = 0;
    GameManager.instance.destroyNum2 = 0;
    Time.timeScale = 1f;
  }
}
