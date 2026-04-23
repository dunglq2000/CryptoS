# Sudoku

Solve standard 9x9 Sudoku's table.

Cách dùng:

```cs
using CryptoS.Misc.Sudoku;

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

Note:

1. Empty cells are filled with $0$.
2. After calling `Solve`, matrix will be filled with the final result. Therefore any instance of `Sudoku` class should call `Solve` only once.
