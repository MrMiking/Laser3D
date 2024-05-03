using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public Color backgroundColor;
    public int cameraZoom;
    public bool completed;
}
