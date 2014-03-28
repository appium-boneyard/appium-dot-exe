timeout 10
xcopy /Y /E %1\Appium\* .
rmdir /S /Q %1\Appium
Appium.exe

