using UnityEngine;

public partial class CriatureController
{
    public class Chase : State
    {
        public Chase(CriatureController criature) : base(criature)
        {
        }

        public override void OnStateEnter()
        {
            criature.SetCriatureDestination();
            criature.ChangeToChaseVelocity();
            Debug.Log("Creature State" + this.GetType().ToString());

        }

        public override void Update()
        {
            if (!criature.PlayerIsVisible() || criature.HavePatrolsPoints())
            {
                criature.SetState(new Patrol(criature));
            }
            else
                criature.SetCriatureDestination();
        }
    }

}
