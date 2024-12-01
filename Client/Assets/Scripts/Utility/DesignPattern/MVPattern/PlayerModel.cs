using R3;
using UnityEngine;

public class PlayerModel : Model
{
    [SerializeField] int _hp;
    [SerializeField] int _mp;
    public ReadOnlyReactiveProperty<int> HP;
    public ReadOnlyReactiveProperty<int> MP;
}
