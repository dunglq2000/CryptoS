> Vietnamese version below!

# CryptoCS project

## Features

Features:

1. Russian block ciphers:

   - implement Russian block ciphers Kuznyechik and Magma (GOST 34.12-2015) in C# with description in [1];
   - unit tests for functions and encryption/decryption, which follow examples (test vectors) also described in [1].

2. Commutative algebra: now there is only an implementation on the ring

   $$\mathbb{B}[x_1, x_2, \ldots, x_n] = \frac{\mathbb{F}_2[x_1, x_2, \ldots, x_n]}{\langle x_1^2 + x_1, x_2^2 + x_2, \ldots, x_n^2 + x_n \rangle}$$

   i.e. Boolean polynomial ring:

   - find Gröbner basis with Buchberger's algorithm and Faugère's F4 algorithm.

3. Sudoku's solver with bruteforce approach. See more at [Sudoku](./src/Sudoku/README.md).
4. Graph theory: traverse through the graph with popular algorithms (DFS, BFS), find path between two vertices, find strong connected components. See more at [GraphTheory](./src/GraphTheory/README.md).

## Project structure

Project structure:

- `src/`:

  - `Algebra/`: 
  
    - `Fraction.cs`: define fraction
    - `Monomial.cs`: organize powers of monomial as bit-packed, because in ring $\mathbb{B}[x_1, x_2, \ldots, x_n]$ the powers can only be $0$ or $1$
    - `MonomialOrdering.cs`: support monomial ordering

      Classes derived from `MonomialOrdering`:
    
      - `LexOrdering.cs`: lexicographic ordering
      - `DeglexOrdering`: grade-lexicographic ordering

    - `Polynomial.cs`: specify one type of monomial ordering and a list of monomials, sorted by ascending order due to specified monomial ordering
    - `GF128.cs` [!]: calculate on finite field $\mathrm{GF}(2^{128})$ with irreducible polynomial $x^{128} + x^7 + x^2 + x + 1$ with big endian

  - `Crypto/`:

    - `Kuznyechik.cs`: Kuznyechik cipher, described in [1]
    - `Magma.cs`: Magma cipher, described in [1]
  
  - `Sudoku/SudokuMatrix.cs`: solve the Sudoku problem using bruteforce approach.
  - `GraphTheory/Graph.cs`: graph structure that allows traverse through the graph with popular algorithms (DFS, BFS), find path between two vertices, find strong connected components.

- `tests/`:

  - `Algebra.Tests/`: 
  
    - `TestFraction.cs`: unit tests for class `Fraction`
    - `TestMonomial.cs`: unit tests for class `Monomial`
    - `TestMonomialOrdering.cs`: unit tests for derived classes of `MonomialOrdering`
    - `TestPolynomial.cs`: unit tests for class `Polynomial`
    - `TestGF128.cs` [!]: unit tests for class `GF128.cs`

  - `Crypto.Tests/`:

    - `TestKuznyechik.cs`: unit tests for class Kuznyechik with test vectors in [1]
    - `TestMagma.cs`: unit tests for class Magma with test vectors in [1]

[!] - classes that are needed to be reconstructed:

- `GF128` should be replaced with:

  - class for describing finite field, for example, `GF`
  - class for describing finite field instance

---

Tài liệu tiếng Việt

## Tính năng

Tính năng:

1. Mã khối của Liên bang Nga:

   - cài đặt các mã khối của Liên bang Nga, bao gồm Kuznyechik và Magma (GOST 34.12-2015), được định nghĩa ở [1]
   - unit tests cho các hàm và mã hóa/giải mã theo test vectors được mô tả ở [1].

2. Đại số giao hoán: hiện tại dự án chỉ thực hiện tính toán trên vành đa thức

   $$\mathbb{B}[x_1, x_2, \ldots, x_n] = \frac{\mathbb{F}_2[x_1, x_2, \ldots, x_n]}{\langle x_1^2 + x_1, x_2^2 + x_2, \ldots, x_n^2 + x_n \rangle}$$

   nói cách khác và vành đa thức Boolean (Boolean polynomial ring):

   - tìm cơ sở Gröbner bằng thuật toán Buchberger và thuật toán F4 của Faugère

3. Giải trò chơi Sudoku với phương pháp vét cạn. Xem thêm tại [Sudoku](./src/Sudoku/README.md).
4. Lý thuyết đồ thị: duyệt đồ thị với các thuật toán phổ biến (DFS, BFS), tìm đường đi giữa hai đỉnh, tìm các thành phần liên thông mạnh. Xem thêm tại [GraphTheory](./src/GraphTheory/README.md).

## Cấu trúc thư mục

Cấu trúc thư mục:

- `src/`:

  - `Algebra/`: 
  
    - `Fraction.cs`: định nghĩa phân số
    - `Monomial.cs`: tổ chức số mũ cho dưới dạng bit-packed vì số mũ của mỗi biến trong đơn thức chỉ là 0 hoặc 1 trên $\mathbb{B}[x_1, x_2, \ldots, x_n]$
    - `MonomialOrdering.cs`: hỗ trợ thứ tự từ điển với các lớp con:
    
      - `LexOrdering.cs`: hỗ trợ thứ tự từ điển (lexicographic)
      - `DeglexOrdering.cs`: hỗ trợ thứ tự bậc-từ điển (graded-lexicographic)
    
    - `Polynomial.cs`: chỉ định một dạng thứ tự đơn thức là lớp con của `MonomialOrdering`, và chứa danh sách các đơn thức được sắp xếp tăng dần theo thứ tự đơn thức được chỉ định
    - `GF128.cs` [!]: thực hiện tính toán trên trường $\mathrm{GF}(2^{128})$ với đa thức tối giản $x^{128} + x^7 + x^2 + x + 1$ ở dạng big endian

  - `Crypto/`:

    - `Kuznyechik.cs`: thuật toán Kuznyechik [1];
    - `Magma.cs`: thuật toán Magma [1];

  - `Sudodu/SudokuMatrix.cs`: giải trò chơi Sudoku bằng phương pháp vét cạn.
  - `GraphTheory/Graph.cs`: cấu trúc mô tả đồ thị, cho phép duyệt đồ thị với các thuật toán phổ biến (DFS, BFS), tìm đường đi giữa hai đỉnh, tìm các thành phần liên thông mạnh.

- `tests/`:

  - `Algebra.Tests/`: 
  
    - `TestFraction.cs`: unit tests cho lớp `Fraction`
    - `TestMonomial.cs`: unit tests cho lớp `Monomial`
    - `TestMonomialOrdering.cs`: unit tests cho các lớp con của interface `MonomialOrdering`
    - `TestPolynomial.cs`: unit tests cho lớp `Polynomial`
    - `TestGF128.cs`: unit tests cho lớp `GF128`

  - `Crypto.Tests/`:

    - `TestKuznyechik.cs`: uint tests cho lớp `Kuznyechik`, sử dụng test vectors trong [1]
    - `TestMagma.cs`: unit tests cho lớp `Magma`, sử dụng test vectors trong [1]

[!] là các lớp cần được tái cấu trúc:

- `GF128` cần được thay thế bởi:

  - một lớp mô tả trường hữu hạn, ví dụ `GF`
  - một lớp mô tả các phần tử trường hữu hạn

## Bibliography

[1] ГОСТ Р 34.12-2015. Информационная технология. Криптографическая защита информации. Блочные шифры.
