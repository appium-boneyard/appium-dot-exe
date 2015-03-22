:: post_build [Release|Debug] [version] [solution_dir] [out_dir]

:: parse cmd line
SET _CONFIG=%1
SET _VER="%~2"
SET _SLN_DIR="%~3"
SET _OUT_DIR="%~4"

IF %_CONFIG%==Debug GOTO :debug

:: build installer
ECHO Deleting old archives...
DEL %_SLN_DIR%installer\appium-installer.exe
ECHO Installing Appium...
%_OUT_DIR%Appium.exe /installapps
%_SLN_DIR%tools\lmza_sdk_cs\7zr.exe a %_OUT_DIR%node_modules.7z %_OUT_DIR%node_modules
%_SLN_DIR%tools\inno_setup_5\ISCC.exe /dMyAppVersion=%_VER% %_SLN_DIR%installer\Appium-dot-exe.iss
GOTO :EOF

:debug
ECHO ====
ECHO NOTE: Neither Node nor Appium is downloaded during DEBUG builds. You may download both from the command line
ECHO with `%_OUT_DIR%Appium.exe /installapps`.