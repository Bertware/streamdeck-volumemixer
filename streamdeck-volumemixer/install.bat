REM USAGE: Install.bat DEBUG/RELEASE UUID
REM Example: Install.bat RELEASE com.barraider.spotify
setlocal
cd /d %~dp0
cd %1

REM *** MAKE SURE THE FOLLOWING VARIABLES ARE CORRECT ***
REM (Distribution tool be downloaded from: https://developer.elgato.com/documentation/stream-deck/sdk/exporting-your-plugin/ )
SET DISTRIBUTION_TOOL="C:\Development\elgato\DistributionTool.exe"
SET STREAM_DECK_FILE="C:\Program Files\Elgato\StreamDeck\StreamDeck.exe"
SET STREAM_DECK_LOAD_TIMEOUT=7

taskkill /f /im streamdeck.exe
taskkill /f /im %2.exe
timeout /t 2
del C:\TEMP\%2.streamDeckPlugin
%DISTRIBUTION_TOOL% -b -i %2.sdPlugin -o C:\TEMP
rmdir %APPDATA%\Elgato\StreamDeck\Plugins\%2.sdPlugin /s /q
START "" %STREAM_DECK_FILE%
timeout /t %STREAM_DECK_LOAD_TIMEOUT%
C:\TEMP\%2.streamDeckPlugin