# Commutative algebra

Commutative algebra: now there is only an implementation on the ring

$$\mathbb{B}[x_1, x_2, \ldots, x_n] = \frac{\mathbb{F}_2[x_1, x_2, \ldots, x_n]}{\langle x_1^2 + x_1, x_2^2 + x_2, \ldots, x_n^2 + x_n \rangle}$$

i.e. Boolean polynomial ring:

- find Gröbner basis with Buchberger's algorithm and Faugère's F4 algorithm.
- `GF128.cs` [!]: calculate on finite field $\mathrm{GF}(2^{128})$ with irreducible polynomial $x^{128} + x^7 + x^2 + x + 1$ with big endian

[!] - classes that are needed to be reconstructed:

- `GF128` should be replaced with:

  - class for describing finite field, for example, `GF`
  - class for describing finite field instance

Đại số giao hoán: hiện tại dự án chỉ thực hiện tính toán trên vành đa thức

$$\mathbb{B}[x_1, x_2, \ldots, x_n] = \frac{\mathbb{F}_2[x_1, x_2, \ldots, x_n]}{\langle x_1^2 + x_1, x_2^2 + x_2, \ldots, x_n^2 + x_n \rangle}$$

nói cách khác và vành đa thức Boolean (Boolean polynomial ring):

- tìm cơ sở Gröbner bằng thuật toán Buchberger và thuật toán F4 của Faugère
- `GF128.cs` [!]: thực hiện tính toán trên trường $\mathrm{GF}(2^{128})$ với đa thức tối giản $x^{128} + x^7 + x^2 + x + 1$ ở dạng big endian

[!] là các lớp cần được tái cấu trúc:

- `GF128` cần được thay thế bởi:

  - một lớp mô tả trường hữu hạn, ví dụ `GF`
  - một lớp mô tả các phần tử trường hữu hạn
