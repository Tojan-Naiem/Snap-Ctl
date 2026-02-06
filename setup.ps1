Write-Host ""
Write-Host "  ███████╗ ███╗   ██╗  █████╗  ██████╗  ██╗   ██╗"
Write-Host "  ██╔════╝ ████╗  ██║ ██╔══██╗ ██╔══██╗ ╚██╗ ██╔╝"
Write-Host "  ███████╗ ██╔██╗ ██║ ███████║ ██████╔╝  ╚████╔╝ "
Write-Host "  ╚════██║ ██║╚██╗██║ ██╔══██║ ██╔═══╝    ╚██╔╝  "
Write-Host "  ███████║ ██║ ╚████║ ██║  ██║ ██║         ██║   "
Write-Host "  ╚══════╝ ╚═╝  ╚═══╝ ╚═╝  ╚═╝ ╚═╝         ╚═╝   "
Write-Host ""
Write-Host "  🔧 Setting up snapy..." -ForegroundColor Cyan
Write-Host ""

$SCRIPT_DIR = Split-Path -Parent $MyInvocation.MyCommand.Path
$MODELS_DIR = Join-Path $SCRIPT_DIR "Models"

Write-Host "  [1/4] Checking Tesseract installation..."
if (Get-Command tesseract -ErrorAction SilentlyContinue) {
    Write-Host "        ✓ Tesseract already installed." -ForegroundColor Green
} else {
    Write-Host "        ✗ Tesseract not found!" -ForegroundColor Red
    Write-Host "        Please install Tesseract from: https://github.com/UB-Mannheim/tesseract/wiki" -ForegroundColor Yellow
    Write-Host "        After installation, add it to your PATH and run this script again." -ForegroundColor Yellow
    exit 1
}

Write-Host ""
Write-Host "  [2/4] Installing Python dependencies..."
pip install torch clip-by-openai numpy
Write-Host "        ✓ Python deps installed." -ForegroundColor Green

Write-Host ""
Write-Host "  [3/4] Exporting CLIP models to ONNX..."
if ((Test-Path "$MODELS_DIR/clip_image.onnx") -and (Test-Path "$MODELS_DIR/clip_text.onnx")) {
    Write-Host "        ✓ ONNX models already exist." -ForegroundColor Green
} else {
    Push-Location $MODELS_DIR
    python export_clip_to_onnx.py
    Pop-Location
    Write-Host "        ✓ ONNX models exported." -ForegroundColor Green
}

Write-Host ""
Write-Host "  [4/4] Generating text embeddings..."
if (Test-Path "$MODELS_DIR/text_embeddings.bin") {
    Write-Host "        ✓ Embeddings already exist." -ForegroundColor Green
} else {
    Push-Location $MODELS_DIR
    python export_text_embeddings.py
    Pop-Location
    Write-Host "        ✓ Embeddings generated." -ForegroundColor Green
}

Write-Host ""
Write-Host "  Setup complete! Use snapy like this:" -ForegroundColor Green
Write-Host ""
Write-Host "      dotnet run --project Snapy.Cli organize <path>"
Write-Host "      dotnet run --project Snapy.Cli search <text>"
Write-Host "      dotnet run --project Snapy.Cli stats <path>"
Write-Host "      dotnet run --project Snapy.Cli info <file_path>"
Write-Host "      dotnet run --project Snapy.Cli restart <path>"
Write-Host ""
Write-Host "  Or build and use the executable:" -ForegroundColor Cyan
Write-Host "      dotnet publish -c Release"
Write-Host "      ./bin/Release/net8.0/publish/snapy.exe organize <path>"
Write-Host ""