# Duplicate Resources - Duplicate File Remover

## 📋 Description

**Duplicate Resources** is a Windows application developed in VB.NET that allows you to find and delete duplicate files on your system. The application uses MD5 hashing to identify identical files and offers an intuitive interface with multi-language support.

## ✨ Main Features

### 🔍 Smart Search
- Recursive search in folders and subfolders
- Precise identification using MD5 hash
- Real-time progress during analysis
- Support for all file types

### 🖼️ Advanced Visualization
- **Real thumbnails** for images and videos
- **System icons** for other file types
- **Large icon view** with zoom (Ctrl + mouse wheel)
- **Details view** with complete information
- **Visual grouping** of duplicate files

### 🎯 Smart Selection
- Automatic selection of duplicates (keeps one copy)
- Quick selection buttons:
  - ✅ Select all
  - ❌ Deselect all
  - 🔄 Invert selection
- Real-time statistics of space to be freed

### 🌍 Multi-Language
- **6 supported languages:**
  - 🇪🇸 Spanish
  - 🇺🇸 English
  - 🇫🇷 French
  - 🇩🇪 German
  - 🇮🇹 Italian
  - 🇵🇹 Portuguese
- Language selection on first start
- Change language anytime from the menu

### 🗑️ Safe Deletion
- Send to recycle bin (not permanent deletion)
- Confirmation before deleting
- Permission and path validation
- Detailed report of deleted files

### ⚡ Optimizations
- Asynchronous processing (doesn't block the interface)
- Smart thumbnail cache
- Automatic memory cleanup
- DoS protection (file limits)

## 🚀 How to Use

### 1. Search for Duplicates
1. Click the **Search** button (📁) in the toolbar
2. Select the folder you want to analyze
3. Wait for the analysis to complete (you'll see progress in the status bar)

### 2. Review Results
- Duplicate files appear grouped
- Each group shows how many duplicate files it contains
- By default, all but one file from each group is automatically selected

### 3. Adjust Selection
- Use checkboxes to select/deselect individual files
- Use quick selection buttons for bulk operations
- Observe statistics in the status bar

### 4. Delete Files
1. Click the **Delete** button (🗑️)
2. Confirm deletion
3. Files will be sent to the recycle bin

## 🎨 Interface Features

### Icon View
- Shows large thumbnails of files
- **Zoom:** Press **Ctrl** and move the mouse wheel to increase/decrease size
- Ideal for reviewing images and videos

### Details View
- Shows complete information in table format
- Columns: File, Path, Size, Type
- **Sorting:** Click any column to sort

### Thumbnail Zoom
- **Increase:** Ctrl + Wheel up
- **Decrease:** Ctrl + Wheel down
- Range: 64px - 256px

## 🌐 Change Language

### First Time
- When starting the application for the first time, a dialog will appear to select the language
- Choose your preferred language and click "Accept"

### Change Language
1. Click the **Language** button (🌐) in the toolbar
2. Select the new language from the dropdown menu
3. Click "Accept"
4. The language will be applied immediately

## ⚙️ System Requirements

- **Operating System:** Windows 10 or higher
- **.NET Framework:** .NET 8.0 or higher
- **Memory:** Minimum 2 GB RAM (4 GB recommended)
- **Disk Space:** 50 MB for the application

## 🔒 Security

- Path and permission validation
- Path normalization to prevent attacks
- DoS protection limits
- Confirmation before deleting files
- Safe deletion to recycle bin (recoverable)

## 📊 Limits and Protections

- **Maximum files:** 50,000 (with warning)
- **Maximum file size:** 50 GB
- **Image cache:** 50,000 entries (automatic cleanup)
- **ImageList:** 50,000 images (smart cleanup)

## 🐛 Troubleshooting

### Application doesn't find duplicates
- Verify you have read permissions in the folder
- Make sure there are actually duplicate files
- Some files may be in use or locked

### Thumbnails don't show
- Verify that files exist
- Some formats may not have thumbnail support
- Try regenerating thumbnails by changing zoom

### Error deleting files
- Verify you have write permissions
- Make sure files are not in use
- Some system files cannot be deleted

## 📝 Notes

- Deleted files go to the recycle bin and can be recovered
- Analysis may take time in folders with many files
- It's recommended to close other programs during intensive analysis
- Thumbnails are generated the first time and stored in cache

## 👨‍💻 Development

Developed in Visual Basic .NET (.NET 8.0)
- Interface: Windows Forms
- Hash: MD5 for duplicate identification
- Thumbnails: Windows Shell API

## 📄 License

This project is open source and available for personal and commercial use.

---

**Version:** 1.0  
**Last update:** 2026
