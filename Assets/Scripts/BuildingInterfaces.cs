using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable
{
    bool CanBuild();
    void Place();
    void Build();
}