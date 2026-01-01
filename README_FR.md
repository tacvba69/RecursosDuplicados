# Ressources Dupliquées - Suppresseur de Fichiers Dupliqués

## 📋 Description

**Ressources Dupliquées** est une application Windows développée en VB.NET qui permet de trouver et supprimer les fichiers dupliqués sur votre système. L'application utilise le hachage MD5 pour identifier les fichiers identiques et offre une interface intuitive avec support multilingue.

## ✨ Caractéristiques Principales

### 🔍 Recherche Intelligente
- Recherche récursive dans les dossiers et sous-dossiers
- Identification précise via hachage MD5
- Progression en temps réel pendant l'analyse
- Support pour tous les types de fichiers

### 🖼️ Visualisation Avancée
- **Miniatures réelles** pour les images et vidéos
- **Icônes système** pour les autres types de fichiers
- **Vue grandes icônes** avec zoom (Ctrl + molette de la souris)
- **Vue détails** avec informations complètes
- **Regroupement visuel** des fichiers dupliqués

### 🎯 Sélection Intelligente
- Sélection automatique des doublons (garde une copie)
- Boutons de sélection rapide :
  - ✅ Sélectionner tout
  - ❌ Tout désélectionner
  - 🔄 Inverser la sélection
- Statistiques en temps réel de l'espace à libérer

### 🌍 Multilingue
- **6 langues supportées :**
  - 🇪🇸 Espagnol
  - 🇺🇸 Anglais
  - 🇫🇷 Français
  - 🇩🇪 Allemand
  - 🇮🇹 Italien
  - 🇵🇹 Portugais
- Sélection de la langue au premier démarrage
- Changement de langue à tout moment depuis le menu

### 🗑️ Suppression Sécurisée
- Envoi à la corbeille (pas de suppression permanente)
- Confirmation avant de supprimer
- Validation des permissions et chemins
- Rapport détaillé des fichiers supprimés

### ⚡ Optimisations
- Traitement asynchrone (ne bloque pas l'interface)
- Cache intelligent des miniatures
- Nettoyage automatique de la mémoire
- Protection contre DoS (limites de fichiers)

## 🚀 Comment Utiliser

### 1. Rechercher les Doublons
1. Cliquez sur le bouton **Rechercher** (📁) dans la barre d'outils
2. Sélectionnez le dossier à analyser
3. Attendez la fin de l'analyse (vous verrez la progression dans la barre d'état)

### 2. Examiner les Résultats
- Les fichiers dupliqués apparaissent regroupés
- Chaque groupe montre combien de fichiers dupliqués il contient
- Par défaut, tous sauf un fichier de chaque groupe sont automatiquement sélectionnés

### 3. Ajuster la Sélection
- Utilisez les cases à cocher pour sélectionner/désélectionner des fichiers individuels
- Utilisez les boutons de sélection rapide pour les opérations en masse
- Observez les statistiques dans la barre d'état

### 4. Supprimer les Fichiers
1. Cliquez sur le bouton **Supprimer** (🗑️)
2. Confirmez la suppression
3. Les fichiers seront envoyés à la corbeille

## 🎨 Fonctionnalités de l'Interface

### Vue Icônes
- Affiche de grandes miniatures des fichiers
- **Zoom :** Appuyez sur **Ctrl** et déplacez la molette de la souris pour augmenter/diminuer la taille
- Idéal pour examiner les images et vidéos

### Vue Détails
- Affiche des informations complètes en format tableau
- Colonnes : Fichier, Chemin, Taille, Type
- **Tri :** Cliquez sur n'importe quelle colonne pour trier

### Zoom des Miniatures
- **Augmenter :** Ctrl + Molette vers le haut
- **Diminuer :** Ctrl + Molette vers le bas
- Plage : 64px - 256px

## 🌐 Changer de Langue

### Première Fois
- Au premier démarrage de l'application, une boîte de dialogue apparaîtra pour sélectionner la langue
- Choisissez votre langue préférée et cliquez sur "Accepter"

### Changer de Langue
1. Cliquez sur le bouton **Langue** (🌐) dans la barre d'outils
2. Sélectionnez la nouvelle langue dans le menu déroulant
3. Cliquez sur "Accepter"
4. La langue sera appliquée immédiatement

## ⚙️ Configuration Système Requise

- **Système d'exploitation :** Windows 10 ou supérieur
- **.NET Framework :** .NET 8.0 ou supérieur
- **Mémoire :** Minimum 2 Go RAM (4 Go recommandé)
- **Espace disque :** 50 Mo pour l'application

## 🔒 Sécurité

- Validation des chemins et permissions
- Normalisation des chemins pour prévenir les attaques
- Limites de protection contre DoS
- Confirmation avant de supprimer les fichiers
- Suppression sécurisée vers la corbeille (récupérable)

## 📊 Limites et Protections

- **Maximum de fichiers :** 50 000 (avec avertissement)
- **Taille maximale de fichier :** 50 Go
- **Cache d'images :** 50 000 entrées (nettoyage automatique)
- **ImageList :** 50 000 images (nettoyage intelligent)

## 🐛 Dépannage

### L'application ne trouve pas de doublons
- Vérifiez que vous avez les permissions de lecture dans le dossier
- Assurez-vous qu'il y a vraiment des fichiers dupliqués
- Certains fichiers peuvent être en cours d'utilisation ou verrouillés

### Les miniatures ne s'affichent pas
- Vérifiez que les fichiers existent
- Certains formats peuvent ne pas avoir de support de miniatures
- Essayez de régénérer les miniatures en changeant le zoom

### Erreur lors de la suppression de fichiers
- Vérifiez que vous avez les permissions d'écriture
- Assurez-vous que les fichiers ne sont pas en cours d'utilisation
- Certains fichiers système ne peuvent pas être supprimés

## 📝 Notes

- Les fichiers supprimés vont à la corbeille et peuvent être récupérés
- L'analyse peut prendre du temps dans les dossiers avec beaucoup de fichiers
- Il est recommandé de fermer d'autres programmes pendant l'analyse intensive
- Les miniatures sont générées la première fois et stockées en cache

## 👨‍💻 Développement

Développé en Visual Basic .NET (.NET 8.0)
- Interface : Windows Forms
- Hachage : MD5 pour l'identification des doublons
- Miniatures : API Windows Shell

## 📄 Licence

Ce projet est open source et disponible pour un usage personnel et commercial.

---

**Version :** 1.0  
**Dernière mise à jour :** 2024

