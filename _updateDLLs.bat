set "local=%cd%"

cd ..\ESystem.NET
call .\_updateRelease.bat

cd %local%
copy ..\ESystem.NET\_Release\* .\DLLs\
del .\DLLs\ESystem.WPF.dll
del .\DLLs\ESystem.WPF.pdb

pause