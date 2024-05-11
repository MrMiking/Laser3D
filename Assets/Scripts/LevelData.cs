using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    public string sceneName;
    public Color backgroundColor;
    public int cameraZoom;
    public bool completed;
}
