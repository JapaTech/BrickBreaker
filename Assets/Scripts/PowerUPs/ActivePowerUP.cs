using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePowerUP : MonoBehaviour
{

    private IPowerUp powerUPCommand;

    public void ActivePowerUp(IPowerUp powerUP)
    {
        powerUPCommand = powerUP;
        powerUPCommand.Active();
    }

    public void DisablePowerUp()
    {
        powerUPCommand.Deactivate();
        powerUPCommand = null;
    }
}
