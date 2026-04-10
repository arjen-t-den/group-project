using System;

namespace Group8.FinalsFrenzy.Destruction
{
    public interface IBreakable
    {
        event Action OnBreak;

        void Break();
    }
}
