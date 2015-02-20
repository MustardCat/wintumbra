[Setup]
AppName = "WintumbraInstaller"
AppVersion = 0.1.0
DefaultDirName = "Antumbra"

[Files]
Source: "dependencies\dotNetFx40_Full_x86_x64.exe"; DestDir: {tmp}; Flags: deleteafterinstall; Check: FrameworkIsNotInstalled
Source: "..\DriverInstaller\Glow\*"; DestDir: {app}\DriverInstaller
Source: "..\DriverInstaller\Glow\amd64\*"; DestDir: {app}\DriverInstaller\amd64
Source: "..\DriverInstaller\Glow\x86\*"; DestDir: {app}\DriverInstaller\x86
Source: "dependencies\*.dll"; DestDir: {app}\Extensions
Source: "dependencies\antumbra.exe"; DestDir: {app}
Source: "../Licenses\*"; DestDir: {app}\Licences
Source: "../README.md"; DestDir: {app}

[Run]
Filename: "{tmp}\dotNetFx40_Full_setup.exe"; Check: FrameworkIsNotInstalled

[code]
function FrameworkIsNotInstalled: Boolean;
begin
  Result := not RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\Microsoft\.NETFramework\policy\v4.0');
end;