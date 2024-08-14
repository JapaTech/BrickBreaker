//Não utilizado, era tipo o invoker do power-up, mas não era necessário
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
