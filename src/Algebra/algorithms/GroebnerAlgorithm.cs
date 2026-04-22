using System.Diagnostics;

namespace CryptoS.Algebra.Boolean;

/// <summary>
/// Algorithm for calculating Groebner's basis.
/// </summary>
public abstract class GroebnerAlgorithm
{
    /// <summary>
    /// Measure time of execution, used when defining bottle neck or evaluating efficiency.
    /// </summary>
    protected Stopwatch _sw;
    /// <summary>
    /// Default constructor.
    /// </summary>
    public GroebnerAlgorithm()
    {
        _sw = new Stopwatch();
    }
    /// <summary>
    /// Abstract method for calculating Groebner's basis.
    /// </summary>
    /// <param name="polynomials">List of polynomials for calculating Groebner's basis.</param>
    /// <returns>New list of polynomials, which is Groebner's basis of input polynomials.</returns>
    public abstract List<Polynomial> Compute(List<Polynomial> polynomials);
    /// <summary>
    /// Select critical pairs of polynomials, used in Groebner's basis calculation.
    /// </summary>
    public abstract void Select();
}
