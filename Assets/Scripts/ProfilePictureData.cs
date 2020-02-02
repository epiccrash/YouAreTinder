using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Profile Picture", menuName = "Profile Picture", order = 51)]
public class ProfilePictureData : ScriptableObject
{
    public Sprite PlayerIcon = null;
    public Color DarkColor = new Color();
    public Color LightColor = new Color();
    public bool IsMale = false;
}
