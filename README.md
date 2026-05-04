# T2-Veristream
 
**T2-VeriStream** is a Windows desktop tool for de-encapsulating and inspecting T2-MI (DVB-T2 Modulator Interface) streams. It allows broadcast engineers to analyse T2-MI packets, extract the inner transport stream, and play back the decoded video — all from a simple GUI.
 
---
 
## Requirements
 
- **Windows** (Windows 7 or later)
- **[TSDuck](https://tsduck.io/)** installed and available in `PATH`
- **[VLC Media Player](https://www.videolan.org/vlc/)** installed at the default path
- **.NET Framework 4.0** or higher
---
 
## Building
 
1. Open `T2MI_Extractor.sln` in **Visual Studio**.
2. Build the solution (`Ctrl+Shift+B`).
---
 
## Usage
 
1. **Browse**: Select a recorded T2-MI stream file.
2. **Analyse TS**: Reads the stream and displays the T2-MI tables and PLP information.
3. **Extract Content**: De-encapsulates the T2-MI stream and writes the output file (e.g. `extr_name_of_stream.ts`) to the same directory as the source file.
4. **Play**: Opens the extracted transport stream in VLC for video playback.
---
 This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.
 
This software uses [TSDuck](https://tsduck.io/) — Copyright © 2005–2026, Thierry Lelégard — released under the BSD 2-Clause License.
 
