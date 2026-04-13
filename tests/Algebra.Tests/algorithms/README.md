# Test for algorithms finding Grebner basis

## Buchberger's algorithm

### Test 1

Đầu vào: $f_0 = xy + y$, $f_1 = x + 1$.

Ban đầu:

$$F = \{ f_0, f_1 \}, P = \{ (0, 1) \}.$$

Vòng lặp 1:

$$S(0, 1) = \frac{xy}{xy} \cdot (xy + y) + \frac{xy}{x} \cdot (x + 1) = xy + y + xy + y = 0.$$

Kết thúc vòng lặp 1:

$$F = \{ f_0, f_1 \}, P = \emptyset.$$

Dừng thuật toán.

### Test 2

Đầu vào $\boxed{f_0 = xy + yz + 1}$ và $\boxed{f_1 = xz + y + z}$. Ban đầu

$$F = \{ f_0, f_1 \}, P = \{ (0, 1) \}.$$

Vòng lặp 1:

$$S(0, 1) = \frac{xyz}{xy} \cdot (xy + yz + 1) + \frac{xyz}{xz} \cdot (xz + y + z) = xyz + yz + z + xyz + y + yz = y + z.$$

Dễ thấy $y + z$ không chia hết cho bất kì đa thức nào trong $F$ nên $\boxed{f_2 = y + z}$. Kết thúc vòng lặp 1:

$$F = \{ f_0, f_1, f_2 \}, P = \{ (0, 2), (1, 2) \}.$$

Vòng lặp 2:

$$S(0, 2) = \frac{xy}{xy} \cdot (xy + yz + 1) + \frac{xy}{y} \cdot (y + z) = xy + yz + 1 + xy + xz = xz + yz + 1.$$

Thực hiện phép chia đa thức

$$xz + yz + 1 = (z + 1) + 0 \cdot \underbrace{(xy + yz + 1)}_{f_0} + 1 \cdot \underbrace{(xz + y + z)}_{f_1} + (z + 1) \cdot \underbrace{(y + z)}_{f_2}.$$

Vì $z + 1 \neq 0$ nên $\boxed{f_3 = z + 1}$. Kết thúc vòng lặp 2:

$$F = \{ f_0, f_1, f_2, f_3 \}, P = \{ (1, 2), (0, 3), (1, 3), (2, 3) \}.$$

Vòng lặp 3:

$$S(1, 2) = \frac{xyz}{xy} \cdot (xz + y + z) + \frac{xyz}{y} \cdot (y + z) = xyz + y + yz + xyz + xz = xz + yz + y.$$

Thực hiện phép chia đa thức

$$xz + yz + y = \boxed{0} + 0 \cdot \underbrace{(xy + yz + 1)}_{f_0} +  1 \cdot \underbrace{(xz + y + z)}_{f_1} + z \cdot \underbrace{(y + z)}_{f_2} + 0 \cdot \underbrace{(z + 1)}_{f_3}.$$

Kết thúc vòng lặp 3:

$$F = \{ f_0, f_1, f_2, f_3 \}, P = \{ (0, 3), (1, 3), (2, 3) \}.$$

Vòng lặp 4:

$$S(0, 3) = \frac{xyz}{xy} \cdot (xy + yz + 1) + \frac{xyz}{z} \cdot (z + 1) = xyz + yz + z + xyz + xy = xy + yz + z.$$

Thực hiện phép chia

$$xy + yz + z = \boxed{0} + 1 \cdot \underbrace{(xy + yz + 1)}_{f_0} + 0 \cdot \underbrace{(xz + y + z)}_{f_1} + 0 \cdot \underbrace{(y + z)}_{f_2} + 1 \cdot \underbrace{(z + 1)}_{f_3}.$$

Kết thúc vòng lặp 4:

$$F = \{ f_0, f_1, f_2, f_3 \}, P = \{ (1, 3), (2, 3) \}.$$

Vòng lặp 5:

$$S(1, 3) = \frac{xz}{xz} \cdot (xz + y + z) + \frac{xz}{z} \cdot (z + 1) = xz + y + z + xz + x = x + y + z.$$

Thực hiện phép chia

$$x + y + z = \boxed{x} + 0 \cdot \underbrace{(xy + yz + 1)}_{f_0} + 0 \cdot \underbrace{(xz + y + z)}_{f_1} + 1 \cdot \underbrace{(y + z)}_{f_2} + 0 \cdot \underbrace{(z + 1)}_{f_3}.$$

Vì $x \neq 0$ nên $\boxed{f_4 = x}$. Kết thúc vòng lặp 5:

$$F = \{ f_0, f_1, f_2, f_3, f_4 \}, P = \{ (2, 3), (0, 4), (1, 4), (2, 4), (3, 4) \}.$$

Các phép chia $S$-poly sau đó luôn trả về $0$. Như vậy thuật toán Buchberger trả về cơ sở gồm $5$ đa thức.
