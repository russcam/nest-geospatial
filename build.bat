@echo off
REM build <target>

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)
.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)
SET TARGET="build"
SET SCRIPT="build\\scripts\\Targets.fsx"
IF NOT [%1]==[] (set TARGET="%1")
ECHO starting build using target=%TARGET%
"packages\build\FAKE\tools\Fake.exe" "%SCRIPT%" "target=%TARGET%"