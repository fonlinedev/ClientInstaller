# ClientInstaller: Build, Publishing & UI Reference Guide

This document serves as a complete technical reference for building, packaging, debugging, and customizing the portable **ClientInstaller** WinForms application.

---

## 1. Project Configuration (`.csproj`)

To ensure the Visual Studio Form Designer works without architecture or path-resolution crashes while maintaining the ability to publish 32-bit executables, keep your `ClientInstaller.csproj` file set to standard `Any CPU` defaults:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <EmbeddedResource Include="payload.zip" />
  </ItemGroup>

</Project>
```

> **Important Note:** Do **not** hardcode `<PlatformTarget>x86</PlatformTarget>` or `<RuntimeIdentifier>win-x86</RuntimeIdentifier>` directly inside the `.csproj` file. Doing so prevents Visual Studio's out-of-process WinForms designer from resolving the assembly output path.

---

## 2. Publishing as a Standalone 32-Bit (`win-x86`) Single-File `.exe`

Instead of using Visual Studio's Publish UI (which can occasionally trigger skipped builds or multi-file outputs), use the `.NET CLI` to force a clean, self-contained, single-file build.

### Step-by-Step Build Process:
1. Open **Developer PowerShell** in Visual Studio (`Tools` > `Command Line` > `Developer PowerShell`).
2. Navigate to your project directory containing `ClientInstaller.csproj`:
   ```powershell
   cd "C:\Games\FOnlineServer\Installer\ClientInstaller\ClientInstaller"
   ```
3. Execute the single-file publish command:
   ```powershell
   dotnet publish -c Release -r win-x86 --self-contained true -p:PublishSingleFile=true
   ```

### Output Location & Distribution:
* **Output Folder:** `bin\Release\net10.0-windows\win-x86\publish\`
* **Files to Distribute:** Only `ClientInstaller.exe` is required. It contains the .NET runtime, all dependencies, UI assets, and `payload.zip` bundled inside.
* **Excluded Files:** You can safely ignore or delete `ClientInstaller.pdb` before distributing to users—it is only used for local crash debugging.

---

## 3. Troubleshooting the WinForms Visual Designer

If `Form1.cs [Design]` fails to load with the error *"Could not retrieve project assembly output path"*:

1. **Verify `.csproj` Settings:** Ensure `<PlatformTarget>` and `<RuntimeIdentifier>` are removed from `ClientInstaller.csproj`.
2. **Check Toolbar Configuration:** In Visual Studio's top toolbar, ensure the configuration dropdowns are set to **Debug** and **Any CPU**.
3. **Rebuild the Solution:** Press `Ctrl + Shift + B` (`Build` > `Build Solution`) so the designer has a fresh assembly to reference.
4. **Reload Designer:** Click **Reload the designer** on the error banner (or close and reopen `Form1.cs`).

---

## 4. Updating Assets & UI Customization

### A. Updating the Game Files (`payload.zip`)
1. Update or add new files/folders inside your `payload.zip` archive.
2. Replace the existing `payload.zip` in your project directory (`C:\Games\FOnlineServer\Installer\ClientInstaller\ClientInstaller\`).
3. Re-run the `dotnet publish` command above to bundle the updated zip into a new `ClientInstaller.exe`.

*(Note: The extraction logic automatically creates any subfolders defined inside `payload.zip` and overwrites existing files at the target path.)*

### B. Adding a Background Image to the Installer Form
When using the Form Designer (`Form1.cs [Design]`):
1. Click once on the main Form canvas.
2. In the **Properties** window (bottom-right), locate **BackgroundImage**.
3. Click **`...`** > **Import**, select your image file (`.png` or `.jpg`), and click **OK**.
4. Set **BackgroundImageLayout** to **Stretch** or **Zoom** so the image scales cleanly to the window dimensions.

*(Visual Studio automatically embeds designer-imported images into `Form1.resx`, meaning they are compiled directly into your single `.exe` without requiring external image files.)*