# Commutative algebra using CryptoS.Algebra

Commutative algebra: now there is only an implementation on the ring

$$\mathbb{B}[x_1, x_2, \ldots, x_n] = \dfrac{\mathbb{F}_2[x_1, x_2, \ldots, x_n]}{\langle x_1^2 + x_1, x_2^2 + x_2, \ldots, x_n^2 + x_n \rangle}$$

i.e. Boolean polynomial ring:

- find Gröbner basis with Buchberger's algorithm and Faugère's F4 algorithm.
- `GF128.cs` [!]: calculate on finite field $\mathrm{GF}(2^{128})$ with irreducible polynomial $x^{128} + x^7 + x^2 + x + 1$ with big endian

[!] - classes that are needed to be reconstructed:

- `GF128` should be replaced with:

  - class for describing finite field, for example, `GF`
  - class for describing finite field instance

## Working with monomials

### Monomial constructors

In other to use monomial, the library now support constructor taking 32-bit unsigned integer as power of variables in monomial.

For example the following code represents monomial $x_0 x_1 x_3$ over the ring $\mathbb{B}[x_0, x_1, x_2, x_3, x_4]$.

```cs
Monomial f = new Monomial(5, [0b11010U << 27]);
```

The library also supports parsing monomial from string. However it only works for only type of symbol, for example `x0*x1`, `x1*x4`. Using two symbols, for example, `x` and `y`, is currently not supported.

In other to represent monomial from string we need to specify the number of variables:

```cs
Monomial f = new Monomial(5, "x0*x1*x4");
Monomial g = new Monomial(5, [0b11001U << 27]);
bool result = f.Equals(g);
```

The `result` above is true because we they are $x_0 x_1 x_4$ on Boolean ring with $5$ variables.

### Monomial multiplication

For multiplying two monomials and getting a new monomial, use operator `*`:

```cs
Monomial a = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)
Monomial b = new Monomial(4, [0b0001U << 28]);   // (0, 0, 0, 1)
Monomial c = a * b;   // (1, 0, 1, 1)
```

For multiplying a monomial with another monomial, use method `Monomial.Mult`:

```cs
Monomial a = new Monomial(4, [0b1011U << 28]);   // a is (1, 0, 1, 1)
a.Multiply(new Monomial(4, [0b0001 << 28]));     // a is multiplied with (0, 0, 0, 1)
```

### Monomial division

For checking if a monomial is divisible by another monomial, use method `Monomial.IsDivisibleBy`:

```cs
Monomial a = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)
Monomial b = new Monomial(4, [0b0001U << 28]);   // (0, 0, 0, 1)
bool result = a.IsDivisibleBy(b);                 // true
```

For dividing two monomials, it is similar with monomial multiplication: use operator `/` for creating a new monomial, and method `Monomial.Divide` to divide current monomial.

[!] Please note that division will throw exception if the current monomial is not divisible by the other. Therefore the best practice for division is to use `Monomial.IsDivisibleBy` before division.

## Working with polynomials

### Polynomial constructors

For creating instance of polynomial, it is now compulsory to specify a monomial ordering (derived class of `MonomialOrdering`). We can also parse polynomial from string.

```cs
Polynomial f = new Polynomial(
    new LexOrdering(),
    [
        new Monomial(12, [0b_1110_0001_1010U << 20]),
        new Monomial(12, [0b_0000_0000_1111U << 20])
    ]
);
Polynomial g = new Polynomial(12, "x0*x1*x2*x7*x8*x10 + x8*x9*x10*x11", new LexOrdering());
var result = f.Equals(g);
```

### Polynomial addition

For adding a polynomial with a monomial, we can use operator `+` for creating a new polynomial, or use method `Polynomial.Add` for adding the monomial into the current polynomial.

```cs
Monomial a = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)
Monomial b = new Monomial(4, [0b0001U << 28]);   // (0, 0, 0, 1)
Monomial c = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)

Polynomial p = new Polynomial(
    new LexOrdering(), [a, b, c]
); // p = a + b + c = b on F_2
Polynomial q = p + a; // p = a + b
```

```cs
Monomial a = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)
Monomial b = new Monomial(4, [0b0001U << 28]);   // (0, 0, 0, 1)
Monomial c = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)

Polynomial p = new Polynomial(
    new LexOrdering(), [a, b, c]
);
p.Add(a);
```

Similarly for adding two polynomials, we can use operator `+` for creating a new polynomial, or use method `Polynomial.Add()` for adding the other polynomial to the current one.

### Polynomial multiplication

Now there are only operators/methods for multiplying a polynomial with a monomial, not with a polynomial, because calculating Groebner's basis does not require multiplication of two polynomials. I will add multiplication of two polynomials later.

Similarly for adding two polynomials, for multiplying a polynomial with a monomial we can use operator `*` for creating a new polynomial, or use method `Polynomial.Multiply()` for multiplying a monomial to the current polynomial.

For example:

```cs
Monomial a = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)
Monomial b = new Monomial(4, [0b0001U << 28]);   // (0, 0, 0, 1)
Monomial c = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)

Polynomial p = new Polynomial(
    new LexOrdering(), [a, b, c]
);                    // p = a + b + c = b on F_2
Polynomial q = p * a; // p = ab
```

```cs
Monomial a = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)
Monomial b = new Monomial(4, [0b0001U << 28]);   // (0, 0, 0, 1)
Monomial c = new Monomial(4, [0b1011U << 28]);   // (1, 0, 1, 1)

Polynomial p = new Polynomial(
    new LexOrdering(), [a, b, c]
);              // p = a + b + c = b on F_2
p.Multiply(a);  // p = ab
```

### S-poly of two polynomial

$S$-poly of two polynomials $f$ and $g$ is defined as

$$S(f, g) = \dfrac{\mathrm{LCM}(\mathrm{LT}(f), \mathrm{LT}(g))}{\mathrm{LT}(f)} f - \dfrac{\mathrm{LCM}(\mathrm{LT}(f), \mathrm{LT}(g))}{\mathrm{LT}(g)}g$$

where $\mathrm{LT}(\cdot)$ is leading term of polynomial according to given monomial ordering.

For calculating $S$-poly of two polynomials we can use static method `SPoly` of class `Polynomial` or call from a polynomial:

```cs
Monomial a = new Monomial(4, [0b1110U << 28]);   // (1, 1, 1, 0)
Monomial b = new Monomial(4, [0b0001U << 28]);   // (0, 0, 0, 1)
Monomial c = new Monomial(4, [0b0101U << 28]);   // (0, 1, 0, 1)
Monomial d = new Monomial(4, [0b0010U << 28]);   // (0, 0, 1, 0)
Monomial e = new Monomial(4, [0b1010U << 28]);   // (1, 0, 1, 0)

Polynomial p = new Polynomial(new LexOrdering(), [a, c, b]);
Polynomial q = new Polynomial(new LexOrdering(), [c, d]);

Polynomial result1 = Polynomial.SPoly(p, q);
Polynomial result2 = p.SPoly(q);                  // result1 and result2 are the same
```

### Polynomial division

Let $f$ is a polynomial and $g_1$, $g_2$, ..., $g_t$ is a list of polynomials, we need to find a polynomial $r$ and a list of polynomials $q_1$, $q_2$, ..., $q_t$ such that

$$f = r + g_1 q_1 + g_2 q_2 + \cdots + g_t q_t$$

where $r$ is the remainder and $q_1$, $q_2$, ..., $q_t$ are quotients.

For example

```cs
// Replace ... with your own monomials
Polynomial p = new Polynomial(new LexOrdering(), [...]);
List<Polynomial> polynomials = [
    new Polynomial(new LexOrdering(), [...]),
    new Polynomial(new LexOrdering(), [...])
];
var (r, q) = p.Divide(polynomials);
```

## Calculating Groebner's basis

At the present supported algorithms are Buchberger and F4. For F4 algorithm there is an implementation with `List` and an implementation with `HashSet` of C#. I implemented two styles of F4 for measuring execution time (^_^).

Please note that the found Groebner's basis is not reduced. The reduction of ideal will be implemented later.

An example for Buchberger's algorithm:

```cs
List<Polynomial> polynomials = [
    new Polynomial(lexOrdering, [
        new Monomial(32, [0b110U << 29]),
        new Monomial(32, [0b011U << 29]),
        new Monomial(32, [0b000U << 29])
    ]),
    new Polynomial(lexOrdering, [
        new Monomial(32, [0b101U << 29]),
        new Monomial(32, [0b010U << 29]),
        new Monomial(32, [0b001U << 29])
    ])
];
GroebnerAlgorithm solver = new BuchbergerAlgorithm();
List<Polynomial> result = solver.Compute(polynomials);
```

An example for F4 algorithm with above list of polynomials:

```cs
GroebnerAlgorithm solver = new F4AlgorithmList();
var result = solver.Compute(polynomials);
```

or with `HashSet`:

```cs
GroebnerAlgorithm solver = new F4AlgorithmHashSet();
var result = solver.Compute(polynomials);
```
