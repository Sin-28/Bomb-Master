// Decompiled with JetBrains decompiler
// Type: MainManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B55C3093-F29A-419E-80BB-BBF80B2D8A87
// Assembly location: C:\Users\sin_x\OneDrive\바탕 화면\Folder\Game Development\만든 게임들\Bomb Master\Bomb Master_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public class MainManager : MonoBehaviour
{
  private void Update()
  {
    if (!Input.anyKeyDown && !Input.GetMouseButtonDown(0))
      return;
    SceneManager.LoadScene("Select Mode");
  }
}
