# FOnline Client Installer

A lightweight, standalone Windows installer built with C# Windows Forms in **Visual Studio 2026** for distributing any *FOnline* game client.

## Features

- **Zero System Footprint:** Extracts client files directly to a user-selected directory without modifying the Windows Registry or requiring administrative privileges.
- **Self-Contained Deployment:** Bundles the game client archive (`payload.zip`) directly inside the compiled executable as an embedded resource.
- **Customizable UI:** Built with Windows Forms, allowing easy customization of background graphics, layout, and branding before compiling.

## Prerequisites

- **Visual Studio 2026** with the **.NET Desktop Development** workload installed.
- **.NET SDK** matching the target framework defined in `ClientInstaller.csproj`.
- A compressed archive named `payload.zip` containing your *FOnline* client files.

## Setup & Building

Because `payload.zip` exceeds standard Git repository file limits, it is excluded from version control and must be supplied locally before building.

1. **Clone the repository:**
   ```bash
   git clone <your-repository-url>
   cd ClientInstaller
