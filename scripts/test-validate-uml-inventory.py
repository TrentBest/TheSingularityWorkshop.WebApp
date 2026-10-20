#!/usr/bin/env python3
"""Regression tests proving the UML inventory validator catches deliberate defects."""
import subprocess
import sys
import tempfile
from pathlib import Path

VALIDATOR = Path(__file__).with_name("validate-uml-inventory.py")


def make_fixture(root: Path) -> None:
    (root / "Infrastructure").mkdir(parents=True)
    uml = root / "docs" / "UML"
    (uml / "assets").mkdir(parents=True)
    (root / "Program.cs").write_text("// top-level entry point\n", encoding="utf-8")
    (root / "Infrastructure" / "A.cs").write_text("class A {}\n", encoding="utf-8")
    (uml / "README.md").write_text(
        "# Inventory\n\n"
        "| C# source file | Type | Documentation |\n"
        "|---|---|---|\n"
        "| `Program.cs` | Entry point | [Program](Program.md) |\n"
        "| `Infrastructure/A.cs` | A | [A](A.md) |\n",
        encoding="utf-8",
    )
    (uml / "Program.md").write_text("# Program\n", encoding="utf-8")
    (uml / "A.md").write_text("# A\n", encoding="utf-8")
    (uml / "assets" / "A.svg").write_text(
        '<svg xmlns="http://www.w3.org/2000/svg"><title>A class</title>'
        '<desc>Class A</desc></svg>\n',
        encoding="utf-8",
    )


def run_validator(root: Path) -> subprocess.CompletedProcess[str]:
    return subprocess.run(
        [sys.executable, str(VALIDATOR), str(root)],
        capture_output=True,
        text=True,
        check=False,
    )


def expect_failure(label: str, mutate, expected: str) -> None:
    with tempfile.TemporaryDirectory() as directory:
        root = Path(directory)
        make_fixture(root)
        mutate(root)
        result = run_validator(root)
        output = result.stdout + result.stderr
        if result.returncode == 0 or expected not in output:
            raise AssertionError(
                f"{label}: expected failure containing {expected!r}; "
                f"exit={result.returncode}, output={output!r}"
            )
        print(f"PASS: {label} is rejected")


def main() -> int:
    with tempfile.TemporaryDirectory() as directory:
        root = Path(directory)
        make_fixture(root)
        result = run_validator(root)
        if result.returncode != 0:
            raise AssertionError("valid fixture failed: " + result.stdout + result.stderr)
        print("PASS: valid fixture is accepted")

    def omit_source(root: Path) -> None:
        readme = root / "docs" / "UML" / "README.md"
        readme.write_text(
            "\n".join(line for line in readme.read_text(encoding="utf-8").splitlines()
                      if "Infrastructure/A.cs" not in line) + "\n",
            encoding="utf-8",
        )

    expect_failure("unlisted C# source", omit_source, "C# source missing from inventory")

    def list_missing_source(root: Path) -> None:
        readme = root / "docs" / "UML" / "README.md"
        with readme.open("a", encoding="utf-8") as stream:
            stream.write("| `Missing.cs` | Missing | [Missing](Missing.md) |\n")

    expect_failure("inventory path that does not exist", list_missing_source,
                   "Inventory references missing source")

    def break_markdown_link(root: Path) -> None:
        (root / "docs" / "UML" / "A.md").write_text(
            "# A\n\n[missing](not-here.md)\n", encoding="utf-8"
        )

    expect_failure("broken local Markdown link", break_markdown_link, "Broken link")

    def remove_svg_title(root: Path) -> None:
        svg = root / "docs" / "UML" / "assets" / "A.svg"
        svg.write_text(svg.read_text(encoding="utf-8").replace("<title>A class</title>",
                                                              "<title></title>"),
                       encoding="utf-8")

    expect_failure("SVG without a title", remove_svg_title, "SVG missing title")
    print("Scope: validator regression tests; no C# declaration drift detection.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
