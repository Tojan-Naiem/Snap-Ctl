# Snapy

**AI-powered screenshot organizer using CLIP and OCR.**

Automatically categorizes your screenshots into folders (Person, Documents, Code, Browser, Chat, Games, Other) and makes them searchable by text content.

## Features

- Smart classification using OpenAI's CLIP model
- OCR-powered text search (English + Arabic)
- Runs completely offline
- Fast ONNX inference

## Installation

**Prerequisites**: .NET 8.0, Python 3.8+, Ubuntu/Debian Linux

```bash
git clone https://github.com/yourusername/snapy.git
cd snapy
chmod +x setup.sh
./setup.sh
```

## Commands

### organize
Classify and organize screenshots into category folders:
```bash
./snapy organize <path>
```

### search
Search screenshots by text content:
```bash
./snapy search <text> from <path>
```

### stats
View statistics about organized screenshots:
```bash
./snapy stats <path>
```

### info
Get detailed information about a specific screenshot:
```bash
./snapy info <file_path>
```

### restart
Undo organization and restore original structure:
```bash
./snapy restart <path>
```

## How It Works

**Classification**: Uses CLIP (ViT-B/32) to convert images to embeddings and match them against category text embeddings.

**Search**: Tesseract OCR extracts text from screenshots, stores it in SQLite, enabling fast full-text search.

## Technical Stack

- C# (.NET 8.0)
- CLIP model (ONNX)
- Tesseract OCR
- SQLite
- ImageSharp

## License

MIT License
