using UnityEngine;

[CreateAssetMenu(fileName = "Pinguin", menuName = "Skins/pinguin", order = 1)]
public class SeletorDeSkin : ScriptableObject
{
    public Sprite sprite;
    public RuntimeAnimatorController animator;
}
