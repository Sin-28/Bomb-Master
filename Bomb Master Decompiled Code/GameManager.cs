// Decompiled with JetBrains decompiler
// Type: GameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  public int playMode;
  public bool doEnterMotion;
  public int campaignPlayerSprite;
  public int playerSprite;
  public int playerSprite2;
  public int bombPower;
  public int heart;
  public int maxNum;
  public int squareBomb;
  public int bombNumInstalled;
  public int itemNumUsed;
  public int destroyNum;
  public int bombPower2;
  public int heart2;
  public int maxNum2;
  public int squareBomb2;
  public int bombNumInstalled2;
  public int itemNumUsed2;
  public int destroyNum2;

  private void Awake()
  {
    if ((Object) GameManager.instance == (Object) null)
    {
      GameManager.instance = this;
      Object.DontDestroyOnLoad((Object) this.gameObject);
    }
    else
    {
      if (!((Object) GameManager.instance != (Object) this))
        return;
      Object.Destroy((Object) this.gameObject);
    }
  }

  private void Start() => this.GameLoad();

  public void GameSave()
  {
    PlayerPrefs.SetInt("playerSprite", this.campaignPlayerSprite);
    PlayerPrefs.Save();
  }

  public void GameLoad()
  {
    if (!PlayerPrefs.HasKey("playerSprite"))
      return;
    this.campaignPlayerSprite = PlayerPrefs.GetInt("playerSprite");
  }
}
