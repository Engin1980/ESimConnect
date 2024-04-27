deltree -Y _Release
mkdir _Release
mkdir .\_Release\ESimConnect
copy readme.md .\_Release\ESimConnect\_Readme.md
copy License .\_Release\ESimConnect\_License.txt
xcopy /e /i /Y ESimConnect\bin\debug\net6.0-windows\* .\_Release\ESimConnect
cd .\_Release
tar.exe -c -f ESimConnect.zip ESimConnect



pause
