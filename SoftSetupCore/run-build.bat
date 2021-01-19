@rem 打包命令
cd /d %~dp0
@call makeskinzip.bat runtime

cd /d %~dp0
".\NSIS\makensis.exe" ".\SetupScripts\runtime\setup.nsi"

@pause