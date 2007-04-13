@echo off
echo installutil.exe /u OpenDentServer.exe
echo.
installutil.exe /u OpenDentServer.exe
echo.
echo If you have the Computer Management window open,
echo and you hit refresh, the service might still show as "disabled".
echo It will be gone if you restart the management window.
echo.
pause
