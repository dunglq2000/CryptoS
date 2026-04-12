# Đại số giao hoán trên C#

Hiện tại dự án thực hiện trên vành đa thức

$$\mathbb{B}[x_1, x_2, \ldots, x_n] = \frac{\mathbb{F}_2[x_1, x_2, \ldots, x_n]}{\langle x_1^2 + x_1, x_2^2 + x_2, \ldots, x_n^2 + x_n \rangle}$$

nói cách khác và vành đa thức Boolean (Boolean polynomial ring) và $n = 2^k$.

Cấu trúc thư mục:

- `src/`:

  - `FiniteField`: định nghĩa trường hữu hạn (`FiniteField`), phân số (`Fraction`)
  - `Polynomial`: định nghĩa đơn thức (`Monomial`), thứ tự đơn thức (`MonomialOrdering`), đa thức (`Polynomial`):

    - `Monomial`: tổ chức số mũ cho dưới dạng bit-packed vì số mũ của mỗi biến trong đơn thức chỉ là 0 hoặc 1 trên $\mathbb{B}[x_1, x_2, \ldots, x_n]$
    - `MonomialOrdering`: hỗ trợ thứ tự từ điển `LexOrdering` và thứ tự bậc-từ điển `DeglexOrdering` được kế thừa từ `MonomialOrdering`
    - `Polynomial`: chỉ định một dạng thứ tự đơn thức và chứa danh sách các đơn thức được sắp xếp giảm dần theo thứ tự đơn thức

- `tests/`:

  - `FiniteField.Tests`: unit test cho lớp `Fraction`
  - `Polynomial.Tests`: unit test cho `Monomial` và `MonomialOrdering`
