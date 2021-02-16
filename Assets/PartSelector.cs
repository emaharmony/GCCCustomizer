using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSelector : MonoBehaviour
{
    public void SelectPart()
    {
        GamecubeCustomizer.Instance.SelectPart(tag);
    }
}
