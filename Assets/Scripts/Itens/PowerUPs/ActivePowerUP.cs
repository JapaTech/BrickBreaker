//N�o utilizado, era tipo o invoker do power-up, mas n�o era necess�rio
public class ActivePowerUp
{
    private IPowerUp powerUPCommand;

    public void Active(IPowerUp powerUP)
    {
        powerUPCommand = powerUP;
        powerUPCommand.Active();
    }

    public void Deactive()
    {
        powerUPCommand.Deactivate();
        powerUPCommand = null;
    }
}
