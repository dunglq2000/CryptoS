namespace Algebra;

public abstract class GroebnerAlgorithm
{
    public abstract List<Polynomial> Compute(List<Polynomial> polynomials);
    public abstract List<Tuple<int, int>> Select(List<Tuple<int, int>> pairs, List<Polynomial> polynomials);
}
