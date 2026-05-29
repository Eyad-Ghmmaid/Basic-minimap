# Basic minimap
# Unity Minimap Setup

## Kurzbeschreibung

Dieses Kurzdokument erklärt die Minimal-Schritte, damit das Unity-Projekt mit der Minimap korrekt funktioniert.

> Das Projekt enthält eine Demo-Szene.

---

# 1. Projekt öffnen

* Öffne **Unity Hub** oder **Unity** und wähle den Projektordner aus
  (z. B. den Ordner, in dem sich diese README befindet).
* Warte, bis Unity alle Assets importiert hat.

---

# 2. Wichtige Player-Einstellungen

Pfad:

```text
Edit > Project Settings > Player > Other Settings
```

## Einstellungen

* Setze die **API Compatibility Level** auf:

```text
.NET 4.x
```

oder

```text
.NET 4.x Equivalent
```

damit Bibliotheken funktionieren, die das .NET Framework 4.7.1 erwarten.

* Falls Probleme mit dem **Scripting Backend** auftreten, verwende standardmäßig:

```text
Mono
```

oder die für deine Unity-Version empfohlene Einstellung.

---

# 3. Minimap in die Szene einfügen

* Suche im **Project-Fenster** nach einem Prefab oder GameObject mit Namen wie:

```text
Minimap
```

oder in einem Ordner wie:

```text
Prefabs / UI
```

* Ziehe das Minimap-Prefab in die Szene.

Alternativ können die UI-Elemente manuell erstellt werden:

* `Canvas`
* `RawImage`
* `RenderTexture`
* `Minimap-Kamera`

## Minimap-Kamera konfigurieren

* Erstelle eine eigene Layer für Minimap-Objekte, z. B.:

```text
Minimap
```

* Stelle die **Culling Mask** der Minimap-Kamera so ein, dass nur diese Layer gerendert werden.

* Verwende bei Bedarf eine `RenderTexture` und weise diese einem `RawImage` in der UI zu.

---

# 4. Player / Ziel zuweisen

Falls ein Minimap-Script ein `target`-Feld benötigt:

* Ziehe das Spieler-GameObject in das entsprechende Inspector-Feld.

---

# 5. UI-Setup

* Stelle sicher, dass Canvas und UI-Elemente korrekt skaliert sind.
* Verwende dafür den:

```text
Canvas Scaler
```

* Positioniere die Minimap an der gewünschten Stelle auf dem Bildschirm.

---

# 6. Szenen in Build Settings

Pfad:

```text
File > Build Settings
```

* Füge die aktuelle Szene zu:

```text
Scenes In Build
```

hinzu, damit sie beim Start des Builds geladen wird.

---

# 7. Häufige Probleme & Fehlerbehebung

## Minimap zeigt nichts

Prüfe:

* Culling Mask
* Layer der zu rendernden Objekte
* Ob die Minimap-Kamera aktiviert ist

## Fehlende Script-Referenzen

* Öffne die Szene
* Wähle das betroffene GameObject
* Setze alle Inspector-Referenzen erneut

## Kompatibilitätsfehler

* Prüfe den:

```text
Api Compatibility Level
```

(siehe Punkt 2)

---

# Ende
