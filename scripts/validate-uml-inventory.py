#!/usr/bin/env python3
"""Check C# inventory rows and local UML references; not a C# declaration parser."""
import re
import sys
from pathlib import Path

root = Path(sys.argv[1]).resolve() if len(sys.argv) > 1 else Path.cwd()
uml = root / "docs" / "UML"
readme = uml / "README.md"
errors = []
if not readme.is_file():
    print("ERROR: missing docs/UML/README.md")
    sys.exit(1)

inventory = set()
for line in readme.read_text(encoding="utf-8").splitlines():
    if not line.startswith("|"):
        continue
    cells = [cell.strip() for cell in line.strip("|").split("|")]
    if not cells or not re.search(r"`[^\`]+\.cs`", cells[0]):
        continue
    source_match = re.search(r"`([^\`]+\.cs)`", cells[0])
    if not source_match:
        continue
    source = source_match.group(1)
    inventory.add(source)
    # Every source inventory row must point to at least one dedicated Markdown page.
    if len(cells) < 3:
        errors.append("Inventory row has no documentation column: " + source)
        continue
    doc_links = re.findall(r"\[[^\]]+\]\(([^)]+\.md)(?:#[^)]*)?\)", cells[2], re.I)
    if not doc_links:
        errors.append("Source inventory row has no Markdown documentation link: " + source)
    for target in doc_links:
        if target.startswith(("https://", "http://", "mailto:")):
            continue
        if not (readme.parent / target.split("#", 1)[0]).resolve().is_file():
            errors.append("Missing source documentation page for " + source + ": " + target)

    # Rows with a dedicated type page must also link a class-card SVG in the UML column.
    # Program.cs is the documented top-level entry-point exception: its sequence is
    # described in Program.md rather than represented as a class-card diagram.
    if len(cells) >= 4 and "entry point" not in cells[1].lower():
        svg_links = re.findall(r"\[[^\]]+\]\(([^)]+\.svg)(?:#[^)]*)?\)", cells[3], re.I)
        if not svg_links:
            errors.append("Source inventory row has no UML SVG link: " + source)
        for target in svg_links:
            if target.startswith(("https://", "http://", "mailto:")):
                continue
            if not (readme.parent / target.split("#", 1)[0]).resolve().is_file():
                errors.append("Missing UML SVG for " + source + ": " + target)

sources = {
    p.relative_to(root).as_posix()
    for p in root.rglob("*.cs")
    if not any(x in {"bin", "obj", ".git"} for x in p.parts)
}
for path in sorted(sources - inventory):
    errors.append("C# source missing from inventory: " + path)
for path in sorted(inventory - sources):
    errors.append("Inventory references missing source: " + path)

for page in uml.glob("*.md"):
    for target in re.findall(r"!?\[[^\]]*\]\(([^)]+)\)", page.read_text(encoding="utf-8")):
        if target.startswith(("https://", "http://", "#", "mailto:")):
            continue
        local = target.split("#", 1)[0]
        if local and not (page.parent / local).resolve().exists():
            errors.append("Broken link in " + str(page.relative_to(root)) + ": " + target)

svgs = list((uml / "assets").glob("*.svg"))
if not svgs:
    errors.append("No UML SVGs found in docs/UML/assets")
for svg in svgs:
    data = svg.read_text(encoding="utf-8")
    if not re.search(r"<title(?:\s[^>]*)?>\s*[^<\s]", data, re.I):
        errors.append("SVG missing title: " + str(svg.relative_to(root)))
    if not re.search(r"<desc(?:\s[^>]*)?>\s*[^<\s]", data, re.I):
        errors.append("SVG missing description: " + str(svg.relative_to(root)))

if errors:
    print("\n".join("ERROR: " + error for error in errors))
    sys.exit(1)
print(f"Passed: {len(sources)} C# source files inventoried; source rows link to documentation; {len(svgs)} SVGs have title/description metadata.")
print("Scope: inventory, documentation links, and SVG metadata only; no C# declaration drift detection.")
