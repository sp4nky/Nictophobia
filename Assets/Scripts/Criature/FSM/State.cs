public abstract class State
{
    public CriatureController criature;

    protected State(CriatureController criature)
    {
        this.criature = criature;
    }

    public virtual void OnStateEnter() { }

    public abstract void Update();

    public virtual void OnStateExit() { }
}
