using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.Health
{
    public interface IDamageable
    {
        void TakeDamage(float value);
        void TakeHeal(float value);
    }
}
