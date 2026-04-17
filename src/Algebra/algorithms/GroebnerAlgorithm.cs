using System.Diagnostics;

namespace Algebra;

public abstract class GroebnerAlgorithm
{
    protected Stopwatch _sw;
    public GroebnerAlgorithm()
    {
        _sw = new Stopwatch();
    }
    public abstract List<Polynomial> Compute(List<Polynomial> polynomials);
    public abstract void Select(List<Polynomial> polynomials);
}
