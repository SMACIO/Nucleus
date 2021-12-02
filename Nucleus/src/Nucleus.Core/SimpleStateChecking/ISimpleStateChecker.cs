using System.Threading.Tasks;

namespace Nucleus.SimpleStateChecking
{
    public interface ISimpleStateChecker<TState>
        where TState : IHasSimpleStateCheckers<TState>
    {
        Task<bool> IsEnabledAsync(SimpleStateCheckerContext<TState> context);
    }
}

