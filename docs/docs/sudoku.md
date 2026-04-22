# Sudoku

Giải Sudoku trên bảng 9x9 tiêu chuẩn.

Cách dùng:

```cs
using Sudoku;

SudokuMatrix sudokuMatrix = new SudokuMatrix([
    2, 9, 5, 7, 0, 0, 8, 6, 0,
    0, 3, 1, 8, 6, 5, 0, 2, 0,
    8, 0, 6, 0, 0, 0, 0, 0, 0,
    0, 0, 7, 0, 5, 0, 0, 0, 6,
    0, 0, 0, 3, 8, 7, 0, 0, 0,
    5, 0, 0, 0, 1, 6, 7, 0, 0,
    0, 0, 0, 5, 0, 0, 1, 0, 9,
    0, 2, 0, 6, 0, 0, 3, 5, 0,
    0, 5, 4, 0, 0, 8, 6, 7, 2
]);

Console.WriteLine("Input sudoku:");
Console.WriteLine(sudokuMatrix);

sudokuMatrix.Solve();

Console.WriteLine("Output sudoku:");
Console.WriteLine(sudokuMatrix);
```

Lưu ý:

1. Các ô trống được điền bởi số $0$.
2. Sau khi gọi lệnh `Solve`, ma trận sẽ được cập nhật thành kết quả. Do đó mỗi bảng ô vuông chỉ được gọi `Solve` một lần.
