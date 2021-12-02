using Nucleus.Collections;

namespace Nucleus.SimpleStateChecking
{
    public class NucleusSimpleStateCheckerOptions<TState>
        where TState : IHasSimpleStateCheckers<TState>
    {
        public ITypeList<ISimpleStateChecker<TState>> GlobalStateCheckers { get; }

        public NucleusSimpleStateCheckerOptions()
        {
            GlobalStateCheckers = new TypeList<ISimpleStateChecker<TState>>();
        }
    }
}




