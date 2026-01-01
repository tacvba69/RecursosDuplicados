# Risorse Duplicate - Rimuovi File Duplicati

## 📋 Descrizione

**Risorse Duplicate** è un'applicazione Windows sviluppata in VB.NET che consente di trovare ed eliminare file duplicati sul tuo sistema. L'applicazione utilizza l'hashing MD5 per identificare file identici e offre un'interfaccia intuitiva con supporto multilingue.

## ✨ Caratteristiche Principali

### 🔍 Ricerca Intelligente
- Ricerca ricorsiva in cartelle e sottocartelle
- Identificazione precisa tramite hash MD5
- Progresso in tempo reale durante l'analisi
- Supporto per tutti i tipi di file

### 🖼️ Visualizzazione Avanzata
- **Miniature reali** per immagini e video
- **Icone di sistema** per altri tipi di file
- **Vista icone grandi** con zoom (Ctrl + rotella del mouse)
- **Vista dettagli** con informazioni complete
- **Raggruppamento visivo** dei file duplicati

### 🎯 Selezione Intelligente
- Selezione automatica dei duplicati (mantiene una copia)
- Pulsanti di selezione rapida:
  - ✅ Seleziona tutto
  - ❌ Deseleziona tutto
  - 🔄 Inverti selezione
- Statistiche in tempo reale dello spazio da liberare

### 🌍 Multilingue
- **6 lingue supportate:**
  - 🇪🇸 Spagnolo
  - 🇺🇸 Inglese
  - 🇫🇷 Francese
  - 🇩🇪 Tedesco
  - 🇮🇹 Italiano
  - 🇵🇹 Portoghese
- Selezione della lingua al primo avvio
- Cambio lingua in qualsiasi momento dal menu

### 🗑️ Eliminazione Sicura
- Invio al cestino (non eliminazione permanente)
- Conferma prima di eliminare
- Validazione di permessi e percorsi
- Report dettagliato dei file eliminati

### ⚡ Ottimizzazioni
- Elaborazione asincrona (non blocca l'interfaccia)
- Cache intelligente delle miniature
- Pulizia automatica della memoria
- Protezione DoS (limiti di file)

## 🚀 Come Usare

### 1. Cercare Duplicati
1. Fai clic sul pulsante **Cerca** (📁) nella barra degli strumenti
2. Seleziona la cartella da analizzare
3. Attendi il completamento dell'analisi (vedrai il progresso nella barra di stato)

### 2. Esaminare i Risultati
- I file duplicati appaiono raggruppati
- Ogni gruppo mostra quanti file duplicati contiene
- Per impostazione predefinita, tutti tranne uno di ogni gruppo sono selezionati automaticamente

### 3. Regolare la Selezione
- Usa le caselle di controllo per selezionare/deselezionare file individuali
- Usa i pulsanti di selezione rapida per operazioni di massa
- Osserva le statistiche nella barra di stato

### 4. Eliminare File
1. Fai clic sul pulsante **Elimina** (🗑️)
2. Conferma l'eliminazione
3. I file verranno inviati al cestino

## 🎨 Funzionalità dell'Interfaccia

### Vista Icone
- Mostra grandi miniature dei file
- **Zoom:** Premi **Ctrl** e muovi la rotella del mouse per aumentare/diminuire la dimensione
- Ideale per rivedere immagini e video

### Vista Dettagli
- Mostra informazioni complete in formato tabella
- Colonne: File, Percorso, Dimensione, Tipo
- **Ordinamento:** Fai clic su qualsiasi colonna per ordinare

### Zoom Miniature
- **Aumenta:** Ctrl + Rotella su
- **Diminuisci:** Ctrl + Rotella giù
- Intervallo: 64px - 256px

## 🌐 Cambiare Lingua

### Prima Volta
- All'avvio dell'applicazione per la prima volta, apparirà una finestra di dialogo per selezionare la lingua
- Scegli la tua lingua preferita e fai clic su "Accetta"

### Cambiare Lingua
1. Fai clic sul pulsante **Lingua** (🌐) nella barra degli strumenti
2. Seleziona la nuova lingua dal menu a discesa
3. Fai clic su "Accetta"
4. La lingua verrà applicata immediatamente

## ⚙️ Requisiti di Sistema

- **Sistema Operativo:** Windows 10 o superiore
- **.NET Framework:** .NET 8.0 o superiore
- **Memoria:** Minimo 2 GB RAM (4 GB consigliati)
- **Spazio su disco:** 50 MB per l'applicazione

## 🔒 Sicurezza

- Validazione di percorsi e permessi
- Normalizzazione dei percorsi per prevenire attacchi
- Limiti di protezione DoS
- Conferma prima di eliminare file
- Eliminazione sicura nel cestino (recuperabile)

## 📊 Limiti e Protezioni

- **Massimo di file:** 50.000 (con avviso)
- **Dimensione massima file:** 50 GB
- **Cache immagini:** 50.000 voci (pulizia automatica)
- **ImageList:** 50.000 immagini (pulizia intelligente)

## 🐛 Risoluzione Problemi

### L'applicazione non trova duplicati
- Verifica di avere i permessi di lettura nella cartella
- Assicurati che ci siano effettivamente file duplicati
- Alcuni file potrebbero essere in uso o bloccati

### Le miniature non vengono visualizzate
- Verifica che i file esistano
- Alcuni formati potrebbero non avere supporto per miniature
- Prova a rigenerare le miniature cambiando lo zoom

### Errore nell'eliminazione di file
- Verifica di avere i permessi di scrittura
- Assicurati che i file non siano in uso
- Alcuni file di sistema non possono essere eliminati

## 📝 Note

- I file eliminati vanno nel cestino e possono essere recuperati
- L'analisi può richiedere tempo in cartelle con molti file
- Si consiglia di chiudere altri programmi durante l'analisi intensiva
- Le miniature vengono generate la prima volta e memorizzate nella cache

## 👨‍💻 Sviluppo

Sviluppato in Visual Basic .NET (.NET 8.0)
- Interfaccia: Windows Forms
- Hash: MD5 per l'identificazione dei duplicati
- Miniature: Windows Shell API

## 📄 Licenza

Questo progetto è open source e disponibile per uso personale e commerciale.

---

**Versione:** 1.0  
**Ultimo aggiornamento:** 2024

