using UnityEngine;

public class ThingThatCanFight : DamageTaker
{
    //[SerializeField] What is the porpuse here?
    //public int Power {  get; protected set; }
    [SerializeField] int _power;

    public int Power
    {
        get => _power;
        protected set => _power = value;
    }

}
