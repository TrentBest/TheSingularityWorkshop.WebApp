#!/usr/bin/env python3
"""Check C# file inventory and local UML references; not a C# declaration parser."""
import re, sys
from pathlib import Path
root = Path(sys.argv[1]).resolve() if len(sys.argv) > 1 else Path.cwd()
uml = root / 'docs' / 'UML'
readme = uml / 'README.md'
errors = []
if not readme.is_file():
    print('ERROR: missing docs/UML/README.md')
    sys.exit(1)
inventory = set()
for line in readme.read_text(encoding='utf-8').splitlines():
    if line.startswith('|'):
        first = line.strip('|').split('|')[0].strip().strip('` ')
        if first.endswith('.cs'):
            inventory.add(first)
sources = {p.relative_to(root).as_posix() for p in root.rglob('*.cs') if not any(x in {'bin','obj','.git'} for x in p.parts)}
for p in sorted(sources - inventory): errors.append('C# source missing from inventory: ' + p)
for p in sorted(inventory - sources): errors.append('Inventory references missing source: ' + p)
for page in uml.glob('*.md'):
    for target in re.findall(r'!?\[[^\]]*\]\(([^)]+)\)', page.read_text(encoding='utf-8')):
        if target.startswith(('https://','http://','#','mailto:')): continue
        local = target.split('#',1)[0]
        if local and not (page.parent / local).resolve().exists(): errors.append('Broken link in ' + str(page.relative_to(root)) + ': ' + target)
svgs = list((uml / 'assets').glob('*.svg'))
if not svgs: errors.append('No UML SVGs found in docs/UML/assets')
for svg in svgs:
    data = svg.read_text(encoding='utf-8')
    if not re.search(r'<title>\s*[^<\s]', data, re.I): errors.append('SVG missing title: ' + str(svg.relative_to(root)))
    if not re.search(r'<desc>\s*[^<\s]', data, re.I): errors.append('SVG missing description: ' + str(svg.relative_to(root)))
if errors:
    print('\n'.join('ERROR: ' + e for e in errors))
    sys.exit(1)
print(f'Passed: {len(sources)} C# source files inventoried; {len(svgs)} SVGs have title/description metadata.')
print('Scope: file inventory and local links only; no C# declaration drift detection.')
