using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MyEnum
{
    GoTo,
    Field,
    Lumber
}

public class POIusage : MonoBehaviour
{
    [SerializeField] public MyEnum usage;

}
