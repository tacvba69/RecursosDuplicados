# Doppelte Ressourcen - Duplikat-Datei-Entferner

## 📋 Beschreibung

**Doppelte Ressourcen** ist eine Windows-Anwendung, die in VB.NET entwickelt wurde und es ermöglicht, doppelte Dateien auf Ihrem System zu finden und zu löschen. Die Anwendung verwendet MD5-Hashing zur Identifizierung identischer Dateien und bietet eine intuitive Benutzeroberfläche mit mehrsprachiger Unterstützung.

## ✨ Hauptfunktionen

### 🔍 Intelligente Suche
- Rekursive Suche in Ordnern und Unterordnern
- Präzise Identifizierung mittels MD5-Hash
- Echtzeit-Fortschritt während der Analyse
- Unterstützung für alle Dateitypen

### 🖼️ Erweiterte Visualisierung
- **Echte Miniaturansichten** für Bilder und Videos
- **System-Icons** für andere Dateitypen
- **Große Icon-Ansicht** mit Zoom (Strg + Mausrad)
- **Detailansicht** mit vollständigen Informationen
- **Visuelle Gruppierung** doppelter Dateien

### 🎯 Intelligente Auswahl
- Automatische Auswahl von Duplikaten (behält eine Kopie)
- Schnellauswahl-Buttons:
  - ✅ Alle auswählen
  - ❌ Alle abwählen
  - 🔄 Auswahl umkehren
- Echtzeit-Statistiken des freizugebenden Speicherplatzes

### 🌍 Mehrsprachig
- **6 unterstützte Sprachen:**
  - 🇪🇸 Spanisch
  - 🇺🇸 Englisch
  - 🇫🇷 Französisch
  - 🇩🇪 Deutsch
  - 🇮🇹 Italienisch
  - 🇵🇹 Portugiesisch
- Sprachauswahl beim ersten Start
- Sprache jederzeit über das Menü ändern

### 🗑️ Sichere Löschung
- Versand in den Papierkorb (keine dauerhafte Löschung)
- Bestätigung vor dem Löschen
- Validierung von Berechtigungen und Pfaden
- Detaillierter Bericht gelöschter Dateien

### ⚡ Optimierungen
- Asynchrone Verarbeitung (blockiert die Oberfläche nicht)
- Intelligenter Miniaturansichten-Cache
- Automatische Speicherbereinigung
- DoS-Schutz (Dateigrenzen)

## 🚀 Verwendung

### 1. Nach Duplikaten Suchen
1. Klicken Sie auf die Schaltfläche **Suchen** (📁) in der Symbolleiste
2. Wählen Sie den zu analysierenden Ordner aus
3. Warten Sie, bis die Analyse abgeschlossen ist (Sie sehen den Fortschritt in der Statusleiste)

### 2. Ergebnisse Überprüfen
- Doppelte Dateien erscheinen gruppiert
- Jede Gruppe zeigt an, wie viele doppelte Dateien sie enthält
- Standardmäßig werden automatisch alle außer einer Datei aus jeder Gruppe ausgewählt

### 3. Auswahl Anpassen
- Verwenden Sie Kontrollkästchen, um einzelne Dateien auszuwählen/abzuwählen
- Verwenden Sie Schnellauswahl-Buttons für Massenoperationen
- Beobachten Sie Statistiken in der Statusleiste

### 4. Dateien Löschen
1. Klicken Sie auf die Schaltfläche **Löschen** (🗑️)
2. Bestätigen Sie die Löschung
3. Dateien werden in den Papierkorb verschoben

## 🎨 Interface-Funktionen

### Icon-Ansicht
- Zeigt große Miniaturansichten der Dateien
- **Zoom:** Drücken Sie **Strg** und bewegen Sie das Mausrad, um die Größe zu erhöhen/verringern
- Ideal zum Überprüfen von Bildern und Videos

### Detailansicht
- Zeigt vollständige Informationen im Tabellenformat
- Spalten: Datei, Pfad, Größe, Typ
- **Sortierung:** Klicken Sie auf eine beliebige Spalte zum Sortieren

### Miniaturansichten-Zoom
- **Vergrößern:** Strg + Rad nach oben
- **Verkleinern:** Strg + Rad nach unten
- Bereich: 64px - 256px

## 🌐 Sprache Ändern

### Erstes Mal
- Beim ersten Start der Anwendung erscheint ein Dialog zur Sprachauswahl
- Wählen Sie Ihre bevorzugte Sprache und klicken Sie auf "Akzeptieren"

### Sprache Ändern
1. Klicken Sie auf die Schaltfläche **Sprache** (🌐) in der Symbolleiste
2. Wählen Sie die neue Sprache aus dem Dropdown-Menü aus
3. Klicken Sie auf "Akzeptieren"
4. Die Sprache wird sofort angewendet

## ⚙️ Systemanforderungen

- **Betriebssystem:** Windows 10 oder höher
- **.NET Framework:** .NET 8.0 oder höher
- **Arbeitsspeicher:** Mindestens 2 GB RAM (4 GB empfohlen)
- **Festplattenspeicher:** 50 MB für die Anwendung

## 🔒 Sicherheit

- Validierung von Pfaden und Berechtigungen
- Pfadnormalisierung zur Angriffsprävention
- DoS-Schutzgrenzen
- Bestätigung vor dem Löschen von Dateien
- Sichere Löschung in den Papierkorb (wiederherstellbar)

## 📊 Grenzen und Schutzmaßnahmen

- **Maximale Dateien:** 50.000 (mit Warnung)
- **Maximale Dateigröße:** 50 GB
- **Bild-Cache:** 50.000 Einträge (automatische Bereinigung)
- **ImageList:** 50.000 Bilder (intelligente Bereinigung)

## 🐛 Fehlerbehebung

### Anwendung findet keine Duplikate
- Überprüfen Sie, ob Sie Leseberechtigungen im Ordner haben
- Stellen Sie sicher, dass es tatsächlich doppelte Dateien gibt
- Einige Dateien können in Verwendung oder gesperrt sein

### Miniaturansichten werden nicht angezeigt
- Überprüfen Sie, ob die Dateien existieren
- Einige Formate haben möglicherweise keine Miniaturansichten-Unterstützung
- Versuchen Sie, die Miniaturansichten durch Ändern des Zooms neu zu generieren

### Fehler beim Löschen von Dateien
- Überprüfen Sie, ob Sie Schreibberechtigungen haben
- Stellen Sie sicher, dass Dateien nicht in Verwendung sind
- Einige Systemdateien können nicht gelöscht werden

## 📝 Hinweise

- Gelöschte Dateien gehen in den Papierkorb und können wiederhergestellt werden
- Die Analyse kann in Ordnern mit vielen Dateien Zeit in Anspruch nehmen
- Es wird empfohlen, andere Programme während der intensiven Analyse zu schließen
- Miniaturansichten werden beim ersten Mal generiert und im Cache gespeichert

## 👨‍💻 Entwicklung

Entwickelt in Visual Basic .NET (.NET 8.0)
- Interface: Windows Forms
- Hash: MD5 zur Duplikat-Identifizierung
- Miniaturansichten: Windows Shell API

## 📄 Lizenz

Dieses Projekt ist Open Source und für den persönlichen und kommerziellen Gebrauch verfügbar.

---

**Version:** 1.0  
**Letzte Aktualisierung:** 2024

