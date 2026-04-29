# CupkekGames RPGStats — AI Agent Instructions

## Package Overview

**CupkekGames RPGStats** (`com.cupkekgames.rpgstats`) is the character stats system. Stats, attributes, modifiers, leveling. Built on the data foundation, with Luna UI integration for stat displays.

## Critical: Do not hand-edit Unity serialized assets or `.meta` files

Apply scene/SO changes in Unity Editor; preserve `.meta` GUIDs across moves.

## Package Structure

```
com.cupkekgames.rpgstats/
  package.json
  README.md
  AGENTS.md
  RPGStats/                      ← CupkekGames.RPGStats.asmdef
    Runtime/                       (stat types, attributes, modifiers)
    Editor/
```

## Dependencies

- `com.cupkekgames.luna` — UI integration (for stat display widgets)
- `com.cupkekgames.data` — stat data persistence + IData
- `com.cupkekgames.services`

## Coding Conventions

- **Namespace**: `CupkekGames.RPGStats`
- **Asmdefs**: GUID references
- **String keys for stat IDs**: cross-references between modifiers and stats use string keys, resolved at runtime
- **Strict typing**

## Related packages

- `com.cupkekgames.inventory` — can reference rpgstats for equipment modifiers via the `InventorySystem.RPGStats` bridge
