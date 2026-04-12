using System;

namespace Group8.FinalsFrenzy.Destruction.Breakables
{
    public interface IBreakable
    {
        event Action OnBreak;

        void Break();
    }
}
