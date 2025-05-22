# KiraLang

**KiraLang** is a simple interpreted programming language with a custom syntax, designed for educational and experimental purposes. This project features a built-in code editor with real-time lexical and syntax error highlighting using GOLD Parser and ScintillaNET.

## 📽️ Project Demo

Watch a full walkthrough of the project on YouTube:  
[Project Demo Video](https://youtu.be/ya9c1cWNaXk).


## Features

- Custom language grammar (`.cgt`) built using GOLD Parser.
- Live syntax and lexical error reporting.
- Scintilla-based editor with:
  - Syntax highlighting for keywords, strings, numbers, and comments.
  - Line numbering and styled text.
- Logical, arithmetic, and comparison operators support.
- Supports multi-line statements and basic control structures (`if`, `else`, `while`, etc.).

## Sample Syntax

```kira
{
    def x = 10;
    def y = 5;

    if (x > y) then {
        def z = x;
    } else if(x < y) then {
        def z = y;
    }

    while (x > 0 && x > y) then {
        x = x - 1;
    }
}
```
## How to Run

1. **Download the executable**

   Download `KiraLang.exe` from the [Repo](https://github.com/A-Qassem/Kira).

2. **Ensure the grammar file is included**

   Make sure `KiraLang.cgt` is in the **same directory** as the `.exe` file.

3. **Run the application**

   Double-click `KiraLang.exe` to launch the application.

