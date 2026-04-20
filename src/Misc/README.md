# Chưa phân loại

## Cây nhị phần tìm kiếm

- `BinaryNode.cs`: node trên cây nhị phân. Mỗi node gồm $3$ trường:
  
  - `Data` - dữ liệu của node;
  - `Left` - con trỏ tới node ở bên trái;
  - `Right` - con trỏ tới node ở bên phải.

- `BinaryTree.cs`: cây nhị phân, cho phép:

  1. Thêm phần tử vào cây theo quy tắc:
     
     - nếu nhỏ hơn node hiện tại thì đi sang cây con bên trái;
     - nếu lớn hơn node hiện tại thì đi sang cây con bên phải.

  2. Tìm kiếm phần tử trên cây theo nguyên tắc:

     - gốc cây có index $0$;
     - nếu node có index là $i$ thì

       - node con bên trái có index là $2i + 1$;
       - node con bên phải có index là $2i + 2$.

  3. Duyệt cây theo $3$ cách:

     - `InOrder` - duyệt node => duyệt cây con bên trái => rồi duyệt cây con bên phải;
     - `PreOrder` - duyệt cây con bên trái => duyệt node => duyệt cây con bên phải;
     - `PostOrder` - duyệt cây con bên trái => duyệt cây con bên phải => duyệt node.
