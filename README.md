# Đại số giao hoán trên C#

Hiện tại dự án thực hiện trên vành đa thức

$$\mathbb{B}[x_1, x_2, \ldots, x_n] = \frac{\mathbb{F}_2[x_1, x_2, \ldots, x_n]}{\langle x_1^2 + x_1, x_2^2 + x_2, \ldots, x_n^2 + x_n \rangle}$$

nói cách khác và vành đa thức Boolean (Boolean polynomial ring) và $n = 2^k$.

Cấu trúc thư mục:

- `src/`:

  - `Algebra`: 
  
    - `FiniteField.cs`: định nghĩa trường hữu hạn (chưa làm)
    - `Fraction.cs`: định nghĩa phân số
    - `Monomial.cs`: tổ chức số mũ cho dưới dạng bit-packed vì số mũ của mỗi biến trong đơn thức chỉ là 0 hoặc 1 trên $\mathbb{B}[x_1, x_2, \ldots, x_n]$
    - `MonomialOrdering.cs`: hỗ trợ thứ tự từ điển `LexOrdering` và thứ tự bậc-từ điển `DeglexOrdering` được kế thừa từ interface `MonomialOrdering`
    - `Polynomial.cs`: chỉ định một dạng thứ tự đơn thức và chứa danh sách các đơn thức được sắp xếp tăng dần theo thứ tự đơn thức

- `tests/`:

  - `Algebra.Tests`: 
  
    - `TestFraction.cs`: unit test cho lớp `Fraction`
    - `TestMonomial.cs`: unit test cho lớp `Monomial`
    - `TestMonomialOrdering.cs`: unit test cho các lớp con của interface `MonomialOrdering`
    - `TestPolynomial.cs`: unit test cho lớp `Polynomial`
