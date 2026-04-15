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

## F4 algorithm

### Test 1

Đầu vào: $\boxed{g_0 = xy + y}$, $\boxed{g_1 = y + 1}$.

Ban đầu:

$$G = \{ g_0, g_1 \}, P = \{ (0, 1) \}.$$

Vòng lặp 1:

$$P' = \{ (0, 1) \}, P = P \setminus P' = \emptyset, G = \{ xy + y, y + 1 \}.$$

$G' = reduction(P', G)$:

- $L = symbolic(P', G)$:

  - đặt $L = \emptyset$
  - for $(i, j)$ in $P'$:

    - $(i, j) = (0, 1)$:

      - $lcm = lcm(g_0, g_1) = xy$
      - $l = \frac{xy}{xy} \cdot (xy + y) = xy + y$, $r = \frac{xy}{y} \cdot (y + 1) = xy + x$
      - $L = L \cup \{ l, r \} = \{ xy + y, xy + x \}$

  - $done = \{ xy \}$
  
  1. $Mon(L) = \{ xy, x, y \} \neq done = \{ xy \}$:

     - $Mon(L) \setminus done = \{ x, y \}$
     - $m = LM(Mon(L) \setminus done) = x$
     - $done = done \cup \{ m \} = \{ xy, x \}$
     - $LM(g_0) \nmid m$, $LM(g_1) \nmid m$ => đi tới vòng lặp kế

  2. $Mon(L) = \{ xy, x, y \} \neq done = \{ xy, x \}$
     
     - $Mon(L) \setminus done = \{ y \}$
     - $m = LM(Mon(L) \setminus done) = y$
     - $done = done \cup \{ m \} = \{ xy, x, y \}$
     - $LM(g_0) \nmid m$, $LM(g_1) \mid m$ => $\frac{m}{LM(g_1)} \cdot g_1 = \frac{y}{y} \cdot (y + 1) = y + 1$ => $L = L \cup \{  y + 1 \} = \{ xy + y, xy + x, y + 1 \}$

  3. $Mon(L) = \{ xy, x, y, 1 \} \neq done = \{ xy, x, y \}$

     - $Mon(L) \setminus done = \{ 1 \}$
     - $m = LM(Mon(L) \setminus done) = 1$
     - $done = done \cup \{ 1 \} = \{ xy, x, y, 1 \}$
     - $LM(g_0) \nmid m$, $LM(g_1) \nmid m$ => đi tới vòng lặp kế

  4. $Mon(L) = \{ xy, x, y, 1 \} = done$ => kết thúc vòng lặp

  Trả về $L = \{ xy + y, xy + x, y + 1 \}$

- ma trận tương ứng với $L = \{ xy + y, xy + x, y + 1 \}$ là

$$\left(\begin{array}{cccc} xy & x & y & 1 \\ \hline 1 & 1 & 0 & 0 \\ 1 & 0 & 1 & 0 \\ 0 & 0 & 1 & 1 \end{array}\right) \sim \left(\begin{array}{cccc} xy & x & y & 1 \\ \hline 1 & 0 & 0 & 1 \\ 0 & 1 & 0 & 1 \\ 0 & 0 & 1 & 1 \end{array}\right)$$

- $L' = \{ xy + 1, x + 1, y + 1 \}$
- Trả về $G' = \{ x + 1 \}$

Đặt $\boxed{g_2 = x + 1}$. Cập nhật

$$G = G \cup G' = \{ xy + y, y + 1, x + 1 \} = \{ g_0, g_1, g_2 \}, P = \{ (0, 2), (1, 2) \}.$$

Vòng lặp 2:

$$P' = \{ (0, 2), (1, 2) \}, P = P \setminus P' = \emptyset, G = \{ g_0, g_1, g_2 \} = \{ xy + y, y + 1, x + 1 \}$$

$G' = reduction(P', G)$:

- $L = symbolic(P', G)$:

  - đặt $L = \emptyset$
  - for $(i, j)$ in $P'$:

    - $(i, j) = (0, 2)$:

      - $lcm = lcm(g_0, g_2) = xy$
      - $l = \frac{xy}{xy} \cdot (xy + y) = xy + y$, $r = \frac{xy}{x} \cdot (x + 1) = xy + y$
      - $L = L \cup \{ l, r \} = \{ xy + y \}$
    
    - $(i, j) = (1, 2)$:

      - $lcm = lcm(g_1, g_2) = xy$
      - $l = \frac{xy}{y} \cdot (y + 1) = xy + x$, $r = \frac{xy}{x} \cdot (x + 1) = xy + y$
      - $L = L \cup \{ l, r \} = \{ xy + y, xy + x \}$

  - $done = \{ xy \}$

  1. $Mon(L) = \{ xy, x, y \} \neq done = \{ xy \}$

     - $Mon(L) \setminus done = \{ x, y \}$
     - $m = LM(Mon(L) \setminus done) = x$
     - $done = done \cup \{ m \} = \{ xy, x \}$
     - $LM(g_2) \mid m$ => $\frac{x}{LM(g_2)} \cdot g_2 = x + 1$ => $L = L \cup \{ x + 1 \} = \{ xy + y, xy + x, x + 1 \}$

  2. $Mon(L) = \{ xy, x, y, 1 \} \neq done = \{ xy, x \}$

     - $Mon(L) \setminus done = \{ y, 1 \}$
     - $m = LM(Mon(L) \setminus done) = y$
     - $done = done \cup \{ m \} = \{ xy, x, y \}$
     - $LM(g_1) \mid m$ => $\frac{y}{LM(g_1)} \cdot g_1 = y + 1$ => $L = L \cup \{ y + 1 \} = \{ xy + y, xy + x, x + 1, y + 1 \}$

  3. $Mon(L) = \{ xy, x, y, 1 \} \neq done = \{ xy, x, y \}$
     
     - $Mon(L) \setminus done = \{ 1 \}$
     - $m = LM(Mon(L) \setminus done) = 1$
     - $done = done \cup \{ m \} = \{ xy, x, y, 1 \}$
     - $LM(g_i) \nmid m$ với mọi $0 \leqslant i \leqslant 2$

  4. $Mon(L) = \{ xy, x, y, 1 \} = done$, kết thúc vòng lặp

  Trả về $L = \{ xy + y, xy + x, x + 1, y + 1\}$

- ma trận tương ứng với $L = \{ xy + y, xy + x, x + 1, y + 1\}$ là

$$\left(\begin{array}{cccc} xy & x & y & 1 \\ \hline 1 & 0 & 1 & 0 \\ 1 & 1 & 0 & 0 \\ 0 & 1 & 0 & 1 \\ 0 & 0 & 1 & 1 \end{array}\right) \sim \left(\begin{array}{cccc} xy & x & y & 1 \\ \hline 1 & 0 & 0 & 1 \\ 0 & 1 & 0 & 1 \\ 0 & 0 & 1 & 1 \\ 0 & 0 & 0 & 0 \end{array}\right)$$

- $L' = \{ xy + 1, x + 1, y + 1 \}$
- Trả về $G' = \emptyset$

Như vậy kết quả cuối cùng là

$$G = \{ xy + y, x + 1, y + 1 \}$$

là cơ sở Grebner.
