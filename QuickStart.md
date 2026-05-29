Kurzbeschreibung

Dieses Kurzdokument erklärt die Minimal-Schritte, damit das Unity-Projekt mit der Minimap korrekt funktioniert.

1) Projekt öffnen
- Öffne Unity Hub oder Unity und wähle den Projektordner (z. B. den Ordner, in dem sich diese README befindet).
- Warte, bis Unity alle Assets importiert hat.

2) Szene öffnen
- Öffne die Hauptszene: `Assets/Scenes` oder suche nach Szenen im `Project`-Fenster. Falls keine Szene vorhanden ist, erstelle eine neue Szene (`File > New Scene`) und speichere sie unter `Assets/Scenes/Main.unity`.

3) Wichtige Player-Einstellungen
- Menü: `Edit > Project Settings > Player > Other Settings`:
  - Setze die API Compatibility Level auf `.NET 4.x` (oder `.NET 4.x Equivalent`), damit Bibliotheken, die .NET Framework 4.7.1 erwarten, funktionieren.
  - Falls Probleme mit dem Scripting Backend auftreten, verwende standardmäßig Mono oder die für dein Unity-Release empfohlene Einstellung.

4) Minimap in die Szene einfügen
- Suche im `Project`-Fenster nach einem Prefab oder GameObject mit Namen wie "Minimap" oder in einem Ordner `Prefabs` / `UI`.
- Ziehe das Minimap-Prefab in die Szene (oder erstelle die benötigten UI-Elemente manuell: `Canvas`, `RawImage` / `RenderTexture`, Minimap-Kamera`).
- Konfiguriere die Minimap-Kamera:
  - Lege eine eigene Layer für Minimap-Objekte an (z. B. `Minimap`) und stelle den Culling Mask der Minimap-Kamera so ein, dass nur diese Layer gerendert werden.
  - Verwende bei Bedarf ein `RenderTexture` für die Minimap und weise dieses einem `RawImage` in der UI zu.

5) Player / Ziel zuweisen
- Falls ein Minimap-Script ein `target`-Feld (z. B. den Spieler) benötigt, ziehe das Spieler-GameObject in das entsprechende Inspector-Feld.

6) UI-Setup
- Stelle sicher, dass Canvas und UI-Elemente korrekt skaliert sind (`Canvas Scaler`) und die Minimap an der gewünschten Position im Screen platziert ist.

7) Szenen in Build Settings
- Menü: `File > Build Settings` und füge die aktuelle Szene zu `Scenes In Build` hinzu, damit sie beim Start des Builds geladen wird.

8) Häufige Probleme & Fehlerbehebung
- Minimap zeigt nichts: Prüfe Culling Mask, Layer der zu rendernden Objekte und ob die Minimap-Kamera aktiviert ist.
- Fehlende Script-Referenzen: Öffne die Szene, wähle das GameObject mit dem Script und setze alle Inspector-Referenzen neu.
- Kompatibilitätsfehler: Prüfe `Api Compatibility Level` (siehe Punkt 3).

Ende
