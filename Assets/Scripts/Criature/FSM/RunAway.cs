using UnityEngine;

public partial class CriatureController
{
    public class RunAway : State
    {
        private float t;

        public RunAway(CriatureController criature) : base(criature)
        {
            t = 0;
        }

        public override void OnStateEnter()
        {
            criature.StunCreature();
            criature.onCriatureStun.Invoke();
            Debug.Log("Creature State" + this.GetType().ToString());
        }

        public override void Update()
        {
            t += Time.deltaTime;
            if (t > criature.stunedTime)
            {
                criature.SetState(new Patrol(criature));
            }
        }

        public override void OnStateExit()
        {
            criature.WakeUpCreature();
        }
    }

}
